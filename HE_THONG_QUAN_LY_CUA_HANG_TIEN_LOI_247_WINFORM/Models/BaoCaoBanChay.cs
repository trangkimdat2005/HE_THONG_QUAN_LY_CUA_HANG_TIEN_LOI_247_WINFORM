namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.BaoCaoBanChay")]
    public partial class BaoCaoBanChay
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string baoCaoId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string sanPhamId { get; set; }

        public int soLuongBan { get; set; }

        [StringLength(50)]
        public string id { get; set; }

        public bool isDelete { get; set; }

        public virtual BaoCao BaoCao { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
