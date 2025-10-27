namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.KiemKe")]
    public partial class KiemKe
    {
        [StringLength(50)]
        public string id { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngayKiemKe { get; set; }

        [Required]
        [StringLength(500)]
        public string ketQua { get; set; }

        [Required]
        [StringLength(50)]
        public string nhanVienId { get; set; }

        public bool isDelete { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
