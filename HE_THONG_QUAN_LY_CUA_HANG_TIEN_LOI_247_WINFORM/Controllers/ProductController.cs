using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services; // Gọi Service
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers
{
    public class ProductController : IDisposable
    {
        // Khai báo Service thay vì DbContext
        private readonly ProductService _service;

        public ProductController()
        {
            _service = new ProductService();
        }

        #region Load Data for ComboBox

        public dynamic GetAllGoods()
        {
            // Service trả về List<SanPham>, ta select lấy id và ten để bind vào ComboBox
            var data = _service.GetAllGoods();
            return data.Select(x => new { id = x.id, ten = x.ten }).ToList();
        }

        public dynamic GetAllUnits()
        {
            // Service trả về List<DonViDoLuong>
            var data = _service.GetAllUnits();
            return data.Select(x => new { id = x.id, ten = x.ten }).ToList();
        }

        #endregion

        #region Grid Operations

        public List<ProductDetailDto> FilterProducts(string keyword, string brandId, string categoryId)
        {
            // Gọi thẳng service, mọi logic lọc đã nằm bên kia
            return _service.FilterProducts(keyword, brandId, categoryId);
        }

        public SanPhamDonVi GetProductUnitById(string id)
        {
            return _service.GetProductUnitById(id);
        }

        #endregion

        #region CRUD

        public (bool success, string message, SanPhamDonVi result) AddProductUnit(SanPhamDonVi model)
        {
            // Validation cơ bản nếu cần, sau đó đẩy sang Service
            if (model.giaBan < 0) return (false, "Giá bán không được âm!", null);

            return _service.AddProductUnit(model);
        }

        public (bool success, string message) UpdateProductUnit(SanPhamDonVi model)
        {
            if (model.giaBan < 0) return (false, "Giá bán không được âm!");

            return _service.UpdateProductUnit(model);
        }

        public (bool success, string message) DeleteProductUnit(string id)
        {
            return _service.DeleteProductUnit(id);
        }

        #endregion

        public void Dispose()
        {
            _service?.Dispose();
        }
    }
}