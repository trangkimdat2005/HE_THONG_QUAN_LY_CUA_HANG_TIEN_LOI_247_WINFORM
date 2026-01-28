using System;
using System.Collections.Generic;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers
{
    public class PromotionController : IDisposable
    {
        private readonly PromotionService _service;
        private readonly QuanLyServices _baseService;
        private bool _disposed;

        public PromotionController()
        {
            _service = new PromotionService();
            _baseService = new QuanLyServices();
        }

        #region Get Operations

        public List<PromotionListDto> GetAllPromotions() => _service.GetAllPromotions();
        
        public List<PromotionListDto> SearchPromotions(string keyword) => _service.SearchPromotions(keyword ?? "");

        public ChuongTrinhKhuyenMai GetPromotionById(string id) =>
            string.IsNullOrWhiteSpace(id) ? throw new ArgumentException("Mã chương trình không hợp lệ") : _service.GetPromotionById(id);

        public List<DieuKienApDung> GetConditionsByProgramId(string chuongTrinhId) =>
            string.IsNullOrWhiteSpace(chuongTrinhId) ? new List<DieuKienApDung>() : _service.GetConditionsByProgramId(chuongTrinhId);

        public List<string> GetSelectedCategoryIds(string chuongTrinhId) =>
            string.IsNullOrWhiteSpace(chuongTrinhId) ? new List<string>() : _service.GetSelectedCategoryIds(chuongTrinhId);

        public List<string> GetSelectedProductIds(string chuongTrinhId) =>
            string.IsNullOrWhiteSpace(chuongTrinhId) ? new List<string>() : _service.GetSelectedProductIds(chuongTrinhId);

        #endregion

        #region CRUD Operations

        public string GenerateNewPromotionId()
        {
            try { return _baseService.GenerateNewId<ChuongTrinhKhuyenMai>("CTKM", 8); }
            catch { return "CTKM" + DateTime.Now.ToString("ddMMyyHHmm"); }
        }

        public (bool success, string message, string promotionId) CreatePromotionComplete(
            ChuongTrinhKhuyenMai promotion, DieuKienApDung condition,
            string promoCode, decimal promoGiaTri, int promoSoLanSuDung, string promoTrangThai,
            List<string> selectedCategoryIds = null, List<string> selectedProductIds = null)
        {
            if (promotion == null) return (false, "Dữ liệu chương trình trống!", null);
            return _service.CreatePromotionComplete(promotion, condition, promoCode, promoGiaTri, 
                promoSoLanSuDung, promoTrangThai, selectedCategoryIds, selectedProductIds);
        }

        public (bool success, string message) UpdatePromotionComplete(
            ChuongTrinhKhuyenMai promotion, DieuKienApDung condition,
            string promoCode, decimal promoGiaTri, int promoSoLanSuDung, string promoTrangThai,
            List<string> selectedCategoryIds = null, List<string> selectedProductIds = null)
        {
            if (promotion == null || string.IsNullOrWhiteSpace(promotion.id))
                return (false, "Dữ liệu chương trình không hợp lệ!");
            return _service.UpdatePromotionComplete(promotion, condition, promoCode, promoGiaTri, 
                promoSoLanSuDung, promoTrangThai, selectedCategoryIds, selectedProductIds);
        }

        public (bool success, string message, string promotionId) CreatePromotion(
            ChuongTrinhKhuyenMai promotion, List<DieuKienApDung> conditions = null)
        {
            if (promotion == null) return (false, "Dữ liệu chương trình trống", null);
            return _service.CreatePromotion(promotion, conditions);
        }

        public (bool success, string message) UpdatePromotion(
            ChuongTrinhKhuyenMai promotion, List<DieuKienApDung> conditions = null)
        {
            if (string.IsNullOrWhiteSpace(promotion?.id)) return (false, "ID chương trình không hợp lệ");
            return _service.UpdatePromotion(promotion, conditions);
        }

        public (bool success, string message) DeletePromotion(string id) =>
            string.IsNullOrWhiteSpace(id) ? (false, "ID không hợp lệ") : _service.DeletePromotion(id);

        #endregion

        #region Promo Code Operations

        public string GenerateNewPromoCodeId()
        {
            try { return _service.GenerateNewPromoCodeId(); }
            catch { return "MKM" + DateTime.Now.ToString("mmss"); }
        }

        public List<PromoCodeDto> GetPromoCodesByProgramId(string chuongTrinhId) =>
            string.IsNullOrWhiteSpace(chuongTrinhId) ? new List<PromoCodeDto>() : _service.GetPromoCodesByProgramId(chuongTrinhId);

        public MaKhuyenMai GetPromoCodeById(string id) =>
            string.IsNullOrWhiteSpace(id) ? null : _service.GetPromoCodeById(id);

        public MaKhuyenMai GetPromoCodeByCode(string code) =>
            string.IsNullOrWhiteSpace(code) ? null : _service.GetPromoCodeByCode(code);

        public bool IsPromoCodeExists(string code, string excludeId = null) =>
            !string.IsNullOrWhiteSpace(code) && _service.IsPromoCodeExists(code, excludeId);

        public (bool success, string message, MaKhuyenMai result) CreatePromoCode(
            string code, string chuongTrinhId, decimal giaTri, int soLanSuDung, string trangThai = "Hoạt động")
        {
            if (string.IsNullOrWhiteSpace(code)) return (false, "Mã khuyến mãi không được để trống!", null);
            if (string.IsNullOrWhiteSpace(chuongTrinhId)) return (false, "Chưa chọn chương trình khuyến mãi!", null);
            if (giaTri < 0) return (false, "Giá trị khuyến mãi không được âm!", null);
            if (soLanSuDung < 0) return (false, "Số lần sử dụng không được âm!", null);

            return _service.CreatePromoCode(new MaKhuyenMai
            {
                code = code.Trim(),
                chuongTrinhId = chuongTrinhId,
                giaTri = giaTri,
                soLanSuDung = soLanSuDung,
                trangThai = string.IsNullOrEmpty(trangThai) ? "Active" : trangThai,
                isDelete = false
            });
        }

        public (bool success, string message, MaKhuyenMai result) CreatePromoCode(MaKhuyenMai promoCode) =>
            promoCode == null ? (false, "Dữ liệu mã khuyến mãi trống!", null) : _service.CreatePromoCode(promoCode);

        public (bool success, string message) UpdatePromoCode(string id, string code, decimal giaTri, int soLanSuDung, string trangThai)
        {
            if (string.IsNullOrWhiteSpace(id)) return (false, "ID mã khuyến mãi không hợp lệ!");
            if (string.IsNullOrWhiteSpace(code)) return (false, "Mã khuyến mãi không được để trống!");

            return _service.UpdatePromoCode(new MaKhuyenMai
            {
                id = id,
                code = code.Trim(),
                giaTri = giaTri,
                soLanSuDung = soLanSuDung,
                trangThai = string.IsNullOrEmpty(trangThai) ? "Hoạt động" : trangThai
            });
        }

        public (bool success, string message) UpdatePromoCode(MaKhuyenMai promoCode) =>
            promoCode == null || string.IsNullOrWhiteSpace(promoCode.id) 
                ? (false, "Dữ liệu mã khuyến mãi không hợp lệ!") 
                : _service.UpdatePromoCode(promoCode);

        public (bool success, string message) DeletePromoCode(string id) =>
            string.IsNullOrWhiteSpace(id) ? (false, "ID mã khuyến mãi không hợp lệ!") : _service.DeletePromoCode(id);

        #endregion

        public void Dispose()
        {
            if (!_disposed)
            {
                _service?.Dispose();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
}