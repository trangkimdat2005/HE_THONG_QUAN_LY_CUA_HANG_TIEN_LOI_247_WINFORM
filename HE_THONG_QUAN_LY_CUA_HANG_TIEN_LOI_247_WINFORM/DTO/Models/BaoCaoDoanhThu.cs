namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.BaoCaoDoanhThu")]
    public partial class BaoCaoDoanhThu
    {
        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string baoCaoId { get; set; }

        public decimal tongDoanhThu { get; set; }

        [Required]
        [StringLength(100)]
        public string kyBaoCao { get; set; }

        public bool isDelete { get; set; }

        public virtual BaoCao BaoCao { get; set; }
    }
}
