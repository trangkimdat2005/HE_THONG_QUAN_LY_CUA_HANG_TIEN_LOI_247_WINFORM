namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.TrangThaiXuLy")]
    public partial class TrangThaiXuLy
    {
        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string donHangId { get; set; }

        [Required]
        [StringLength(20)]
        public string trangThai { get; set; }

        public DateTime ngayCapNhat { get; set; }

        public bool isDelete { get; set; }

        public virtual DonHangOnline DonHangOnline { get; set; }
    }
}
