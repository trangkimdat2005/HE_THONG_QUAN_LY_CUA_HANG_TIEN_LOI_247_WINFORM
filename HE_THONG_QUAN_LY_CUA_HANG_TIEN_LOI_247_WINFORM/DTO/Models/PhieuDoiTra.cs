namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.PhieuDoiTra")]
    public partial class PhieuDoiTra
    {
        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string hoaDonId { get; set; }

        [Required]
        [StringLength(50)]
        public string sanPhamDonViId { get; set; }

        public DateTime ngayDoiTra { get; set; }

        [Required]
        [StringLength(500)]
        public string lyDo { get; set; }

        public decimal soTienHoan { get; set; }

        [Required]
        [StringLength(50)]
        public string chinhSachId { get; set; }

        public bool isDelete { get; set; }

        public virtual ChinhSachHoanTra ChinhSachHoanTra { get; set; }

        public virtual HoaDon HoaDon { get; set; }
    }
}
