using System;
using System.Drawing;
using System.Windows.Forms;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.Forms.Inventory; // Import namespace chứa frmStockIn

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Inventory
{
    public partial class frmInventoryMain : Form
    {
        public frmInventoryMain()
        {
            InitializeComponent();
            CustomizeInterface();
            LoadDummyData(); // Tải dữ liệu giả lập
        }

        private void CustomizeInterface()
        {
            // --- STYLE GRIDVIEW ---

            dgvImportList.RowTemplate.Height = 40;
            dgvImportList.ColumnHeadersHeight = 45;

            dgvImportList.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            // --------------------

            dgvImportList.GridColor = Color.FromArgb(230, 230, 230); // Màu kẻ lưới (xám nhạt)

            // Font chữ
            dgvImportList.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvImportList.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

 
        }

        private void LoadDummyData()
        {
            dgvImportList.Rows.Add("PN001", "Công Ty Cổ Phần Vinamilk", "19/11/2025", "15,500,000 đ", "Nguyễn Văn A");
            dgvImportList.Rows.Add("PN002", "Công Ty TNHH Cocacola VN", "18/11/2025", "8,200,000 đ", "Trần Thị B");
            dgvImportList.Rows.Add("PN003", "Nhà Phân Phối Bánh Kẹo Kinh Đô", "15/11/2025", "5,100,000 đ", "Nguyễn Văn A");
            dgvImportList.Rows.Add("PN004", "Unilever Việt Nam", "10/11/2025", "22,000,000 đ", "Lê Văn C");
        }

        // SỰ KIỆN: Bấm nút Nhập Hàng Mới
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Khởi tạo form Nhập kho (frmStockIn)
            frmStockIn stockInForm = new frmStockIn();

            // Hiển thị form (ShowDialog để bắt buộc xử lý xong mới quay lại form này)
            stockInForm.ShowDialog();
        }

        // SỰ KIỆN: Bấm nút Xem Chi Tiết
        private void btnDetail_Click(object sender, EventArgs e)
        {
            if (dgvImportList.CurrentRow == null || dgvImportList.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Vui lòng chọn một phiếu nhập để xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string selectedId = dgvImportList.CurrentRow.Cells[0].Value.ToString();

            // Mở lại frmStockIn nhưng ở chế độ xem (Bạn cần xử lý logic truyền ID vào constructor của frmStockIn sau này)
            MessageBox.Show($"Đang mở chi tiết phiếu: {selectedId} \n(Chức năng này sẽ load lại dữ liệu lên form StockIn)", "Thông báo");

            frmStockIn stockInForm = new frmStockIn();
            stockInForm.ShowDialog();
        }
    }
}