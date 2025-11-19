using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services
{
    public class ProductService : IDisposable
    {
        private readonly AppDbContext _readContext;
        private readonly QuanLyServices _services;

        public ProductService()
        {
            _readContext = new AppDbContext();
            _services = new QuanLyServices();
        }

        #region Get/Search Operations

        public SanPhamDonVi GetProductUnitById(string id)
        {
            return _readContext.SanPhamDonVis.AsNoTracking()
                .Include(x => x.SanPham)
                .Include(x => x.SanPham.NhanHieu)
                .Include(x => x.SanPham.SanPhamDanhMucs.Select(dm => dm.DanhMuc))
                .Include(x => x.DonViDoLuong)
                .FirstOrDefault(p => p.id == id && !p.isDelete);
        }

        public List<ProductDetailDto> FilterProducts(string keyword = null, string brandId = null, string categoryId = null)
        {
            try
            {
                var query = _readContext.SanPhamDonVis.AsNoTracking()
                    .Include(x => x.SanPham)
                    .Include(x => x.SanPham.NhanHieu)
                    .Include(x => x.SanPham.SanPhamDanhMucs.Select(dm => dm.DanhMuc))
                    .Include(x => x.DonViDoLuong)
                    .Where(spd => !spd.isDelete && !spd.SanPham.isDelete)
                    .AsQueryable();

                // 1. Lọc theo từ khóa (tìm theo ID hoặc tên sản phẩm)
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    keyword = keyword.ToLower();
                    query = query.Where(spd =>
                        spd.SanPham.id.ToLower().Contains(keyword) ||
                        spd.SanPham.ten.ToLower().Contains(keyword) ||
                        spd.id.ToLower().Contains(keyword));
                }

                // 2. Lọc theo Nhãn hiệu
                if (!string.IsNullOrEmpty(brandId))
                {
                    query = query.Where(spd => spd.SanPham.nhanHieuId == brandId);
                }

                // 3. Lọc theo Danh mục (quan hệ n-n)
                if (!string.IsNullOrEmpty(categoryId))
                {
                    query = query.Where(spd => spd.SanPham.SanPhamDanhMucs.Any(dm => dm.danhMucId == categoryId && !dm.isDelete));
                }

                // 4. Map sang DTO
                return query.Select(spd => new ProductDetailDto
                {
                    Id = spd.id,                           // Mã SPDV
                    SanPhamId = spd.sanPhamId,             // ID Sản phẩm
                    DonViId = spd.donViId,                 // ID Đơn vị
                    MaSP = spd.SanPham.id,                 // Mã SP từ frmGoods
                    TenSanPham = spd.SanPham.ten,          // Tên SP từ frmGoods
                    NhanHieu = spd.SanPham.NhanHieu != null ? spd.SanPham.NhanHieu.ten : "Chưa có", // Nhãn hiệu từ frmGoods
                    
                    // Danh mục từ frmGoods (lấy danh mục đầu tiên)
                    DanhMuc = spd.SanPham.SanPhamDanhMucs
                        .Where(dm => !dm.isDelete)
                        .Select(dm => dm.DanhMuc.ten)
                        .FirstOrDefault() ?? "Chưa phân loại",
                    
                    DonVi = spd.DonViDoLuong.ten,          // Đơn vị từ frmMeasurements
                    HeSoQuyDoi = spd.heSoQuyDoi,           // Hệ số quy đổi
                    GiaBan = spd.giaBan,                   // Giá bán từ SanPhamDonVi
                    TrangThai = spd.trangThai,             // Trạng thái từ SanPhamDonVi
                    MoTa = spd.SanPham.moTa                // Mô tả từ SanPham
                })
                .OrderBy(x => x.MaSP)
                .ThenBy(x => x.DonVi)
                .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lọc sản phẩm: {ex.Message}", ex);
            }
        }

        public List<SanPham> GetAllGoods(string keyword = null, string brandId = null, string categoryId = null)
        {
            try
            {
                var query = _readContext.SanPhams.AsNoTracking()
                    .Where(sp => !sp.isDelete)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    keyword = keyword.ToLower();
                    query = query.Where(sp => sp.id.ToLower().Contains(keyword) || sp.ten.ToLower().Contains(keyword));
                }

                if (!string.IsNullOrEmpty(brandId))
                    query = query.Where(sp => sp.nhanHieuId == brandId);

                if (!string.IsNullOrEmpty(categoryId))
                    query = query.Where(sp => sp.SanPhamDanhMucs.Any(dm => dm.danhMucId == categoryId && !dm.isDelete));

                return query.Include(x => x.NhanHieu).OrderBy(x => x.ten).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy danh sách hàng hóa: {ex.Message}", ex);
            }
        }

        public List<DonViDoLuong> GetAllUnits()
        {
            try
            {
                return _readContext.DonViDoLuongs.AsNoTracking()
                    .Where(x => !x.isDelete)
                    .OrderBy(x => x.ten)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy danh sách đơn vị: {ex.Message}", ex);
            }
        }

        public string GenerateNewProductUnitId()
        {
            return _services.GenerateNewId<SanPhamDonVi>("SPDV", 6);
        }

        #endregion

        #region CRUD Operations

        public (bool success, string message, SanPhamDonVi result) AddProductUnit(SanPhamDonVi model)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    // Kiểm tra sản phẩm có tồn tại không
                    var good = db.SanPhams.FirstOrDefault(x => x.id == model.sanPhamId && !x.isDelete);
                    if (good == null)
                        return (false, "Hàng hóa không tồn tại!", null);

                    // Kiểm tra đơn vị có tồn tại không
                    var unit = db.DonViDoLuongs.FirstOrDefault(x => x.id == model.donViId && !x.isDelete);
                    if (unit == null)
                        return (false, "Đơn vị không tồn tại!", null);

                    // Kiểm tra trùng: cùng sản phẩm + cùng đơn vị
                    bool exists = db.SanPhamDonVis.Any(x => x.sanPhamId == model.sanPhamId &&
                                                          x.donViId == model.donViId &&
                                                          !x.isDelete);
                    if (exists)
                        return (false, "Sản phẩm với đơn vị này đã tồn tại!", null);

                    // Tạo ID mới
                    model.id = _services.GenerateNewId<SanPhamDonVi>("SPDV", 6);
                    model.isDelete = false;
                    
                    if (string.IsNullOrEmpty(model.trangThai))
                        model.trangThai = "Available";

                    db.SanPhamDonVis.Add(model);
                    db.SaveChanges();

                    return (true, $"Thêm thành công! Mã: {model.id}", model);
                }
                catch (Exception ex)
                {
                    return (false, $"Lỗi thêm: {ex.Message}", null);
                }
            }
        }

        public (bool success, string message) UpdateProductUnit(SanPhamDonVi model)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    var item = db.SanPhamDonVis.FirstOrDefault(x => x.id == model.id);
                    if (item == null)
                        return (false, "Dữ liệu không tồn tại!");

                    // Kiểm tra trùng khi sửa (trừ chính nó)
                    bool exists = db.SanPhamDonVis.Any(x => x.sanPhamId == model.sanPhamId &&
                                                          x.donViId == model.donViId &&
                                                          x.id != model.id &&
                                                          !x.isDelete);
                    if (exists)
                        return (false, "Đơn vị này đã có cho sản phẩm!");

                    // Cập nhật
                    item.sanPhamId = model.sanPhamId;
                    item.donViId = model.donViId;
                    item.giaBan = model.giaBan;
                    item.heSoQuyDoi = model.heSoQuyDoi;
                    item.trangThai = model.trangThai;

                    db.SaveChanges();
                    return (true, "Cập nhật thành công!");
                }
                catch (Exception ex)
                {
                    return (false, $"Lỗi cập nhật: {ex.Message}");
                }
            }
        }

        public (bool success, string message) DeleteProductUnit(string id)
        {
            using (var db = new AppDbContext())
            {
                using (var tran = db.Database.BeginTransaction())
                {
                    try
                    {
                        var item = db.SanPhamDonVis.FirstOrDefault(x => x.id == id);
                        if (item == null)
                            return (false, "Không tìm thấy dữ liệu!");

                        // Kiểm tra ràng buộc: Đã phát sinh giao dịch chưa?
                        bool hasOrders = db.ChiTietHoaDons.Any(ct => ct.sanPhamDonViId == id && !ct.isDelete);
                        if (hasOrders)
                            return (false, "Không thể xóa! Sản phẩm này đã phát sinh giao dịch.");

                        // Xóa mềm
                        item.isDelete = true;
                        db.SaveChanges();
                        tran.Commit();

                        return (true, "Xóa thành công!");
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        return (false, $"Lỗi xóa: {ex.Message}");
                    }
                }
            }
        }

        #endregion

        public void Dispose()
        {
            _readContext?.Dispose();
        }
    }
}