using MiniExcelLibs.Attributes;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO
{
    public class SanPhamDonViImportDto
    {
        [ExcelColumnName("Mã sản phẩm")]
        public string SanPhamId { get; set; } // Ví dụ: SP0001

        [ExcelColumnName("Mã đơn vị")]
        public string DonViId { get; set; }   // Ví dụ: DV0001 (Cái), DV0002 (Thùng)

        [ExcelColumnName("Hệ số quy đổi")]
        public decimal HeSoQuyDoi { get; set; } // Ví dụ: 1 (Cái), 24 (Thùng)

        [ExcelColumnName("Giá bán")]
        public decimal GiaBan { get; set; }

        [ExcelColumnName("Trạng thái")]
        public string TrangThai { get; set; } // Đang bán / Ngừng bán
    }
}