namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.MaDinhDanhSanPham")]
    public partial class MaDinhDanhSanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MaDinhDanhSanPham()
        {
            TemNhans = new HashSet<TemNhan>();
        }

        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string sanPhamDonViId { get; set; }

        [Required]
        [StringLength(10)]
        public string loaiMa { get; set; }

        [Required]
        [StringLength(200)]
        public string maCode { get; set; }

        [Required]
        [StringLength(500)]
        public string duongDan { get; set; }

        public bool isDelete { get; set; }

        [StringLength(50)]
        public string anhId { get; set; }

        public virtual HinhAnh HinhAnh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TemNhan> TemNhans { get; set; }
    }
}
