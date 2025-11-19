using System;
using System.Drawing;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Employees
{
    public partial class frmWorkSchedule : Form
    {
        public frmWorkSchedule()
        {
            InitializeComponent();
            CustomizeInterface();
            LoadDummyData();
        }

        private void CustomizeInterface()
        {
            // Style GridView
            dgvSchedule.BorderStyle = BorderStyle.None;
            dgvSchedule.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvSchedule.GridColor = Color.FromArgb(230, 230, 230);
            dgvSchedule.RowHeadersVisible = false;
            dgvSchedule.EnableHeadersVisualStyles = false;
            dgvSchedule.ColumnHeadersHeight = 40;
            dgvSchedule.RowTemplate.Height = 40;

            dgvSchedule.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvSchedule.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSchedule.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvSchedule.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            dgvSchedule.DefaultCellStyle.SelectionBackColor = Color.FromArgb(211, 233, 252);
            dgvSchedule.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvSchedule.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvSchedule.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

            // Căn giữa các cột
            colDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colShift.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colStartTime.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colEndTime.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void LoadDummyData()
        {
            cmbEmployee.Items.Add("Tất cả nhân viên");
            cmbEmployee.Items.Add("Nguyễn Văn An");
            cmbEmployee.Items.Add("Trần Thị Bích");
            cmbEmployee.SelectedIndex = 0;

            dtpStartDate.Value = DateTime.Now.AddDays(-7);
            dtpEndDate.Value = DateTime.Now.AddDays(7);

            dgvSchedule.Rows.Clear();
            dgvSchedule.Rows.Add("SCH001", "Nguyễn Văn An", "20/11/2025", "Sáng", "06:00", "14:00", "");
            dgvSchedule.Rows.Add("SCH002", "Trần Thị Bích", "20/11/2025", "Chiều", "14:00", "22:00", "");
            dgvSchedule.Rows.Add("SCH003", "Lê Hoàng Nam", "20/11/2025", "Đêm", "22:00", "06:00", "Trực kho");
            dgvSchedule.Rows.Add("SCH004", "Nguyễn Văn An", "21/11/2025", "Sáng", "06:00", "14:00", "");

            lblTotal.Text = $"Tổng số: {dgvSchedule.Rows.Count} ca làm";
        }
    }
}