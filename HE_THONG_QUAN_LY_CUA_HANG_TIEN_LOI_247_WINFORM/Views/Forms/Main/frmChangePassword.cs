using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Main
{
    public partial class frmChangePassword : Form
    {
        private readonly AuthenticationService _authService;
        private readonly AppDbContext _context;

        public frmChangePassword()
        {
            InitializeComponent();
            _authService = new AuthenticationService();
            _context = new AppDbContext();
            
            // Gắn sự kiện
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            this.Load += new System.EventHandler(this.frmChangePasswork_Load);
            
            // Cho phép nhấn Enter để đổi mật khẩu
            this.txtOldPassword.KeyPress += new KeyPressEventHandler(this.txt_KeyPress);
            this.txtNewPassword.KeyPress += new KeyPressEventHandler(this.txt_KeyPress);
            this.txtConfirmPassword.KeyPress += new KeyPressEventHandler(this.txt_KeyPress);
        }

        private void frmChangePasswork_Load(object sender, EventArgs e)
        {
            // Kiểm tra người dùng đã đăng nhập chưa
            if (!UserSession.Instance.IsLoggedIn)
            {
                MessageBox.Show(
                    "Bạn cần đăng nhập để thực hiện chức năng này!",
                    "Lỗi xác thực",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                this.Close();
                return;
            }

            // Set focus vào textbox mật khẩu cũ
            txtOldPassword.Focus();
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Nhấn Enter để đổi mật khẩu
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                PerformChangePassword();
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            PerformChangePassword();
        }

        private void PerformChangePassword()
        {
            // Lấy giá trị từ textbox
            string oldPassword = txtOldPassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            // Validate input
            if (!ValidateInput(oldPassword, newPassword, confirmPassword))
            {
                return;
            }

            // Disable button để tránh click nhiều lần
            btnChangePassword.Enabled = false;
            btnChangePassword.Text = "Đang xử lý...";
            this.Cursor = Cursors.WaitCursor;

            try
            {
                // Lấy thông tin tài khoản hiện tại
                string userId = UserSession.Instance.UserId;
                var account = _context.TaiKhoans.FirstOrDefault(t => t.id == userId);

                if (account == null)
                {
                    MessageBox.Show(
                        "Không tìm thấy tài khoản!\n\nVui lòng đăng nhập lại.",
                        "❌ Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }

                // Verify mật khẩu cũ
                string oldPasswordHash = AuthenticationService.HashPassword(oldPassword);
                if (account.matKhauHash != oldPasswordHash && account.matKhauHash != oldPassword)
                {
                    MessageBox.Show(
                        "Mật khẩu cũ không chính xác!\n\nVui lòng kiểm tra lại.",
                        "❌ Lỗi xác thực",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    
                    // Clear và focus lại vào mật khẩu cũ
                    txtOldPassword.Clear();
                    txtOldPassword.Focus();
                    return;
                }

                // Hash mật khẩu mới
                string newPasswordHash = AuthenticationService.HashPassword(newPassword);

                // Kiểm tra mật khẩu mới không được trùng với mật khẩu cũ
                if (oldPasswordHash == newPasswordHash)
                {
                    MessageBox.Show(
                        "Mật khẩu mới không được trùng với mật khẩu cũ!\n\nVui lòng chọn mật khẩu khác.",
                        "⚠️ Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    
                    txtNewPassword.Clear();
                    txtConfirmPassword.Clear();
                    txtNewPassword.Focus();
                    return;
                }

                // Cập nhật mật khẩu mới
                account.matKhauHash = newPasswordHash;
                _context.SaveChanges();

                // Log hoạt động
                _authService.LogActivity(userId, "Đổi mật khẩu thành công");

                // Thông báo thành công
                var result = MessageBox.Show(
                    "✅ Đổi mật khẩu thành công!\n\n" +
                    "Mật khẩu của bạn đã được cập nhật.\n" +
                    "Bạn có muốn đăng xuất để đăng nhập lại không?",
                    "Thành công",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information
                );

                if (result == DialogResult.Yes)
                {
                    // Đăng xuất và đóng form
                    this.DialogResult = DialogResult.OK; // Signal to parent form to logout
                    this.Close();
                }
                else
                {
                    // Chỉ đóng form đổi mật khẩu
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ Lỗi hệ thống khi đổi mật khẩu!\n\n{ex.Message}\n\nVui lòng liên hệ quản trị viên.",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                // Enable lại button
                btnChangePassword.Enabled = true;
                btnChangePassword.Text = "Đổi Mật Khẩu";
                this.Cursor = Cursors.Default;
            }
        }

        private bool ValidateInput(string oldPassword, string newPassword, string confirmPassword)
        {
            // Kiểm tra các trường không được để trống
            if (string.IsNullOrWhiteSpace(oldPassword))
            {
                MessageBox.Show(
                    "Vui lòng nhập mật khẩu cũ!",
                    "⚠️ Thiếu thông tin",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtOldPassword.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show(
                    "Vui lòng nhập mật khẩu mới!",
                    "⚠️ Thiếu thông tin",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtNewPassword.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show(
                    "Vui lòng nhập lại mật khẩu mới!",
                    "⚠️ Thiếu thông tin",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtConfirmPassword.Focus();
                return false;
            }

            // Kiểm tra độ dài mật khẩu (tối thiểu 6 ký tự)
            if (newPassword.Length < 6)
            {
                MessageBox.Show(
                    "Mật khẩu mới phải có ít nhất 6 ký tự!\n\nVui lòng chọn mật khẩu dài hơn.",
                    "⚠️ Mật khẩu không hợp lệ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtNewPassword.Focus();
                return false;
            }

            // Kiểm tra mật khẩu mới và xác nhận có khớp không
            if (newPassword != confirmPassword)
            {
                MessageBox.Show(
                    "Mật khẩu mới và xác nhận mật khẩu không khớp!\n\nVui lòng kiểm tra lại.",
                    "❌ Mật khẩu không khớp",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                txtConfirmPassword.Clear();
                txtConfirmPassword.Focus();
                return false;
            }

            // Kiểm tra mật khẩu mới có chứa khoảng trắng
            if (newPassword.Contains(" "))
            {
                MessageBox.Show(
                    "Mật khẩu không được chứa khoảng trắng!\n\nVui lòng chọn mật khẩu khác.",
                    "⚠️ Mật khẩu không hợp lệ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                txtNewPassword.Focus();
                return false;
            }

           

            return true;
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _authService?.Dispose();
            _context?.Dispose();
        }
    }
}
