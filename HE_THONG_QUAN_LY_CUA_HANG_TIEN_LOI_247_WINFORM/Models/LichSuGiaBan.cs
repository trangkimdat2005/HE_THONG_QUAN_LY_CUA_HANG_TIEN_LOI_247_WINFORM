namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.LichSuGiaBan")]
    public partial class LichSuGiaBan
    {
        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string sanPhamId { get; set; }

        [Required]
        [StringLength(50)]
        public string donViId { get; set; }

        public decimal giaBan { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngayBatDau { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngayKetThuc { get; set; }

        public bool isDelete { get; set; }

        public virtual DonViDoLuong DonViDoLuong { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
