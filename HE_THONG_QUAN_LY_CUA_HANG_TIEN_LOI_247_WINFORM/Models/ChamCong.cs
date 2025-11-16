namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.ChamCong")]
    public partial class ChamCong
    {
        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string nhanVienId { get; set; }

        public DateTime ngay { get; set; }

        public TimeSpan gioVao { get; set; }

        public TimeSpan gioRa { get; set; }

        [StringLength(500)]
        public string ghiChu { get; set; }

        public bool isDelete { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
