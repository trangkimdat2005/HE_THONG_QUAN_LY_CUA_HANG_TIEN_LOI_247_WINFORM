using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Main
{
    public partial class frmLogin : Form
    {
        private AuthenticationService _authService;

        public frmLogin()
        {
            InitializeComponent();
            _authService = new AuthenticationService();
            
            // Gắn sự kiện
            this.btn_ShowPassword.CheckedChanged += new System.EventHandler(this.btn_ShowPassword_CheckedChanged);
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            this.lbl_ForgotPassword.Click += new System.EventHandler(this.lbl_ForgotPassword_Click);
            
            // Cho phép nhấn Enter để login
            this.taikhoantxt.KeyPress += new KeyPressEventHandler(this.txt_KeyPress);
            this.matkhautxt.KeyPress += new KeyPressEventHandler(this.txt_KeyPress);
            
            // Set focus vào textbox username
            this.Load += (s, e) => taikhoantxt.Focus();
        }

        private void btn_ShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            // Logic để chuyển đổi ký tự mật khẩu
            if (btn_ShowPassword.Checked)
            {
                matkhautxt.PasswordChar = '\0'; // Hiển thị ký tự thường
            }
            else
            {
                matkhautxt.PasswordChar = '*'; // Ẩn ký tự
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            PerformLogin();
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Nhấn Enter để login
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                PerformLogin();
            }
        }

        private void PerformLogin()
        {
            string username = taikhoantxt.Text.Trim();
            string password = matkhautxt.Text.Trim();

            // Disable button để tránh click nhiều lần
            btn_login.Enabled = false;
            btn_login.Text = "Đang đăng nhập...";
            this.Cursor = Cursors.WaitCursor;

            try
            {
                // Gọi service đăng nhập
                var result = _authService.Login(username, password);
                bool isSuccess = result.Item1;
                string message = result.Item2;
                string accountType = result.Item3;

                if (isSuccess)
                {
                    // Đăng nhập thành công - đóng form login ngay
                    // Không hiển thị thông báo, để frmMain xử lý welcome message nếu cần
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // Hiển thị lỗi
                    MessageBox.Show(
                        message,
                        "❌ Đăng nhập thất bại",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    // Clear password field và focus lại
                    matkhautxt.Clear();
                    matkhautxt.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi hệ thống: {ex.Message}\n\nVui lòng liên hệ quản trị viên!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                // Enable lại button
                btn_login.Enabled = true;
                btn_login.Text = "ĐĂNG NHẬP";
                this.Cursor = Cursors.Default;
            }
        }

        private void lbl_ForgotPassword_Click(object sender, EventArgs e)
        {
            // TODO: Implement forgot password functionality
            MessageBox.Show(
                "Vui lòng liên hệ quản trị viên để được hỗ trợ reset mật khẩu!\n\n" +
                "Hotline: 1900-xxxx\n" +
                "Email: support@cstore.com",
                "Quên mật khẩu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _authService?.Dispose();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btn_login_Click_1(object sender, EventArgs e)
        {

        }
    }
}
