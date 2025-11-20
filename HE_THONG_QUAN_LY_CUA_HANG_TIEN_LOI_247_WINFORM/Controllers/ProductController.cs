using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers
{
    public class ProductController : IDisposable
    {
        private readonly ProductService _service;

        public ProductController()
        {
            _service = new ProductService();
        }

        #region Load Data for ComboBox

        public object GetAllGoods()
        {
            var data = _service.GetAllGoods();
            return data.Select(x => new { id = x.id, ten = x.ten }).ToList();
        }

        public object GetAllUnits()
        {
            var data = _service.GetAllUnits();
            return data.Select(x => new { id = x.id, ten = x.ten }).ToList();
        }

        #endregion

        #region Grid Operations

        public List<ProductDetailDto> FilterProducts(string keyword, string brandId, string categoryId)
        {
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
            if (model.giaBan < 0)
                return (false, "Giá bán không được âm!", null);

            if (model.heSoQuyDoi <= 0)
                return (false, "Hệ số quy đổi phải lớn hơn 0!", null);

            return _service.AddProductUnit(model);
        }

        public (bool success, string message) UpdateProductUnit(SanPhamDonVi model)
        {
            if (model.giaBan < 0)
                return (false, "Giá bán không được âm!");

            if (model.heSoQuyDoi <= 0)
                return (false, "Hệ số quy đổi phải lớn hơn 0!");

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