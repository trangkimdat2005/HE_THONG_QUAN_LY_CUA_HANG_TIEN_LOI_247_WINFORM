namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.PhieuNhap")]
    public partial class PhieuNhap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuNhap()
        {
            ChiTietPhieuNhaps = new HashSet<ChiTietPhieuNhap>();
        }

        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string nhaCungCapId { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngayNhap { get; set; }

        public decimal tongTien { get; set; }

        public bool isDelete { get; set; }

        [Required]
        [StringLength(50)]
        public string nhanVienId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }

        public virtual NhaCungCap NhaCungCap { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
