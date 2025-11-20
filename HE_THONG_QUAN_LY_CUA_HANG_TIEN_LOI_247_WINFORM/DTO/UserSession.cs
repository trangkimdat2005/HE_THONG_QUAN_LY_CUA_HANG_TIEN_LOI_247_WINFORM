using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using System;
using System.Linq;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Utils
{
    /// <summary>
    /// Class singleton để lưu trữ thông tin người dùng đã đăng nhập
    /// </summary>
    public sealed class UserSession
    {
        private static UserSession instance = null;
        private static readonly object padlock = new object();

        // Thông tin user đăng nhập
        public string UserId { get; set; }
        public string Username { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; } // Chức vụ
        public string Address { get; set; }
        public string Role { get; set; }
        public byte[] Avatar { get; set; } // Lưu byte array của ảnh
        public DateTime LoginTime { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool Gender { get; set; } // true = Nam, false = Nữ
        public DateTime? EmploymentDate { get; set; }

        private UserSession()
        {
            IsLoggedIn = false;
        }

        public static UserSession Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new UserSession();
                    }
                    return instance;
                }
            }
        }

        /// <summary>
        /// Set thông tin user khi đăng nhập
        /// </summary>
        public void SetUserInfo(TaiKhoan account, NhanVien employee)
        {
            if (account != null && employee != null)
            {
                UserId = account.id;
                Username = account.tenDangNhap;
                EmployeeId = employee.id;
                EmployeeName = employee.hoTen;
                Email = employee.email;
                PhoneNumber = employee.soDienThoai;
                Position = employee.chucVu;
                Address = employee.diaChi;
                Gender = employee.gioiTinh;
                EmploymentDate = employee.ngayVaoLam;
                
                // Lấy role từ UserRoles (lấy role đầu tiên nếu có nhiều)
                var activeRole = account.UserRoles
                    .Where(ur => !ur.isDelete && 
                           (!ur.hieuLucDen.HasValue || ur.hieuLucDen.Value >= DateTime.Now))
                    .FirstOrDefault();
                
                Role = activeRole?.Role?.ten ?? "User";
                
                // Lấy avatar nếu có
                Avatar = employee.HinhAnh?.Anh;
                
                LoginTime = DateTime.Now;
                IsLoggedIn = true;
            }
        }

        /// <summary>
        /// Xóa thông tin user khi đăng xuất
        /// </summary>
        public void ClearSession()
        {
            UserId = null;
            Username = null;
            EmployeeId = null;
            EmployeeName = null;
            Email = null;
            PhoneNumber = null;
            Position = null;
            Address = null;
            Role = null;
            Avatar = null;
            LoginTime = DateTime.MinValue;
            IsLoggedIn = false;
            Gender = false;
            EmploymentDate = null;
        }

        /// <summary>
        /// Lấy tên hiển thị (dạng tắt - 2 chữ cái đầu)
        /// </summary>
        public string GetDisplayName()
        {
            if (string.IsNullOrEmpty(EmployeeName))
                return "U";
            
            // Lấy chữ cái đầu của họ và tên
            var names = EmployeeName.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (names.Length >= 2)
            {
                // Lấy chữ đầu của tên đệm và tên
                return names[names.Length - 2].Substring(0, 1).ToUpper() + 
                       names[names.Length - 1].Substring(0, 1).ToUpper();
            }
            else if (names.Length == 1)
            {
                // Nếu chỉ có 1 từ, lấy 2 ký tự đầu
                return names[0].Substring(0, Math.Min(2, names[0].Length)).ToUpper();
            }
            return "U";
        }

        /// <summary>
        /// Lấy giới tính dạng text
        /// </summary>
        public string GetGenderText()
        {
            return Gender ? "Nam" : "Nữ";
        }
    }
}