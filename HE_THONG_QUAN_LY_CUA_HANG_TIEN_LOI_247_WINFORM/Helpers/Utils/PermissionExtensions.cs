using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Utils
{
    /// <summary>
    /// Extension methods cho phân quyền
    /// </summary>
    public static class PermissionExtensions
    {
        private static AuthenticationService _authService = new AuthenticationService();

        /// <summary>
        /// Kiểm tra user có permission và hiển thị thông báo nếu không có quyền
        /// </summary>
        public static bool CheckPermission(string permissionCode, bool showMessage = true)
        {
            bool hasPermission = _authService.HasPermission(permissionCode);
            
            if (!hasPermission && showMessage)
            {
                MessageBox.Show(
                    "Bạn không có quyền thực hiện chức năng này!\n\n" +
                    "Vui lòng liên hệ quản trị viên để được cấp quyền.",
                    "⚠️ Không có quyền truy cập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            
            return hasPermission;
        }

        /// <summary>
        /// Kiểm tra user có role cụ thể
        /// </summary>
        public static bool CheckRole(string roleCode, bool showMessage = true)
        {
            bool hasRole = _authService.HasRole(roleCode);
            
            if (!hasRole && showMessage)
            {
                MessageBox.Show(
                    "Bạn không có vai trò phù hợp để truy cập chức năng này!",
                    "⚠️ Không có quyền truy cập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            
            return hasRole;
        }

        /// <summary>
        /// Disable control nếu user không có permission
        /// </summary>
        public static void SetPermission(this Control control, string permissionCode)
        {
            control.Enabled = _authService.HasPermission(permissionCode);
            
            if (!control.Enabled && control is Button)
            {
                control.Cursor = Cursors.No;
                control.BackColor = System.Drawing.Color.LightGray;
            }
        }

        /// <summary>
        /// Hide control nếu user không có permission
        /// </summary>
        public static void HideIfNoPermission(this Control control, string permissionCode)
        {
            control.Visible = _authService.HasPermission(permissionCode);
        }

        /// <summary>
        /// Log activity của user
        /// </summary>
        public static void LogActivity(string activity)
        {
            var session = UserSession.Instance;
            if (session.IsLoggedIn)
            {
                _authService.LogActivity(session.UserId, activity);
            }
        }

        /// <summary>
        /// Kiểm tra user có bất kỳ permission nào trong list
        /// </summary>
        public static bool HasAnyPermission(params string[] permissionCodes)
        {
            foreach (var code in permissionCodes)
            {
                if (_authService.HasPermission(code))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Kiểm tra user có tất cả permissions trong list
        /// </summary>
        public static bool HasAllPermissions(params string[] permissionCodes)
        {
            foreach (var code in permissionCodes)
            {
                if (!_authService.HasPermission(code))
                    return false;
            }
            return true;
        }
    }
}
