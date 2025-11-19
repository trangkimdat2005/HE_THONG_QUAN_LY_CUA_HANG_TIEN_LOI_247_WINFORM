namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.ChiTietPhieuNhap")]
    public partial class ChiTietPhieuNhap
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string phieuNhapId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string sanPhamDonViId { get; set; }

        public int soLuong { get; set; }

        public decimal donGia { get; set; }

        public bool isDelete { get; set; }

        public decimal tongTien { get; set; }

        public DateTime hanSuDung { get; set; }

        public virtual PhieuNhap PhieuNhap { get; set; }
    }
}
