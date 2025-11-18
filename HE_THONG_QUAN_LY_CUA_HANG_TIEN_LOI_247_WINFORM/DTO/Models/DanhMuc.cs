namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.DanhMuc")]
    public partial class DanhMuc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DanhMuc()
        {
            ChinhSachHoanTra_DanhMuc = new HashSet<ChinhSachHoanTra_DanhMuc>();
            DieuKienApDungDanhMucs = new HashSet<DieuKienApDungDanhMuc>();
            SanPhamDanhMucs = new HashSet<SanPhamDanhMuc>();
        }

        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(200)]
        public string ten { get; set; }

        public bool isDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChinhSachHoanTra_DanhMuc> ChinhSachHoanTra_DanhMuc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DieuKienApDungDanhMuc> DieuKienApDungDanhMucs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPhamDanhMuc> SanPhamDanhMucs { get; set; }
    }
}
