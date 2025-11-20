using System;
using System.Drawing;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Employees
{
    public partial class frmEmployeeList : Form
    {
        public frmEmployeeList()
        {
            InitializeComponent();
            CustomizeInterface();
            LoadDummyData(); // Tải dữ liệu giả lập để test
        }

        private void CustomizeInterface()
        {
            // Style GridView
            dgvEmployees.BorderStyle = BorderStyle.None;
            dgvEmployees.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvEmployees.GridColor = Color.FromArgb(230, 230, 230);
            dgvEmployees.RowHeadersVisible = false;
            dgvEmployees.EnableHeadersVisualStyles = false;
            dgvEmployees.ColumnHeadersHeight = 40;
            dgvEmployees.RowTemplate.Height = 40;

            dgvEmployees.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185); // Xanh Dương
            dgvEmployees.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvEmployees.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvEmployees.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            dgvEmployees.DefaultCellStyle.SelectionBackColor = Color.FromArgb(211, 233, 252); // Xanh Nhạt
            dgvEmployees.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvEmployees.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvEmployees.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

            // Căn giữa các cột ngắn
            colId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colGender.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void LoadDummyData()
        {
            dgvEmployees.Rows.Clear();
            dgvEmployees.Rows.Add("NV001", "Nguyễn Văn An", "Nam", "0901234567", "an.nguyen@email.com", "Quản lý", "Hoạt động");
            dgvEmployees.Rows.Add("NV002", "Trần Thị Bích", "Nữ", "0902345678", "bich.tran@email.com", "Thu ngân", "Hoạt động");
            dgvEmployees.Rows.Add("NV003", "Lê Hoàng Nam", "Nam", "0903456789", "nam.le@email.com", "Kho", "Nghỉ phép");
            dgvEmployees.Rows.Add("NV004", "Phạm Thu Hà", "Nữ", "0904567890", "ha.pham@email.com", "Thu ngân", "Hoạt động");

            lblTotal.Text = $"Tổng số: {dgvEmployees.Rows.Count} nhân viên";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //load form thêm nhân viên
            frmEmployees addEmployeeForm = new frmEmployees();
            addEmployeeForm.ShowDialog();

        }
    }
}