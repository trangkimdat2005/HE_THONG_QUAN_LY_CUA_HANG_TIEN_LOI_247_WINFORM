using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Main
{
    public partial class frmMain : Form
    {
        private Form currentChildForm;

        public frmMain()
        {
            InitializeComponent();
            
            // Phóng to cửa sổ khi khởi động (vẫn có title bar và nút minimize/maximize/close)
            this.WindowState = FormWindowState.Maximized;
            // Giữ nguyên FormBorderStyle để có title bar
            // this.FormBorderStyle = FormBorderStyle.Sizable; // Mặc định đã có sẵn
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Load Dashboard mặc định khi mở chương trình
            LoadFormIntoPanel(new frmDashboard());
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_dashboard_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu Dashboard đã được load, không load lại
            if (currentChildForm != null && currentChildForm is frmDashboard)
            {
                // Nếu đang hiển thị Dashboard, không làm gì cả
                return;
            }
            
            LoadFormIntoPanel(new frmDashboard());
        }

        private void LoadFormIntoPanel(Form childForm)
        {
            // Kiểm tra nếu form mới trùng với form hiện tại
            if (currentChildForm != null && currentChildForm.GetType() == childForm.GetType())
            {
                // Không load lại nếu cùng loại form
                childForm.Dispose();
                return;
            }

            // Cleanup form cũ nếu có
            if (currentChildForm != null)
            {
                currentChildForm.Close();
                currentChildForm.Dispose();
            }

            // Xóa các control cũ trong sharePanel
            sharePanel.Controls.Clear();

            // Setup child form
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            
            // Thêm form vào sharePanel
            sharePanel.Controls.Add(childForm);
            sharePanel.Tag = childForm;
            
            // Hiển thị form
            childForm.Show();
            childForm.BringToFront();
        }

        private void btn_products_Click(object sender, EventArgs e)
        {

        }
    }
}
