using System;
using System.Collections.Generic;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers
{
    /// <summary>
    /// Controller xử lý các thao tác liên quan đến Hóa đơn
    /// Đóng vai trò trung gian giữa GUI và Business Logic Layer
    /// </summary>
    public class InvoiceController : IDisposable
    {
        private readonly InvoiceService _invoiceService;

        public InvoiceController()
        {
            _invoiceService = new InvoiceService();
        }

        #region Get Data

        /// <summary>
        /// Lấy tất cả hóa đơn
        /// </summary>
        public List<HoaDon> GetAllInvoices()
        {
            return _invoiceService.GetAllInvoices();
        }

        /// <summary>
        /// Lấy hóa đơn theo ID
        /// </summary>
        public HoaDon GetInvoiceById(string id)
        {
            return _invoiceService.GetInvoiceById(id);
        }

        /// <summary>
        /// Tìm kiếm hóa đơn theo nhiều tiêu chí
        /// </summary>
        public List<HoaDon> SearchInvoices(string keyword, DateTime? fromDate, DateTime? toDate, string status)
        {
            return _invoiceService.SearchInvoices(keyword, fromDate, toDate, status);
        }

        /// <summary>
        /// Lấy chi tiết hóa đơn
        /// </summary>
        public List<ChiTietHoaDon> GetInvoiceDetails(string invoiceId)
        {
            return _invoiceService.GetInvoiceDetails(invoiceId);
        }

        /// <summary>
        /// Lấy lịch sử thanh toán của hóa đơn
        /// </summary>
        public List<GiaoDichThanhToan> GetPaymentHistory(string invoiceId)
        {
            return _invoiceService.GetPaymentHistory(invoiceId);
        }

        /// <summary>
        /// Lấy danh sách phương thức thanh toán
        /// </summary>
        public List<KenhThanhToan> GetPaymentMethods()
        {
            return _invoiceService.GetPaymentMethods();
        }

        /// <summary>
        /// Tạo mã hóa đơn tự động
        /// </summary>
        public string GenerateNewInvoiceId()
        {
            return _invoiceService.GenerateNewInvoiceId();
        }

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Tạo hóa đơn mới
        /// </summary>
        /// <param name="invoice">Thông tin hóa đơn</param>
        /// <param name="details">Chi tiết hóa đơn</param>
        /// <returns>Tuple(success, message, invoiceId)</returns>
        public (bool success, string message, string invoiceId) CreateInvoice(HoaDon invoice, List<ChiTietHoaDon> details)
        {
            // Validation
            if (invoice == null)
                return (false, "Thông tin hóa đơn không hợp lệ", null);

            if (details == null || details.Count == 0)
                return (false, "Hóa đơn phải có ít nhất một sản phẩm", null);

            if (string.IsNullOrEmpty(invoice.nhanVienId))
                return (false, "Vui lòng chọn nhân viên", null);

            var result = _invoiceService.CreateInvoice(invoice, details);
            return (result.Item1, result.Item2, result.Item3);
        }

        /// <summary>
        /// Cập nhật hóa đơn
        /// </summary>
        /// <param name="invoice">Thông tin hóa đơn</param>
        /// <param name="details">Chi tiết hóa đơn</param>
        /// <returns>Tuple(success, message)</returns>
        public (bool success, string message) UpdateInvoice(HoaDon invoice, List<ChiTietHoaDon> details)
        {
            // Validation
            if (invoice == null)
                return (false, "Thông tin hóa đơn không hợp lệ");

            if (string.IsNullOrEmpty(invoice.id))
                return (false, "Mã hóa đơn không được để trống");

            if (details == null || details.Count == 0)
                return (false, "Hóa đơn phải có ít nhất một sản phẩm");

            var result = _invoiceService.UpdateInvoice(invoice, details);
            return (result.Item1, result.Item2);
        }

        /// <summary>
        /// Xóa hóa đơn (soft delete)
        /// </summary>
        /// <param name="invoiceId">Mã hóa đơn</param>
        /// <returns>Tuple(success, message)</returns>
        public (bool success, string message) DeleteInvoice(string invoiceId)
        {
            if (string.IsNullOrEmpty(invoiceId))
                return (false, "Mã hóa đơn không được để trống");

            var result = _invoiceService.DeleteInvoice(invoiceId);
            return (result.Item1, result.Item2);
        }

        #endregion

        #region Payment Operations

        /// <summary>
        /// Xử lý thanh toán cho hóa đơn
        /// </summary>
        /// <param name="invoiceId">Mã hóa đơn</param>
        /// <param name="paymentMethodId">Mã phương thức thanh toán</param>
        /// <param name="amount">Số tiền thanh toán</param>
        /// <param name="description">Mô tả giao dịch</param>
        /// <returns>Tuple(success, message)</returns>
        public (bool success, string message) ProcessPayment(string invoiceId, string paymentMethodId, decimal amount, string description)
        {
            // Validation
            if (string.IsNullOrEmpty(invoiceId))
                return (false, "Mã hóa đơn không được để trống");

            if (string.IsNullOrEmpty(paymentMethodId))
                return (false, "Vui lòng chọn phương thức thanh toán");

            if (amount <= 0)
                return (false, "Số tiền thanh toán phải lớn hơn 0");

            var result = _invoiceService.ProcessPayment(invoiceId, paymentMethodId, amount, description);
            return (result.Item1, result.Item2);
        }

        #endregion

        public void Dispose()
        {
            _invoiceService?.Dispose();
        }
    }
}
