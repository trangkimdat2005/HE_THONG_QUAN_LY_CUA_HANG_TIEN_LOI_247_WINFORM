namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("core.MaKhuyenMai")]
    public partial class MaKhuyenMai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MaKhuyenMai()
        {
            ChiTietHoaDonKhuyenMais = new HashSet<ChiTietHoaDonKhuyenMai>();
            HoaDonKhuyenMais = new HashSet<HoaDonKhuyenMai>();
        }

        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(50)]
        public string chuongTrinhId { get; set; }

        [Required]
        [StringLength(100)]
        public string code { get; set; }

        public decimal giaTri { get; set; }

        public int soLanSuDung { get; set; }

        [Required]
        [StringLength(20)]
        public string trangThai { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngayBatDau { get; set; }

        [Column(TypeName = "date")]
        public DateTime ngayKetThuc { get; set; }

        public bool isDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDonKhuyenMai> ChiTietHoaDonKhuyenMais { get; set; }

        public virtual ChuongTrinhKhuyenMai ChuongTrinhKhuyenMai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonKhuyenMai> HoaDonKhuyenMais { get; set; }
    }
}
