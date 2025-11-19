using System;
using System.Drawing;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Employees
{
    public partial class frmEmployees : Form
    {
        public frmEmployees()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Thêm items cho ComboBox
            cmbPosition.Items.AddRange(new object[] { "Quản lý", "Thu ngân", "Kho", "Bảo vệ" });
            cmbPosition.SelectedIndex = 0;

            cmbStatus.Items.AddRange(new object[] { "Hoạt động", "Nghỉ phép", "Đã nghỉ việc" });
            cmbStatus.SelectedIndex = 0;

            rbMale.Checked = true;
        }

        // TODO: Thêm các hàm xử lý sự kiện btnSave_Click, btnCancel_Click tại đây...
    }
}