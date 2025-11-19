namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.RolePermission")]
    public partial class RolePermission
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string roleId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string permissionId { get; set; }

        [StringLength(50)]
        public string id { get; set; }

        public bool isDelete { get; set; }

        public virtual Permission Permission { get; set; }

        public virtual Role Role { get; set; }
    }
}
