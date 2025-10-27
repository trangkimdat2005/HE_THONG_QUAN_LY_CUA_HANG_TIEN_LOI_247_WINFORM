namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.LichSuGiaoDich")]
    public partial class LichSuGiaoDich
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LichSuGiaoDich()
        {
            ChiTietGiaoDichNCCs = new HashSet<ChiTietGiaoDichNCC>();
        }

        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string nhaCungCapId { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngayGD { get; set; }

        public decimal tongTien { get; set; }

        public bool isDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietGiaoDichNCC> ChiTietGiaoDichNCCs { get; set; }

        public virtual NhaCungCap NhaCungCap { get; set; }
    }
}
