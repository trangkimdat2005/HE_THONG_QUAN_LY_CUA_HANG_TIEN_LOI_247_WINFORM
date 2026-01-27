using MiniExcelLibs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO
{
    public class NhanVienImportDto
    {
        [ExcelColumnName("Họ và tên")]
        public string HoTen { get; set; }

        [ExcelColumnName("Giới tính")]
        public string GioiTinhText { get; set; }

        [ExcelColumnName("Số điện thoại")]
        public string SoDienThoai { get; set; }

        [ExcelColumnName("Email")]
        public string Email { get; set; }

        [ExcelColumnName("Địa chỉ")]
        public string DiaChi { get; set; }

        [ExcelColumnName("Chức vụ")]
        public string ChucVu { get; set; }

        [ExcelColumnName("Lương cơ bản")]
        public decimal LuongCoBan { get; set; }
    }
}
