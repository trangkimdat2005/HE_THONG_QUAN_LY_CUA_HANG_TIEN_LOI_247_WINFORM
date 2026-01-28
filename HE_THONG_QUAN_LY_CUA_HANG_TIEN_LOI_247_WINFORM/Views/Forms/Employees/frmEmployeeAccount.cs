using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Employees
{
    public partial class frmEmployeeAccount : Form
    {
        private readonly EmployeeAccountController _controller;
        private string _selectedEmployeeId;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        private const int EM_SETCUEBANNER = 0x1501;

        private void SetPlaceholder(TextBox txt, string text)
        {
            if (txt != null)
            {
                SendMessage(txt.Handle, EM_SETCUEBANNER, 0, text);
            }
        }

        public frmEmployeeAccount()
        {
            InitializeComponent();
            _controller = new EmployeeAccountController();

            SetPlaceholder(txtSearch, "Nhập tên nhân viên hoặc tên đăng nhập để tìm...");

            if (txtSearch != null)
            {
                txtSearch.TextChanged += (s, e) => { btnSearch_Click(null, null); };
            }

            if (dgvAccounts != null)
                this.dgvAccounts.SelectionChanged += dgvAccounts_SelectionChanged;

            if (txtSearch != null)
                txtSearch.KeyPress += (s, e) => { if (e.KeyChar == 13) { btnSearch_Click(s, e); e.Handled = true; } };

            this.VisibleChanged += (s, e) => {
                if (this.Visible) LoadAccounts();
            };
        }

        private void frmEmployeeAccount_Load(object sender, EventArgs e)
        {
            try
            {
                if (dgvAccounts != null) dgvAccounts.AutoGenerateColumns = false;
                ConfigureGridColumns();
                LoadRoles();
                LoadAccounts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải form: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGridColumns()
        {
            if (dgvAccounts == null) return;

            colEmployeeId.DataPropertyName = "NhanVienId";
            colName.DataPropertyName = "HoTen";
            colPosition.DataPropertyName = "ChucVu";
            colPhone.DataPropertyName = "SoDienThoai";
            colUsername.DataPropertyName = "TenDangNhap";
            colEmail.DataPropertyName = "EmailTK";
            colRole.DataPropertyName = "RoleName";
            colStatus.DataPropertyName = "TrangThaiTK";
        }

        private void LoadRoles()
        {
            try
            {
                var roles = _controller.GetRoles();
                cboRole.DisplayMember = "ten";
                cboRole.ValueMember = "id";
                cboRole.DataSource = roles;
                cboRole.SelectedIndex = roles.Count > 0 ? 0 : -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách vai trò: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAccounts()
        {
            try
            {
                var accounts = _controller.GetAllEmployeeAccounts();
                dgvAccounts.DataSource = accounts;

                if (accounts is IList list)
                    lblStatus.Text = $"Tổng số: {list.Count} tài khoản";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetCurrentEmployeeId()
        {
            if (dgvAccounts.CurrentRow == null) return null;

            var dataItem = dgvAccounts.CurrentRow.DataBoundItem;
            if (dataItem != null)
            {
                var prop = dataItem.GetType().GetProperty("NhanVienId");
                if (prop != null) return prop.GetValue(dataItem)?.ToString();
            }

            if (dgvAccounts.ColumnCount > 0 && dgvAccounts.CurrentRow.Cells[0].Value != null)
                return dgvAccounts.CurrentRow.Cells[0].Value.ToString();

            return null;
        }

        private string GetCurrentAccountId()
        {
            if (dgvAccounts.CurrentRow == null) return null;

            var dataItem = dgvAccounts.CurrentRow.DataBoundItem;
            if (dataItem != null)
            {
                var prop = dataItem.GetType().GetProperty("TaiKhoanId");
                if (prop != null) return prop.GetValue(dataItem)?.ToString();
            }

            return null;
        }

        private void dgvAccounts_SelectionChanged(object sender, EventArgs e)
        {
            string id = GetCurrentEmployeeId();
            if (!string.IsNullOrEmpty(id))
            {
                _selectedEmployeeId = id;
            }

            SetSelectedRoleFromGrid();
        }

        private void SetSelectedRoleFromGrid()
        {
            if (dgvAccounts.CurrentRow == null || dgvAccounts.CurrentRow.DataBoundItem == null)
            {
                cboRole.SelectedIndex = -1;
                return;
            }

            var dataItem = dgvAccounts.CurrentRow.DataBoundItem;
            var prop = dataItem.GetType().GetProperty("RoleId");
            var roleId = prop?.GetValue(dataItem)?.ToString();

            if (!string.IsNullOrEmpty(roleId))
                cboRole.SelectedValue = roleId;
            else
                cboRole.SelectedIndex = -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (var frm = new frmAddEmployeeAccount())
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        LoadAccounts();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form thêm tài khoản: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string accountId = GetCurrentAccountId();
            if (string.IsNullOrEmpty(accountId))
            {
                MessageBox.Show("Vui lòng chọn tài khoản!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var frm = new frmResetPassword(accountId))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show("Đặt lại mật khẩu thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnToggleStatus_Click(object sender, EventArgs e)
        {
            string accountId = GetCurrentAccountId();
            if (string.IsNullOrEmpty(accountId))
            {
                MessageBox.Show("Vui lòng chọn tài khoản!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn thay đổi trạng thái tài khoản này?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var (success, message) = _controller.ToggleAccountStatus(accountId);
                if (success)
                {
                    MessageBox.Show(message, "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAccounts();
                }
                else
                {
                    MessageBox.Show(message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string employeeId = GetCurrentEmployeeId();
            if (string.IsNullOrEmpty(employeeId))
            {
                MessageBox.Show("Vui lòng chọn tài khoản!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var (success, message) = _controller.DeleteEmployeeAccount(employeeId);
                if (success)
                {
                    MessageBox.Show(message, "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAccounts();
                }
                else
                {
                    MessageBox.Show(message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var list = _controller.SearchEmployeeAccounts(txtSearch.Text.Trim());
            dgvAccounts.DataSource = list;
            if (list is IList l)
                lblStatus.Text = $"Tìm thấy: {l.Count}";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadAccounts();
        }

        private void btnAssignRole_Click(object sender, EventArgs e)
        {
            string accountId = GetCurrentAccountId();
            if (string.IsNullOrEmpty(accountId))
            {
                MessageBox.Show("Vui lòng chọn tài khoản!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboRole.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn vai trò!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var roleId = cboRole.SelectedValue.ToString();
            var (success, message) = _controller.UpdateAccountRole(accountId, roleId);
            if (success)
            {
                MessageBox.Show(message, "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAccounts();
            }
            else
            {
                MessageBox.Show(message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _controller?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
