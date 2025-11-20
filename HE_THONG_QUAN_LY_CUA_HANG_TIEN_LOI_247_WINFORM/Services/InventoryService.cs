using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services
{
    public class InventoryService : IDisposable
    {
        private readonly AppDbContext _context;

        public InventoryService()
        {
            _context = new AppDbContext();
        }

        #region Public Get Operations

        public List<InventoryDetailDto> GetAllInventory()
        {
            try
            {
                var query = from tk in _context.TonKhoes
                           where !tk.isDelete
                           join spDv in _context.SanPhamDonVis on tk.sanPhamDonViId equals spDv.id
                           where !spDv.isDelete && spDv.trangThai == "available"
                           join sp in _context.SanPhams on spDv.sanPhamId equals sp.id
                           where !sp.isDelete
                           join dv in _context.DonViDoLuongs on spDv.donViId equals dv.id
                           where !dv.isDelete
                           join nh in _context.NhanHieux on sp.nhanHieuId equals nh.id
                           where !nh.isDelete
                           orderby sp.id ascending  // Sắp xếp theo ID sản phẩm tăng dần
                           select new InventoryDetailDto
                           {
                               MaSP = sp.id,
                               TenSP = sp.ten,
                               NhanHieu = nh.ten,
                               DonVi = dv.ten,
                               SoLuongTon = tk.soLuongTon,
                               GiaBan = spDv.giaBan,
                               TrangThai = spDv.trangThai,
                               SanPhamDonViId = spDv.id,
                               TonKhoId = tk.id
                           };

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy danh sách tồn kho: {ex.Message}");
            }
        }

        public List<InventoryDetailDto> SearchInventory(string keyword)
        {
            try
            {
                keyword = keyword?.ToLower().Trim() ?? "";

                var query = from tk in _context.TonKhoes
                           where !tk.isDelete
                           join spDv in _context.SanPhamDonVis on tk.sanPhamDonViId equals spDv.id
                           where !spDv.isDelete && spDv.trangThai == "available"
                           join sp in _context.SanPhams on spDv.sanPhamId equals sp.id
                           where !sp.isDelete
                           join dv in _context.DonViDoLuongs on spDv.donViId equals dv.id
                           where !dv.isDelete
                           join nh in _context.NhanHieux on sp.nhanHieuId equals nh.id
                           where !nh.isDelete
                           where sp.id.ToLower().Contains(keyword) ||
                                 sp.ten.ToLower().Contains(keyword) ||
                                 nh.ten.ToLower().Contains(keyword) ||
                                 dv.ten.ToLower().Contains(keyword)
                           orderby sp.id ascending  // Sắp xếp theo ID sản phẩm tăng dần
                           select new InventoryDetailDto
                           {
                               MaSP = sp.id,
                               TenSP = sp.ten,
                               NhanHieu = nh.ten,
                               DonVi = dv.ten,
                               SoLuongTon = tk.soLuongTon,
                               GiaBan = spDv.giaBan,
                               TrangThai = spDv.trangThai,
                               SanPhamDonViId = spDv.id,
                               TonKhoId = tk.id
                           };

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi tìm kiếm tồn kho: {ex.Message}");
            }
        }
        //lay ton kho theo sanPhamDonViId
        public TonKho GetInventoryByProductUnitId(string sanPhamDonViId)
        {
            try
            {
                return _context.TonKhoes
                    .AsNoTracking()
                    .FirstOrDefault(tk => tk.sanPhamDonViId == sanPhamDonViId && !tk.isDelete);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy thông tin tồn kho: {ex.Message}");
            }
        }

        public dynamic GetProductLocations(string sanPhamDonViId)
        {
            try
            {
                var locations = (from spVt in _context.SanPhamViTris
                                where spVt.sanPhamDonViId == sanPhamDonViId && !spVt.isDelete
                                join vt in _context.ViTris on spVt.viTriId equals vt.id
                                where !vt.isDelete
                                select new
                                {
                                    MaViTri = vt.maViTri,
                                    LoaiViTri = vt.loaiViTri,
                                    MoTa = vt.moTa,
                                    SoLuong = spVt.soLuong
                                }).ToList();

                return locations;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy vị trí sản phẩm: {ex.Message}");
            }
        }

        public dynamic GetInventoryStatistics()
        {
            try
            {
                var totalProducts = _context.TonKhoes.Count(tk => !tk.isDelete);
                var totalQuantity = _context.TonKhoes.Where(tk => !tk.isDelete).Sum(tk => tk.soLuongTon);
                var lowStockCount = _context.TonKhoes.Count(tk => !tk.isDelete && tk.soLuongTon < 10);
                var outOfStockCount = _context.TonKhoes.Count(tk => !tk.isDelete && tk.soLuongTon == 0);

                return new
                {
                    TongSoSanPham = totalProducts,
                    TongSoLuongTon = totalQuantity,
                    SanPhamSapHet = lowStockCount,
                    SanPhamHetHang = outOfStockCount
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy thống kê: {ex.Message}");
            }
        }

        #endregion

        #region CRUD Operations

        public (bool success, string message) UpdateInventoryQuantity(string tonKhoId, int newQuantity)
        {
            try
            {
                var tonKho = _context.TonKhoes.Find(tonKhoId);
                if (tonKho == null)
                    return (false, "Không tìm thấy thông tin tồn kho");

                if (newQuantity < 0)
                    return (false, "Số lượng tồn không được âm");

                tonKho.soLuongTon = newQuantity;
                _context.SaveChanges();

                return (true, "Cập nhật tồn kho thành công");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi cập nhật tồn kho: {ex.Message}");
            }
        }

        #endregion

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}