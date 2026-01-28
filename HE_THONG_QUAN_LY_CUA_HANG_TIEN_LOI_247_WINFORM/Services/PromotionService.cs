using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services
{
    public class PromotionService : IDisposable
    {
        private const string STATUS_UPCOMING = "Sắp diễn ra";
        private const string STATUS_ONGOING = "Đang diễn ra";
        private const string STATUS_ENDED = "Đã kết thúc";
        private const string GIAM_THEO_PHAN_TRAM = "Phần trăm";
        private const string GIAM_THEO_GIA_TIEN = "Giá tiền";

        private readonly AppDbContext _context;
        private readonly QuanLyServices _baseService;
        private bool _disposed;

        public PromotionService()
        {
            _context = new AppDbContext();
            _baseService = new QuanLyServices();
        }

        #region Get Operations

        public List<PromotionListDto> GetAllPromotions()
        {
            var promotions = _context.ChuongTrinhKhuyenMais
                .AsNoTracking()
                .Where(ct => !ct.isDelete)
                .OrderBy(ct => ct.id)
                .Select(ct => new { ct.id, ct.ten, ct.loai, ct.ngayBatDau, ct.ngayKetThuc, ct.moTa })
                .ToList();

            return promotions.Select(p => new PromotionListDto
            {
                Id = p.id,
                Ten = p.ten,
                Loai = p.loai,
                NgayBatDau = p.ngayBatDau,
                NgayKetThuc = p.ngayKetThuc,
                SoLuongMa = GetPromoCodeCount(p.id),
                TrangThai = GetStatus(p.ngayBatDau, p.ngayKetThuc),
                MoTa = p.moTa
            }).ToList();
        }

        public List<PromotionListDto> SearchPromotions(string keyword)
        {
            keyword = (keyword ?? "").ToLower().Trim();

            var promotions = _context.ChuongTrinhKhuyenMais
                .AsNoTracking()
                .Where(ct => !ct.isDelete &&
                            (ct.id.ToLower().Contains(keyword) ||
                             ct.ten.ToLower().Contains(keyword) ||
                             ct.loai.ToLower().Contains(keyword)))
                .OrderBy(ct => ct.id)
                .Select(ct => new { ct.id, ct.ten, ct.loai, ct.ngayBatDau, ct.ngayKetThuc, ct.moTa })
                .ToList();

            return promotions.Select(p => new PromotionListDto
            {
                Id = p.id,
                Ten = p.ten,
                Loai = p.loai,
                NgayBatDau = p.ngayBatDau,
                NgayKetThuc = p.ngayKetThuc,
                SoLuongMa = GetPromoCodeCount(p.id),
                TrangThai = GetStatus(p.ngayBatDau, p.ngayKetThuc),
                MoTa = p.moTa
            }).ToList();
        }

        public ChuongTrinhKhuyenMai GetPromotionById(string id) =>
            _context.ChuongTrinhKhuyenMais.AsNoTracking().FirstOrDefault(c => c.id == id && !c.isDelete);

        public List<DieuKienApDung> GetConditionsByProgramId(string chuongTrinhId) =>
            _context.DieuKienApDungs.AsNoTracking()
                .Where(dk => dk.chuongTrinhId == chuongTrinhId && !dk.isDelete)
                .OrderBy(dk => dk.id).ToList();

        public List<string> GetSelectedCategoryIds(string chuongTrinhId)
        {
            var condition = _context.DieuKienApDungs.AsNoTracking()
                .FirstOrDefault(dk => dk.chuongTrinhId == chuongTrinhId && !dk.isDelete);
            if (condition == null) return new List<string>();

            return _context.DieuKienApDungDanhMucs.AsNoTracking()
                .Where(d => d.dieuKienId == condition.id && !d.isDelete)
                .Select(d => d.danhMucId).ToList();
        }

        public List<string> GetSelectedProductIds(string chuongTrinhId)
        {
            var condition = _context.DieuKienApDungs.AsNoTracking()
                .FirstOrDefault(dk => dk.chuongTrinhId == chuongTrinhId && !dk.isDelete);
            if (condition == null) return new List<string>();

            return _context.DieuKienApDungSanPhams.AsNoTracking()
                .Where(d => d.dieuKienId == condition.id && !d.isDelete)
                .Select(d => d.sanPhamId).ToList();
        }

        #endregion

        #region Promo Code Operations

        public string GenerateNewPromoCodeId() => GenerateId<MaKhuyenMai>("MKM", 7, 3);
        public string GenerateNewConditionId() => GenerateId<DieuKienApDung>("DK", 6, 2);
        private int GetPromoCodeCount(string chuongTrinhId) =>
            _context.MaKhuyenMais.Count(m => m.chuongTrinhId == chuongTrinhId && !m.isDelete);

        public List<PromoCodeDto> GetPromoCodesByProgramId(string chuongTrinhId) =>
            _context.MaKhuyenMais.AsNoTracking()
                .Where(m => m.chuongTrinhId == chuongTrinhId && !m.isDelete)
                .OrderBy(m => m.id)
                .Select(m => new PromoCodeDto
                {
                    Id = m.id, Code = m.code, GiaTri = m.giaTri,
                    SoLanSuDung = m.soLanSuDung, TrangThai = m.trangThai, ChuongTrinhId = m.chuongTrinhId
                }).ToList();

        public MaKhuyenMai GetPromoCodeById(string id) =>
            _context.MaKhuyenMais.AsNoTracking().FirstOrDefault(m => m.id == id && !m.isDelete);

        public MaKhuyenMai GetPromoCodeByCode(string code) =>
            _context.MaKhuyenMais.AsNoTracking().FirstOrDefault(m => m.code == code && !m.isDelete);

        public bool IsPromoCodeExists(string code, string excludeId = null)
        {
            var query = _context.MaKhuyenMais.Where(m => m.code == code && !m.isDelete);
            if (!string.IsNullOrEmpty(excludeId))
                query = query.Where(m => m.id != excludeId);
            return query.Any();
        }

        public (bool success, string message, MaKhuyenMai result) CreatePromoCode(MaKhuyenMai promoCode)
        {
            var validation = ValidatePromoCode(promoCode);
            if (!validation.isValid) return (false, validation.message, null);

            if (IsPromoCodeExists(promoCode.code))
                return (false, $"Mã khuyến mãi '{promoCode.code}' đã tồn tại!", null);

            var program = _context.ChuongTrinhKhuyenMais.Find(promoCode.chuongTrinhId);
            if (program == null || program.isDelete)
                return (false, "Chương trình khuyến mãi không tồn tại!", null);

            promoCode.id = GenerateNewPromoCodeId();
            promoCode.isDelete = false;
            promoCode.trangThai = promoCode.trangThai ?? "Hoạt động";

            _context.MaKhuyenMais.Add(promoCode);
            _context.SaveChanges();

            return (true, $"Thêm mã khuyến mãi thành công!\nMã: {promoCode.id}\nCode: {promoCode.code}", promoCode);
        }

        public (bool success, string message) UpdatePromoCode(MaKhuyenMai promoCode)
        {
            var validation = ValidatePromoCode(promoCode, true);
            if (!validation.isValid) return (false, validation.message);

            var existing = _context.MaKhuyenMais.FirstOrDefault(m => m.id == promoCode.id);
            if (existing == null || existing.isDelete)
                return (false, "Không tìm thấy mã khuyến mãi!");

            if (IsPromoCodeExists(promoCode.code, promoCode.id))
                return (false, $"Mã khuyến mãi '{promoCode.code}' đã tồn tại!");

            existing.code = promoCode.code;
            existing.giaTri = promoCode.giaTri;
            existing.soLanSuDung = promoCode.soLanSuDung;
            existing.trangThai = promoCode.trangThai ?? "Hoạt động";

            _context.SaveChanges();
            return (true, "Cập nhật mã khuyến mãi thành công!");
        }

        public (bool success, string message) DeletePromoCode(string id)
        {
            var promoCode = _context.MaKhuyenMais.FirstOrDefault(m => m.id == id);
            if (promoCode == null) return (false, "Không tìm thấy mã khuyến mãi!");

            promoCode.isDelete = true;
            _context.SaveChanges();
            return (true, "Xóa mã khuyến mãi thành công!");
        }

        #endregion

        #region CRUD Operations - Complete

        public (bool success, string message, string promotionId) CreatePromotionComplete(
            ChuongTrinhKhuyenMai promotion, DieuKienApDung condition,
            string promoCodeValue, decimal promoGiaTri, int promoSoLanSuDung, string promoTrangThai,
            List<string> selectedCategoryIds = null, List<string> selectedProductIds = null)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var validation = ValidatePromotionData(promotion);
                    if (!validation.isValid) return (false, validation.message, null);

                    if (condition != null && string.IsNullOrWhiteSpace(condition.giamTheo))
                        return (false, "Phải chọn loại giảm (Phần trăm hoặc Giá tiền)!", null);

                    if (!string.IsNullOrWhiteSpace(promoCodeValue) && IsPromoCodeExists(promoCodeValue))
                        return (false, $"Mã khuyến mãi '{promoCodeValue}' đã tồn tại!", null);

                    string promotionId = promotion.id ?? _baseService.GenerateNewId<ChuongTrinhKhuyenMai>("CTKM", 8);
                    promotion.id = promotionId;
                    promotion.isDelete = false;
                    _context.ChuongTrinhKhuyenMais.Add(promotion);

                    if (condition != null)
                    {
                        string conditionId = GenerateNewConditionId();
                        condition.id = conditionId;
                        condition.chuongTrinhId = promotionId;
                        condition.isDelete = false;
                        _context.DieuKienApDungs.Add(condition);

                        AddCategoryLinks(conditionId, selectedCategoryIds);
                        AddProductLinks(conditionId, selectedProductIds);
                    }

                    if (!string.IsNullOrWhiteSpace(promoCodeValue))
                    {
                        _context.MaKhuyenMais.Add(new MaKhuyenMai
                        {
                            id = GenerateNewPromoCodeId(),
                            chuongTrinhId = promotionId,
                            code = promoCodeValue.Trim(),
                            giaTri = promoGiaTri,
                            soLanSuDung = promoSoLanSuDung > 0 ? promoSoLanSuDung : 100,
                            trangThai = string.IsNullOrEmpty(promoTrangThai) ? "Hoạt động" : promoTrangThai,
                            isDelete = false
                        });
                    }

                    _context.SaveChanges();
                    transaction.Commit();
                    return (true, $"Tạo chương trình khuyến mãi thành công!\nMã CT: {promotionId}", promotionId);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return (false, $"Lỗi tạo chương trình: {GetExceptionMessage(ex)}", null);
                }
            }
        }

        public (bool success, string message) UpdatePromotionComplete(
            ChuongTrinhKhuyenMai promotion, DieuKienApDung condition,
            string promoCodeValue, decimal promoGiaTri, int promoSoLanSuDung, string promoTrangThai,
            List<string> selectedCategoryIds = null, List<string> selectedProductIds = null)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var existing = _context.ChuongTrinhKhuyenMais.Find(promotion.id);
                    if (existing == null || existing.isDelete)
                        return (false, "Không tìm thấy chương trình!");

                    var validation = ValidatePromotionData(promotion);
                    if (!validation.isValid) return (false, validation.message);

                    existing.ten = promotion.ten;
                    existing.loai = promotion.loai;
                    existing.ngayBatDau = promotion.ngayBatDau;
                    existing.ngayKetThuc = promotion.ngayKetThuc;
                    existing.moTa = promotion.moTa;

                    string conditionId = null;
                    if (condition != null)
                    {
                        var existingCondition = _context.DieuKienApDungs
                            .FirstOrDefault(d => d.chuongTrinhId == promotion.id && !d.isDelete);

                        if (existingCondition != null)
                        {
                            existingCondition.dieuKien = condition.dieuKien;
                            existingCondition.giaTriToiThieu = condition.giaTriToiThieu;
                            existingCondition.giamTheo = condition.giamTheo;
                            existingCondition.giaTriToiDa = condition.giaTriToiDa;
                            conditionId = existingCondition.id;
                        }
                        else
                        {
                            conditionId = GenerateNewConditionId();
                            condition.id = conditionId;
                            condition.chuongTrinhId = promotion.id;
                            condition.isDelete = false;
                            _context.DieuKienApDungs.Add(condition);
                        }

                        UpdateCategoryLinks(conditionId, selectedCategoryIds);
                        UpdateProductLinks(conditionId, selectedProductIds);
                    }

                    if (!string.IsNullOrWhiteSpace(promoCodeValue))
                    {
                        var existingPromoCode = _context.MaKhuyenMais
                            .FirstOrDefault(m => m.chuongTrinhId == promotion.id && !m.isDelete);

                        if (existingPromoCode != null)
                        {
                            if (IsPromoCodeExists(promoCodeValue, existingPromoCode.id))
                                return (false, $"Mã khuyến mãi '{promoCodeValue}' đã tồn tại!");

                            existingPromoCode.code = promoCodeValue.Trim();
                            existingPromoCode.giaTri = promoGiaTri;
                            existingPromoCode.soLanSuDung = promoSoLanSuDung;
                            existingPromoCode.trangThai = string.IsNullOrEmpty(promoTrangThai) ? "Hoạt động" : promoTrangThai;
                        }
                        else
                        {
                            if (IsPromoCodeExists(promoCodeValue))
                                return (false, $"Mã khuyến mãi '{promoCodeValue}' đã tồn tại!");

                            _context.MaKhuyenMais.Add(new MaKhuyenMai
                            {
                                id = GenerateNewPromoCodeId(),
                                chuongTrinhId = promotion.id,
                                code = promoCodeValue.Trim(),
                                giaTri = promoGiaTri,
                                soLanSuDung = promoSoLanSuDung > 0 ? promoSoLanSuDung : 100,
                                trangThai = string.IsNullOrEmpty(promoTrangThai) ? "Hoạt động" : promoTrangThai,
                                isDelete = false
                            });
                        }
                    }

                    _context.SaveChanges();
                    transaction.Commit();
                    return (true, $"Cập nhật chương trình {promotion.id} thành công!");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return (false, $"Lỗi cập nhật chương trình: {GetExceptionMessage(ex)}");
                }
            }
        }

        public (bool success, string message) DeletePromotion(string id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var promotion = _context.ChuongTrinhKhuyenMais.Find(id);
                    if (promotion == null || promotion.isDelete)
                        return (false, "Không tìm thấy chương trình");

                    promotion.isDelete = true;

                    _context.DieuKienApDungs.Where(d => d.chuongTrinhId == id && !d.isDelete)
                        .ToList().ForEach(c => c.isDelete = true);

                    _context.MaKhuyenMais.Where(m => m.chuongTrinhId == id && !m.isDelete)
                        .ToList().ForEach(m => m.isDelete = true);

                    _context.SaveChanges();
                    transaction.Commit();
                    return (true, "Xóa chương trình thành công!");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return (false, $"Lỗi xóa chương trình: {ex.Message}");
                }
            }
        }

        public (bool success, string message, string promotionId) CreatePromotion(
            ChuongTrinhKhuyenMai promotion, List<DieuKienApDung> conditions = null)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var validation = ValidatePromotionData(promotion);
                    if (!validation.isValid) return (false, validation.message, null);

                    var promotionId = promotion.id ?? _baseService.GenerateNewId<ChuongTrinhKhuyenMai>("CTKM", 8);
                    promotion.id = promotionId;
                    promotion.isDelete = false;
                    _context.ChuongTrinhKhuyenMais.Add(promotion);

                    if (conditions?.Count > 0)
                    {
                        foreach (var condition in conditions)
                        {
                            condition.id = condition.id ?? GenerateNewConditionId();
                            condition.chuongTrinhId = promotionId;
                            condition.isDelete = false;
                            _context.DieuKienApDungs.Add(condition);
                        }
                    }

                    _context.SaveChanges();
                    transaction.Commit();
                    return (true, $"Tạo chương trình {promotionId} thành công!", promotionId);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return (false, $"Lỗi tạo chương trình: {ex.Message}", null);
                }
            }
        }

        public (bool success, string message) UpdatePromotion(
            ChuongTrinhKhuyenMai promotion, List<DieuKienApDung> conditions = null)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var existing = _context.ChuongTrinhKhuyenMais.Find(promotion.id);
                    if (existing == null || existing.isDelete)
                        return (false, "Không tìm thấy chương trình");

                    var validation = ValidatePromotionData(promotion);
                    if (!validation.isValid) return (false, validation.message);

                    existing.ten = promotion.ten;
                    existing.loai = promotion.loai;
                    existing.ngayBatDau = promotion.ngayBatDau;
                    existing.ngayKetThuc = promotion.ngayKetThuc;
                    existing.moTa = promotion.moTa;

                    // Xóa điều kiện cũ
                    _context.DieuKienApDungs.Where(d => d.chuongTrinhId == promotion.id && !d.isDelete)
                        .ToList().ForEach(d => d.isDelete = true);

                    // Thêm điều kiện mới
                    if (conditions?.Count > 0)
                    {
                        foreach (var condition in conditions)
                        {
                            var existingCond = _context.DieuKienApDungs
                                .FirstOrDefault(c => c.id == condition.id && c.chuongTrinhId == promotion.id);
                            
                            if (existingCond != null)
                            {
                                existingCond.isDelete = false;
                                existingCond.dieuKien = condition.dieuKien;
                                existingCond.giaTriToiThieu = condition.giaTriToiThieu;
                                existingCond.giamTheo = condition.giamTheo;
                                existingCond.giaTriToiDa = condition.giaTriToiDa;
                            }
                            else
                            {
                                condition.id = GenerateNewConditionId();
                                condition.chuongTrinhId = promotion.id;
                                condition.isDelete = false;
                                _context.DieuKienApDungs.Add(condition);
                            }
                        }
                    }

                    _context.SaveChanges();
                    transaction.Commit();
                    return (true, $"Cập nhật chương trình {promotion.id} thành công!");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return (false, $"Lỗi cập nhật chương trình: {ex.Message}");
                }
            }
        }

        #endregion

        #region Helper Methods

        private string GenerateId<T>(string prefix, int totalLength, int prefixLength) where T : class
        {
            try
            {
                List<string> allIds;
                
                if (typeof(T) == typeof(MaKhuyenMai))
                {
                    allIds = _context.MaKhuyenMais
                        .Where(m => m.id.StartsWith(prefix) && m.id.Length == totalLength)
                        .Select(m => m.id).ToList();
                }
                else if (typeof(T) == typeof(DieuKienApDung))
                {
                    allIds = _context.DieuKienApDungs
                        .Where(d => d.id.StartsWith(prefix) && d.id.Length == totalLength)
                        .Select(d => d.id).ToList();
                }
                else
                {
                    return prefix + DateTime.Now.ToString("mmss");
                }

                if (allIds.Count == 0) return $"{prefix}0001";

                var maxNumber = allIds
                    .Select(id => int.TryParse(id.Substring(prefixLength), out int num) ? num : 0)
                    .Max();

                return $"{prefix}{(maxNumber + 1):D4}";
            }
            catch
            {
                return prefix + DateTime.Now.ToString("mmss");
            }
        }

        private void AddCategoryLinks(string conditionId, List<string> categoryIds)
        {
            if (categoryIds == null || categoryIds.Count == 0) return;
            foreach (var categoryId in categoryIds)
            {
                _context.DieuKienApDungDanhMucs.Add(new DieuKienApDungDanhMuc
                {
                    id = Guid.NewGuid().ToString(),
                    dieuKienId = conditionId,
                    danhMucId = categoryId,
                    isDelete = false
                });
            }
        }

        private void AddProductLinks(string conditionId, List<string> productIds)
        {
            if (productIds == null || productIds.Count == 0) return;
            foreach (var productId in productIds)
            {
                _context.DieuKienApDungSanPhams.Add(new DieuKienApDungSanPham
                {
                    id = Guid.NewGuid().ToString(),
                    dieuKienId = conditionId,
                    sanPhamId = productId,
                    isDelete = false
                });
            }
        }

        private void UpdateCategoryLinks(string conditionId, List<string> categoryIds)
        {
            var oldLinks = _context.DieuKienApDungDanhMucs.Where(d => d.dieuKienId == conditionId).ToList();
            oldLinks.ForEach(l => l.isDelete = true);

            if (categoryIds == null || categoryIds.Count == 0) return;
            foreach (var categoryId in categoryIds)
            {
                var existing = oldLinks.FirstOrDefault(l => l.danhMucId == categoryId);
                if (existing != null) existing.isDelete = false;
                else _context.DieuKienApDungDanhMucs.Add(new DieuKienApDungDanhMuc
                {
                    id = Guid.NewGuid().ToString(),
                    dieuKienId = conditionId,
                    danhMucId = categoryId,
                    isDelete = false
                });
            }
        }

        private void UpdateProductLinks(string conditionId, List<string> productIds)
        {
            var oldLinks = _context.DieuKienApDungSanPhams.Where(d => d.dieuKienId == conditionId).ToList();
            oldLinks.ForEach(l => l.isDelete = true);

            if (productIds == null || productIds.Count == 0) return;
            foreach (var productId in productIds)
            {
                var existing = oldLinks.FirstOrDefault(l => l.sanPhamId == productId);
                if (existing != null) existing.isDelete = false;
                else _context.DieuKienApDungSanPhams.Add(new DieuKienApDungSanPham
                {
                    id = Guid.NewGuid().ToString(),
                    dieuKienId = conditionId,
                    sanPhamId = productId,
                    isDelete = false
                });
            }
        }

        private (bool isValid, string message) ValidatePromotionData(ChuongTrinhKhuyenMai promotion)
        {
            if (string.IsNullOrWhiteSpace(promotion.ten))
                return (false, "Tên chương trình không được để trống");
            if (string.IsNullOrWhiteSpace(promotion.loai))
                return (false, "Loại chương trình không được để trống");
            if (promotion.ngayBatDau >= promotion.ngayKetThuc)
                return (false, "Ngày kết thúc phải sau ngày bắt đầu");
            return (true, string.Empty);
        }

        private (bool isValid, string message) ValidatePromoCode(MaKhuyenMai promoCode, bool isUpdate = false)
        {
            if (string.IsNullOrWhiteSpace(promoCode.code))
                return (false, "Mã khuyến mãi không được để trống!");
            if (promoCode.code.Length > 100)
                return (false, "Mã khuyến mãi không được vượt quá 100 ký tự!");
            if (string.IsNullOrWhiteSpace(promoCode.chuongTrinhId) && !isUpdate)
                return (false, "Chưa chọn chương trình khuyến mãi!");
            if (promoCode.giaTri < 0)
                return (false, "Giá trị khuyến mãi không được âm!");
            if (promoCode.soLanSuDung < 0)
                return (false, "Số lần sử dụng không được âm!");
            return (true, string.Empty);
        }

        private string GetStatus(DateTime startDate, DateTime endDate)
        {
            var now = DateTime.Now;
            if (now < startDate) return STATUS_UPCOMING;
            if (now <= endDate) return STATUS_ONGOING;
            return STATUS_ENDED;
        }

        private string GetExceptionMessage(Exception ex) =>
            ex.InnerException?.InnerException?.Message ?? ex.InnerException?.Message ?? ex.Message;

        #endregion

        public void Dispose()
        {
            if (!_disposed)
            {
                _context?.Dispose();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
}