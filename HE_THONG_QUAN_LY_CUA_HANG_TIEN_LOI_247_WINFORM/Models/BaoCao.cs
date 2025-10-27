namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.BaoCao")]
    public partial class BaoCao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BaoCao()
        {
            BaoCaoBanChays = new HashSet<BaoCaoBanChay>();
            BaoCaoDoanhThus = new HashSet<BaoCaoDoanhThu>();
            BaoCaoTonKhoes = new HashSet<BaoCaoTonKho>();
        }

        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(20)]
        public string loaiBaoCao { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngayLap { get; set; }

        [Required]
        [StringLength(500)]
        public string fileBaoCao { get; set; }

        public bool isDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaoCaoBanChay> BaoCaoBanChays { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaoCaoDoanhThu> BaoCaoDoanhThus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaoCaoTonKho> BaoCaoTonKhoes { get; set; }
    }
}
