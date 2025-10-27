namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.Shipper")]
    public partial class Shipper
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shipper()
        {
            DonGiaoHangs = new HashSet<DonGiaoHang>();
        }

        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(200)]
        public string hoTen { get; set; }

        [Required]
        [StringLength(50)]
        public string soDienThoai { get; set; }

        public bool isDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonGiaoHang> DonGiaoHangs { get; set; }
    }
}
