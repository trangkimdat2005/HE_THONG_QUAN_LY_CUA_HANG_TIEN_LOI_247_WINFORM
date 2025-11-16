namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.HinhAnh")]
    public partial class HinhAnh
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HinhAnh()
        {
            Anh_SanPhamDonVi = new HashSet<Anh_SanPhamDonVi>();
            KhachHangs = new HashSet<KhachHang>();
            MaDinhDanhSanPhams = new HashSet<MaDinhDanhSanPham>();
            NhanViens = new HashSet<NhanVien>();
            TemNhans = new HashSet<TemNhan>();
        }

        [StringLength(50)]
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TenAnh { get; set; }

        [Required]
        public byte[] Anh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Anh_SanPhamDonVi> Anh_SanPhamDonVi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhachHang> KhachHangs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaDinhDanhSanPham> MaDinhDanhSanPhams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NhanVien> NhanViens { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TemNhan> TemNhans { get; set; }
    }
}
