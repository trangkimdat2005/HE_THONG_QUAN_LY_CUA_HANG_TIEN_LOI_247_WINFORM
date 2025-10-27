namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.DieuKienApDungDanhMuc")]
    public partial class DieuKienApDungDanhMuc
    {
        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string dieuKienId { get; set; }

        [Required]
        [StringLength(50)]
        public string danhMucId { get; set; }

        public bool isDelete { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }

        public virtual DieuKienApDung DieuKienApDung { get; set; }
    }
}
