namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.LichSuMuaHang")]
    public partial class LichSuMuaHang
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string khachHangId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string hoaDonId { get; set; }

        public decimal tongTien { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngayMua { get; set; }

        public bool isDelete { get; set; }

        public virtual HoaDon HoaDon { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}
