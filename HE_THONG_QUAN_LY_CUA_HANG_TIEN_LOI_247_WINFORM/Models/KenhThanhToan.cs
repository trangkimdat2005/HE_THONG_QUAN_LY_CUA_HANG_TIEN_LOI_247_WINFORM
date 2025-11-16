namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.KenhThanhToan")]
    public partial class KenhThanhToan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KenhThanhToan()
        {
            GiaoDichThanhToans = new HashSet<GiaoDichThanhToan>();
        }

        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(200)]
        public string tenKenh { get; set; }

        [Required]
        [StringLength(20)]
        public string loaiKenh { get; set; }

        public decimal phiGiaoDich { get; set; }

        [Required]
        [StringLength(20)]
        public string trangThai { get; set; }

        public string cauHinh { get; set; }

        public DateTime ngayTao { get; set; }

        public DateTime ngayCapNhat { get; set; }

        public bool isDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GiaoDichThanhToan> GiaoDichThanhToans { get; set; }
    }
}
