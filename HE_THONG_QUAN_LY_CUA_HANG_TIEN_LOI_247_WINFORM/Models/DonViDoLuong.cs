namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.DonViDoLuong")]
    public partial class DonViDoLuong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonViDoLuong()
        {
            LichSuGiaBans = new HashSet<LichSuGiaBan>();
            SanPhamDonVis = new HashSet<SanPhamDonVi>();
        }

        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(200)]
        public string ten { get; set; }

        [Required]
        [StringLength(50)]
        public string kyHieu { get; set; }

        public bool isDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuGiaBan> LichSuGiaBans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPhamDonVi> SanPhamDonVis { get; set; }
    }
}
