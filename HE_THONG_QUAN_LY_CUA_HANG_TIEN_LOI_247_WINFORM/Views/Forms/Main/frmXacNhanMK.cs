using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views
{
    public partial class frmXacNhanMK : Form
    {
        private AppDbContext _context;
        private string _targetEmail; // Email cần đổi pass

        // Constructor nhận Email
        public frmXacNhanMK(string email)
        {
            InitializeComponent();
            _context = new AppDbContext();
            _targetEmail = email;
        }

        private void frmXacNhanMK_Load(object sender, EventArgs e)
        {
            // Đăng ký sự kiện
            btnLuu.Click += BtnLuu_Click;
            btnHuy.Click += BtnHuy_Click;

            // Cấu hình ẩn mật khẩu
            txtMatKhauMoi.UseSystemPasswordChar = true;
            txtXacNhanMatKhau.UseSystemPasswordChar = true;

            txtMatKhauMoi.Focus();
        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            string pass1 = txtMatKhauMoi.Text;
            string pass2 = txtXacNhanMatKhau.Text;

            if (pass1.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải từ 6 ký tự trở lên!");
                return;
            }

            if (pass1 != pass2)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!");
                return;
            }

            try
            {
                var user = _context.TaiKhoans.FirstOrDefault(u => u.email == _targetEmail);
                if (user != null)
                {
                    // Update mật khẩu
                    user.matKhauHash = HashPassword(pass1);
                    _context.SaveChanges();

                    MessageBox.Show("Đổi mật khẩu thành công! Vui lòng đăng nhập lại.", "Hoàn tất");

                    // Đóng toàn bộ chuỗi form
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void BtnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Hàm Hash MD5/SHA256 (Copy từ bài trước)
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}