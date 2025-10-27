namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.NhatKyHoatDong")]
    public partial class NhatKyHoatDong
    {
        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string taiKhoanId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime thoiGian { get; set; }

        [Required]
        [StringLength(500)]
        public string hanhDong { get; set; }

        public bool isDelete { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
