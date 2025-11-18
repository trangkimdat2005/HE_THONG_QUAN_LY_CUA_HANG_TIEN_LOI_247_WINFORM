namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.ChiTietHoaDonKhuyenMai")]
    public partial class ChiTietHoaDonKhuyenMai
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string hoaDonId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string sanPhamDonViId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string maKhuyenMaiId { get; set; }

        public decimal giaTriApDung { get; set; }

        [Required]
        [StringLength(50)]
        public string id { get; set; }

        public bool isDelete { get; set; }

        public virtual HoaDon HoaDon { get; set; }

        public virtual MaKhuyenMai MaKhuyenMai { get; set; }
    }
}
