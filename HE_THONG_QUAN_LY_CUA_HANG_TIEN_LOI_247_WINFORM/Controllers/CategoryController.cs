using System;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers
{
    public class CategoryController : IDisposable
    {
        private readonly CategoryService _categoryService;
        private readonly IQuanLyServices _quanLyServices;

        public CategoryController()
        {
            _categoryService = new CategoryService();
            _quanLyServices = new QuanLyServices();
        }

        public dynamic GetAllCategories()
        {
            return _categoryService.GetAllCategories();
        }

        public dynamic SearchCategories(string keyword)
        {
            return _categoryService.SearchCategories(keyword);
        }

        public DanhMuc GetCategoryById(string id)
        {
            return _categoryService.GetCategoryById(id);
        }

        public int GetProductCountFromLocation(string id)
        {
            return _categoryService.GetProductCountFromLocation(id);
        }

        public (bool success, string message, DanhMuc category) AddCategory(DanhMuc category)
        {
            return _categoryService.AddCategory(category);
        }

        public (bool success, string message) UpdateCategory(DanhMuc category)
        {
            return _categoryService.UpdateCategory(category);
        }

        public (bool success, string message) DeleteCategory(string categoryId)
        {
            return _categoryService.DeleteCategory(categoryId);
        }
        
        public string GenerateNewCategoryId()
        {
            return _categoryService.GenerateNewCategoryId();
        }
        
        public void Dispose() => _categoryService?.Dispose();
    }
}