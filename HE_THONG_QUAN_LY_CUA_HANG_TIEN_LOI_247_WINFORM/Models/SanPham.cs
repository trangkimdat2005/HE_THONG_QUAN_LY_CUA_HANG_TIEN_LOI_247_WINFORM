namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            BaoCaoBanChays = new HashSet<BaoCaoBanChay>();
            DieuKienApDungSanPhams = new HashSet<DieuKienApDungSanPham>();
            LichSuGiaBans = new HashSet<LichSuGiaBan>();
            SanPhamDanhMucs = new HashSet<SanPhamDanhMuc>();
            SanPhamDonVis = new HashSet<SanPhamDonVi>();
        }

        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(255)]
        public string ten { get; set; }

        [Required]
        [StringLength(50)]
        public string nhanHieuId { get; set; }

        public string moTa { get; set; }

        public bool isDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaoCaoBanChay> BaoCaoBanChays { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DieuKienApDungSanPham> DieuKienApDungSanPhams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuGiaBan> LichSuGiaBans { get; set; }

        public virtual NhanHieu NhanHieu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPhamDanhMuc> SanPhamDanhMucs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPhamDonVi> SanPhamDonVis { get; set; }
    }
}
