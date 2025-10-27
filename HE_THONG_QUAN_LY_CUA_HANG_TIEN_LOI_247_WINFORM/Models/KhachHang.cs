namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            DonHangOnlines = new HashSet<DonHangOnline>();
            HoaDons = new HashSet<HoaDon>();
            LichSuMuaHangs = new HashSet<LichSuMuaHang>();
            PhieuXuats = new HashSet<PhieuXuat>();
            TaiKhoanKhachHangs = new HashSet<TaiKhoanKhachHang>();
            TheThanhViens = new HashSet<TheThanhVien>();
        }

        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(200)]
        public string hoTen { get; set; }

        [Required]
        [StringLength(50)]
        public string soDienThoai { get; set; }

        [Required]
        [StringLength(200)]
        public string email { get; set; }

        [Required]
        [StringLength(500)]
        public string diaChi { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngayDangKy { get; set; }

        [Required]
        [StringLength(20)]
        public string trangThai { get; set; }

        public bool isDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonHangOnline> DonHangOnlines { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuMuaHang> LichSuMuaHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuXuat> PhieuXuats { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaiKhoanKhachHang> TaiKhoanKhachHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TheThanhVien> TheThanhViens { get; set; }
    }
}
