using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services
{
    public class MeasurementService
    {
        private readonly AppDbContext _readContext;
        private readonly QuanLyServices _services; 

        public MeasurementService()
        {
            _readContext = new AppDbContext();
            _services = new QuanLyServices();
        }

        #region Get/Search Operations

        public string GenerateNewMeasurementId()
        {
            return _services.GenerateNewId<DonViDoLuong>("DV", 5);
        }

        public dynamic GetAllMeasurements()
        {
            try
            {
                return _readContext.DonViDoLuongs
                    .AsNoTracking()
                    .Where(u => !u.isDelete)
                    .OrderBy(u => u.id) 
                    .Select(u => new
                    {
                        Id = u.id,
                        Ten = u.ten,
                        KyHieu = u.kyHieu,
                        SoLuongSanPham = _readContext.SanPhamDonVis.Count(sp => sp.donViId == u.id && !sp.isDelete),
                        TrangThai = u.isDelete ? "Không hoạt động" : "Hoạt động"
                    })
                    .ToList();
            }
            catch (Exception ex) { throw new Exception($"Lỗi lấy danh sách: {ex.Message}"); }
        }

        public dynamic SearchMeasurements(string keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? "";
                return _readContext.DonViDoLuongs
                    .AsNoTracking()
                    .Where(u => !u.isDelete &&
                               (u.ten.ToLower().Contains(keyword) ||
                                u.kyHieu.ToLower().Contains(keyword) ||
                                u.id.ToLower().Contains(keyword))) 
                    .OrderBy(u => u.id)
                    .Select(u => new
                    {
                        Id = u.id,
                        Ten = u.ten,
                        KyHieu = u.kyHieu,
                        SoLuongSanPham = _readContext.SanPhamDonVis.Count(sp => sp.donViId == u.id && !sp.isDelete),
                        TrangThai = u.isDelete ? "Không hoạt động" : "Hoạt động"
                    })
                    .ToList();
            }
            catch (Exception ex) { throw new Exception($"Lỗi tìm kiếm: {ex.Message}"); }
        }

        public DonViDoLuong GetMeasurementById(string id)
        {
            return _readContext.DonViDoLuongs.AsNoTracking().FirstOrDefault(u => u.id == id);
        }

        public int GetProductCount(string unitId)
        {
            return _readContext.SanPhamDonVis.Count(sp => sp.donViId == unitId && !sp.isDelete);
        }

        #endregion

        #region CRUD Operations

        public (bool success, string message, DonViDoLuong unit) AddMeasurement(DonViDoLuong unit)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    // Check trùng tên
                    if (db.DonViDoLuongs.Any(u => u.ten.ToLower() == unit.ten.ToLower() && !u.isDelete))
                        return (false, "Tên đơn vị đã tồn tại!", null);

                    // Check trùng ký hiệu
                    if (db.DonViDoLuongs.Any(u => u.kyHieu.ToLower() == unit.kyHieu.ToLower() && !u.isDelete))
                        return (false, "Ký hiệu đơn vị đã tồn tại!", null);

                    // Sinh mã tự động nếu chưa có
                    if (string.IsNullOrEmpty(unit.id) || unit.id == "Tự động tạo")
                    {
                        unit.id = _services.GenerateNewId<DonViDoLuong>("DV", 5);
                    }

                    unit.isDelete = false;
                    db.DonViDoLuongs.Add(unit);
                    db.SaveChanges();

                    return (true, $"Thêm thành công. Mã: {unit.id}", unit);
                }
                catch (Exception ex) { return (false, $"Lỗi thêm: {ex.Message}", null); }
            }
        }

        public (bool success, string message) UpdateMeasurement(DonViDoLuong unit)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    var existing = db.DonViDoLuongs.Find(unit.id);
                    if (existing == null) return (false, "Không tìm thấy đơn vị.");

                    // Check trùng tên (trừ chính nó)
                    if (db.DonViDoLuongs.Any(u => u.id != unit.id && u.ten.ToLower() == unit.ten.ToLower() && !u.isDelete))
                        return (false, "Tên đơn vị đã tồn tại!");

                    if (db.DonViDoLuongs.Any(u => u.id != unit.id && u.kyHieu.ToLower() == unit.kyHieu.ToLower() && !u.isDelete))
                        return (false, "Ký hiệu đơn vị đã tồn tại!");

                    existing.ten = unit.ten;
                    existing.kyHieu = unit.kyHieu;
                    db.SaveChanges();

                    return (true, "Cập nhật thành công.");
                }
                catch (Exception ex) { return (false, $"Lỗi cập nhật: {ex.Message}"); }
            }
        }

        public (bool success, string message) DeleteMeasurement(string unitId)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    var unit = db.DonViDoLuongs.Find(unitId);
                    if (unit == null) return (false, "Không tìm thấy đơn vị.");

                    // Kiểm tra ràng buộc
                    int productCount = db.SanPhamDonVis.Count(sp => sp.donViId == unitId && !sp.isDelete);
                    if (productCount > 0)
                        return (false, $"Không thể xóa! Đang có {productCount} sản phẩm sử dụng đơn vị này.");

                    unit.isDelete = true;
                    db.SaveChanges();
                    return (true, "Xóa thành công.");
                }
                catch (Exception ex) { return (false, $"Lỗi xóa: {ex.Message}"); }
            }
        }

        #endregion

        public void Dispose() => _readContext?.Dispose();
    }
}