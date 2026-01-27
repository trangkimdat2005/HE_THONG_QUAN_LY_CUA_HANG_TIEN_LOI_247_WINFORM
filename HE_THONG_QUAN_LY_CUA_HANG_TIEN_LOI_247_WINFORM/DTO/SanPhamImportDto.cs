using MiniExcelLibs.Attributes;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO
{
    public class SanPhamImportDto
    {
        [ExcelColumnName("Tên sản phẩm")]
        public string TenSanPham { get; set; }

        [ExcelColumnName("Mã nhãn hiệu")] // Người dùng nhập NH0001, NH0002...
        public string NhanHieuId { get; set; }

        [ExcelColumnName("Mô tả")]
        public string MoTa { get; set; }
    }
}