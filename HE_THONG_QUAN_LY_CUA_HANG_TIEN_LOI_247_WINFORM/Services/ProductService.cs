using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models; 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services
{
    public class ProductService
    {
        private readonly AppDbContext _readContext;
        private readonly QuanLyServices _services;

        public ProductService()
        {
            _readContext = new AppDbContext();
            _services = new QuanLyServices();
        }

        #region Get/Search Operations (Sử dụng AsNoTracking để đọc nhanh)

        public SanPham GetProductById(string id)
        {
            return _readContext.SanPhams.AsNoTracking()
                .Include("SanPhamDonVis")
                .Include("SanPhamDonVis.DonViDoLuong")
                .FirstOrDefault(p => p.id == id && !p.isDelete);
        }

        public List<ProductDetailDto> FilterProducts(string keyword = null, string brandId = null, string categoryId = null)
        {
            try
            {
                var query = _services.GetList<SanPhamDonVi>();

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    keyword = keyword.ToLower();
                    query = query.Where(p => 
                        (p.sanPhamId != null && p.sanPhamId.ToLower().Contains(keyword)) ||
                        (p.SanPham != null && p.SanPham.ten != null && p.SanPham.ten.ToLower().Contains(keyword))
                    ).ToList();
                }
                
                if (!string.IsNullOrEmpty(brandId))
                {
                    query = query.Where(p => p.SanPham.nhanHieuId == brandId).ToList();
                }
                
                if (!string.IsNullOrEmpty(categoryId))
                {
                    query = query.Where(p => 
                        p.SanPham.SanPhamDanhMucs.Any(spdm => spdm.danhMucId == categoryId && !spdm.isDelete)
                    ).ToList();
                }

                var result = query.Select(p => new
                {
                    Id = p.id,                                 
                    SanPhamId = p.sanPhamId,                 
                    DonViId = p.donViId,                       
                    TenSanPham = p.SanPham.ten,
                    NhanHieuTen = p.SanPham.NhanHieu != null ? p.SanPham.NhanHieu.ten : "",
                    DonViTen = p.DonViDoLuong != null ? p.DonViDoLuong.ten : "",
                    MoTa = p.SanPham.moTa,
                    GiaBan = p.giaBan,
                    TrangThai = p.trangThai,
                    IsDelete = p.isDelete,
                    DanhMucTen = p.SanPham.SanPhamDanhMucs
                        .Where(dm => !dm.isDelete)
                        .Select(dm => dm.DanhMuc.ten)
                        .FirstOrDefault()
                }).ToList();

                return result.Select(x => new ProductDetailDto
                {
                    Id = x.Id,                                 
                    SanPhamId = x.SanPhamId,                  
                    DonViId = x.DonViId,                      
                    Ten = x.TenSanPham,
                    TenSanPham = x.TenSanPham,
                    NhanHieu = x.NhanHieuTen,
                    DanhMuc = x.DanhMucTen ?? "Chưa phân loại",
                    MoTa = x.MoTa,
                    GiaBan = x.GiaBan,
                    DonVi = x.DonViTen,
                    TrangThai = x.IsDelete ? "Ngừng kinh doanh" : 
                               (x.TrangThai ?? "Đang kinh doanh")
                }).OrderBy(x => x.TenSanPham).Take(1000).ToList();
            }
            catch (Exception ex) 
            { 
                throw new Exception($"Lỗi lọc: {ex.Message}"); 
            }
        }

        public string GetProductCategoryId(string productId)
        {
            return _readContext.SanPhamDanhMucs.AsNoTracking()
                .Where(x => x.sanPhamId == productId && !x.isDelete)
                .Select(x => x.danhMucId).FirstOrDefault();
        }

        public string GenerateNewProductId() => _services.GenerateNewId<SanPham>("SP", 6);

        #endregion

        #region CRUD Operations (Thêm, Sửa, Xóa)

        public (bool success, string message, SanPham product) AddProduct(SanPham product, string categoryId = null)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    if (db.SanPhams.Any(p => p.ten.ToLower() == product.ten.ToLower() && !p.isDelete))
                        return (false, "Tên sản phẩm đã tồn tại.", null);

                    product.id = _services.GenerateNewId<SanPham>("SP", 6);
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

        public (bool success, string message) UpdateProduct(SanPham product, string categoryId = null)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    var existingProduct = db.SanPhams.FirstOrDefault(p => p.id == product.id);
                    if (existingProduct == null) return (false, "Không tìm thấy sản phẩm.");

                    if (db.SanPhams.Any(p => p.ten.ToLower() == product.ten.ToLower() && p.id != product.id && !p.isDelete))
                        return (false, "Tên sản phẩm đã tồn tại.");

                    existingProduct.ten = product.ten;
                    existingProduct.nhanHieuId = product.nhanHieuId;
                    existingProduct.moTa = product.moTa;

                    var allLinks = db.SanPhamDanhMucs.Where(x => x.sanPhamId == product.id).ToList();
                    foreach (var link in allLinks) link.isDelete = true;

                    if (!string.IsNullOrEmpty(categoryId))
                    {
                        var matchLink = allLinks.FirstOrDefault(x => x.danhMucId == categoryId);
                        if (matchLink != null) matchLink.isDelete = false;
                        else
                        {
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
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var product = db.SanPhams.FirstOrDefault(p => p.id == productId);
                        if (product == null) return (false, "Không tìm thấy sản phẩm.");

                        product.isDelete = true;

                        var units = db.SanPhamDonVis.Where(x => x.sanPhamId == productId).ToList();
                        foreach (var unit in units) unit.isDelete = true;

                        var links = db.SanPhamDanhMucs.Where(x => x.sanPhamId == productId).ToList();
                        foreach (var link in links) link.isDelete = true;

                        db.SaveChanges();
                        transaction.Commit();
                        return (true, "Xóa thành công.");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return (false, $"Lỗi xóa: {ex.Message}");
                    }
                }
            }
        }

        #endregion

        public void Dispose()
        {
            _readContext?.Dispose();
        }
    }
}