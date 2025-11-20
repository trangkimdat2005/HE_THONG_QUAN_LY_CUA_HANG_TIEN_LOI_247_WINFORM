using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models; 
using System;
using System.Collections.Generic;
using System.Linq;

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

        public List<ProductDetailDto> FilterProducts(string keyword = null, string brandId = null, string categoryId = null)
        {
            return _productService.FilterProducts(keyword, brandId, categoryId);
        }

        public List<ProductDetailDto> SearchProducts(string keyword)
        {
            return _productService.FilterProducts(keyword, null, null);
        }

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

        public string GenerateNewProductUnitId()
        {
            using (var context = new AppDbContext())
            {
                var allIds = context.SanPhamDonVis
                    .Where(p => p.id.StartsWith("SPDV") && p.id.Length == 8)
                    .Select(p => p.id)
                    .ToList();

                if (allIds.Count == 0)
                    return "SPDV0001";

                var maxNumber = allIds
                    .Select(id => {
                        int num;
                        if (int.TryParse(id.Substring(4), out num))
                            return num;
                        return 0;
                    })
                    .Max();

                return $"SPDV{(maxNumber + 1):D4}";
            }
        }

        // Lấy danh sách hàng hóa cho ComboBox
        public List<object> GetAllGoods()
        {
            using (var context = new AppDbContext())
            {
                return context.SanPhams
                    .Where(sp => !sp.isDelete)
                    .OrderBy(sp => sp.ten)
                    .Select(sp => new { id = sp.id, ten = sp.ten })
                    .ToList<object>();
            }
        }

        // Lấy danh sách đơn vị đo lường cho ComboBox
        public List<object> GetAllUnits()
        {
            using (var context = new AppDbContext())
            {
                return context.DonViDoLuongs
                    .Where(u => !u.isDelete)
                    .OrderBy(u => u.ten)
                    .Select(u => new { id = u.id, ten = u.ten })
                    .ToList<object>();
            }
        }

        #endregion

        #region CRUD for SanPham (Goods)

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

        #region CRUD for SanPhamDonVi (Product Unit)

        // Thêm mới đơn vị sản phẩm
        public (bool success, string message, SanPhamDonVi result) AddProductUnit(SanPhamDonVi model)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // Kiểm tra trùng lặp
                    var exists = context.SanPhamDonVis.Any(p =>
                        p.sanPhamId == model.sanPhamId &&
                        p.donViId == model.donViId &&
                        !p.isDelete);

                    if (exists)
                        return (false, "Sản phẩm với đơn vị này đã tồn tại!", null);

                    // Tạo ID mới theo format SPDV0001
                    model.id = GenerateNewProductUnitId();
                    model.isDelete = false;

                    context.SanPhamDonVis.Add(model);
                    context.SaveChanges();

                    return (true, $"Thêm thành công! Mã: {model.id}", model);
                }
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}", null);
            }
        }

        // Cập nhật đơn vị sản phẩm
        public (bool success, string message) UpdateProductUnit(SanPhamDonVi model)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var existing = context.SanPhamDonVis.FirstOrDefault(p => p.id == model.id);
                    if (existing == null)
                        return (false, "Không tìm thấy sản phẩm!");

                    // Kiểm tra trùng lặp (trừ chính nó)
                    var duplicate = context.SanPhamDonVis.Any(p =>
                        p.id != model.id &&
                        p.sanPhamId == model.sanPhamId &&
                        p.donViId == model.donViId &&
                        !p.isDelete);

                    if (duplicate)
                        return (false, "Sản phẩm với đơn vị này đã tồn tại!");

                    // Cập nhật
                    existing.sanPhamId = model.sanPhamId;
                    existing.donViId = model.donViId;
                    existing.giaBan = model.giaBan;
                    existing.heSoQuyDoi = model.heSoQuyDoi;
                    existing.trangThai = model.trangThai;

                    context.SaveChanges();
                    return (true, "Cập nhật thành công!");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        // Xóa đơn vị sản phẩm (soft delete)
        public (bool success, string message) DeleteProductUnit(string id)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var productUnit = context.SanPhamDonVis.FirstOrDefault(p => p.id == id);
                    if (productUnit == null)
                        return (false, "Không tìm thấy sản phẩm!");

                    productUnit.isDelete = true;
                    context.SaveChanges();

                    return (true, "Xóa thành công!");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        #endregion

        public void Dispose()
        {
            _productService?.Dispose();
        }
    }
}