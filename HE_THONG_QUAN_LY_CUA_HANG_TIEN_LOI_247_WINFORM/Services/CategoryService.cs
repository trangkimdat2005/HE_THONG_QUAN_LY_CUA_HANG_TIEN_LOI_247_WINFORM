using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services
{
<<<<<<< HEAD
    public class CategoryService    
=======
    public class CategoryService
>>>>>>> e72f1789f0293da72f8c023d8dd62baad211c6f0
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
                        SoLuongSanPham = _readContext.SanPhamViTris.Count(vt =>
                            !vt.isDelete &&
                            _readContext.SanPhamDonVis.Any(dv =>
                                dv.id == vt.sanPhamDonViId &&
                                !dv.isDelete &&
                                _readContext.SanPhamDanhMucs.Any(dm =>
                                    dm.sanPhamId == dv.sanPhamId &&
                                    dm.danhMucId == c.id &&
                                    !dm.isDelete
                                )
                            )
                        ),
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
                        SoLuongSanPham = _readContext.SanPhamViTris.Count(vt =>
                            !vt.isDelete &&
                            _readContext.SanPhamDonVis.Any(dv =>
                                dv.id == vt.sanPhamDonViId &&
                                !dv.isDelete &&
                                _readContext.SanPhamDanhMucs.Any(dm =>
                                    dm.sanPhamId == dv.sanPhamId &&
                                    dm.danhMucId == c.id &&
                                    !dm.isDelete
                                )
                            )
                        ),
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

        public int GetProductCountFromLocation(string categoryId)
        {
            return _readContext.SanPhamViTris.Count(vt =>
                !vt.isDelete &&
                _readContext.SanPhamDonVis.Any(dv =>
                    dv.id == vt.sanPhamDonViId &&
                    !dv.isDelete &&
                    _readContext.SanPhamDanhMucs.Any(dm =>
                        dm.sanPhamId == dv.sanPhamId &&
                        dm.danhMucId == categoryId &&
                        !dm.isDelete
                    )
                )
            );
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
                        category.id = _services.GenerateNewId<DanhMuc>("DM", 5);
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

                    int countInUse = db.SanPhamViTris.Count(vt =>
                        !vt.isDelete &&
                        db.SanPhamDonVis.Any(dv =>
                            dv.id == vt.sanPhamDonViId &&
                            !dv.isDelete &&
                            db.SanPhamDanhMucs.Any(dm =>
                                dm.sanPhamId == dv.sanPhamId &&
                                dm.danhMucId == categoryId &&
                                !dm.isDelete
                            )
                        )
                    );

                    if (countInUse > 0)
                        return (false, $"Không thể xóa! Đang có {countInUse} lô hàng trong kho thuộc danh mục này.");

                    category.isDelete = true;

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
            return _services.GenerateNewId<DanhMuc>("DM", 5);
        }

        #endregion

        public void Dispose() => _readContext?.Dispose();
    }
}