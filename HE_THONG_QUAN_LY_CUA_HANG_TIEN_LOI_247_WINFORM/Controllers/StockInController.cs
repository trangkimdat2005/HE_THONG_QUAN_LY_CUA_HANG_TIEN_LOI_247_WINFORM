using System;
using System.Collections.Generic;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers
{
    public class StockInController : IDisposable
    {
        private readonly StockInService _stockInService;

        public StockInController()
        {
            _stockInService = new StockInService();
        }

        // Lấy tất cả phiếu nhập
        public dynamic GetAllImportReceipts()
        {
            return _stockInService.GetAllImportReceipts();
        }

        public dynamic SearchImportReceipts(string keyword)
        {
            return _stockInService.SearchImportReceipts(keyword);
        }

        // Lấy chi tiết phiếu nhập
        public dynamic GetImportReceiptDetails(string phieuNhapId)
        {
            if (string.IsNullOrEmpty(phieuNhapId))
                throw new ArgumentException("Mã phiếu nhập không hợp lệ");

            return _stockInService.GetImportReceiptDetails(phieuNhapId);
        }

        public List<NhaCungCap> GetAllSuppliers()
        {
            return _stockInService.GetAllSuppliers();
        }

        public List<NhanVien> GetAllEmployees()
        {
            return _stockInService.GetAllEmployees();
        }

        // Lấy danh sách sản phẩm>
        public dynamic GetAllProductUnits()
        {
            return _stockInService.GetAllProductUnits();
        }

        public string GetLastSupplierByProduct(string sanPhamDonViId)
        {
            if (string.IsNullOrEmpty(sanPhamDonViId))
                return null;

            return _stockInService.GetLastSupplierByProduct(sanPhamDonViId);
        }

        public (bool success, string message, string phieuNhapId) CreateImportReceipt(
            string nhaCungCapId,
            string nhanVienId,
            DateTime ngayNhap,
            List<ChiTietPhieuNhap> chiTietList)
        {
            if (string.IsNullOrEmpty(nhaCungCapId))
                return (false, "Vui lòng chọn nhà cung cấp", null);

            if (string.IsNullOrEmpty(nhanVienId))
                return (false, "Vui lòng chọn nhân viên", null);

            if (chiTietList == null || chiTietList.Count == 0)
                return (false, "Phiếu nhập phải có ít nhất 1 sản phẩm", null);

            return _stockInService.CreateImportReceipt(nhaCungCapId, nhanVienId, ngayNhap, chiTietList);
        }
        public (bool success, string message) DeleteImportReceipt(string id)
        {
            return _stockInService.DeleteImportReceipt(id);
        }
        public void Dispose()
        {
            _stockInService?.Dispose();
        }
    }
}
