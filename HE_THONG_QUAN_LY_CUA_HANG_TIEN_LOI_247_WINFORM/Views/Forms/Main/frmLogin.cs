using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Main
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            // Gắn sự kiện cho CheckBox ngay khi Form được khởi tạo
            this.btn_ShowPassword.CheckedChanged += new System.EventHandler(this.btn_ShowPassword_CheckedChanged);
        }

        private void btn_ShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            // Logic để chuyển đổi ký tự mật khẩu
            if (btn_ShowPassword.Checked)
            {
                matkhautxt.PasswordChar = '\0'; // Hiển thị ký tự thường (dùng null character)
            }
            else
            {
                matkhautxt.PasswordChar = '*'; // Ẩn ký tự (ký tự * ban đầu)
            }
        }

        // Thêm các sự kiện khác nếu cần (ví dụ: btn_login_Click, lbl_ForgotPassword_Click)
    }
}
