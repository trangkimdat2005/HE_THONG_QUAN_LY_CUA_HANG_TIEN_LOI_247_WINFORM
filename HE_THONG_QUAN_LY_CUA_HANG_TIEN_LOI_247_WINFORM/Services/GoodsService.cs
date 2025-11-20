using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services
{
    public class GoodsService
    {
        private readonly AppDbContext _context;

        public GoodsService()
        {
            _context = new AppDbContext();
        }

        #region Get/Search Operations

        public List<GoodDetailDto> GetAllGoods(string keyword = null, string brandId = null, string categoryId = null)
        {
            try
            {
                var query = _context.SanPhams
                    .AsNoTracking()
                    .Where(sp => !sp.isDelete)
                    .AsQueryable();

                // Filter by keyword
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    keyword = keyword.ToLower();
                    query = query.Where(sp => sp.id.ToLower().Contains(keyword) ||
                                               sp.ten.ToLower().Contains(keyword));
                }

                // Filter by brand
                if (!string.IsNullOrEmpty(brandId))
                {
                    query = query.Where(sp => sp.nhanHieuId == brandId);
                }

                if (!string.IsNullOrEmpty(categoryId))
                {
                    query = query.Where(sp => sp.SanPhamDanhMucs
                        .Any(spdm => spdm.danhMucId == categoryId && !spdm.isDelete));
                }

                return query
                    .Select(sp => new GoodDetailDto
                    {
                        // 4 c?t chính
                        Id = sp.id,
                        Ten = sp.ten,
                        NhanHieu = sp.NhanHieu.ten ?? "Chưa có nhãn hiệu",
                        DanhMuc = sp.SanPhamDanhMucs
                            .Where(dm => !dm.isDelete)
                            .Select(dm => dm.DanhMuc.ten)
                            .FirstOrDefault() ?? "Chưa phân loại"
                    })
                    .OrderBy(x => x.Id)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách hàng hóa: {ex.Message}", ex);
            }
        }

        public SanPham GetGoodById(string id)
        {
            try
            {
                return _context.SanPhams
                    .AsNoTracking()
                    .Include(sp => sp.NhanHieu)
                    .Include(sp => sp.SanPhamDanhMucs.Select(spdm => spdm.DanhMuc))
                    .Include(sp => sp.SanPhamDonVis.Select(spdv => spdv.DonViDoLuong))
                    .FirstOrDefault(sp => sp.id == id && !sp.isDelete);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy chi tiêt hàng hóa: {ex.Message}", ex);
            }
        }

        public string GetGoodCategoryId(string productId)
        {
            try
            {
                return _context.SanPhamDanhMucs
                    .AsNoTracking()
                    .Where(spdm => spdm.sanPhamId == productId && !spdm.isDelete)
                    .Select(spdm => spdm.danhMucId)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danhn mục: {ex.Message}", ex);
            }
        }

        public List<NhanHieu> GetAllBrands()
        {
            try
            {
                return _context.NhanHieux
                    .AsNoTracking()
                    .Where(nh => !nh.isDelete)
                    .OrderBy(nh => nh.ten)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy nhãn hiệu: {ex.Message}", ex);
            }
        }

        public List<DanhMuc> GetAllCategories()
        {
            try
            {
                return _context.DanhMucs
                    .AsNoTracking()
                    .Where(dm => !dm.isDelete)
                    .OrderBy(dm => dm.ten)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh mục: {ex.Message}", ex);
            }
        }

        public bool IsGoodNameExists(string name, string excludeId = null)
        {
            try
            {
                var query = _context.SanPhams
                    .AsNoTracking()
                    .Where(sp => sp.ten.ToLower() == name.ToLower() && !sp.isDelete);

                if (!string.IsNullOrEmpty(excludeId))
                {
                    query = query.Where(sp => sp.id != excludeId);
                }

                return query.Any();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi kiểm tra tên hàng hóa: {ex.Message}", ex);
            }
        }

        #endregion

        #region CRUD Operations

        public (bool success, string message, string productId) AddGood(SanPham product, string categoryId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(product.ten))
                        return (false, "Tên hàng hóa không được bỏ trống", null);

                    if (IsGoodNameExists(product.ten))
                        return (false, "Tên hàng hóa đã tồn tại", null);

                    product.id = GenerateNewGoodId();
                    product.isDelete = false;

                    _context.SanPhams.Add(product);

                    // Add category link
                    if (!string.IsNullOrEmpty(categoryId))
                    {
                        var sanPhamDanhMuc = new SanPhamDanhMuc
                        {
                            id = Guid.NewGuid().ToString(),
                            sanPhamId = product.id,
                            danhMucId = categoryId,
                            isDelete = false
                        };
                        _context.SanPhamDanhMucs.Add(sanPhamDanhMuc);
                    }

                    _context.SaveChanges();
                    transaction.Commit();

                    return (true, $"Thêm hàng hóa thành công. Mã: {product.id}", product.id);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return (false, $"Lỗi khi thêm hàng hóa: {ex.Message}", null);
                }
            }
        }

        public (bool success, string message) UpdateGood(SanPham product, string categoryId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Validate
                    if (string.IsNullOrWhiteSpace(product.ten))
                        return (false, "Tên hàng hóa không được bỏ trống");

                    var existingProduct = _context.SanPhams.FirstOrDefault(sp => sp.id == product.id);
                    if (existingProduct == null)
                        return (false, "Không tìm thấy hàng hóa");

                    if (IsGoodNameExists(product.ten, product.id))
                        return (false, "Tên hàng hóa đã tồn tại");

                    // Update product
                    existingProduct.ten = product.ten;
                    existingProduct.nhanHieuId = product.nhanHieuId;
                    existingProduct.moTa = product.moTa;

                    var allCategoryLinks = _context.SanPhamDanhMucs
                        .Where(spdm => spdm.sanPhamId == product.id)
                        .ToList();

                    foreach (var link in allCategoryLinks)
                    {
                        link.isDelete = true;
                    }

                    if (!string.IsNullOrEmpty(categoryId))
                    {
                        var existingLink = allCategoryLinks
                            .FirstOrDefault(l => l.danhMucId == categoryId);

                        if (existingLink != null)
                        {
                            existingLink.isDelete = false;
                        }
                        else
                        {
                            _context.SanPhamDanhMucs.Add(new SanPhamDanhMuc
                            {
                                id = Guid.NewGuid().ToString(),
                                sanPhamId = product.id,
                                danhMucId = categoryId,
                                isDelete = false
                            });
                        }
                    }

                    _context.SaveChanges();
                    transaction.Commit();

                    return (true, "Cập nhật hàng hóa thành công");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return (false, $"Lỗi khi cập nhật hàng hóa: {ex.Message}");
                }
            }
        }

        public (bool success, string message) DeleteGood(string productId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var product = _context.SanPhams.FirstOrDefault(sp => sp.id == productId);
                    if (product == null)
                        return (false, "Không tìm thấy hàng hóa");

                    product.isDelete = true;

                    var categoryLinks = _context.SanPhamDanhMucs
                        .Where(spdm => spdm.sanPhamId == productId)
                        .ToList();
                    foreach (var link in categoryLinks)
                    {
                        link.isDelete = true;
                    }

                    var units = _context.SanPhamDonVis
                        .Where(spdv => spdv.sanPhamId == productId)
                        .ToList();
                    foreach (var unit in units)
                    {
                        unit.isDelete = true;
                    }

                    _context.SaveChanges();
                    transaction.Commit();

                    return (true, "Xóa hàng hóa thành công");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return (false, $"Lỗi khi xóa hàng hóa: {ex.Message}");
                }
            }
        }

        public string GenerateNewGoodId()
        {
            try
            {
                var lastProduct = _context.SanPhams
                    .Where(sp => sp.id.StartsWith("SP"))
                    .OrderByDescending(sp => sp.id)
                    .FirstOrDefault();

                if (lastProduct == null)
                    return "SP0001";

                var lastNumber = int.Parse(lastProduct.id.Substring(2));
                return $"SP{(lastNumber + 1):D4}";
            }
            catch
            {
                return $"SP{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";
            }
        }

        #endregion

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}