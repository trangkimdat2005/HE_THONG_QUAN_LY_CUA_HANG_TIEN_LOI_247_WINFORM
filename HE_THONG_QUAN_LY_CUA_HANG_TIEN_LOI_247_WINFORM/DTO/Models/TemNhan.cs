namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.TemNhan")]
    public partial class TemNhan
    {
        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string maDinhDanhId { get; set; }

        [Required]
        public string noiDungTem { get; set; }

        public DateTime ngayIn { get; set; }

        public bool isDelete { get; set; }

        [Required]
        [StringLength(50)]
        public string anhId { get; set; }

        public virtual HinhAnh HinhAnh { get; set; }

        public virtual MaDinhDanhSanPham MaDinhDanhSanPham { get; set; }
    }
}
