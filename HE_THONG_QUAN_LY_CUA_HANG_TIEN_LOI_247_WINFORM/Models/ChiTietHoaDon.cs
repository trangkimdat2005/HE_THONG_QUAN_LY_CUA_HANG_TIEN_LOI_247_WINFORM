namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.ChiTietHoaDon")]
    public partial class ChiTietHoaDon
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string hoaDonId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string sanPhamDonViId { get; set; }

        public int soLuong { get; set; }

        public decimal donGia { get; set; }

        public decimal giamGia { get; set; }

        public bool isDelete { get; set; }

        public virtual HoaDon HoaDon { get; set; }
    }
}
