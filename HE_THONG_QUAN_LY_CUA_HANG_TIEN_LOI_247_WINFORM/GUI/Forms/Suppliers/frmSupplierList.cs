using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Suppliers
{
    public partial class frmSupplierList : Form
    {
        private AppDbContext _context;
        private string _selectedSupplierId;
        private bool _isAddMode = false;

        public frmSupplierList()
        {
            InitializeComponent();
            _context = new AppDbContext();
        }

        private void frmSupplierList_Load(object sender, EventArgs e)
        {
            try
            {
                SetupDataGridView();
                LoadSuppliers();
                SetFormMode(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            if (dgvSuppliers.Columns.Count == 0)
            {
                dgvSuppliers.AutoGenerateColumns = false;
                
                dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colId",
                    HeaderText = "Mã NCC",
                    DataPropertyName = "id",
                    Width = 150
                });

                dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colName",
                    HeaderText = "Tên nhà cung cấp",
                    DataPropertyName = "ten",
                    Width = 250
                });

                dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colPhone",
                    HeaderText = "Số điện thoại",
                    DataPropertyName = "soDienThoai",
                    Width = 120
                });

                dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colEmail",
                    HeaderText = "Email",
                    DataPropertyName = "email",
                    Width = 180
                });

                dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colAddress",
                    HeaderText = "Địa chỉ",
                    DataPropertyName = "diaChi",
                    Width = 200
                });

                dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colTaxCode",
                    HeaderText = "Mã số thuế",
                    DataPropertyName = "maSoThue",
                    Width = 120
                });

                dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colStatus",
                    HeaderText = "Trạng thái",
                    DataPropertyName = "TrangThai",
                    Width = 100
                });
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                var suppliers = _context.NhaCungCaps
                    .Where(s => !s.isDelete)
                    .Select(s => new
                    {
                        s.id,
                        s.ten,
                        s.soDienThoai,
                        s.email,
                        s.diaChi,
                        s.maSoThue,
                        TrangThai = s.isDelete ? "Không hoạt động" : "Hoạt động"
                    })
                    .OrderBy(s => s.ten)
                    .ToList();

                dgvSuppliers.DataSource = suppliers;
                lblStatus.Text = $"Tổng số: {suppliers.Count} nhà cung cấp";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhà cung cấp: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSuppliers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSuppliers.CurrentRow != null && !_isAddMode)
            {
                _selectedSupplierId = dgvSuppliers.CurrentRow.Cells["colId"].Value?.ToString();
                LoadSupplierDetail(_selectedSupplierId);
            }
        }

        private void LoadSupplierDetail(string supplierId)
        {
            if (string.IsNullOrEmpty(supplierId))
                return;

            try
            {
                var supplier = _context.NhaCungCaps.Find(supplierId);

                if (supplier != null)
                {
                    txtSupplierId.Text = supplier.id;
                    txtSupplierName.Text = supplier.ten;
                    txtPhone.Text = supplier.soDienThoai;
                    txtEmail.Text = supplier.email;
                    txtAddress.Text = supplier.diaChi;
                    txtTaxCode.Text = supplier.maSoThue;

                    // Load số phiếu nhập
                    int receiptCount = _context.PhieuNhaps
                        .Count(pn => pn.nhaCungCapId == supplierId && !pn.isDelete);
                    
                    lblReceiptCount.Text = $"Số phiếu nhập: {receiptCount}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết nhà cung cấp: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetFormMode(true);
            _isAddMode = true;
            ClearForm();
            txtSupplierId.Text = "Tự động tạo";
            txtSupplierName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần chỉnh sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetFormMode(true);
            _isAddMode = false;
            txtSupplierName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra có phiếu nhập không
            int receiptCount = _context.PhieuNhaps
                .Count(pn => pn.nhaCungCapId == _selectedSupplierId && !pn.isDelete);

            if (receiptCount > 0)
            {
                MessageBox.Show(
                    $"Không thể xóa nhà cung cấp này vì có {receiptCount} phiếu nhập!\n\n" +
                    "Vui lòng xử lý các phiếu nhập trước.",
                    "Không thể xóa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa nhà cung cấp '{txtSupplierName.Text}'?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var supplier = _context.NhaCungCaps.Find(_selectedSupplierId);
                    if (supplier != null)
                    {
                        supplier.isDelete = true;
                        _context.SaveChanges();

                        MessageBox.Show("Xóa nhà cung cấp thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadSuppliers();
                        ClearForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa nhà cung cấp: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                if (_isAddMode)
                {
                    // Kiểm tra trùng tên
                    bool exists = _context.NhaCungCaps
                        .Any(s => s.ten.ToLower() == txtSupplierName.Text.Trim().ToLower() && !s.isDelete);

                    if (exists)
                    {
                        MessageBox.Show("Tên nhà cung cấp đã tồn tại!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSupplierName.Focus();
                        return;
                    }

                    // Thêm mới
                    var newSupplier = new NhaCungCap
                    {
                        id = Guid.NewGuid().ToString(),
                        ten = txtSupplierName.Text.Trim(),
                        soDienThoai = txtPhone.Text.Trim(),
                        email = txtEmail.Text.Trim(),
                        diaChi = txtAddress.Text.Trim(),
                        maSoThue = txtTaxCode.Text.Trim(),
                        isDelete = false
                    };

                    _context.NhaCungCaps.Add(newSupplier);
                    _context.SaveChanges();

                    MessageBox.Show("Thêm nhà cung cấp thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Kiểm tra trùng tên (trừ chính nó)
                    bool exists = _context.NhaCungCaps
                        .Any(s => s.id != _selectedSupplierId && 
                             s.ten.ToLower() == txtSupplierName.Text.Trim().ToLower() && 
                             !s.isDelete);

                    if (exists)
                    {
                        MessageBox.Show("Tên nhà cung cấp đã tồn tại!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSupplierName.Focus();
                        return;
                    }

                    // Cập nhật
                    var supplier = _context.NhaCungCaps.Find(_selectedSupplierId);
                    if (supplier != null)
                    {
                        supplier.ten = txtSupplierName.Text.Trim();
                        supplier.soDienThoai = txtPhone.Text.Trim();
                        supplier.email = txtEmail.Text.Trim();
                        supplier.diaChi = txtAddress.Text.Trim();
                        supplier.maSoThue = txtTaxCode.Text.Trim();
                        
                        _context.SaveChanges();

                        MessageBox.Show("Cập nhật nhà cung cấp thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                LoadSuppliers();
                SetFormMode(false);
                _isAddMode = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu nhà cung cấp: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetFormMode(false);
            _isAddMode = false;

            if (dgvSuppliers.CurrentRow != null)
            {
                LoadSupplierDetail(_selectedSupplierId);
            }
            else
            {
                ClearForm();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchText = txtSearch.Text.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(searchText))
                {
                    LoadSuppliers();
                    return;
                }

                var suppliers = _context.NhaCungCaps
                    .Where(s => !s.isDelete && 
                           (s.ten.ToLower().Contains(searchText) || 
                            s.soDienThoai.Contains(searchText) ||
                            s.maSoThue.Contains(searchText)))
                    .Select(s => new
                    {
                        s.id,
                        s.ten,
                        s.soDienThoai,
                        s.email,
                        s.diaChi,
                        s.maSoThue,
                        TrangThai = s.isDelete ? "Không hoạt động" : "Hoạt động"
                    })
                    .OrderBy(s => s.ten)
                    .ToList();

                dgvSuppliers.DataSource = suppliers;
                lblStatus.Text = $"Tìm thấy: {suppliers.Count} nhà cung cấp";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadSuppliers();
            ClearForm();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xuất Excel đang được phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSupplierName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            // Validate phone number format
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtPhone.Text.Trim(), @"^[0-9]{10,11}$"))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Vui lòng nhập 10-11 số.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            // Validate email if provided
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text.Trim(), 
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    MessageBox.Show("Email không hợp lệ!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }

            return true;
        }

        private void SetFormMode(bool isEditMode)
        {
            // Enable/disable input fields
            txtSupplierName.Enabled = isEditMode;
            txtPhone.Enabled = isEditMode;
            txtEmail.Enabled = isEditMode;
            txtAddress.Enabled = isEditMode;
            txtTaxCode.Enabled = isEditMode;
            btnSave.Enabled = isEditMode;
            btnCancel.Enabled = isEditMode;

            // Enable/disable action buttons
            btnAdd.Enabled = !isEditMode;
            btnEdit.Enabled = !isEditMode;
            btnDelete.Enabled = !isEditMode;
            btnSearch.Enabled = !isEditMode;
            btnRefresh.Enabled = !isEditMode;
            btnExport.Enabled = !isEditMode;

            // Enable/disable search
            txtSearch.Enabled = !isEditMode;

            // Enable/disable grid selection
            dgvSuppliers.Enabled = !isEditMode;
        }

        private void ClearForm()
        {
            txtSupplierId.Clear();
            txtSupplierName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtTaxCode.Clear();
            lblReceiptCount.Text = "Số phiếu nhập: 0";
            _selectedSupplierId = null;
        }

        // Event handlers
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(sender, e);
                e.Handled = true;
            }
        }

        private void txtSupplierName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPhone.Focus();
                e.Handled = true;
            }
        }

        // Cleanup
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _context?.Dispose();
        }
    }
}
