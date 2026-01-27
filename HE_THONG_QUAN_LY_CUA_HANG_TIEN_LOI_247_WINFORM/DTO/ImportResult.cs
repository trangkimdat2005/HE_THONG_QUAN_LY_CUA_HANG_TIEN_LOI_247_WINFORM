using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO
{
    public class ImportResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<string> ErrorLogs { get; set; } = new List<string>();
    }
}
