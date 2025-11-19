namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.TheThanhVien")]
    public partial class TheThanhVien
    {
        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string khachHangId { get; set; }

        [Required]
        [StringLength(20)]
        public string hang { get; set; }

        public int diemTichLuy { get; set; }

        public DateTime ngayCap { get; set; }

        public bool isDelete { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}
