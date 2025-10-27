namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.DonHangOnline")]
    public partial class DonHangOnline
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHangOnline()
        {
            ChiTietDonOnlines = new HashSet<ChiTietDonOnline>();
            TrangThaiXuLies = new HashSet<TrangThaiXuLy>();
        }

        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string hoaDonId { get; set; }

        [Required]
        [StringLength(50)]
        public string khachHangId { get; set; }

        [Required]
        [StringLength(20)]
        public string kenhDat { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngayDat { get; set; }

        public decimal tongTien { get; set; }

        public bool isDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonOnline> ChiTietDonOnlines { get; set; }

        public virtual HoaDon HoaDon { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrangThaiXuLy> TrangThaiXuLies { get; set; }
    }
}
