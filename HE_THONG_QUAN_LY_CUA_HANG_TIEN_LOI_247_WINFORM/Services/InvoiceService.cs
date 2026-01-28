using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services
{
    /// <summary>
    /// Service x? lý logic nghi?p v? cho Hóa ??n
    /// </summary>
    public class InvoiceService : IDisposable
    {
        private readonly AppDbContext _context;
        private readonly QuanLyServices _services;

        public InvoiceService()
        {
            _context = new AppDbContext();
            _services = new QuanLyServices();
        }

        /// <summary>
        /// L?y t?t c? hóa ??n
        /// </summary>
        public List<HoaDon> GetAllInvoices()
        {
            return _context.HoaDons
                .Include(h => h.KhachHang)
                .Include(h => h.NhanVien)
                .Where(h => !h.isDelete)
                .OrderByDescending(h => h.ngayLap)
                .ToList();
        }

        /// <summary>
        /// L?y hóa ??n theo ID
        /// </summary>
        public HoaDon GetInvoiceById(string id)
        {
            return _context.HoaDons
                .Include(h => h.KhachHang)
                .Include(h => h.NhanVien)
                .FirstOrDefault(h => h.id == id && !h.isDelete);
        }

        /// <summary>
        /// Tìm ki?m hóa ??n
        /// </summary>
        public List<HoaDon> SearchInvoices(string keyword, DateTime? fromDate, DateTime? toDate, string status)
        {
            var query = _context.HoaDons
                .Include(h => h.KhachHang)
                .Include(h => h.NhanVien)
                .Where(h => !h.isDelete);

            // Filter by keyword (ID or customer name)
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();
                query = query.Where(h => 
                    h.id.ToLower().Contains(keyword) || 
                    h.KhachHang.hoTen.ToLower().Contains(keyword));
            }

            // Filter by date range
            if (fromDate.HasValue)
            {
                query = query.Where(h => h.ngayLap >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                var endOfDay = toDate.Value.Date.AddDays(1).AddSeconds(-1);
                query = query.Where(h => h.ngayLap <= endOfDay);
            }

            // Filter by status
            if (!string.IsNullOrEmpty(status) && status != "Tất cả")
            {
                query = query.Where(h => h.trangThai == status);
            }

            return query.OrderByDescending(h => h.ngayLap).ToList();
        }

        /// <summary>
        /// T?o hóa ??n m?i
        /// </summary>
        public Tuple<bool, string, string> CreateInvoice(HoaDon invoice, List<ChiTietHoaDon> details)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Generate ID if not provided
                    if (string.IsNullOrEmpty(invoice.id))
                    {
                        invoice.id = GenerateNewInvoiceId();
                    }

                    // Set default values
                    invoice.ngayLap = DateTime.Now;
                    invoice.trangThai = "Chờ thanh toán";
                    invoice.isDelete = false;

                    // Calculate total
                    decimal total = 0;
                    foreach (var detail in details)
                    {
                        detail.hoaDonId = invoice.id;
                        detail.tongTien = (detail.donGia * detail.soLuong) - (detail.giamGia ?? 0);
                        detail.isDelete = false;
                        total += detail.tongTien;
                    }

                    invoice.tongTien = total;

                    // Add to database
                    _context.HoaDons.Add(invoice);
                    _context.ChiTietHoaDons.AddRange(details);

                    _context.SaveChanges();
                    transaction.Commit();

                    return Tuple.Create(true, "Tạo hóa đơn thành công", invoice.id);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Tuple.Create(false, $"Lỗi: {ex.Message}", (string)null);
                }
            }
        }

        /// <summary>
        /// C?p nh?t hóa ??n
        /// </summary>
        public Tuple<bool, string> UpdateInvoice(HoaDon invoice, List<ChiTietHoaDon> details)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var existingInvoice = _context.HoaDons.FirstOrDefault(h => h.id == invoice.id);
                    if (existingInvoice == null)
                        return Tuple.Create(false, "Không tìm thấy hóa đơn");

                    // Update invoice info
                    existingInvoice.khachHangId = invoice.khachHangId;
                    existingInvoice.nhanVienId = invoice.nhanVienId;
                    existingInvoice.trangThai = invoice.trangThai;

                    // Delete old details
                    var oldDetails = _context.ChiTietHoaDons.Where(d => d.hoaDonId == invoice.id);
                    _context.ChiTietHoaDons.RemoveRange(oldDetails);

                    // Add new details
                    decimal total = 0;
                    foreach (var detail in details)
                    {
                        detail.hoaDonId = invoice.id;
                        detail.tongTien = (detail.donGia * detail.soLuong) - (detail.giamGia ?? 0);
                        detail.isDelete = false;
                        total += detail.tongTien;
                    }

                    existingInvoice.tongTien = total;
                    _context.ChiTietHoaDons.AddRange(details);

                    _context.SaveChanges();
                    transaction.Commit();

                    return Tuple.Create(true, "Cập nhật hóa đơn thành công");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Tuple.Create(false, $"Lỗi: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Xóa hóa ??n (soft delete)
        /// </summary>
        public Tuple<bool, string> DeleteInvoice(string id)
        {
            try
            {
                var invoice = _context.HoaDons.FirstOrDefault(h => h.id == id);
                if (invoice == null)
                    return Tuple.Create(false, "Không tìm thấy hóa đơn");

                // Only allow deleting unpaid invoices
                if (invoice.trangThai == "Đã thanh toán")
                    return Tuple.Create(false, "Không thể xóa hóa Đơn Đã thanh toán");

                invoice.isDelete = true;

                // Soft delete details
                var details = _context.ChiTietHoaDons.Where(d => d.hoaDonId == id);
                foreach (var detail in details)
                {
                    detail.isDelete = true;
                }

                _context.SaveChanges();
                return Tuple.Create(true, "Xóa hóa đơn thành công");
            }
            catch (Exception ex)
            {
                return Tuple.Create(false, $"Lỗi: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy chi tiết hoá đơn
        /// </summary>
        public List<ChiTietHoaDon> GetInvoiceDetails(string invoiceId)
        {
            return _context.ChiTietHoaDons
                .Where(d => d.hoaDonId == invoiceId && !d.isDelete)
                .ToList();
        }

        /// <summary>
        /// Thanh toán hóa đơn
        /// </summary>
        public Tuple<bool, string> ProcessPayment(string invoiceId, string paymentMethodId, decimal amount, string description)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var invoice = _context.HoaDons.FirstOrDefault(h => h.id == invoiceId);
                    if (invoice == null)
                        return Tuple.Create(false, "Không tìm thấy hóa đơn");

                    // Check if already paid
                    if (invoice.trangThai == "?ã thanh toán")
                        return Tuple.Create(false, "Hóa đơn đã đượcc thanh toán");

                    // Calculate total paid
                    var totalPaid = _context.GiaoDichThanhToans
                        .Where(g => g.hoaDonId == invoiceId && !g.isDelete)
                        .Sum(g => (decimal?)g.soTien) ?? 0;

                    totalPaid += amount;

                    // Create payment transaction
                    var payment = new GiaoDichThanhToan
                    {
                        id = GeneratePaymentId(),
                        hoaDonId = invoiceId,
                        soTien = amount,
                        ngayGD = DateTime.Now,
                        kenhThanhToanId = paymentMethodId,
                        moTa = description,
                        isDelete = false
                    };

                    _context.GiaoDichThanhToans.Add(payment);

                    // Update invoice status
                    if (totalPaid >= invoice.tongTien)
                    {
                        invoice.trangThai = "Đã thanh toán";
                    }
                    else
                    {
                        invoice.trangThai = "Đang thanh toán";
                    }

                    _context.SaveChanges();
                    transaction.Commit();

                    return Tuple.Create(true, "Thanh toán thành công");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Tuple.Create(false, $"Lỗi: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Lấy danh sách thanh toán
        /// </summary>
        public List<GiaoDichThanhToan> GetPaymentHistory(string invoiceId)
        {
            return _context.GiaoDichThanhToans
                .Include(g => g.KenhThanhToan)
                .Where(g => g.hoaDonId == invoiceId && !g.isDelete)
                .OrderByDescending(g => g.ngayGD)
                .ToList();
        }

        /// <summary>
        /// lấy danh sách pt thanh toán
        /// </summary>
        public List<KenhThanhToan> GetPaymentMethods()
        {
            return _context.KenhThanhToans
                .Where(k => !k.isDelete && k.trangThai == "Hoạt Động")
                .OrderBy(k => k.tenKenh)
                .ToList();
        }

        /// <summary>
        /// Tạo mã hóa đơn
        /// </summary>
        public string GenerateNewInvoiceId()
        {
            var lastInvoice = _context.HoaDons
                .Where(h => h.id.StartsWith("HD"))
                .OrderByDescending(h => h.id)
                .FirstOrDefault();

            if (lastInvoice != null)
            {
                var lastNumber = int.Parse(lastInvoice.id.Substring(2));
                return $"HD{(lastNumber + 1):D3}";
            }

            return "HD001";
        }

        /// <summary>
        /// Tạo mã giao dịch thanh toán
        /// </summary>
        private string GeneratePaymentId()
        {
            var lastPayment = _context.GiaoDichThanhToans
                .Where(g => g.id.StartsWith("GD"))
                .OrderByDescending(g => g.id)
                .FirstOrDefault();

            if (lastPayment != null)
            {
                var lastNumber = int.Parse(lastPayment.id.Substring(2));
                return $"GD{(lastNumber + 1):D3}";
            }

            return "GD001";
        }

        /// <summary>
        /// Xử lý hoàn trả hàng: tạo PhieuDoiTra và tùy chọn tăng tồn kho
        /// </summary>
        public Tuple<bool, string> ProcessReturn(
            string hoaDonId,
            string sanPhamDonViId,
            int soLuong,
            string lyDo,
            decimal soTienHoan,
            string chinhSachId,
            bool addToStock)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (string.IsNullOrEmpty(hoaDonId))
                        return Tuple.Create(false, "Mã hóa đơn không hợp lệ");

                    if (string.IsNullOrEmpty(sanPhamDonViId))
                        return Tuple.Create(false, "Sản phẩm không hợp lệ");

                    if (soLuong <=0)
                        return Tuple.Create(false, "Số lượng hoàn trả phải lớn hơn 0");

                    var hoaDon = _context.HoaDons.FirstOrDefault(h => h.id == hoaDonId && !h.isDelete);
                    if (hoaDon == null)
                        return Tuple.Create(false, "Không tìm thấy hóa đơn");

                    // Chỉ cho phép hoàn trả khi hóa đơn đã thanh toán
                    if (!string.Equals(hoaDon.trangThai?.Trim(), "Đã thanh toán", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return Tuple.Create(false, "Chỉ cho phép hoàn trả khi hóa đơn đã thanh toán");
                    }

                    // Tạo Phiếu Đổi Trả
                    var phieuId = _services.GenerateNewId<PhieuDoiTra>("PDT",6) ?? Guid.NewGuid().ToString();

                    var phieu = new PhieuDoiTra
                    {
                        id = phieuId,
                        hoaDonId = hoaDonId,
                        sanPhamDonViId = sanPhamDonViId,
                        ngayDoiTra = DateTime.Now,
                        lyDo = lyDo ?? string.Empty,
                        soTienHoan = soTienHoan,
                        chinhSachId = chinhSachId ?? string.Empty,
                        isDelete = false
                    };

                    _context.PhieuDoiTras.Add(phieu);

                    if (addToStock)
                    {
                        // Cập nhật TonKho
                        var tonKho = _context.TonKhoes.FirstOrDefault(tk => tk.sanPhamDonViId == sanPhamDonViId && !tk.isDelete);
                        if (tonKho != null)
                        {
                            tonKho.soLuongTon += soLuong;
                            _context.Entry(tonKho).State = EntityState.Modified;
                        }
                        else
                        {
                            var newTkId = _services.GenerateNewId<TonKho>("TK",8) ?? Guid.NewGuid().ToString();
                            var newTonKho = new TonKho
                            {
                                id = newTkId,
                                sanPhamDonViId = sanPhamDonViId,
                                soLuongTon = soLuong,
                                isDelete = false
                            };
                            _context.TonKhoes.Add(newTonKho);
                        }

                        // Cập nhật SanPhamViTri nếu có (tăng số lượng tại vị trí tồn kho đầu tiên)
                        var spvt = _context.SanPhamViTris.FirstOrDefault(s => s.sanPhamDonViId == sanPhamDonViId && !s.isDelete);
                        if (spvt != null)
                        {
                            spvt.soLuong += soLuong;
                            _context.Entry(spvt).State = EntityState.Modified;
                        }
                    }

                    _context.SaveChanges();
                    transaction.Commit();

                    return Tuple.Create(true, "Hoàn trả thành công");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Tuple.Create(false, $"Lỗi hoàn trả: {ex.Message}");
                }
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
