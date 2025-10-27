namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.TrangThaiGiaoHang")]
    public partial class TrangThaiGiaoHang
    {
        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string donGiaoHangId { get; set; }

        [Required]
        [StringLength(20)]
        public string trangThai { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ngayCapNhat { get; set; }

        [Required]
        [StringLength(500)]
        public string ghiChu { get; set; }

        public bool isDelete { get; set; }

        public virtual DonGiaoHang DonGiaoHang { get; set; }
    }
}
