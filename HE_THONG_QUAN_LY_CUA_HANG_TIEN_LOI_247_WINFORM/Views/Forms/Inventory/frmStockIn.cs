using System;
using System.Drawing;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.Forms.Inventory
{
    public partial class frmStockIn : Form
    {
        public frmStockIn()
        {
            InitializeComponent();
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            // --- STYLE CHO GRIDVIEW GIỐNG ẢNH ---

            // 1. Cấu hình chung
            dgvDetail.BackgroundColor = Color.White;
            dgvDetail.BorderStyle = BorderStyle.None;
            dgvDetail.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // Chỉ kẻ ngang
            dgvDetail.GridColor = Color.FromArgb(230, 230, 230); // Màu kẻ nhạt
            dgvDetail.RowHeadersVisible = false;
            dgvDetail.EnableHeadersVisualStyles = false;

            // 2. Header Style (Trắng, chữ đậm đen)
            dgvDetail.ColumnHeadersHeight = 45;
            dgvDetail.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvDetail.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvDetail.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            dgvDetail.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // 3. Row Style
            dgvDetail.RowTemplate.Height = 40;
            dgvDetail.DefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 245, 255); // Xanh rất nhạt khi chọn
            dgvDetail.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvDetail.DefaultCellStyle.Font = new Font("Segoe UI", 9.75F);
            dgvDetail.DefaultCellStyle.Padding = new Padding(5, 0, 0, 0); // Cách lề chữ

            // 4. Căn chỉnh cột
            colQty.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetail.EnableHeadersVisualStyles = false; // Bắt buộc dòng này
            dgvDetail.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185); // Xanh header
            dgvDetail.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // Chữ trắng
            dgvDetail.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvDetail.GridColor = Color.FromArgb(224, 224, 224); // Kẻ lưới nhạt
        }
    }
}