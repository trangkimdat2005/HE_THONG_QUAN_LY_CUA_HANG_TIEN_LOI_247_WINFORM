using System;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Config
{
    /// <summary>
    /// Cấu hình thông tin thanh toán và ngân hàng
    /// </summary>
    public static class PaymentConfig
    {
        // ========================================
        // THÔNG TIN NGÂN HÀNG - VUI LÒNG CẬP NHẬT
        // ========================================
        
        /// <summary>
        /// Mã ngân hàng theo chuẩn VietQR
        /// Danh sách: https://vietqr.net/danh-sach-ma-ngan-hang
        /// </summary>
        public static string BankId = "970436"; // VCB //mã bin của VCB
        
        /// <summary>
        /// Tên ngân hàng hiển thị
        /// </summary>
        public static string BankName = "NHTMCP Ngoại Thương Việt Nam (Vietcombank)";
        
        /// <summary>
        /// Số tài khoản nhận tiền
        /// </summary>
        public static string AccountNumber = "1040302953";
        
        /// <summary>
        /// Tên chủ tài khoản
        /// </summary>
        public static string AccountName = "CONG TY TNHH CUA HANG TIEN LOI 247";
        
        /// <summary>
        /// Template QR Code (compact2, print, qr_only)
        /// </summary>
        public static string QRTemplate = "compact2";
        
        // ========================================
        // DANH SÁCH MÃ NGÂN HÀNG PHỔ BIẾN
        // ========================================
        /*
         * 970422 - MB Bank (Ngân hàng TMCP Quân đội)
         * 970415 - Vietinbank (Ngân hàng TMCP Công thương Việt Nam)
         * 970436 - Vietcombank (Ngân hàng TMCP Ngoại thương Việt Nam)
         * 970405 - Agribank (Ngân hàng Nông nghiệp và Phát triển Nông thôn)
         * 970416 - ACB (Ngân hàng TMCP Á Châu)
         * 970432 - VPBank (Ngân hàng TMCP Việt Nam Thịnh Vượng)
         * 970418 - BIDV (Ngân hàng TMCP Đầu tư và Phát triển Việt Nam)
         * 970407 - Techcombank (Ngân hàng TMCP Kỹ thương Việt Nam)
         * 970423 - TPBank (Ngân hàng TMCP Tiên Phong)
         * 970403 - Sacombank (Ngân hàng TMCP Sài Gòn Thương Tín)
         * 970454 - VietCapitalBank
         * 970429 - SCB (Ngân hàng TMCP Sài Gòn)
         * 970441 - VIB (Ngân hàng TMCP Quốc tế Việt Nam)
         * 970448 - OCB (Ngân hàng TMCP Phương Đông)
         * 970414 - Oceanbank
         * 970406 - DongA Bank
         * 970433 - VietBank
         * 970431 - Eximbank
         * 970426 - MSB (Ngân hàng TMCP Hàng Hải)
         * 970438 - BaoViet Bank
         */
        
        /// <summary>
        /// Tạo URL cho VietQR API
        /// </summary>
        public static string GenerateQRUrl(decimal amount, string description)
        {
            // Format: https://img.vietqr.io/image/{BANK_ID}-{ACCOUNT_NO}-{TEMPLATE}.jpg?amount={AMOUNT}&addInfo={DESCRIPTION}&accountName={NAME}
            return $"https://img.vietqr.io/image/{BankId}-{AccountNumber}-{QRTemplate}.jpg" +
                   $"?amount={amount}" +
                   $"&addInfo={Uri.EscapeDataString(description)}" +
                   $"&accountName={Uri.EscapeDataString(AccountName)}";
        }
    }
}
