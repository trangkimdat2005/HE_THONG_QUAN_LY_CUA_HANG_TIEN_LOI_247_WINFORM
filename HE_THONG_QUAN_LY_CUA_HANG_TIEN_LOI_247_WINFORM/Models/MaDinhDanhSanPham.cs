namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.MaDinhDanhSanPham")]
    public partial class MaDinhDanhSanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MaDinhDanhSanPham()
        {
            Barcodes = new HashSet<Barcode>();
            QRCodes = new HashSet<QRCode>();
            TemNhans = new HashSet<TemNhan>();
        }

        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string sanPhamDonViId { get; set; }

        [Required]
        [StringLength(10)]
        public string loaiMa { get; set; }

        [Required]
        [StringLength(200)]
        public string maCode { get; set; }

        [Required]
        [StringLength(500)]
        public string duongDan { get; set; }

        public bool isDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Barcode> Barcodes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QRCode> QRCodes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TemNhan> TemNhans { get; set; }
    }
}
