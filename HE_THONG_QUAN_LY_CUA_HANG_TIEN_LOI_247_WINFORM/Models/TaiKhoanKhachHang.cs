namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.TaiKhoanKhachHang")]
    public partial class TaiKhoanKhachHang
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string khachHangId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string taiKhoanid { get; set; }

        public bool isDelete { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
