namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.DonGiaoHang")]
    public partial class DonGiaoHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonGiaoHang()
        {
            TrangThaiGiaoHangs = new HashSet<TrangThaiGiaoHang>();
        }

        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string hoaDonId { get; set; }

        [Required]
        [StringLength(50)]
        public string shipperId { get; set; }

        [Required]
        [StringLength(50)]
        public string phiVanChuyenId { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngayGiao { get; set; }

        [Required]
        [StringLength(20)]
        public string trangThaiHienTai { get; set; }

        public bool isDelete { get; set; }

        public virtual HoaDon HoaDon { get; set; }

        public virtual PhiVanChuyen PhiVanChuyen { get; set; }

        public virtual Shipper Shipper { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TrangThaiGiaoHang> TrangThaiGiaoHangs { get; set; }
    }
}
