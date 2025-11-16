namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.GiaoDichThanhToan")]
    public partial class GiaoDichThanhToan
    {
        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string hoaDonId { get; set; }

        public decimal soTien { get; set; }

        public DateTime ngayGD { get; set; }

        [Required]
        [StringLength(50)]
        public string kenhThanhToanId { get; set; }

        [StringLength(500)]
        public string moTa { get; set; }

        public bool isDelete { get; set; }

        public virtual HoaDon HoaDon { get; set; }

        public virtual KenhThanhToan KenhThanhToan { get; set; }
    }
}
