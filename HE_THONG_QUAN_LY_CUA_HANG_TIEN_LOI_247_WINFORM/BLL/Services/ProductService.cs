using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity; // EF6
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services
{
    public class ProductService
    {
        private readonly AppDbContext _readContext;
        private readonly QuanLyServices _services;

        public ProductService()
        {
            _readContext = new AppDbContext();
            _services = new QuanLyServices(_readContext);
        }

        #region Get/Search Operations (Sử dụng AsNoTracking để đọc nhanh và không bị khóa)

        public List<SanPham> GetAllProducts()
        {
            return _readContext.SanPhams.AsNoTracking()
                .Where(p => !p.isDelete).OrderBy(p => p.id).Take(1000).ToList();
        }

        public SanPham GetProductById(string id)
        {
            return _readContext.SanPhams.AsNoTracking()
                .FirstOrDefault(p => p.id == id && !p.isDelete);
        }

        public List<SanPham> FilterProducts(string keyword = null, string brandId = null, string categoryId = null)
        {
            try
            {
                var query = _readContext.SanPhams.AsNoTracking().Where(p => !p.isDelete).AsQueryable();

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    keyword = keyword.ToLower();
                    query = query.Where(p => p.id.ToLower().Contains(keyword) ||
                                             p.ten.ToLower().Contains(keyword) ||
                                             (p.moTa != null && p.moTa.ToLower().Contains(keyword)));
                }
                if (!string.IsNullOrEmpty(brandId)) query = query.Where(p => p.nhanHieuId == brandId);
                if (!string.IsNullOrEmpty(categoryId))
                {
                    query = query.Where(p => p.SanPhamDanhMucs.Any(spdm => spdm.danhMucId == categoryId && !spdm.isDelete));
                }
                return query.OrderBy(p => p.id).Take(1000).ToList();
            }
            catch (Exception ex) { throw new Exception($"Lỗi lọc: {ex.Message}"); }
        }

        #endregion

        #region CRUD Operations

        public (bool success, string message, SanPham product) AddProduct(SanPham product, string categoryId = null)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    if (db.SanPhams.Any(p => p.ten.ToLower() == product.ten.ToLower() && !p.isDelete))
                        return (false, "Tên sản phẩm đã tồn tại.", null);

                    product.id = _services.GenerateNewId<SanPham>("SP", 5);
                    product.isDelete = false;
                    db.SanPhams.Add(product);

                    if (!string.IsNullOrEmpty(categoryId))
                    {
                        db.SanPhamDanhMucs.Add(new SanPhamDanhMuc
                        {
                            id = Guid.NewGuid().ToString(),
                            sanPhamId = product.id,
                            danhMucId = categoryId,
                            isDelete = false
                        });
                    }

                    db.SaveChanges();
                    return (true, $"Thêm thành công. Mã: {product.id}", product);
                }
                catch (Exception ex) { return (false, $"Lỗi thêm: {ex.Message}", null); }
            }
        }

        // --- HÀM NÀY ĐÃ ĐƯỢC SỬA LOGIC ---
        public (bool success, string message) UpdateProduct(SanPham product, string categoryId = null)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    // 1. Lấy sản phẩm gốc
                    var existingProduct = db.SanPhams.FirstOrDefault(p => p.id == product.id);
                    if (existingProduct == null) return (false, "Không tìm thấy sản phẩm.");

                    // 2. Check trùng tên (trừ chính nó ra)
                    if (db.SanPhams.Any(p => p.ten.ToLower() == product.ten.ToLower() && p.id != product.id && !p.isDelete))
                        return (false, "Tên sản phẩm đã tồn tại.");

                    // 3. Cập nhật thông tin cơ bản
                    existingProduct.ten = product.ten;
                    existingProduct.nhanHieuId = product.nhanHieuId;
                    existingProduct.moTa = product.moTa;

                    // 4. XỬ LÝ DANH MỤC THÔNG MINH (Smart Update)
                    // Lấy TẤT CẢ liên kết cũ (kể cả đã xóa hay chưa)
                    var allLinks = db.SanPhamDanhMucs
                                     .Where(x => x.sanPhamId == product.id)
                                     .ToList();

                    // Mặc định đánh dấu xóa mềm tất cả trước
                    foreach (var link in allLinks) link.isDelete = true;

                    if (!string.IsNullOrEmpty(categoryId))
                    {
                        // Tìm xem trong đống cũ có cái nào trùng CategoryId không?
                        var matchLink = allLinks.FirstOrDefault(x => x.danhMucId == categoryId);

                        if (matchLink != null)
                        {
                            // NẾU CÓ RỒI: Chỉ cần khôi phục lại (isDelete = false)
                            // Đây là bước quan trọng để tránh lỗi "Duplicate/Conflict"
                            matchLink.isDelete = false;
                        }
                        else
                        {
                            // NẾU CHƯA CÓ: Thì mới tạo mới
                            db.SanPhamDanhMucs.Add(new SanPhamDanhMuc
                            {
                                id = Guid.NewGuid().ToString(),
                                sanPhamId = product.id,
                                danhMucId = categoryId,
                                isDelete = false
                            });
                        }
                    }

                    db.SaveChanges();
                    return (true, "Cập nhật thành công.");
                }
                catch (Exception ex) { return (false, $"Lỗi cập nhật: {ex.Message}"); }
            }
        }

        public (bool success, string message) DeleteProduct(string productId)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    var product = db.SanPhams.FirstOrDefault(p => p.id == productId);
                    if (product == null) return (false, "Không tìm thấy sản phẩm.");

                    if (db.SanPhamDonVis.Any(x => x.sanPhamId == productId && !x.isDelete))
                        return (false, "Sản phẩm đang có đơn vị tính.");

                    product.isDelete = true;

                    // Xóa tất cả danh mục liên quan
                    var links = db.SanPhamDanhMucs.Where(x => x.sanPhamId == productId).ToList();
                    foreach (var link in links) link.isDelete = true;

                    db.SaveChanges();
                    return (true, "Xóa thành công.");
                }
                catch (Exception ex) { return (false, $"Lỗi xóa: {ex.Message}"); }
            }
        }

        #endregion

        #region Helper & Disposal

        public string GetProductCategoryId(string productId)
        {
            return _readContext.SanPhamDanhMucs.AsNoTracking()
                .Where(x => x.sanPhamId == productId && !x.isDelete)
                .Select(x => x.danhMucId).FirstOrDefault();
        }

        public bool IsProductInUse(string productId)
        {
            return _readContext.SanPhamDonVis.AsNoTracking()
                .Any(x => x.sanPhamId == productId && !x.isDelete);
        }

        public string GenerateNewProductId() => _services.GenerateNewId<SanPham>("SP", 5);

        public void Dispose() => _readContext?.Dispose();

        #endregion
    }
}