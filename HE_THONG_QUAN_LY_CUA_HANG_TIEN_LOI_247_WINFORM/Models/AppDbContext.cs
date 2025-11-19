using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("name=AppDbContext1")
        {
        }

        public virtual DbSet<Anh_SanPhamDonVi> Anh_SanPhamDonVi { get; set; }
        public virtual DbSet<BaoCao> BaoCaos { get; set; }
        public virtual DbSet<BaoCaoBanChay> BaoCaoBanChays { get; set; }
        public virtual DbSet<BaoCaoDoanhThu> BaoCaoDoanhThus { get; set; }
        public virtual DbSet<BaoCaoTonKho> BaoCaoTonKhoes { get; set; }
        public virtual DbSet<CaLamViec> CaLamViecs { get; set; }
        public virtual DbSet<ChamCong> ChamCongs { get; set; }
        public virtual DbSet<ChinhSachHoanTra> ChinhSachHoanTras { get; set; }
        public virtual DbSet<ChinhSachHoanTra_DanhMuc> ChinhSachHoanTra_DanhMuc { get; set; }
        public virtual DbSet<ChiTietDonOnline> ChiTietDonOnlines { get; set; }
        public virtual DbSet<ChiTietGiaoDichNCC> ChiTietGiaoDichNCCs { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<ChiTietHoaDonKhuyenMai> ChiTietHoaDonKhuyenMais { get; set; }
        public virtual DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
        public virtual DbSet<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; }
        public virtual DbSet<ChuongTrinhKhuyenMai> ChuongTrinhKhuyenMais { get; set; }
        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<DieuKienApDung> DieuKienApDungs { get; set; }
        public virtual DbSet<DieuKienApDungDanhMuc> DieuKienApDungDanhMucs { get; set; }
        public virtual DbSet<DieuKienApDungSanPham> DieuKienApDungSanPhams { get; set; }
        public virtual DbSet<DieuKienApDungToanBo> DieuKienApDungToanBoes { get; set; }
        public virtual DbSet<DonGiaoHang> DonGiaoHangs { get; set; }
        public virtual DbSet<DonHangOnline> DonHangOnlines { get; set; }
        public virtual DbSet<DonViDoLuong> DonViDoLuongs { get; set; }
        public virtual DbSet<GiaoDichThanhToan> GiaoDichThanhToans { get; set; }
        public virtual DbSet<GioHang> GioHangs { get; set; }
        public virtual DbSet<HinhAnh> HinhAnhs { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KenhThanhToan> KenhThanhToans { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<KiemKe> KiemKes { get; set; }
        public virtual DbSet<LichSuGiaBan> LichSuGiaBans { get; set; }
        public virtual DbSet<LichSuGiaoDich> LichSuGiaoDiches { get; set; }
        public virtual DbSet<LichSuMuaHang> LichSuMuaHangs { get; set; }
        public virtual DbSet<MaDinhDanhSanPham> MaDinhDanhSanPhams { get; set; }
        public virtual DbSet<MaKhuyenMai> MaKhuyenMais { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<NhanHieu> NhanHieux { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<NhatKyHoatDong> NhatKyHoatDongs { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PhanCongCaLamViec> PhanCongCaLamViecs { get; set; }
        public virtual DbSet<PhieuDoiTra> PhieuDoiTras { get; set; }
        public virtual DbSet<PhieuNhap> PhieuNhaps { get; set; }
        public virtual DbSet<PhieuXuat> PhieuXuats { get; set; }
        public virtual DbSet<PhiVanChuyen> PhiVanChuyens { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<SanPhamDanhMuc> SanPhamDanhMucs { get; set; }
        public virtual DbSet<SanPhamDonVi> SanPhamDonVis { get; set; }
        public virtual DbSet<SanPhamViTri> SanPhamViTris { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<TaiKhoanKhachHang> TaiKhoanKhachHangs { get; set; }
        public virtual DbSet<TaiKhoanNhanVien> TaiKhoanNhanViens { get; set; }
        public virtual DbSet<TemNhan> TemNhans { get; set; }
        public virtual DbSet<TheThanhVien> TheThanhViens { get; set; }
        public virtual DbSet<TonKho> TonKhoes { get; set; }
        public virtual DbSet<TrangThaiGiaoHang> TrangThaiGiaoHangs { get; set; }
        public virtual DbSet<TrangThaiXuLy> TrangThaiXuLies { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<ViTri> ViTris { get; set; }
        public virtual DbSet<C__EFMigrationsHistory> C__EFMigrationsHistory { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaoCao>()
                .HasMany(e => e.BaoCaoBanChays)
                .WithRequired(e => e.BaoCao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BaoCao>()
                .HasMany(e => e.BaoCaoDoanhThus)
                .WithRequired(e => e.BaoCao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BaoCao>()
                .HasMany(e => e.BaoCaoTonKhoes)
                .WithRequired(e => e.BaoCao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CaLamViec>()
                .HasMany(e => e.PhanCongCaLamViecs)
                .WithRequired(e => e.CaLamViec)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChinhSachHoanTra>()
                .HasMany(e => e.ChinhSachHoanTra_DanhMuc)
                .WithRequired(e => e.ChinhSachHoanTra)
                .HasForeignKey(e => e.chinhSachId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChinhSachHoanTra>()
                .HasMany(e => e.PhieuDoiTras)
                .WithRequired(e => e.ChinhSachHoanTra)
                .HasForeignKey(e => e.chinhSachId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChuongTrinhKhuyenMai>()
                .HasMany(e => e.DieuKienApDungs)
                .WithRequired(e => e.ChuongTrinhKhuyenMai)
                .HasForeignKey(e => e.chuongTrinhId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChuongTrinhKhuyenMai>()
                .HasMany(e => e.MaKhuyenMais)
                .WithRequired(e => e.ChuongTrinhKhuyenMai)
                .HasForeignKey(e => e.chuongTrinhId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DanhMuc>()
                .HasMany(e => e.ChinhSachHoanTra_DanhMuc)
                .WithRequired(e => e.DanhMuc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DanhMuc>()
                .HasMany(e => e.DieuKienApDungDanhMucs)
                .WithRequired(e => e.DanhMuc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DanhMuc>()
                .HasMany(e => e.SanPhamDanhMucs)
                .WithRequired(e => e.DanhMuc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DieuKienApDung>()
                .HasMany(e => e.DieuKienApDungDanhMucs)
                .WithRequired(e => e.DieuKienApDung)
                .HasForeignKey(e => e.dieuKienId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DieuKienApDung>()
                .HasMany(e => e.DieuKienApDungSanPhams)
                .WithRequired(e => e.DieuKienApDung)
                .HasForeignKey(e => e.dieuKienId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DieuKienApDung>()
                .HasMany(e => e.DieuKienApDungToanBoes)
                .WithRequired(e => e.DieuKienApDung)
                .HasForeignKey(e => e.dieuKienId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonGiaoHang>()
                .HasMany(e => e.TrangThaiGiaoHangs)
                .WithRequired(e => e.DonGiaoHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonHangOnline>()
                .HasMany(e => e.ChiTietDonOnlines)
                .WithRequired(e => e.DonHangOnline)
                .HasForeignKey(e => e.donHangId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonHangOnline>()
                .HasMany(e => e.TrangThaiXuLies)
                .WithRequired(e => e.DonHangOnline)
                .HasForeignKey(e => e.donHangId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonViDoLuong>()
                .HasMany(e => e.LichSuGiaBans)
                .WithRequired(e => e.DonViDoLuong)
                .HasForeignKey(e => e.donViId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonViDoLuong>()
                .HasMany(e => e.SanPhamDonVis)
                .WithRequired(e => e.DonViDoLuong)
                .HasForeignKey(e => e.donViId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HinhAnh>()
                .HasMany(e => e.Anh_SanPhamDonVi)
                .WithRequired(e => e.HinhAnh)
                .HasForeignKey(e => e.anhId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HinhAnh>()
                .HasMany(e => e.KhachHangs)
                .WithOptional(e => e.HinhAnh)
                .HasForeignKey(e => e.anhId);

            modelBuilder.Entity<HinhAnh>()
                .HasMany(e => e.MaDinhDanhSanPhams)
                .WithOptional(e => e.HinhAnh)
                .HasForeignKey(e => e.anhId);

            modelBuilder.Entity<HinhAnh>()
                .HasMany(e => e.NhanViens)
                .WithRequired(e => e.HinhAnh)
                .HasForeignKey(e => e.anhId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HinhAnh>()
                .HasMany(e => e.TemNhans)
                .WithRequired(e => e.HinhAnh)
                .HasForeignKey(e => e.anhId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.ChiTietHoaDons)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.ChiTietHoaDonKhuyenMais)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.DonGiaoHangs)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.DonHangOnlines)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.GiaoDichThanhToans)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.LichSuMuaHangs)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.PhieuDoiTras)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KenhThanhToan>()
                .HasMany(e => e.GiaoDichThanhToans)
                .WithRequired(e => e.KenhThanhToan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.DonHangOnlines)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.HoaDons)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.LichSuMuaHangs)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.PhieuXuats)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.TaiKhoanKhachHangs)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.TheThanhViens)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LichSuGiaoDich>()
                .HasMany(e => e.ChiTietGiaoDichNCCs)
                .WithRequired(e => e.LichSuGiaoDich)
                .HasForeignKey(e => e.giaoDichId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MaDinhDanhSanPham>()
                .HasMany(e => e.TemNhans)
                .WithRequired(e => e.MaDinhDanhSanPham)
                .HasForeignKey(e => e.maDinhDanhId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MaKhuyenMai>()
                .HasMany(e => e.ChiTietHoaDonKhuyenMais)
                .WithRequired(e => e.MaKhuyenMai)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhaCungCap>()
                .HasMany(e => e.LichSuGiaoDiches)
                .WithRequired(e => e.NhaCungCap)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhaCungCap>()
                .HasMany(e => e.PhieuNhaps)
                .WithRequired(e => e.NhaCungCap)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanHieu>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.NhanHieu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.ChamCongs)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.HoaDons)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.KiemKes)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.PhanCongCaLamViecs)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.PhieuNhaps)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.PhieuXuats)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.TaiKhoanNhanViens)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Permission>()
                .HasMany(e => e.RolePermissions)
                .WithRequired(e => e.Permission)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhieuNhap>()
                .HasMany(e => e.ChiTietPhieuNhaps)
                .WithRequired(e => e.PhieuNhap)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhieuXuat>()
                .HasMany(e => e.ChiTietPhieuXuats)
                .WithRequired(e => e.PhieuXuat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhiVanChuyen>()
                .HasMany(e => e.DonGiaoHangs)
                .WithRequired(e => e.PhiVanChuyen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.RolePermissions)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.BaoCaoBanChays)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.DieuKienApDungSanPhams)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.LichSuGiaBans)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.SanPhamDanhMucs)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.SanPhamDonVis)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPhamDonVi>()
                .Property(e => e.heSoQuyDoi)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Shipper>()
                .HasMany(e => e.DonGiaoHangs)
                .WithRequired(e => e.Shipper)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.GioHangs)
                .WithRequired(e => e.TaiKhoan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.NhatKyHoatDongs)
                .WithRequired(e => e.TaiKhoan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.TaiKhoanKhachHangs)
                .WithRequired(e => e.TaiKhoan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.TaiKhoanNhanViens)
                .WithRequired(e => e.TaiKhoan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.UserRoles)
                .WithRequired(e => e.TaiKhoan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ViTri>()
                .HasMany(e => e.SanPhamViTris)
                .WithRequired(e => e.ViTri)
                .WillCascadeOnDelete(false);
        }
    }
}
