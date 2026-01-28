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
                    // ✅ TÌM BẢN GHI (Kể cả đã xóa mềm)
                    var existingProduct = context.SanPhamDonVis
                        .Where(p =>
                            p.sanPhamId == model.sanPhamId &&
                            p.donViId == model.donViId)
                        .FirstOrDefault();

                    if (existingProduct != null)
                    {
                        if (existingProduct.isDelete)
                        {
                            // ✅ TỰ ĐỘNG KHÔI PHỤC - KHÔNG BÁO LỖI
                            existingProduct.isDelete = false;
                            existingProduct.giaBan = model.giaBan;
                            existingProduct.heSoQuyDoi = model.heSoQuyDoi;
                            existingProduct.trangThai = model.trangThai ?? "Còn hàng";

                            context.SaveChanges();

                            return (true, 
                                $" Thêm sản phẩm thành công!\nMã: {existingProduct.id}", 
                                existingProduct);
                        }
                        else
                        {
                            // ✅ ĐÃ TỒN TẠI VÀ ĐANG ACTIVE → BÁO LỖI CHI TIẾT
                            var detailInfo = context.SanPhamDonVis
                                .AsNoTracking()
                                .Where(p => p.id == existingProduct.id)
                                .Select(p => new {
                                    p.id,
                                    p.giaBan,
                                    p.trangThai,
                                    TenSanPham = p.SanPham.ten,
                                    TenDonVi = p.DonViDoLuong.ten
                                })
                                .FirstOrDefault();

                            return (false, 
                                $"Sản phẩm với đơn vị này đã tồn tại!\n\n" +
                                $"Mã: {detailInfo.id}\n" +
                                $"Tên: {detailInfo.TenSanPham ?? "Không rõ"}\n" +
                                $"Đơn vị: {detailInfo.TenDonVi ?? "Không rõ"}\n" +
                                $"Giá bán: {detailInfo.giaBan:N0} đ\n" +
                                $"Trạng thái: {detailInfo.trangThai ?? "Không rõ"}\n\n", 
                                null);
                        }
                    }

                    // ✅ THÊM MỚI (không tồn tại)
                    model.id = GenerateNewProductUnitId();
                    model.isDelete = false;

                    if (string.IsNullOrEmpty(model.trangThai))
                        model.trangThai = "Còn hàng";

                    context.SanPhamDonVis.Add(model);
                    context.SaveChanges();

                    return (true, $"✓ Thêm sản phẩm thành công!\nMã: {model.id}", model);
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException dbEx)
            {
                var innerMsg = dbEx.InnerException?.InnerException?.Message ?? dbEx.InnerException?.Message ?? dbEx.Message;
                
                if (innerMsg.Contains("PRIMARY KEY") || innerMsg.Contains("PK_"))
                {
                    return (false,
                        $"Sản phẩm với Hàng hóa và Đơn vị này đã tồn tại trong hệ thống.\n\n", 
                        null);
                }
                
                return (false, $"Lỗi cơ sở dữ liệu:\n{innerMsg}", null);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi thêm sản phẩm:\n{ex.Message}", null);
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
                        return (false, " Không tìm thấy sản phẩm!");

                    //  Kiểm tra trùng lặp (trừ chính nó) với thông báo chi tiết
                    var duplicateProduct = context.SanPhamDonVis
                        .AsNoTracking()
                        .Where(p =>
                            p.id != model.id &&
                            p.sanPhamId == model.sanPhamId &&
                            p.donViId == model.donViId &&
                            !p.isDelete)
                        .Select(p => new {
                            p.id,
                            p.giaBan,
                            TenSanPham = p.SanPham.ten,
                            TenDonVi = p.DonViDoLuong.ten
                        })
                        .FirstOrDefault();

                    if (duplicateProduct != null)
                    {
                        return (false,
                            $"Sản phẩm với đơn vị này đã tồn tại!\n\n" +
                            $"Mã trùng: {duplicateProduct.id}\n" +
                            $"Tên: {duplicateProduct.TenSanPham ?? "Không rõ"}\n" +
                            $"Đơn vị: {duplicateProduct.TenDonVi ?? "Không rõ"}\n" +
                            $"Giá bán: {duplicateProduct.giaBan:N0} đ\n\n");
                    }

                    // ✅ Cập nhật - KHÔNG SỬA sanPhamId và donViId (là khóa chính
                    // Chỉ sửa giá bán, trạng thái, hệ số quy đổi
                    existing.giaBan = model.giaBan;
                    existing.heSoQuyDoi = model.heSoQuyDoi;
                    existing.trangThai = model.trangThai ?? "Còn hàng";

                    context.SaveChanges();
                    return (true, "✓ Cập nhật sản phẩm thành công!");
                }
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException dbEx)
            {
                var innerMsg = dbEx.InnerException?.InnerException?.Message ?? dbEx.InnerException?.Message ?? dbEx.Message;
                
                if (innerMsg.Contains("PRIMARY KEY") || innerMsg.Contains("PK_"))
                {
                    return (false, 
                        $"Không thể thay đổi Hàng hóa hoặc Đơn vị vì đã tồn tại.\n\n" );
                }
                
                return (false, $"Lỗi cơ sở dữ liệu:\n{innerMsg}");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi cập nhật:\n{ex.Message}");
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