namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.SanPhamDonVi")]
    public partial class SanPhamDonVi
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string sanPhamId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string donViId { get; set; }

        [Required]
        [StringLength(50)]
        public string id { get; set; }

        public decimal heSoQuyDoi { get; set; }

        public decimal giaBan { get; set; }

        public bool isDelete { get; set; }

        public virtual DonViDoLuong DonViDoLuong { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
