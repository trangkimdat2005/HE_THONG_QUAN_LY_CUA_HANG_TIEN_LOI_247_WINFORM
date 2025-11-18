using System;
using System.Collections.Generic;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers
{
    /// <summary>
    /// Controller điều phối các thao tác liên quan đến Sản phẩm
    /// </summary>
    public class ProductController
    {
        private readonly ProductService _productService;

        public ProductController()
        {
            _productService = new ProductService();
        }

        #region Get/Search Operations

        public List<SanPham> GetAllProducts()
        {
            return _productService.GetAllProducts();
        }

        public SanPham GetProductById(string id)
        {
            return _productService.GetProductById(id);
        }

        public List<SanPham> FilterProducts(string keyword = null, string brandId = null, string categoryId = null)
        {
            return _productService.FilterProducts(keyword, brandId, categoryId);
        }

        #endregion

        #region CRUD Operations

        /// <summary>
        /// Thêm sản phẩm mới
        /// </summary>
        public (bool success, string message, SanPham product) AddProduct(SanPham product, string categoryId = null)
        {
            // Service đã xử lý try-catch và Detach, Controller chỉ cần chuyển tiếp kết quả
            return _productService.AddProduct(product, categoryId);
        }

        /// <summary>
        /// Cập nhật thông tin sản phẩm
        /// </summary>
        public (bool success, string message) UpdateProduct(SanPham product, string categoryId = null)
        {
            return _productService.UpdateProduct(product, categoryId);
        }

        /// <summary>
        /// Xóa sản phẩm
        /// </summary>
        public (bool success, string message) DeleteProduct(string productId)
        {
            return _productService.DeleteProduct(productId);
        }

        #endregion

        #region Helper Methods & Validation

        public string GenerateNewProductId()
        {
            return _productService.GenerateNewProductId();
        }

        public string GetProductCategoryId(string productId)
        {
            return _productService.GetProductCategoryId(productId);
        }

        public bool IsProductInUse(string productId)
        {
            return _productService.IsProductInUse(productId);
        }

        #endregion

        #region Disposal

        public void Dispose()
        {
            _productService?.Dispose();
        }

        #endregion
    }
}