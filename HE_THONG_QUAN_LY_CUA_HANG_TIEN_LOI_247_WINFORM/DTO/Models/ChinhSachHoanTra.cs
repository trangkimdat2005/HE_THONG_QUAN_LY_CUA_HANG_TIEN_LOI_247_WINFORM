namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.ChinhSachHoanTra")]
    public partial class ChinhSachHoanTra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChinhSachHoanTra()
        {
            ChinhSachHoanTra_DanhMuc = new HashSet<ChinhSachHoanTra_DanhMuc>();
            PhieuDoiTras = new HashSet<PhieuDoiTra>();
        }

        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(200)]
        public string tenChinhSach { get; set; }

        public int? thoiHan { get; set; }

        [Required]
        [StringLength(500)]
        public string dieuKien { get; set; }

        public bool? apDungToanBo { get; set; }

        public DateTime apDungTuNgay { get; set; }

        public DateTime apDungDenNgay { get; set; }

        public bool isDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChinhSachHoanTra_DanhMuc> ChinhSachHoanTra_DanhMuc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuDoiTra> PhieuDoiTras { get; set; }
    }
}
