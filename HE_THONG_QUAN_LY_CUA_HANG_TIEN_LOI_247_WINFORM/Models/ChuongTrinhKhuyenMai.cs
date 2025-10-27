namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.ChuongTrinhKhuyenMai")]
    public partial class ChuongTrinhKhuyenMai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChuongTrinhKhuyenMai()
        {
            DieuKienApDungs = new HashSet<DieuKienApDung>();
            MaKhuyenMais = new HashSet<MaKhuyenMai>();
        }

        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(255)]
        public string ten { get; set; }

        [Required]
        [StringLength(30)]
        public string loai { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngayBatDau { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngayKetThuc { get; set; }

        [Required]
        public string moTa { get; set; }

        public bool isDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DieuKienApDung> DieuKienApDungs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaKhuyenMai> MaKhuyenMais { get; set; }
    }
}
