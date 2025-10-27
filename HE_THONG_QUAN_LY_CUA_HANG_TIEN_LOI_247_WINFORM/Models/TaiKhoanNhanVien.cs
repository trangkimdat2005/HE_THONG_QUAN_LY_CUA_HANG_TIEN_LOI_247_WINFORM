namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.TaiKhoanNhanVien")]
    public partial class TaiKhoanNhanVien
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string nhanVienId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string taiKhoanId { get; set; }

        public bool isDelete { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
