using System;
using System.Collections.Generic;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers
{
    public class PromotionController : IDisposable
    {
        #region Fields
        private readonly PromotionService _promotionService;
        private readonly QuanLyServices _baseService;
        private bool _disposed;
        #endregion

        #region Constructor
        public PromotionController()
        {
            _promotionService = new PromotionService();
            _baseService = new QuanLyServices();
        }
        #endregion

        #region Get Operations

        public List<PromotionListDto> GetAllPromotions()
        {
            return _promotionService.GetAllPromotions();
        }

        public List<PromotionListDto> SearchPromotions(string keyword)
        {
            return _promotionService.SearchPromotions(keyword ?? string.Empty);
        }

        public ChuongTrinhKhuyenMai GetPromotionById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Mã chương trình không hợp lệ");

            return _promotionService.GetPromotionById(id);
        }

        public List<DieuKienApDung> GetConditionsByProgramId(string chuongTrinhId)
        {
            if (string.IsNullOrWhiteSpace(chuongTrinhId))
                return new List<DieuKienApDung>();

            return _promotionService.GetConditionsByProgramId(chuongTrinhId);
        }

        #endregion

        #region CRUD Operations

        public string GenerateNewPromotionId()
        {
            try
            {
                return _baseService.GenerateNewId<ChuongTrinhKhuyenMai>("CTKM", 8);
            }
            catch
            {
                return "CTKM" + DateTime.Now.ToString("ddMMyyHHmm");
            }
        }

        public (bool success, string message, string promotionId) CreatePromotion(
            ChuongTrinhKhuyenMai promotion,
            List<DieuKienApDung> conditions = null)
        {
            try
            {
                if (promotion == null)
                    return (false, "Dữ liệu chương trình trống", null);

                return _promotionService.CreatePromotion(promotion, conditions);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi Controller: {ex.Message}", null);
            }
        }

        public (bool success, string message) UpdatePromotion(
            ChuongTrinhKhuyenMai promotion,
            List<DieuKienApDung> conditions = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(promotion?.id))
                    return (false, "ID chương trình không hợp lệ");

                return _promotionService.UpdatePromotion(promotion, conditions);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi Controller: {ex.Message}");
            }
        }

        public (bool success, string message) DeletePromotion(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                    return (false, "ID không hợp lệ");

                return _promotionService.DeletePromotion(id);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi Controller: {ex.Message}");
            }
        }

        #endregion

        #region Dispose Pattern

        public void Dispose()
        {
            if (!_disposed)
            {
                _promotionService?.Dispose();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}