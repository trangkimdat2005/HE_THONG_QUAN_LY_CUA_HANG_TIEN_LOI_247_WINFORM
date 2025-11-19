namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.DieuKienApDung")]
    public partial class DieuKienApDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DieuKienApDung()
        {
            DieuKienApDungDanhMucs = new HashSet<DieuKienApDungDanhMuc>();
            DieuKienApDungSanPhams = new HashSet<DieuKienApDungSanPham>();
            DieuKienApDungToanBoes = new HashSet<DieuKienApDungToanBo>();
        }

        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string chuongTrinhId { get; set; }

        [Required]
        [StringLength(500)]
        public string dieuKien { get; set; }

        public decimal giaTriToiThieu { get; set; }

        [Required]
        [StringLength(20)]
        public string giamTheo { get; set; }

        public decimal giaTriToiDa { get; set; }

        public bool isDelete { get; set; }

        public virtual ChuongTrinhKhuyenMai ChuongTrinhKhuyenMai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DieuKienApDungDanhMuc> DieuKienApDungDanhMucs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DieuKienApDungSanPham> DieuKienApDungSanPhams { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DieuKienApDungToanBo> DieuKienApDungToanBoes { get; set; }
    }
}
