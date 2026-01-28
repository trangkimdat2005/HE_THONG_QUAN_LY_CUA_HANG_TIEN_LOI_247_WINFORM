using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services
{
    public class CategoryService 
    {
        private readonly AppDbContext _readContext;
        private readonly QuanLyServices _services;

        public CategoryService()
        {
            _readContext = new AppDbContext();
            _services = new QuanLyServices();
        }

        #region Get/Search Operations

        public dynamic GetAllCategories()
        {
            try
            {
                return _readContext.DanhMucs
                    .AsNoTracking()
                    .Where(c => !c.isDelete)
                    .OrderBy(c => c.id)
                    .Select(c => new
                    {
                        Id = c.id,
                        Ten = c.ten,
                        //  Đếm SanPhamDonVi: Kiểm tra cả SanPham.isDelete
                        SoLuongSanPham = (from dm in _readContext.SanPhamDanhMucs
                                          where dm.danhMucId == c.id && !dm.isDelete
                                          join sp in _readContext.SanPhams on dm.sanPhamId equals sp.id
                                          where !sp.isDelete 
                                          join spDv in _readContext.SanPhamDonVis on sp.id equals spDv.sanPhamId
                                          where !spDv.isDelete 
                                                && (spDv.trangThai == "Còn hàng" || spDv.trangThai == "Hết hàng")
                                          select spDv.id).Count(),
                        TrangThai = c.isDelete ? "Không hoạt động" : "Hoạt động"
                    })
                    .ToList();
            }
            catch (Exception ex) { throw new Exception($"Lỗi lấy danh sách: {ex.Message}"); }
        }

        public dynamic SearchCategories(string keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? "";
                return _readContext.DanhMucs
                    .AsNoTracking()
                    .Where(c => !c.isDelete && (
                        c.ten.ToLower().Contains(keyword) ||
                        c.id.ToLower().Contains(keyword)))
                    .OrderBy(c => c.id)
                    .Select(c => new
                    {
                        Id = c.id,
                        Ten = c.ten,
                        //  Đếm SanPhamDonVi: Kiểm tra cả SanPham.isDelete
                        SoLuongSanPham = (from dm in _readContext.SanPhamDanhMucs
                                          where dm.danhMucId == c.id && !dm.isDelete
                                          join sp in _readContext.SanPhams on dm.sanPhamId equals sp.id
                                          where !sp.isDelete // ✅ THÊM ĐIỀU KIỆN NÀY
                                          join spDv in _readContext.SanPhamDonVis on sp.id equals spDv.sanPhamId
                                          where !spDv.isDelete 
                                                && (spDv.trangThai == "Còn hàng" || spDv.trangThai == "Hết hàng")
                                          select spDv.id).Count(),
                        TrangThai = c.isDelete ? "Không hoạt động" : "Hoạt động"
                    })
                    .ToList();
            }
            catch (Exception ex) { throw new Exception($"Lỗi tìm kiếm: {ex.Message}"); }
        }

        public DanhMuc GetCategoryById(string id)
        {
            return _readContext.DanhMucs.AsNoTracking().FirstOrDefault(c => c.id == id);
        }

        // Hàm này lấy số lượng hiển thị lên Label bên phải
        public int GetProductCountFromLocation(string categoryId)
        {
            //  Đếm SanPhamDonVi: Kiểm tra cả SanPham.isDelete
            return (from dm in _readContext.SanPhamDanhMucs
                    where dm.danhMucId == categoryId && !dm.isDelete
                    join sp in _readContext.SanPhams on dm.sanPhamId equals sp.id
                    where !sp.isDelete //  THÊM ĐIỀU KIỆN NÀY
                    join spDv in _readContext.SanPhamDonVis on sp.id equals spDv.sanPhamId
                    where !spDv.isDelete 
                          && (spDv.trangThai == "Còn hàng" || spDv.trangThai == "Hết hàng")
                    select spDv.id).Count();
        }

        #endregion

        #region CRUD Operations

        public (bool success, string message, DanhMuc category) AddCategory(DanhMuc category)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    if (db.DanhMucs.Any(c => c.ten.ToLower() == category.ten.ToLower() && !c.isDelete))
                        return (false, "Tên danh mục đã tồn tại!", null);

                    if (string.IsNullOrEmpty(category.id) || category.id == "Tự động tạo")
                    {
                        category.id = _services.GenerateNewId<DanhMuc>("DM", 6);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(category.id)) category.id = Guid.NewGuid().ToString();
                    }

                    category.isDelete = false;
                    db.DanhMucs.Add(category);
                    db.SaveChanges();

                    return (true, $"Thêm thành công. Mã: {category.id}", category);
                }
                catch (Exception ex) { return (false, "Lỗi: " + ex.Message, null); }
            }
        }

        public (bool success, string message) UpdateCategory(DanhMuc category)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    var existing = db.DanhMucs.Find(category.id);
                    if (existing == null) return (false, "Không tìm thấy danh mục.");

                    if (db.DanhMucs.Any(c => c.id != category.id && c.ten.ToLower() == category.ten.ToLower() && !c.isDelete))
                        return (false, "Tên danh mục đã tồn tại!");

                    existing.ten = category.ten;
                    db.SaveChanges();
                    return (true, "Cập nhật thành công.");
                }
                catch (Exception ex) { return (false, "Lỗi: " + ex.Message); }
            }
        }

        public (bool success, string message) DeleteCategory(string categoryId)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    var category = db.DanhMucs.Find(categoryId);
                    if (category == null) return (false, "Không tìm thấy danh mục.");

                    //  Kiểm tra nếu danh mục đang có sản phẩm: Kiểm tra cả SanPham.isDelete
                    int countInUse = (from dm in db.SanPhamDanhMucs
                                      where dm.danhMucId == categoryId && !dm.isDelete
                                      join sp in db.SanPhams on dm.sanPhamId equals sp.id
                                      where !sp.isDelete // ✅ THÊM ĐIỀU KIỆN NÀY
                                      join spDv in db.SanPhamDonVis on sp.id equals spDv.sanPhamId
                                      where !spDv.isDelete 
                                            && (spDv.trangThai == "Còn hàng" || spDv.trangThai == "Hết hàng")
                                      select spDv.id).Count();

                    if (countInUse > 0)
                        return (false, $"Không thể xóa! Đang có {countInUse} sản phẩm thuộc danh mục này.");

                    category.isDelete = true;

                    // Xóa mềm các liên kết
                    var links = db.SanPhamDanhMucs.Where(x => x.danhMucId == categoryId).ToList();
                    foreach (var link in links) link.isDelete = true;

                    db.SaveChanges();
                    return (true, "Xóa thành công.");
                }
                catch (Exception ex) { return (false, "Lỗi: " + ex.Message); }
            }
        }

        #endregion

        #region Helper Methods

        public string GenerateNewCategoryId()
        {
            return _services.GenerateNewId<DanhMuc>("DM", 6);
        }

        #endregion

        public void Dispose() => _readContext?.Dispose();
    }
}