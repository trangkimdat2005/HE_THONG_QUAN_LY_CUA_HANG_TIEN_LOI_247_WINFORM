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
        #region Constants
        private const string STATUS_UPCOMING = "Sắp diễn ra";
        private const string STATUS_ONGOING = "Đang diễn ra";
        private const string STATUS_ENDED = "Đã kết thúc";
        private const string GIAM_THEO_PHAN_TRAM = "Phần trăm";
        private const string GIAM_THEO_GIA_TIEN = "Giá tiền";
        #endregion

        #region Fields
        private readonly AppDbContext _context;
        private readonly QuanLyServices _baseService;
        private bool _disposed;
        #endregion

        #region Constructor
        public PromotionService()
        {
            _context = new AppDbContext();
            _baseService = new QuanLyServices();
        }
        #endregion

        #region Get Operations

        /// <summary>
        /// Lấy tất cả chương trình khuyến mãi - SẮP XẾP TĂNG DẦN THEO ID
        /// </summary>
        public List<PromotionListDto> GetAllPromotions()
        {
            try
            {
                var promotions = _context.ChuongTrinhKhuyenMais
                    .AsNoTracking()
                    .Where(ct => !ct.isDelete)
                    .OrderBy(ct => ct.id) // ✅ Sắp xếp TĂNG DẦN theo ID
                    .Select(ct => new
                    {
                        ct.id,
                        ct.ten,
                        ct.loai,
                        ct.ngayBatDau,
                        ct.ngayKetThuc,
                        ct.moTa
                    })
                    .ToList();

                return promotions.Select(p => new PromotionListDto
                {
                    Id = p.id,
                    Ten = p.ten,
                    Loai = p.loai,
                    NgayBatDau = p.ngayBatDau,
                    NgayKetThuc = p.ngayKetThuc,
                    SoLuongMa = 0, // Không còn mã khuyến mãi
                    TrangThai = DetermineProgramStatus(p.ngayBatDau, p.ngayKetThuc),
                    MoTa = p.moTa
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Lỗi khi truy vấn danh sách chương trình khuyến mãi", ex);
            }
        }

        /// <summary>
        /// Tìm kiếm chương trình khuyến mãi - SẮP XẾP TĂNG DẦN THEO ID
        /// </summary>
        public List<PromotionListDto> SearchPromotions(string keyword)
        {
            try
            {
                keyword = (keyword ?? string.Empty).ToLower().Trim();

                var promotions = _context.ChuongTrinhKhuyenMais
                    .AsNoTracking()
                    .Where(ct => !ct.isDelete &&
                                (ct.id.ToLower().Contains(keyword) ||
                                 ct.ten.ToLower().Contains(keyword) ||
                                 ct.loai.ToLower().Contains(keyword)))
                    .OrderBy(ct => ct.id) // ✅ Sắp xếp TĂNG DẦN theo ID
                    .Select(ct => new
                    {
                        ct.id,
                        ct.ten,
                        ct.loai,
                        ct.ngayBatDau,
                        ct.ngayKetThuc,
                        ct.moTa
                    })
                    .ToList();

                return promotions.Select(p => new PromotionListDto
                {
                    Id = p.id,
                    Ten = p.ten,
                    Loai = p.loai,
                    NgayBatDau = p.ngayBatDau,
                    NgayKetThuc = p.ngayKetThuc,
                    SoLuongMa = 0,
                    TrangThai = DetermineProgramStatus(p.ngayBatDau, p.ngayKetThuc),
                    MoTa = p.moTa
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Lỗi khi tìm kiếm với từ khóa '{keyword}'", ex);
            }
        }

        public ChuongTrinhKhuyenMai GetPromotionById(string id)
        {
            try
            {
                return _context.ChuongTrinhKhuyenMais
                    .AsNoTracking()
                    .FirstOrDefault(c => c.id == id && !c.isDelete);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Lỗi khi lấy thông tin chương trình {id}", ex);
            }
        }

        public List<DieuKienApDung> GetConditionsByProgramId(string chuongTrinhId)
        {
            try
            {
                return _context.DieuKienApDungs
                    .AsNoTracking()
                    .Where(dk => dk.chuongTrinhId == chuongTrinhId && !dk.isDelete)
                    .OrderBy(dk => dk.id)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Lỗi khi lấy điều kiện áp dụng của chương trình {chuongTrinhId}", ex);
            }
        }

        #endregion

        #region CRUD Operations

        public (bool success, string message, string promotionId) CreatePromotion(
            ChuongTrinhKhuyenMai promotion, 
            List<DieuKienApDung> conditions = null)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var validationResult = ValidatePromotionData(promotion);
                    if (!validationResult.isValid)
                        return (false, validationResult.message, null);

                    if (conditions != null && conditions.Count > 0)
                    {
                        var conditionValidation = ValidateConditions(conditions);
                        if (!conditionValidation.isValid)
                            return (false, conditionValidation.message, null);
                    }

                    var promotionId = string.IsNullOrEmpty(promotion.id)
                        ? _baseService.GenerateNewId<ChuongTrinhKhuyenMai>("CTKM", 8)
                        : promotion.id;

                    promotion.id = promotionId;
                    promotion.isDelete = false;
                    _context.ChuongTrinhKhuyenMais.Add(promotion);

                    if (conditions != null && conditions.Count > 0)
                    {
                        AddConditions(conditions, promotionId);
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
            ChuongTrinhKhuyenMai promotion,
            List<DieuKienApDung> conditions = null)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var existing = _context.ChuongTrinhKhuyenMais.Find(promotion.id);
                    if (existing == null || existing.isDelete)
                        return (false, "Không tìm thấy chương trình");

                    var validationResult = ValidatePromotionData(promotion);
                    if (!validationResult.isValid)
                        return (false, validationResult.message);

                    if (conditions != null && conditions.Count > 0)
                    {
                        var conditionValidation = ValidateConditions(conditions);
                        if (!conditionValidation.isValid)
                            return (false, conditionValidation.message);
                    }

                    existing.ten = promotion.ten;
                    existing.loai = promotion.loai;
                    existing.ngayBatDau = promotion.ngayBatDau;
                    existing.ngayKetThuc = promotion.ngayKetThuc;
                    existing.moTa = promotion.moTa;

                    UpdateConditions(promotion.id, conditions);

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

                    var conditions = _context.DieuKienApDungs
                        .Where(d => d.chuongTrinhId == id && !d.isDelete)
                        .ToList();
                    
                    foreach (var condition in conditions)
                        condition.isDelete = true;

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

        #endregion

        #region Helper Methods

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

        private (bool isValid, string message) ValidateConditions(List<DieuKienApDung> conditions)
        {
            foreach (var condition in conditions)
            {
                if (string.IsNullOrWhiteSpace(condition.dieuKien))
                    return (false, "Điều kiện không được để trống");

                if (string.IsNullOrWhiteSpace(condition.giamTheo))
                    return (false, "Phải chọn loại giảm (Phần trăm hoặc Giá tiền)");

                if (condition.giamTheo != GIAM_THEO_PHAN_TRAM && condition.giamTheo != GIAM_THEO_GIA_TIEN)
                    return (false, $"Loại giảm không hợp lệ. Chỉ cho phép '{GIAM_THEO_PHAN_TRAM}' hoặc '{GIAM_THEO_GIA_TIEN}'");

                if (condition.giaTriToiThieu < 0)
                    return (false, "Giá trị tối thiểu phải >= 0");

                if (condition.giaTriToiDa > 0 && condition.giaTriToiDa < condition.giaTriToiThieu)
                    return (false, "Giá trị tối đa phải >= giá trị tối thiểu");

                if (condition.giamTheo == GIAM_THEO_PHAN_TRAM)
                {
                    if (condition.giaTriToiThieu > 100)
                        return (false, "Phần trăm giảm không được vượt quá 100%");
                    
                    if (condition.giaTriToiDa > 100)
                        return (false, "Phần trăm tối đa không được vượt quá 100%");
                }
            }

            return (true, string.Empty);
        }

        private void AddConditions(List<DieuKienApDung> conditions, string promotionId)
        {
            foreach (var condition in conditions)
            {
                if (string.IsNullOrEmpty(condition.id))
                    condition.id = _baseService.GenerateNewId<DieuKienApDung>("DK", 6);
                
                condition.chuongTrinhId = promotionId;
                condition.isDelete = false;
                _context.DieuKienApDungs.Add(condition);
            }
        }

        private void UpdateConditions(string promotionId, List<DieuKienApDung> newConditions)
        {
            var oldConditions = _context.DieuKienApDungs
                .Where(d => d.chuongTrinhId == promotionId && !d.isDelete)
                .ToList();

            foreach (var oldCondition in oldConditions)
                oldCondition.isDelete = true;

            if (newConditions != null && newConditions.Count > 0)
            {
                foreach (var condition in newConditions)
                {
                    if (!string.IsNullOrEmpty(condition.id) && condition.id.StartsWith("DK"))
                    {
                        var existingCondition = oldConditions.FirstOrDefault(c => c.id == condition.id);
                        if (existingCondition != null)
                        {
                            existingCondition.isDelete = false;
                            existingCondition.dieuKien = condition.dieuKien;
                            existingCondition.giaTriToiThieu = condition.giaTriToiThieu;
                            existingCondition.giamTheo = condition.giamTheo;
                            existingCondition.giaTriToiDa = condition.giaTriToiDa;
                        }
                        else
                        {
                            AddNewCondition(condition, promotionId);
                        }
                    }
                    else
                    {
                        AddNewCondition(condition, promotionId);
                    }
                }
            }
        }
        private void AddNewCondition(DieuKienApDung condition, string promotionId)
        {
            var newCondition = new DieuKienApDung
            {
                id = _baseService.GenerateNewId<DieuKienApDung>("DK", 6),
                chuongTrinhId = promotionId,
                dieuKien = condition.dieuKien,
                giaTriToiThieu = condition.giaTriToiThieu,
                giamTheo = condition.giamTheo,
                giaTriToiDa = condition.giaTriToiDa,
                isDelete = false
            };
            _context.DieuKienApDungs.Add(newCondition);
        }

        private string DetermineProgramStatus(DateTime startDate, DateTime endDate)
        {
            var now = DateTime.Now;

            if (now < startDate)
                return STATUS_UPCOMING;
            
            if (now >= startDate && now <= endDate)
                return STATUS_ONGOING;
            
            return STATUS_ENDED;
        }

        #endregion

        #region Dispose Pattern

        public void Dispose()
        {
            if (!_disposed)
            {
                _context?.Dispose();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}