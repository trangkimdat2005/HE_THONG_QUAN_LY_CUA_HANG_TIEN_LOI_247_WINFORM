using System;
using System.Collections.Generic;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers
{
    public class BarcodeController
    {
        private readonly BarcodeService _barcodeService;

        public BarcodeController()
        {
            _barcodeService = new BarcodeService();
        }

        public dynamic GetAllBarcodes()
        {
            return _barcodeService.GetAllBarcodes();
        }

        public dynamic SearchBarcodes(string keyword)
        {
            return _barcodeService.SearchBarcodes(keyword);
        }

        public MaDinhDanhSanPham GetBarcodeById(string id)
        {
            return _barcodeService.GetBarcodeById(id);
        }

        public List<SanPhamDonVi> GetAllProductUnits()
        {
            return _barcodeService.GetAllProductUnits();
        }

        public string GenerateNewBarcodeId()
        {
            return _barcodeService.GenerateNewBarcodeId();
        }

        public (bool success, string message, MaDinhDanhSanPham barcode) AddBarcode(MaDinhDanhSanPham barcode)
        {
            return _barcodeService.AddBarcode(barcode);
        }

        public (bool success, string message) UpdateBarcode(MaDinhDanhSanPham barcode)
        {
            return _barcodeService.UpdateBarcode(barcode);
        }

        public (bool success, string message) DeleteBarcode(string barcodeId)
        {
            return _barcodeService.DeleteBarcode(barcodeId);
        }

        public void Dispose()
        {
            _barcodeService?.Dispose();
        }
    }
}
