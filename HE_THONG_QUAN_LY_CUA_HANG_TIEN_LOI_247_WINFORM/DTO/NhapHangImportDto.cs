using System;
using MiniExcelLibs.Attributes;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO
{
    public class NhapHangImportDto
    {
        [ExcelColumnName("Mã quy cách")]
        public string SanPhamDonViId { get; set; } // Ví dụ: SPDV0001 (Mã vạch hoặc ID hệ thống)

        [ExcelColumnName("Số lượng")]
        public int SoLuong { get; set; }

        [ExcelColumnName("Đơn giá nhập")]
        public decimal DonGia { get; set; }

        [ExcelColumnName("Hạn sử dụng")]
        public DateTime? HanSuDung { get; set; } // Có thể để trống nếu hàng không có date
    }
}