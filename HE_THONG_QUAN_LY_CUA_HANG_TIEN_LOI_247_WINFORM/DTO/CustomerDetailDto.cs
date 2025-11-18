using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO
{
    /// <summary>
    /// DTO ch?a thông tin chi ti?t khách hàng
    /// </summary>
    public class CustomerDetailDto
    {
        /// <summary>
        /// Thông tin khách hàng
        /// </summary>
        public KhachHang Customer { get; set; }

        /// <summary>
        /// T?ng ti?n ?ã mua
        /// </summary>
        public decimal TotalPurchase { get; set; }

        /// <summary>
        /// S? l?n mua hàng
        /// </summary>
        public int PurchaseCount { get; set; }

        /// <summary>
        /// Th? thành viên
        /// </summary>
        public TheThanhVien MemberCard { get; set; }

        /// <summary>
        /// L?ch s? mua hàng
        /// </summary>
        public System.Collections.Generic.List<LichSuMuaHang> PurchaseHistory { get; set; }
    }
}
