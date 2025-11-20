using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO
{
    public class ProductDetailDto
    {
        public string Id { get; set; }              // Mã SPDV
        public string MaSP { get; set; }            // Mã SP
        public string TenSanPham { get; set; }      // Tên SP
        public string Ten => TenSanPham;            // Alias cho tương thích ngược
        public string NhanHieu { get; set; }        // Nhãn hiệu
        public string DanhMuc { get; set; }         // Danh mục
        public string DonVi { get; set; }           // Đơn vị
        public decimal GiaBan { get; set; }         // Giá bán
        public string TrangThai { get; set; }       // Trạng thái
        public string MoTa { get; set; }            // Mô tả
        
        // Thêm các thuộc tính liên quan
        public string SanPhamId { get; set; }       // ID Sản phẩm
        public string DonViId { get; set; }         // ID Đơn vị
        public decimal HeSoQuyDoi { get; set; }     // Hệ số quy đổi
    }
}
