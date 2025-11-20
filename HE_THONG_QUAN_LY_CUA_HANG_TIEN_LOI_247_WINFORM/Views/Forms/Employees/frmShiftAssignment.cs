using System;
using System.Drawing;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.Forms.Employees
{
    public partial class frmShiftAssignment : Form
    {
        public frmShiftAssignment()
        {
            InitializeComponent();
            CustomizeInterface();
            LoadDummyData();
        }

        private void CustomizeInterface()
        {
            // Style GridView
            dgvShiftMatrix.BorderStyle = BorderStyle.None;
            dgvShiftMatrix.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvShiftMatrix.GridColor = Color.FromArgb(230, 230, 230);
            dgvShiftMatrix.RowHeadersVisible = false;
            dgvShiftMatrix.EnableHeadersVisualStyles = false;
            dgvShiftMatrix.ColumnHeadersHeight = 50;
            dgvShiftMatrix.RowTemplate.Height = 60; // Dòng cao để hiển thị giờ làm việc rõ

            // Header Style
            dgvShiftMatrix.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219); // Xanh dương nhạt hơn
            dgvShiftMatrix.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvShiftMatrix.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvShiftMatrix.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvShiftMatrix.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // Cell Style
            dgvShiftMatrix.DefaultCellStyle.SelectionBackColor = Color.White; // Không đổi màu nền khi chọn
            dgvShiftMatrix.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvShiftMatrix.DefaultCellStyle.Font = new Font("Segoe UI", 10F);

            // Cột tên ca căn trái, các cột checkbox căn giữa
            colShiftName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            colShiftName.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            colShiftName.DefaultCellStyle.WrapMode = DataGridViewTriState.True; // Cho phép xuống dòng tên ca
        }

        private void LoadDummyData()
        {
            // Load nhân viên mẫu
            cmbEmployee.Items.Add("Chọn nhân viên...");
            cmbEmployee.Items.Add("NV001 - Nguyễn Văn An");
            cmbEmployee.Items.Add("NV002 - Trần Thị Bích");
            cmbEmployee.SelectedIndex = 0;

            // Load các ca làm việc vào lưới (Giống hình)
            dgvShiftMatrix.Rows.Clear();

            dgvShiftMatrix.Rows.Add("Ca Sáng\n(06:00 - 14:00)", false, true, false, false, false, false, false); // Ví dụ thứ 3 được chọn
            dgvShiftMatrix.Rows.Add("Ca Chiều\n(14:00 - 22:00)", false, false, false, false, false, false, false);
            dgvShiftMatrix.Rows.Add("Ca Tối\n(18:00 - 22:00)", false, false, false, false, false, false, false);
            dgvShiftMatrix.Rows.Add("Ca Đêm\n(22:00 - 06:00)", false, false, false, false, false, false, false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbEmployee.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần phân công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Đã lưu phân công ca làm việc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Logic lưu xuống DB sẽ viết ở đây
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}