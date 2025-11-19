using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models; // <-- Đảm bảo namespace này đúng nơi chứa ProductDetailDto
using System;
using System.Collections.Generic;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers
{
    public class ProductController
    {
        private readonly ProductService _productService;

        public ProductController()
        {
            _productService = new ProductService();
        }

        #region Get Data

        // SỬA QUAN TRỌNG: Trả về List<ProductDetailDto> (đúng tên file DTO bạn vừa tạo)
        public List<ProductDetailDto> FilterProducts(string keyword = null, string brandId = null, string categoryId = null)
        {
            // Gọi Service (Lưu ý: Bên Service cũng phải trả về List<ProductDetailDto> nhé)
            return _productService.FilterProducts(keyword, brandId, categoryId);
        }

        // Hàm này trả về Entity gốc (SanPham) để fill vào các ô nhập liệu khi sửa
        public SanPham GetProductById(string id)
        {
            return _productService.GetProductById(id);
        }

        public string GetProductCategoryId(string productId)
        {
            return _productService.GetProductCategoryId(productId);
        }

        public string GenerateNewProductId()
        {
            return _productService.GenerateNewProductId();
        }

        #endregion

        #region CRUD

        public (bool success, string message, SanPham product) AddProduct(SanPham product, string categoryId)
        {
            if (string.IsNullOrEmpty(product.ten))
                return (false, "Tên sản phẩm không được để trống", null);

            return _productService.AddProduct(product, categoryId);
        }

        public (bool success, string message) UpdateProduct(SanPham product, string categoryId)
        {
            return _productService.UpdateProduct(product, categoryId);
        }

        public (bool success, string message) DeleteProduct(string productId)
        {
            return _productService.DeleteProduct(productId);
        }

        #endregion

        public void Dispose()
        {
            _productService?.Dispose();
        }
    }
}