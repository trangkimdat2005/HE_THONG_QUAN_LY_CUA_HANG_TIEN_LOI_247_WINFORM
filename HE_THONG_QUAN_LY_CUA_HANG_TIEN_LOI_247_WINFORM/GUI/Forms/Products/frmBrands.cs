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
    public partial class frmBrands : Form
    {
        private AppDbContext _context;
        private string _selectedBrandId;
        private bool _isAddMode = false;

        public frmBrands()
        {
            InitializeComponent();
            _context = new AppDbContext();
        }

        private void frmBrands_Load(object sender, EventArgs e)
        {
            try
            {
                SetupDataGridView();
                LoadBrands();
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
            // Cấu hình DataGridView nếu chưa có trong Designer
            if (dgvBrands.Columns.Count == 0)
            {
                dgvBrands.AutoGenerateColumns = false;
                
                dgvBrands.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colId",
                    HeaderText = "Mã nhãn hiệu",
                    DataPropertyName = "id",
                    Width = 200
                });

                dgvBrands.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colName",
                    HeaderText = "Tên nhãn hiệu",
                    DataPropertyName = "ten",
                    Width = 300
                });

                dgvBrands.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colProductCount",
                    HeaderText = "Số sản phẩm",
                    DataPropertyName = "SoLuongSanPham",
                    Width = 150
                });

                dgvBrands.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colStatus",
                    HeaderText = "Trạng thái",
                    DataPropertyName = "TrangThai",
                    Width = 150
                });
            }
        }

        private void LoadBrands()
        {
            try
            {
                var brands = _context.NhanHieux
                    .Where(b => !b.isDelete)
                    .Select(b => new
                    {
                        b.id,
                        b.ten,
                        SoLuongSanPham = b.SanPhams.Count(sp => !sp.isDelete),
                        TrangThai = b.isDelete ? "Không hoạt động" : "Hoạt động"
                    })
                    .OrderBy(b => b.ten)
                    .ToList();

                dgvBrands.DataSource = brands;

                // Update status
                lblStatus.Text = $"Tổng số: {brands.Count} nhãn hiệu";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhãn hiệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvBrands_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBrands.CurrentRow != null && !_isAddMode)
            {
                _selectedBrandId = dgvBrands.CurrentRow.Cells["colId"].Value?.ToString();
                LoadBrandDetail(_selectedBrandId);
            }
        }

        private void LoadBrandDetail(string brandId)
        {
            if (string.IsNullOrEmpty(brandId))
                return;

            try
            {
                var brand = _context.NhanHieux.Find(brandId);

                if (brand != null)
                {
                    txtBrandId.Text = brand.id;
                    txtBrandName.Text = brand.ten;

                    // Load số lượng sản phẩm
                    int productCount = _context.SanPhams
                        .Count(sp => sp.nhanHieuId == brandId && !sp.isDelete);
                    
                    lblProductCount.Text = $"Số sản phẩm: {productCount}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết nhãn hiệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetFormMode(true);
            _isAddMode = true;
            ClearForm();
            txtBrandId.Text = "Tự động tạo";
            txtBrandName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvBrands.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn nhãn hiệu cần chỉnh sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetFormMode(true);
            _isAddMode = false;
            txtBrandName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvBrands.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn nhãn hiệu cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xem nhãn hiệu có sản phẩm không
            int productCount = _context.SanPhams
                .Count(sp => sp.nhanHieuId == _selectedBrandId && !sp.isDelete);

            if (productCount > 0)
            {
                MessageBox.Show(
                    $"Không thể xóa nhãn hiệu này vì có {productCount} sản phẩm đang sử dụng!\n\n" +
                    "Vui lòng xóa hoặc chuyển các sản phẩm sang nhãn hiệu khác trước.",
                    "Không thể xóa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa nhãn hiệu '{txtBrandName.Text}'?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var brand = _context.NhanHieux.Find(_selectedBrandId);
                    if (brand != null)
                    {
                        brand.isDelete = true;
                        _context.SaveChanges();

                        MessageBox.Show("Xóa nhãn hiệu thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadBrands();
                        ClearForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa nhãn hiệu: {ex.Message}", "Lỗi",
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
                    bool exists = _context.NhanHieux
                        .Any(b => b.ten.ToLower() == txtBrandName.Text.Trim().ToLower() && !b.isDelete);

                    if (exists)
                    {
                        MessageBox.Show("Tên nhãn hiệu đã tồn tại!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtBrandName.Focus();
                        return;
                    }

                    // Thêm mới
                    var newBrand = new NhanHieu
                    {
                        id = Guid.NewGuid().ToString(),
                        ten = txtBrandName.Text.Trim(),
                        isDelete = false
                    };

                    _context.NhanHieux.Add(newBrand);
                    _context.SaveChanges();

                    MessageBox.Show("Thêm nhãn hiệu thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Kiểm tra trùng tên (trừ chính nó)
                    bool exists = _context.NhanHieux
                        .Any(b => b.id != _selectedBrandId && 
                             b.ten.ToLower() == txtBrandName.Text.Trim().ToLower() && 
                             !b.isDelete);

                    if (exists)
                    {
                        MessageBox.Show("Tên nhãn hiệu đã tồn tại!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtBrandName.Focus();
                        return;
                    }

                    // Cập nhật
                    var brand = _context.NhanHieux.Find(_selectedBrandId);
                    if (brand != null)
                    {
                        brand.ten = txtBrandName.Text.Trim();
                        _context.SaveChanges();

                        MessageBox.Show("Cập nhật nhãn hiệu thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                LoadBrands();
                SetFormMode(false);
                _isAddMode = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu nhãn hiệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetFormMode(false);
            _isAddMode = false;

            if (dgvBrands.CurrentRow != null)
            {
                LoadBrandDetail(_selectedBrandId);
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
                    LoadBrands();
                    return;
                }

                var brands = _context.NhanHieux
                    .Where(b => !b.isDelete && b.ten.ToLower().Contains(searchText))
                    .Select(b => new
                    {
                        b.id,
                        b.ten,
                        SoLuongSanPham = b.SanPhams.Count(sp => !sp.isDelete),
                        TrangThai = b.isDelete ? "Không hoạt động" : "Hoạt động"
                    })
                    .OrderBy(b => b.ten)
                    .ToList();

                dgvBrands.DataSource = brands;
                lblStatus.Text = $"Tìm thấy: {brands.Count} nhãn hiệu";
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
            LoadBrands();
            ClearForm();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xuất Excel đang được phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtBrandName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhãn hiệu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBrandName.Focus();
                return false;
            }

            if (txtBrandName.Text.Trim().Length < 2)
            {
                MessageBox.Show("Tên nhãn hiệu phải có ít nhất 2 ký tự!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBrandName.Focus();
                return false;
            }

            return true;
        }

        private void SetFormMode(bool isEditMode)
        {
            // Enable/disable input fields
            txtBrandName.Enabled = isEditMode;
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
            dgvBrands.Enabled = !isEditMode;
        }

        private void ClearForm()
        {
            txtBrandId.Clear();
            txtBrandName.Clear();
            lblProductCount.Text = "Số sản phẩm: 0";
            _selectedBrandId = null;
        }

        // Event handler cho Enter key trong txtSearch
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(sender, e);
                e.Handled = true;
            }
        }

        // Event handler cho Enter key trong txtBrandName
        private void txtBrandName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSave_Click(sender, e);
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
