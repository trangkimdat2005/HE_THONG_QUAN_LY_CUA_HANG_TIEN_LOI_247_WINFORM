using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Main;
using System;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views
{
    public partial class frmNhapOTP : Form
    {
        private string _emailCanKhoiPhuc; // Email nhận từ Form 1
        private string _otpHeThong;       // OTP nhận từ Form 1

        // Sửa Constructor để nhận dữ liệu truyền sang
        public frmNhapOTP(string email, string otpServer)
        {
            InitializeComponent();
            _emailCanKhoiPhuc = email;
            _otpHeThong = otpServer;
        }

        private void frmNhapOTP_Load(object sender, EventArgs e)
        {
            // Đăng ký sự kiện trong Load
            btnXacNhan.Click += BtnXacNhan_Click;
            btnHuy.Click += BtnHuy_Click;

        }

        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            string otpNhap = txtOTP.Text.Trim();

            if (otpNhap == _otpHeThong)
            {
                MessageBox.Show("Xác thực chính xác!", "Thông báo");

                // Mở Form 3: Xác nhận mật khẩu (Truyền tiếp Email sang để biết đổi cho ai)
                frmXacNhanMK frm3 = new frmXacNhanMK(_emailCanKhoiPhuc);
                this.Hide();
                frm3.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Mã OTP sai. Vui lòng kiểm tra lại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}