using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services
{
    public class BrandService
    {
        private readonly AppDbContext _readContext;
        private readonly QuanLyServices _services;

        public BrandService()
        {
            _readContext = new AppDbContext();
            _services = new QuanLyServices(); 
        }

        #region Get/Search Operations

        public string GenerateNewBrandId()
        {
            var lastBrand = _readContext.NhanHieux
                .AsNoTracking()
                .OrderByDescending(x => x.id)
                .FirstOrDefault();

            if (lastBrand == null)
            {
                return "NH001"; 
            }

            string lastId = lastBrand.id;
            string prefix = "NH";

            // Phòng trường hợp mã cũ không đúng chuẩn NHxxx
            if (!lastId.StartsWith(prefix)) return "NH001";

            string numberPart = lastId.Substring(prefix.Length); 

            if (int.TryParse(numberPart, out int number))
            {
                return prefix + (number + 1).ToString("D3"); 
            }

            return "NH001"; 
        }

        public dynamic GetAllBrands()
        {
            try
            {
                return _readContext.NhanHieux
                    .AsNoTracking()
                    .Where(b => !b.isDelete)
                    .OrderBy(b => b.id)
                    .Select(b => new
                    {
                        Id = b.id,
                        Ten = b.ten,
                        SoLuongSanPham = b.SanPhams.Count(sp => !sp.isDelete),
                        TrangThai = b.isDelete ? "Không hoạt động" : "Hoạt động"
                    })
                    .ToList();
            }
            catch (Exception ex) { throw new Exception($"Lỗi lấy danh sách: {ex.Message}"); }
        }

        public dynamic SearchBrands(string keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? "";
                return _readContext.NhanHieux
                    .AsNoTracking()
                    .Where(b => !b.isDelete &&
                    (
                       b.ten.ToLower().Contains(keyword) ||
                       b.id.ToLower().Contains(keyword)))
                    .OrderBy(b => b.id)
                    .Select(b => new
                    {
                        Id = b.id,
                        Ten = b.ten,
                        SoLuongSanPham = b.SanPhams.Count(sp => !sp.isDelete),
                        TrangThai = b.isDelete ? "Không hoạt động" : "Hoạt động"
                    })
                    .ToList();
            }
            catch (Exception ex) { throw new Exception($"Lỗi tìm kiếm: {ex.Message}"); }
        }

        public NhanHieu GetBrandById(string id)
        {
            return _readContext.NhanHieux.AsNoTracking().FirstOrDefault(b => b.id == id);
        }

        public int GetProductCount(string brandId)
        {
            return _readContext.SanPhams.Count(sp => sp.nhanHieuId == brandId && !sp.isDelete);
        }

        #endregion

        #region CRUD Operations

        public (bool success, string message, NhanHieu brand) AddBrand(NhanHieu brand)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    if (db.NhanHieux.Any(b => b.ten.ToLower() == brand.ten.ToLower() && !b.isDelete))
                        return (false, "Tên nhãn hiệu đã tồn tại!", null);

                    if (string.IsNullOrEmpty(brand.id) || brand.id == "Tự động tạo")
                    {
                        brand.id = _services.GenerateNewId<NhanHieu>("NH", 3);
                    }

                    brand.isDelete = false;
                    db.NhanHieux.Add(brand);
                    db.SaveChanges();

                    return (true, $"Thêm thành công. Mã: {brand.id}", brand);
                }
                catch (Exception ex) { return (false, "Lỗi: " + ex.Message, null); }
            }
        }

        public (bool success, string message) UpdateBrand(NhanHieu brand)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    var existing = db.NhanHieux.Find(brand.id);
                    if (existing == null) return (false, "Không tìm thấy nhãn hiệu.");

                    if (db.NhanHieux.Any(b => b.id != brand.id && b.ten.ToLower() == brand.ten.ToLower() && !b.isDelete))
                        return (false, "Tên nhãn hiệu đã tồn tại!");

                    existing.ten = brand.ten;
                    db.SaveChanges();
                    return (true, "Cập nhật thành công.");
                }
                catch (Exception ex) { return (false, "Lỗi: " + ex.Message); }
            }
        }

        public (bool success, string message) DeleteBrand(string brandId)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    var brand = db.NhanHieux.Find(brandId);
                    if (brand == null) return (false, "Không tìm thấy nhãn hiệu.");

                    int countInUse = db.SanPhams.Count(sp => sp.nhanHieuId == brandId && !sp.isDelete);

                    if (countInUse > 0)
                        return (false, $"Không thể xóa! Đang có {countInUse} sản phẩm thuộc nhãn hiệu này.");

                    brand.isDelete = true;
                    db.SaveChanges();
                    return (true, "Xóa thành công.");
                }
                catch (Exception ex) { return (false, "Lỗi: " + ex.Message); }
            }
        }

        #endregion

        public void Dispose()
        {
            _readContext?.Dispose();
        }
    }
}