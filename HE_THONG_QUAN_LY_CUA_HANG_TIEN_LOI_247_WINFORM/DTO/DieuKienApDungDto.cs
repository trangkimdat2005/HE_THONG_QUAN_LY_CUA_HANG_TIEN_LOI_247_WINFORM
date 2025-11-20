using System;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO
{
    /// <summary>
    /// DTO cho điều kiện áp dụng khuyến mãi
    /// </summary>
    public class DieuKienApDungDto
    {

        public string Id { get; set; }

        public string ChuongTrinhId { get; set; }

        public string DieuKien { get; set; }

        public decimal GiaTriToiThieu { get; set; }

        public string GiamTheo { get; set; }

        public decimal GiaTriToiDa { get; set; }
    }
}
