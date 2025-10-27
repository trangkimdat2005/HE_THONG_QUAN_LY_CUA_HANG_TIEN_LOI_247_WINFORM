namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.PhiVanChuyen")]
    public partial class PhiVanChuyen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhiVanChuyen()
        {
            DonGiaoHangs = new HashSet<DonGiaoHang>();
        }

        [StringLength(50)]
        public string id { get; set; }

        public decimal soTien { get; set; }

        [Required]
        [StringLength(100)]
        public string phuongThucTinh { get; set; }

        public bool isDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonGiaoHang> DonGiaoHangs { get; set; }
    }
}
