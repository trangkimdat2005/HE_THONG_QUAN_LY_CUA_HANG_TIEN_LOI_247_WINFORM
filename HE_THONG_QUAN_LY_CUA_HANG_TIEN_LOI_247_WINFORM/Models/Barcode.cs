namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.Barcode")]
    public partial class Barcode
    {
        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string maDinhDanhId { get; set; }

        [Required]
        [StringLength(500)]
        public string barcodeImage { get; set; }

        public bool isDelete { get; set; }

        public virtual MaDinhDanhSanPham MaDinhDanhSanPham { get; set; }
    }
}
