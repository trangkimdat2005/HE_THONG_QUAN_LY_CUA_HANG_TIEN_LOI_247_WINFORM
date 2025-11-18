using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Products
{
    public partial class frmMeasurements : Form
    {
        private AppDbContext _context;
        private string _selectedUnitId;
        private bool _isAddMode = false;

        public frmMeasurements()
        {
            InitializeComponent();
            _context = new AppDbContext();
        }

        private void frmMeasurements_Load(object sender, EventArgs e)
        {
            try
            {
                SetupDataGridView();
                LoadUnits();
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
            if (dgvUnits.Columns.Count == 0)
            {
                dgvUnits.AutoGenerateColumns = false;
                
                dgvUnits.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colId",
                    HeaderText = "Mã đơn vị",
                    DataPropertyName = "id",
                    Width = 150
                });

                dgvUnits.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colName",
                    HeaderText = "Tên đơn vị",
                    DataPropertyName = "ten",
                    Width = 250
                });

                dgvUnits.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colSymbol",
                    HeaderText = "Ký hiệu",
                    DataPropertyName = "kyHieu",
                    Width = 120
                });

                dgvUnits.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colProductCount",
                    HeaderText = "Số sản phẩm",
                    DataPropertyName = "SoLuongSanPham",
                    Width = 120
                });

                dgvUnits.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colStatus",
                    HeaderText = "Trạng thái",
                    DataPropertyName = "TrangThai",
                    Width = 120
                });
            }
        }

        private void LoadUnits()
        {
            try
            {
                var units = _context.DonViDoLuongs
                    .Where(u => !u.isDelete)
                    .Select(u => new
                    {
                        u.id,
                        u.ten,
                        u.kyHieu,
                        SoLuongSanPham = u.SanPhamDonVis.Count(sp => !sp.isDelete),
                        TrangThai = u.isDelete ? "Không hoạt động" : "Hoạt động"
                    })
                    .OrderBy(u => u.ten)
                    .ToList();

                dgvUnits.DataSource = units;
                lblStatus.Text = $"Tổng số: {units.Count} đơn vị";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách đơn vị: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUnits_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUnits.CurrentRow != null && !_isAddMode)
            {
                _selectedUnitId = dgvUnits.CurrentRow.Cells["colId"].Value?.ToString();
                LoadUnitDetail(_selectedUnitId);
            }
        }

        private void LoadUnitDetail(string unitId)
        {
            if (string.IsNullOrEmpty(unitId))
                return;

            try
            {
                var unit = _context.DonViDoLuongs.Find(unitId);

                if (unit != null)
                {
                    txtUnitId.Text = unit.id;
                    txtUnitName.Text = unit.ten;
                    txtSymbol.Text = unit.kyHieu;

                    // Load số lượng sản phẩm
                    int productCount = _context.SanPhamDonVis
                        .Count(sp => sp.donViId == unitId && !sp.isDelete);
                    
                    lblProductCount.Text = $"Số sản phẩm: {productCount}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết đơn vị: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetFormMode(true);
            _isAddMode = true;
            ClearForm();
            txtUnitId.Text = "Tự động tạo";
            txtUnitName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUnits.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn đơn vị cần chỉnh sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetFormMode(true);
            _isAddMode = false;
            txtUnitName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUnits.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn đơn vị cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra có sản phẩm sử dụng không
            int productCount = _context.SanPhamDonVis
                .Count(sp => sp.donViId == _selectedUnitId && !sp.isDelete);

            if (productCount > 0)
            {
                MessageBox.Show(
                    $"Không thể xóa đơn vị này vì có {productCount} sản phẩm đang sử dụng!\n\n" +
                    "Vui lòng chuyển các sản phẩm sang đơn vị khác trước.",
                    "Không thể xóa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa đơn vị '{txtUnitName.Text}'?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var unit = _context.DonViDoLuongs.Find(_selectedUnitId);
                    if (unit != null)
                    {
                        unit.isDelete = true;
                        _context.SaveChanges();

                        MessageBox.Show("Xóa đơn vị thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadUnits();
                        ClearForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa đơn vị: {ex.Message}", "Lỗi",
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
                    // Kiểm tra trùng tên hoặc ký hiệu
                    bool nameExists = _context.DonViDoLuongs
                        .Any(u => u.ten.ToLower() == txtUnitName.Text.Trim().ToLower() && !u.isDelete);

                    if (nameExists)
                    {
                        MessageBox.Show("Tên đơn vị đã tồn tại!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUnitName.Focus();
                        return;
                    }

                    bool symbolExists = _context.DonViDoLuongs
                        .Any(u => u.kyHieu.ToLower() == txtSymbol.Text.Trim().ToLower() && !u.isDelete);

                    if (symbolExists)
                    {
                        MessageBox.Show("Ký hiệu đơn vị đã tồn tại!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSymbol.Focus();
                        return;
                    }

                    // Thêm mới
                    var newUnit = new DonViDoLuong
                    {
                        id = Guid.NewGuid().ToString(),
                        ten = txtUnitName.Text.Trim(),
                        kyHieu = txtSymbol.Text.Trim(),
                        isDelete = false
                    };

                    _context.DonViDoLuongs.Add(newUnit);
                    _context.SaveChanges();

                    MessageBox.Show("Thêm đơn vị thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Kiểm tra trùng tên (trừ chính nó)
                    bool nameExists = _context.DonViDoLuongs
                        .Any(u => u.id != _selectedUnitId && 
                             u.ten.ToLower() == txtUnitName.Text.Trim().ToLower() && 
                             !u.isDelete);

                    if (nameExists)
                    {
                        MessageBox.Show("Tên đơn vị đã tồn tại!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUnitName.Focus();
                        return;
                    }

                    bool symbolExists = _context.DonViDoLuongs
                        .Any(u => u.id != _selectedUnitId && 
                             u.kyHieu.ToLower() == txtSymbol.Text.Trim().ToLower() && 
                             !u.isDelete);

                    if (symbolExists)
                    {
                        MessageBox.Show("Ký hiệu đơn vị đã tồn tại!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSymbol.Focus();
                        return;
                    }

                    // Cập nhật
                    var unit = _context.DonViDoLuongs.Find(_selectedUnitId);
                    if (unit != null)
                    {
                        unit.ten = txtUnitName.Text.Trim();
                        unit.kyHieu = txtSymbol.Text.Trim();

                        _context.SaveChanges();

                        MessageBox.Show("Cập nhật đơn vị thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                LoadUnits();
                SetFormMode(false);
                _isAddMode = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu đơn vị: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetFormMode(false);
            _isAddMode = false;

            if (dgvUnits.CurrentRow != null)
            {
                LoadUnitDetail(_selectedUnitId);
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
                    LoadUnits();
                    return;
                }

                var units = _context.DonViDoLuongs
                    .Where(u => !u.isDelete && 
                           (u.ten.ToLower().Contains(searchText) || 
                            u.kyHieu.ToLower().Contains(searchText)))
                    .Select(u => new
                    {
                        u.id,
                        u.ten,
                        u.kyHieu,
                        SoLuongSanPham = u.SanPhamDonVis.Count(sp => !sp.isDelete),
                        TrangThai = u.isDelete ? "Không hoạt động" : "Hoạt động"
                    })
                    .OrderBy(u => u.ten)
                    .ToList();

                dgvUnits.DataSource = units;
                lblStatus.Text = $"Tìm thấy: {units.Count} đơn vị";
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
            LoadUnits();
            ClearForm();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xuất Excel đang được phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtUnitName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đơn vị!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUnitName.Focus();
                return false;
            }

            if (txtUnitName.Text.Trim().Length < 1)
            {
                MessageBox.Show("Tên đơn vị phải có ít nhất 1 ký tự!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUnitName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSymbol.Text))
            {
                MessageBox.Show("Vui lòng nhập ký hiệu đơn vị!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSymbol.Focus();
                return false;
            }

            return true;
        }

        private void SetFormMode(bool isEditMode)
        {
            // Enable/disable input fields
            txtUnitName.Enabled = isEditMode;
            txtSymbol.Enabled = isEditMode;
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
            dgvUnits.Enabled = !isEditMode;
        }

        private void ClearForm()
        {
            txtUnitId.Clear();
            txtUnitName.Clear();
            txtSymbol.Clear();
            lblProductCount.Text = "Số sản phẩm: 0";
            _selectedUnitId = null;
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

        private void txtUnitName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtSymbol.Focus();
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
