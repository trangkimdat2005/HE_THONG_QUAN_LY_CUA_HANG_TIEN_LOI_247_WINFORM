using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Utils;
using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services
{
    /// <summary>
    /// Service xử lý authentication và authorization
    /// </summary>
    public class AuthenticationService
    {
        private readonly AppDbContext _context;

        public AuthenticationService()
        {
            _context = new AppDbContext();
        }

        /// <summary>
        /// Đăng nhập với username và password
        /// </summary>
        /// <returns>Tuple: (isSuccess, message, accountType)</returns>
        public Tuple<bool, string, string> Login(string username, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    return Tuple.Create(false, "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "");
                }

                // Tìm tài khoản
                var account = _context.TaiKhoans
                    .Include(t => t.TaiKhoanNhanViens.Select(tn => tn.NhanVien))
                    .Include(t => t.TaiKhoanNhanViens.Select(tn => tn.NhanVien.HinhAnh))
                    .Include(t => t.TaiKhoanKhachHangs.Select(tk => tk.KhachHang))
                    .Include(t => t.UserRoles.Select(ur => ur.Role))
                    .Include(t => t.UserRoles.Select(ur => ur.Role.RolePermissions.Select(rp => rp.Permission)))
                    .FirstOrDefault(t => t.tenDangNhap == username && !t.isDelete);

                if (account == null)
                {
                    return Tuple.Create(false, "Tên đăng nhập không tồn tại!", "");
                }

                // Kiểm tra trạng thái tài khoản
                // Chỉ chấp nhận: "Hoạt động", "Active", "hoạt động", "active"
                string trangThai = account.trangThai?.Trim().ToLower() ?? "";
                
                if (trangThai != "hoạt động" && trangThai != "active")
                {
                    // Tài khoản bị khóa hoặc không hoạt động
                    string statusMessage = trangThai == "không hoạt động" || trangThai == "đã khóa" 
                        ? "Tài khoản đã bị khóa" 
                        : $"Tài khoản đang ở trạng thái: {account.trangThai}";
                    
                    return Tuple.Create(false, 
                        $"❌ {statusMessage}\n\n" +
                        "Vui lòng liên hệ quản trị viên để được hỗ trợ!", 
                        "");
                }

                // Verify password
                if (!VerifyPassword(password, account.matKhauHash))
                {
                    // Log failed attempt
                    LogLoginAttempt(account.id, false);
                    return Tuple.Create(false, "Mật khẩu không chính xác!", "");
                }

                // Kiểm tra loại tài khoản (Nhân viên hay Khách hàng)
                string accountType = "";
                
                var employeeAccount = account.TaiKhoanNhanViens.FirstOrDefault(tn => !tn.isDelete);
                if (employeeAccount != null && employeeAccount.NhanVien != null)
                {
                    var employee = employeeAccount.NhanVien;
                    
                    // Kiểm tra trạng thái nhân viên
                    string empStatus = employee.trangThai?.Trim().ToLower() ?? "";
                    
                    if (empStatus != "đang làm việc" && empStatus != "active" && empStatus != "hoạt động")
                    {
                        string empStatusMessage = empStatus == "nghỉ việc" 
                            ? "Nhân viên đã nghỉ việc" 
                            : $"Nhân viên đang ở trạng thái: {employee.trangThai}";
                        
                        return Tuple.Create(false, 
                            $"❌ {empStatusMessage}\n\n" +
                            "Không thể đăng nhập vào hệ thống!", 
                            "");
                    }

                    if (employee.isDelete)
                    {
                        return Tuple.Create(false, "❌ Nhân viên đã bị xóa khỏi hệ thống!", "");
                    }

                    // Lưu thông tin nhân viên vào session
                    UserSession.Instance.SetUserInfo(account, employee);
                    accountType = "Employee";

                    // Log successful login
                    LogLoginAttempt(account.id, true);
                    
                    // Return success without message - let frmMain handle welcome
                    return Tuple.Create(true, "", accountType);
                }

                var customerAccount = account.TaiKhoanKhachHangs.FirstOrDefault(tk => !tk.isDelete);
                if (customerAccount != null && customerAccount.KhachHang != null)
                {
                    // Khách hàng không được phép đăng nhập vào hệ thống quản lý
                    return Tuple.Create(false, 
                        "❌ Tài khoản khách hàng không có quyền truy cập hệ thống quản lý!\n\n" +
                        "Vui lòng sử dụng ứng dụng dành cho khách hàng.", 
                        "");
                }

                return Tuple.Create(false, "❌ Tài khoản chưa được liên kết với nhân viên!", "");
            }
            catch (Exception ex)
            {
                return Tuple.Create(false, $"❌ Lỗi hệ thống: {ex.Message}\n\nVui lòng liên hệ IT!", "");
            }
        }

        /// <summary>
        /// Verify password với hash stored trong database
        /// </summary>
        private bool VerifyPassword(string password, string storedHash)
        {
            // TODO: Implement proper password hashing (BCrypt recommended)
            // Hiện tại tạm thời check trực tiếp hoặc hash đơn giản
            
            // Nếu password đã được hash bằng SHA256
            string hashedPassword = HashPassword(password);
            
            // So sánh hash hoặc plain text (tùy cách bạn lưu trong DB)
            return storedHash == hashedPassword || storedHash == password;
        }

        /// <summary>
        /// Hash password bằng SHA256
        /// </summary>
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Chuyển đổi kết quả băm thành chuỗi Base64
                return Convert.ToBase64String(bytes);
            }
        }

        /// <summary>
        /// Kiểm tra user có permission cụ thể không
        /// </summary>
        public bool HasPermission(string permissionCode)
        {
            var session = UserSession.Instance;
            if (!session.IsLoggedIn)
                return false;

            try
            {
                var account = _context.TaiKhoans
                    .Include(t => t.UserRoles.Select(ur => ur.Role.RolePermissions.Select(rp => rp.Permission)))
                    .FirstOrDefault(t => t.id == session.UserId);

                if (account == null)
                    return false;

                // Lấy tất cả permissions của user qua roles
                var permissions = account.UserRoles
                    .Where(ur => !ur.isDelete && 
                           (!ur.hieuLucDen.HasValue || ur.hieuLucDen.Value >= DateTime.Now) &&
                           (!ur.hieuLucTu.HasValue || ur.hieuLucTu.Value <= DateTime.Now))
                    .SelectMany(ur => ur.Role.RolePermissions
                        .Where(rp => !rp.isDelete)
                        .Select(rp => rp.Permission))
                    .Where(p => !p.isDelete)
                    .ToList();

                return permissions.Any(p => p.code == permissionCode);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra user có role cụ thể không
        /// </summary>
        public bool HasRole(string roleCode)
        {
            var session = UserSession.Instance;
            if (!session.IsLoggedIn)
                return false;

            try
            {
                var account = _context.TaiKhoans
                    .Include(t => t.UserRoles.Select(ur => ur.Role))
                    .FirstOrDefault(t => t.id == session.UserId);

                if (account == null)
                    return false;

                return account.UserRoles
                    .Where(ur => !ur.isDelete &&
                           (!ur.hieuLucDen.HasValue || ur.hieuLucDen.Value >= DateTime.Now) &&
                           (!ur.hieuLucTu.HasValue || ur.hieuLucTu.Value <= DateTime.Now))
                    .Any(ur => ur.Role.code == roleCode && !ur.Role.isDelete);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Lấy tất cả permissions của user hiện tại
        /// </summary>
        public string[] GetUserPermissions()
        {
            var session = UserSession.Instance;
            if (!session.IsLoggedIn)
                return new string[0];

            try
            {
                var account = _context.TaiKhoans
                    .Include(t => t.UserRoles.Select(ur => ur.Role.RolePermissions.Select(rp => rp.Permission)))
                    .FirstOrDefault(t => t.id == session.UserId);

                if (account == null)
                    return new string[0];

                return account.UserRoles
                    .Where(ur => !ur.isDelete &&
                           (!ur.hieuLucDen.HasValue || ur.hieuLucDen.Value >= DateTime.Now) &&
                           (!ur.hieuLucTu.HasValue || ur.hieuLucTu.Value <= DateTime.Now))
                    .SelectMany(ur => ur.Role.RolePermissions
                        .Where(rp => !rp.isDelete)
                        .Select(rp => rp.Permission))
                    .Where(p => !p.isDelete)
                    .Select(p => p.code)
                    .Distinct()
                    .ToArray();
            }
            catch
            {
                return new string[0];
            }
        }

        /// <summary>
        /// Log login attempt vào NhatKyHoatDong
        /// </summary>
        private void LogLoginAttempt(string accountId, bool isSuccess)
        {
            try
            {
                var log = new NhatKyHoatDong
                {
                    id = Guid.NewGuid().ToString(),
                    taiKhoanId = accountId,
                    hanhDong = isSuccess ? "Đăng nhập thành công" : "Đăng nhập thất bại",
                    thoiGian = DateTime.Now,
                    isDelete = false
                };

                _context.NhatKyHoatDongs.Add(log);
                _context.SaveChanges();
            }
            catch
            {
                // Ignore logging errors
            }
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        public void Logout()
        {
            var session = UserSession.Instance;
            if (session.IsLoggedIn)
            {
                try
                {
                    LogActivity(session.UserId, "Đăng xuất");
                }
                catch { }
                
                session.ClearSession();
            }
        }

        /// <summary>
        /// Log hoạt động của user
        /// </summary>
        public void LogActivity(string accountId, string activity)
        {
            try
            {
                var log = new NhatKyHoatDong
                {
                    id = Guid.NewGuid().ToString(),
                    taiKhoanId = accountId,
                    hanhDong = activity,
                    thoiGian = DateTime.Now,
                    isDelete = false
                };

                _context.NhatKyHoatDongs.Add(log);
                _context.SaveChanges();
            }
            catch
            {
                // Ignore logging errors
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
