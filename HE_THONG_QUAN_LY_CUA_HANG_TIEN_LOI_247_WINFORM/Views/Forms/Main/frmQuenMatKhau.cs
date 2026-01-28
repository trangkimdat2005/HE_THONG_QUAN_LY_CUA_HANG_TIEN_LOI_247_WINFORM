using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Helper;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Main;
using System;
using System.Linq;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views
{
    public partial class frmQuenMatKhau : Form
    {
        private AppDbContext _context;

        public frmQuenMatKhau()
        {
            InitializeComponent();
            _context = new AppDbContext();
        }

        private void frmQuenMatKhau_Load(object sender, EventArgs e)
        {
            // Đăng ký sự kiện trong hàm Load theo yêu cầu
            btnGuiOTP.Click += BtnGuiOTP_Click;
            btnHuy.Click += BtnHuy_Click;

            txtEmail.Focus();
        }

        private void BtnGuiOTP_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập Email!", "Thông báo");
                return;
            }

            // 1. Kiểm tra Email có tồn tại
            var user = _context.TaiKhoans.FirstOrDefault(u => u.email == email && !u.isDelete);
            if (user == null)
            {
                MessageBox.Show("Email không tồn tại trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Tạo & Gửi OTP
            Random r = new Random();
            string otpCode = r.Next(100000, 999999).ToString();

            this.Cursor = Cursors.WaitCursor;
            bool sent = EmailHelper.SendMail(email, "MÃ OTP KHÔI PHỤC MẬT KHẨU", $"<h3>Mã OTP: <b style='color:red'>{otpCode}</b></h3>");
            this.Cursor = Cursors.Default;

            if (sent)
            {
                // 3. Mở Form Nhập OTP (Truyền Email và OTP sang form kia)
                frmNhapOTP frm2 = new frmNhapOTP(email, otpCode);
                this.Hide(); // Ẩn form hiện tại
                frm2.ShowDialog(); // Hiện form 2
                this.Close(); // Khi form 2 đóng xong thì đóng luôn form này
            }
            else
            {
                MessageBox.Show("Không gửi được Email. Vui lòng thử lại sau.");
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}