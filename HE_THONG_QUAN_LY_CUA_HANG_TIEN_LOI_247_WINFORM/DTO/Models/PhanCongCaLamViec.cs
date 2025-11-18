namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.PhanCongCaLamViec")]
    public partial class PhanCongCaLamViec
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string nhanVienId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string caLamViecId { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime ngay { get; set; }

        [StringLength(50)]
        public string id { get; set; }

        public bool isDelete { get; set; }

        public virtual CaLamViec CaLamViec { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
