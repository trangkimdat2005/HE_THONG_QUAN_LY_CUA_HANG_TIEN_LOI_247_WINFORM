namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("__EFMigrationsHistory")]
    public partial class C__EFMigrationsHistory
    {
        [Key]
        [StringLength(150)]
        public string MigrationId { get; set; }

        [Required]
        [StringLength(32)]
        public string ProductVersion { get; set; }
    }
}
