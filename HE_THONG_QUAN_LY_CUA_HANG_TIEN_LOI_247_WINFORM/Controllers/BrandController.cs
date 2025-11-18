using System;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers
{
    public class BrandController 
    {
        private readonly BrandService _brandService;

        public BrandController()
        {
            _brandService = new BrandService();
        }

        public dynamic GetAllBrands()
        {
            return _brandService.GetAllBrands();
        }

        public dynamic SearchBrands(string keyword)
        {
            return _brandService.SearchBrands(keyword);
        }

        public NhanHieu GetBrandById(string id)
        {
            return _brandService.GetBrandById(id);
        }

        public int GetProductCount(string id)
        {
            return _brandService.GetProductCount(id);
        }

        public string GenerateNewBrandId()
        {
            return _brandService.GenerateNewBrandId();
        }

        public (bool success, string message, NhanHieu brand) AddBrand(NhanHieu brand)
        {
            return _brandService.AddBrand(brand);
        } 
        public (bool success, string message) UpdateBrand(NhanHieu brand)
        {
             return _brandService.UpdateBrand(brand);
        }

        public (bool success, string message) DeleteBrand(string brandId)
        {
            return _brandService.DeleteBrand(brandId);
        }

        public void Dispose()
        {
            _brandService?.Dispose();
        }
    }
}