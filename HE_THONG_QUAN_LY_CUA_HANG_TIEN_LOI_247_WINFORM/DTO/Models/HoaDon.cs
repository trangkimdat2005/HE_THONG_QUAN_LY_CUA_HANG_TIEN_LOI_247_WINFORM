namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.HoaDon")]
    public partial class HoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDon()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
            ChiTietHoaDonKhuyenMais = new HashSet<ChiTietHoaDonKhuyenMai>();
            DonGiaoHangs = new HashSet<DonGiaoHang>();
            DonHangOnlines = new HashSet<DonHangOnline>();
            GiaoDichThanhToans = new HashSet<GiaoDichThanhToan>();
            LichSuMuaHangs = new HashSet<LichSuMuaHang>();
            PhieuDoiTras = new HashSet<PhieuDoiTra>();
        }

        [StringLength(50)]
        public string id { get; set; }

        public DateTime ngayLap { get; set; }

        public decimal? tongTien { get; set; }

        [Required]
        [StringLength(50)]
        public string nhanVienId { get; set; }

        [Required]
        [StringLength(50)]
        public string khachHangId { get; set; }

        [Required]
        [StringLength(20)]
        public string trangThai { get; set; }

        public bool isDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDonKhuyenMai> ChiTietHoaDonKhuyenMais { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonGiaoHang> DonGiaoHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHangOnline> DonHangOnlines { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GiaoDichThanhToan> GiaoDichThanhToans { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuMuaHang> LichSuMuaHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuDoiTra> PhieuDoiTras { get; set; }
    }
}
