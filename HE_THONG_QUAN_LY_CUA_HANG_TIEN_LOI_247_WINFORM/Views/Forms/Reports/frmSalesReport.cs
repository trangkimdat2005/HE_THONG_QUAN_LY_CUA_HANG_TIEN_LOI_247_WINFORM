using System;
using System.Drawing;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Reports
{
    public partial class frmSalesReport : Form
    {
        public frmSalesReport()
        {
            InitializeComponent();
            CustomizeInterface();
            LoadDummyData();
        }

        private void CustomizeInterface()
        {
            // --- Style GridView (Đồng bộ các form trước) ---
            dgvReports.BorderStyle = BorderStyle.None;
            dgvReports.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // Chỉ kẻ ngang
            dgvReports.GridColor = Color.FromArgb(230, 230, 230);
            dgvReports.RowHeadersVisible = false;
            dgvReports.EnableHeadersVisualStyles = false;
            dgvReports.ColumnHeadersHeight = 40;
            dgvReports.RowTemplate.Height = 40;

            // Header màu xanh
            dgvReports.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvReports.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvReports.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvReports.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // Row style
            dgvReports.DefaultCellStyle.SelectionBackColor = Color.FromArgb(211, 233, 252);
            dgvReports.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvReports.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvReports.DefaultCellStyle.Padding = new Padding(5, 0, 0, 0);

            // Căn chỉnh cột
            colSTT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTotalRevenue.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colAction.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Chọn mặc định PageSize
            cmbPageSize.SelectedIndex = 0;
        }

        private void LoadDummyData()
        {
            dgvReports.Rows.Clear();
            // Dữ liệu mẫu giống ảnh
            dgvReports.Rows.Add("1", "BC001", "01/11/2025 - 11/11/2025", "532,700 đ");
            dgvReports.Rows.Add("2", "BC002", "01/10/2025 - 31/10/2025", "15,000,000 đ");
            dgvReports.Rows.Add("3", "BC003", "01/09/2025 - 30/09/2025", "12,000,000 đ");
            dgvReports.Rows.Add("4", "BC004", "01/08/2025 - 31/08/2025", "18,000,000 đ");
            dgvReports.Rows.Add("5", "BC005", "01/07/2025 - 31/07/2025", "14,500,000 đ");
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đang xuất file Excel...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvReports_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Xử lý khi bấm nút Download
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvReports.Columns["colAction"].Index)
            {
                string reportId = dgvReports.Rows[e.RowIndex].Cells["colReportId"].Value.ToString();
                MessageBox.Show($"Tải xuống báo cáo: {reportId}", "Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}