using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using System;
using System.Collections.Generic;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers
{
    public class GoodsController
    {
        private readonly GoodsService _goodsService;

        public GoodsController()
        {
            _goodsService = new GoodsService();
        }

        #region Get Data Operations

        public List<GoodDetailDto> GetAllGoods(string keyword = null, string brandId = null, string categoryId = null)
        {
            try
            {
                return _goodsService.GetAllGoods(keyword, brandId, categoryId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Controller - L?i l?y danh sách: {ex.Message}", ex);
            }
        }

        public SanPham GetGoodById(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentException("Mã hàng hóa không ???c ?? tr?ng");

                return _goodsService.GetGoodById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Controller - L?i l?y chi ti?t: {ex.Message}", ex);
            }
        }

        public string GetGoodCategoryId(string productId)
        {
            try
            {
                return _goodsService.GetGoodCategoryId(productId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Controller - L?i l?y danh m?c: {ex.Message}", ex);
            }
        }

        public List<NhanHieu> GetAllBrands()
        {
            try
            {
                return _goodsService.GetAllBrands();
            }
            catch (Exception ex)
            {
                throw new Exception($"Controller - L?i l?y nhãn hi?u: {ex.Message}", ex);
            }
        }

        public List<DanhMuc> GetAllCategories()
        {
            try
            {
                return _goodsService.GetAllCategories();
            }
            catch (Exception ex)
            {
                throw new Exception($"Controller - L?i l?y danh m?c: {ex.Message}", ex);
            }
        }

        #endregion

        #region CRUD Operations

        public (bool success, string message, string productId) AddGood(SanPham product, string categoryId)
        {
            try
            {

                if (product == null)
                    return (false, "Thông tin hàng hóa không h?p l?", null);

                if (string.IsNullOrWhiteSpace(product.ten))
                    return (false, "Tên hàng hóa không ???c ?? tr?ng", null);

                if (string.IsNullOrEmpty(product.nhanHieuId))
                    return (false, "Vui lòng ch?n nhãn hi?u", null);

                return _goodsService.AddGood(product, categoryId);
            }
            catch (Exception ex)
            {
                return (false, $"Controller - L?i thêm: {ex.Message}", null);
            }
        }

        public (bool success, string message) UpdateGood(SanPham product, string categoryId)
        {
            try
            {
                if (product == null)
                    return (false, "Thông tin hàng hóa không h?p l?");

                if (string.IsNullOrEmpty(product.id))
                    return (false, "Mã hàng hóa không h?p l?");

                if (string.IsNullOrWhiteSpace(product.ten))
                    return (false, "Tên hàng hóa không ???c ?? tr?ng");

                if (string.IsNullOrEmpty(product.nhanHieuId))
                    return (false, "Vui lòng ch?n nhãn hi?u");

                return _goodsService.UpdateGood(product, categoryId);
            }
            catch (Exception ex)
            {
                return (false, $"Controller - L?i c?p nh?t: {ex.Message}");
            }
        }

        public (bool success, string message) DeleteGood(string productId)
        {
            try
            {
                if (string.IsNullOrEmpty(productId))
                    return (false, "Mã hàng hóa không h?p l?");

                return _goodsService.DeleteGood(productId);
            }
            catch (Exception ex)
            {
                return (false, $"Controller - L?i xóa: {ex.Message}");
            }
        }

        #endregion

        public string GenerateNewProductId()
        {
            try
            {
                return _goodsService.GenerateNewGoodId();
            }
            catch (Exception ex)
            {
                throw new Exception($"Controller - L?i sinh mã: {ex.Message}", ex);
            }
        }

        public void Dispose()
        {
            _goodsService?.Dispose();
        }
    }
}