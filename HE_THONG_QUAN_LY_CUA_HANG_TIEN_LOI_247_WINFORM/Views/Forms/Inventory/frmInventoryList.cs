using System;
using System.Windows.Forms;
using System.Drawing;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Inventory
{
    public partial class frmInventoryList : Form
    {
        public frmInventoryList()
        {
            InitializeComponent();
        }

 

        // Sự kiện nút Tìm Kiếm
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.ToLower().Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Duyệt qua các dòng để ẩn dòng không khớp (Giả lập tìm kiếm)
            // Lưu ý: Khi kết nối SQL thì đoạn này sẽ là câu lệnh SELECT ... WHERE Name LIKE ...
            foreach (DataGridViewRow row in dgvInventory.Rows)
            {
                if (row.IsNewRow) continue;

                // Lấy tên sản phẩm
                string productName = row.Cells["colTenSP"].Value?.ToString().ToLower() ?? "";
                string productId = row.Cells["colMaSP"].Value?.ToString().ToLower() ?? "";

                // Nếu tên hoặc mã chứa từ khóa thì hiện, không thì ẩn
                if (productName.Contains(keyword) || productId.Contains(keyword))
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false; // WinForms cho phép ẩn dòng
                }
            }
        }

        // Sự kiện nút Làm Mới
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = ""; // Xóa ô tìm kiếm

            // Hiển thị lại tất cả các dòng
            foreach (DataGridViewRow row in dgvInventory.Rows)
            {
                row.Visible = true;
            }

            // Hoặc gọi lại hàm load data: LoadDummyData();
        }
    }
}