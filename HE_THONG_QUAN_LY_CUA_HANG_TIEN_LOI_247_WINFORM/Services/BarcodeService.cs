using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services
{
    public class BarcodeService
    {
        private readonly AppDbContext _readContext;
        private readonly QuanLyServices _services;

        public BarcodeService()
        {
            _readContext = new AppDbContext();
            _services = new QuanLyServices();
        }

        public dynamic GetAllBarcodes()
        {
            try
            {
                return (from b in _readContext.MaDinhDanhSanPhams
                        where !b.isDelete
                        join spdv in _readContext.SanPhamDonVis on b.sanPhamDonViId equals spdv.id
                        join sp in _readContext.SanPhams on spdv.sanPhamId equals sp.id
                        orderby b.id
                        select new
                        {
                            Id = b.id,
                            SanPhamDonViId = b.sanPhamDonViId,
                            TenSanPham = sp.ten,
                            LoaiMa = b.loaiMa,
                            MaCode = b.maCode,
                            DuongDan = b.duongDan,
                            TrangThai = b.isDelete ? "Không hoạt động" : "Hooạt động"
                        }).ToList();
            }
            catch (Exception ex) 
            { 
                throw new Exception($"Lỗi lấy danh sách barcode: {ex.Message}"); 
            }
        }

        public dynamic SearchBarcodes(string keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? "";
                return (from b in _readContext.MaDinhDanhSanPhams
                        where !b.isDelete
                        join spdv in _readContext.SanPhamDonVis on b.sanPhamDonViId equals spdv.id
                        join sp in _readContext.SanPhams on spdv.sanPhamId equals sp.id
                        where b.maCode.ToLower().Contains(keyword) ||
                              b.id.ToLower().Contains(keyword) ||
                              sp.ten.ToLower().Contains(keyword)
                        orderby b.id
                        select new
                        {
                            Id = b.id,
                            SanPhamDonViId = b.sanPhamDonViId,
                            TenSanPham = sp.ten,
                            LoaiMa = b.loaiMa,
                            MaCode = b.maCode,
                            DuongDan = b.duongDan,
                            TrangThai = b.isDelete ? "Không hoạt động" : "Hooạt động"
                        }).ToList();
            }
            catch (Exception ex) 
            { 
                throw new Exception($"Lỗi tìm kiếm barcode: {ex.Message}"); 
            }
        }

        public MaDinhDanhSanPham GetBarcodeById(string id)
        {
            return _readContext.MaDinhDanhSanPhams.AsNoTracking().FirstOrDefault(b => b.id == id);
        }

        public List<SanPhamDonVi> GetAllProductUnits()
        {
            return _readContext.SanPhamDonVis
                .AsNoTracking()
                .Include(p => p.SanPham)
                .Include(p => p.DonViDoLuong)
                .Where(p => !p.isDelete && p.trangThai == "Còn hàng")
                .OrderBy(p => p.SanPham.ten)
                .ToList();
        }

        public (bool success, string message, MaDinhDanhSanPham barcode) AddBarcode(MaDinhDanhSanPham barcode)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    if (db.MaDinhDanhSanPhams.Any(b => b.maCode.ToLower() == barcode.maCode.ToLower() && !b.isDelete))
                        return (false, "Mã barcode đã tồn tại!", null);

                    if (string.IsNullOrEmpty(barcode.id) || barcode.id == "Tự động tạo")
                    {
                        barcode.id = _services.GenerateNewId<MaDinhDanhSanPham>("MDD", 3);
                    }

                    barcode.isDelete = false;
                    db.MaDinhDanhSanPhams.Add(barcode);
                    db.SaveChanges();

                    return (true, $"Thêm thành công. Mã: {barcode.id}", barcode);
                }
                catch (Exception ex) 
                { 
                    return (false, "Lỗi: " + ex.Message, null); 
                }
            }
        }

        public (bool success, string message) UpdateBarcode(MaDinhDanhSanPham barcode)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    var existing = db.MaDinhDanhSanPhams.Find(barcode.id);
                    if (existing == null) 
                        return (false, "Không tìm thấy barcode.");

                    if (db.MaDinhDanhSanPhams.Any(b => b.id != barcode.id && 
                        b.maCode.ToLower() == barcode.maCode.ToLower() && !b.isDelete))
                        return (false, "Mã barcode đã tồn tại!");

                    existing.sanPhamDonViId = barcode.sanPhamDonViId;
                    existing.loaiMa = barcode.loaiMa;
                    existing.maCode = barcode.maCode;
                    existing.duongDan = barcode.duongDan;
                    existing.anhId = barcode.anhId;

                    db.SaveChanges();
                    return (true, "Cập nhật thành công.");
                }
                catch (Exception ex) 
                { 
                    return (false, "Lỗi: " + ex.Message); 
                }
            }
        }

        public (bool success, string message) DeleteBarcode(string barcodeId)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    var barcode = db.MaDinhDanhSanPhams.Find(barcodeId);
                    if (barcode == null) 
                        return (false, "Không tìm thấy barcode.");

                    barcode.isDelete = true;
                    db.SaveChanges();
                    return (true, "Xóa thành công.");
                }
                catch (Exception ex) 
                { 
                    return (false, "L?i: " + ex.Message); 
                }
            }
        }

        public string GenerateNewBarcodeId()
        {
            return _services.GenerateNewId<MaDinhDanhSanPham>("MDD", 3);
        }

        public void Dispose()
        {
            _readContext?.Dispose();
        }
    }
}
