using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO
{
    public class ProductDetailDto
    {
        public string Id { get; set; }             
        public string MaSP { get; set; }            
        public string TenSanPham { get; set; }      
        public string Ten => TenSanPham;            
        public string NhanHieu { get; set; }      
        public string DanhMuc { get; set; }        
        public string DonVi { get; set; }          
        public decimal GiaBan { get; set; }        
        public string TrangThai { get; set; }             

        public string SanPhamId { get; set; }    
        public string DonViId { get; set; }         
        public decimal HeSoQuyDoi { get; set; }     
    }
}
