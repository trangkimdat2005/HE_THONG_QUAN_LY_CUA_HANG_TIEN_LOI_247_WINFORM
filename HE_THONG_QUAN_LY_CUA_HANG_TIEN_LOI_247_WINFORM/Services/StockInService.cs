using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services
{
    public class StockInService : IDisposable
    {
        private readonly AppDbContext _context;
        private readonly QuanLyServices _services;

        public StockInService()
        {
            _context = new AppDbContext();
            _services = new QuanLyServices();
        }

        #region Get Operations

        // Lấy tất cả phiếu nhập
        public dynamic GetAllImportReceipts()
        {
            try
            {
                var receipts = (from pn in _context.PhieuNhaps
                               where !pn.isDelete
                               join ncc in _context.NhaCungCaps on pn.nhaCungCapId equals ncc.id
                               where !ncc.isDelete
                               join nv in _context.NhanViens on pn.nhanVienId equals nv.id
                               where !nv.isDelete
                               orderby pn.id ascending  // Sắp xếp theo ID tăng dần
                               select new
                               {
                                   Id = pn.id,
                                   NhaCungCap = ncc.ten,
                                   NgayNhap = pn.ngayNhap,
                                   TongTien = pn.tongTien,
                                   NhanVien = nv.hoTen
                               }).ToList();

                return receipts;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy danh sách phiếu nhập: {ex.Message}");
            }
        }

        //Tìm kiếm phiếu nhập theo từ khóa
        public dynamic SearchImportReceipts(string keyword)
        {
            try
            {
                keyword = keyword?.ToLower().Trim() ?? "";

                var receipts = (from pn in _context.PhieuNhaps
                               where !pn.isDelete
                               join ncc in _context.NhaCungCaps on pn.nhaCungCapId equals ncc.id
                               where !ncc.isDelete
                               join nv in _context.NhanViens on pn.nhanVienId equals nv.id
                               where !nv.isDelete
                               where pn.id.ToLower().Contains(keyword) ||
                                     ncc.ten.ToLower().Contains(keyword) ||
                                     nv.hoTen.ToLower().Contains(keyword)
                               orderby pn.id ascending  // Sắp xếp theo ID tăng dần
                               select new
                               {
                                   Id = pn.id,
                                   NhaCungCap = ncc.ten,
                                   NgayNhap = pn.ngayNhap,
                                   TongTien = pn.tongTien,
                                   NhanVien = nv.hoTen
                               }).ToList();

                return receipts;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi tìm kiếm: {ex.Message}");
            }
        }

        // Lấy chi tiết phiếu nhập
        public dynamic GetImportReceiptDetails(string phieuNhapId)
        {
            try
            {
                var details = (from ct in _context.ChiTietPhieuNhaps
                              where ct.phieuNhapId == phieuNhapId && !ct.isDelete
                              join spDv in _context.SanPhamDonVis on ct.sanPhamDonViId equals spDv.id
                              where !spDv.isDelete
                              join sp in _context.SanPhams on spDv.sanPhamId equals sp.id
                              where !sp.isDelete
                              join dv in _context.DonViDoLuongs on spDv.donViId equals dv.id
                              where !dv.isDelete
                              select new
                              {
                                  SanPham = sp.ten + " - " + dv.ten,
                                  SoLuong = ct.soLuong,
                                  DonGia = ct.donGia,
                                  ThanhTien = ct.tongTien
                              }).ToList();

                return details;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy chi tiết phiếu nhập: {ex.Message}");
            }
        }

        public (bool success, string message) DeleteImportReceipt(string phieuNhapId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // 1. Tìm phiếu nhập
                    var phieuNhap = _context.PhieuNhaps.FirstOrDefault(p => p.id == phieuNhapId && !p.isDelete);
                    if (phieuNhap == null) return (false, "Phiếu nhập không tồn tại hoặc đã bị xóa.");

                    // 2. Lấy danh sách chi tiết hàng đã nhập
                    var details = _context.ChiTietPhieuNhaps
                        .Where(ct => ct.phieuNhapId == phieuNhapId && !ct.isDelete)
                        .ToList();

                    foreach (var item in details)
                    {
                        var tonKho = _context.TonKhoes.FirstOrDefault(tk => tk.sanPhamDonViId == item.sanPhamDonViId);

                        if (tonKho == null || tonKho.soLuongTon < item.soLuong)
                        {
                            return (false, $"Không thể xóa! Sản phẩm mã {item.sanPhamDonViId} đã được xuất bán hoặc không đủ tồn kho để hoàn tác.");
                        }

                        tonKho.soLuongTon -= item.soLuong;

                        item.isDelete = true;
                    }

                    phieuNhap.isDelete = true;

                    _context.SaveChanges();
                    transaction.Commit();

                    return (true, "Xóa phiếu nhập thành công (đã cập nhật lại tồn kho).");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return (false, "Lỗi: " + ex.Message);
                }
            }
        }
        public List<NhaCungCap> GetAllSuppliers()
        {
            return _context.NhaCungCaps.Where(n => !n.isDelete).OrderBy(n => n.ten).ToList();
        }

        public List<NhanVien> GetAllEmployees()
        {
            return _context.NhanViens.Where(n => !n.isDelete).OrderBy(n => n.hoTen).ToList();
        }

        // Lấy danh sách sản phẩm (với đơn vị) - Bao gồm cả sản phẩm hết hàng để nhập thêm
        public dynamic GetAllProductUnits()
        {
            try
            {
                var products = (from spDv in _context.SanPhamDonVis
                               where !spDv.isDelete 
                                     && (spDv.trangThai == "Còn hàng" || spDv.trangThai == "Hết hàng")
                               join sp in _context.SanPhams on spDv.sanPhamId equals sp.id
                               where !sp.isDelete
                               join dv in _context.DonViDoLuongs on spDv.donViId equals dv.id
                               where !dv.isDelete
                               orderby sp.ten, dv.ten
                               select new
                               {
                                   Id = spDv.id,
                                   Ten = sp.ten + " - " + dv.ten + 
                                         (spDv.trangThai == "Hết hàng" ? " (Hết hàng)" : ""),
                                   TenSanPham = sp.ten,
                                   DonVi = dv.ten,
                                   GiaBanHienTai = spDv.giaBan,
                                   TrangThai = spDv.trangThai
                               }).ToList();

                return products;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy danh sách sản phẩm: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy nhà cung cấp gần nhất của sản phẩm (từ phiếu nhập gần nhất)
        /// </summary>
        public string GetLastSupplierByProduct(string sanPhamDonViId)
        {
            try
            {
                // Tìm phiếu nhập gần nhất có sản phẩm này
                var lastReceipt = (from ct in _context.ChiTietPhieuNhaps
                                  where ct.sanPhamDonViId == sanPhamDonViId && !ct.isDelete
                                  join pn in _context.PhieuNhaps on ct.phieuNhapId equals pn.id
                                  where !pn.isDelete
                                  orderby pn.ngayNhap descending
                                  select pn.nhaCungCapId).FirstOrDefault();

                return lastReceipt;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region CRUD Operations

        //Tạo phiếu nhập mới
        public (bool success, string message, string phieuNhapId) CreateImportReceipt(
            string nhaCungCapId,
            string nhanVienId,
            DateTime ngayNhap,
            List<ChiTietPhieuNhap> chiTietList)
        {
            using (var db = new AppDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (string.IsNullOrEmpty(nhaCungCapId))
                            return (false, "Vui lòng chọn nhà cung cấp", null);

                        if (string.IsNullOrEmpty(nhanVienId))
                            return (false, "Vui lòng chọn nhân viên", null);

                        if (chiTietList == null || chiTietList.Count == 0)
                            return (false, "Phiếu nhập phải có ít nhất 1 sản phẩm", null);

                        db.Configuration.AutoDetectChangesEnabled = false;

                        string phieuNhapId = _services.GenerateNewId<PhieuNhap>("PN", 6);
                        decimal tongTien = chiTietList.Sum(ct => ct.tongTien);


                        var phieuNhap = new PhieuNhap
                        {
                            id = phieuNhapId,
                            nhaCungCapId = nhaCungCapId,
                            nhanVienId = nhanVienId,
                            ngayNhap = ngayNhap,
                            tongTien = tongTien,
                            isDelete = false
                        };

                        db.PhieuNhaps.Add(phieuNhap);

                        foreach (var ct in chiTietList)
                        {
                            ct.phieuNhapId = phieuNhapId;
                            ct.tongTien = ct.soLuong * ct.donGia;
                            ct.isDelete = false;
                            
                            db.ChiTietPhieuNhaps.Add(ct);


                            // ✅ Database trigger sẽ tự động cập nhật TonKho
                            // Nếu không có trigger, uncomment đoạn code dưới:
                            /*
                            var tonKho = db.TonKhoes.FirstOrDefault(tk =>
                                tk.sanPhamDonViId == ct.sanPhamDonViId && !tk.isDelete);

                            if (tonKho != null)
                            {
                                tonKho.soLuongTon += ct.soLuong;
                            }
                            else
                            {
                                string tonKhoId = _services.GenerateNewId<TonKho>("TK_SPDV", 11);
                                var newTonKho = new TonKho
                                {
                                    id = tonKhoId,
                                    sanPhamDonViId = ct.sanPhamDonViId,
                                    soLuongTon = ct.soLuong,
                                    isDelete = false
                                };
                                db.TonKhoes.Add(newTonKho);
                            }
                            */

                            // ✅ Cập nhật trạng thái sản phẩm
                            var sanPhamDonVi = db.SanPhamDonVis
                                .FirstOrDefault(spDv => spDv.id == ct.sanPhamDonViId && !spDv.isDelete);

                            if (sanPhamDonVi != null && sanPhamDonVi.trangThai == "Hết hàng")
                            {
                                sanPhamDonVi.trangThai = "Còn hàng";
                            }
                        }

                        db.Configuration.AutoDetectChangesEnabled = true;
                        db.ChangeTracker.DetectChanges();

                        db.SaveChanges();
                        transaction.Commit();

                        return (true, $"Tạo phiếu nhập {phieuNhapId} thành công!", phieuNhapId);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return (false, $"Lỗi tạo phiếu nhập: {ex.Message}", null);
                    }
                }
            }
        }

        #endregion

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
