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
    public partial class frmCategorys : Form
    {
        private AppDbContext _context;
        private string _selectedCategoryId;
        private bool _isAddMode = false;

        public frmCategorys()
        {
            InitializeComponent();
            _context = new AppDbContext();
        }

        private void frmCategorys_Load(object sender, EventArgs e)
        {
            try
            {
                SetupDataGridView();
                LoadCategories();
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
            if (dgvCategories.Columns.Count == 0)
            {
                dgvCategories.AutoGenerateColumns = false;
                
                dgvCategories.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colId",
                    HeaderText = "Mã danh mục",
                    DataPropertyName = "id",
                    Width = 200
                });

                dgvCategories.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colName",
                    HeaderText = "Tên danh mục",
                    DataPropertyName = "ten",
                    Width = 300
                });

                dgvCategories.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colProductCount",
                    HeaderText = "Số sản phẩm",
                    DataPropertyName = "SoLuongSanPham",
                    Width = 150
                });

                dgvCategories.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colStatus",
                    HeaderText = "Trạng thái",
                    DataPropertyName = "TrangThai",
                    Width = 150
                });
            }
        }

        private void LoadCategories()
        {
            try
            {
                var categories = _context.DanhMucs
                    .Where(c => !c.isDelete)
                    .Select(c => new
                    {
                        c.id,
                        c.ten,
                        SoLuongSanPham = c.SanPhamDanhMucs.Count(sp => !sp.isDelete),
                        TrangThai = c.isDelete ? "Không hoạt động" : "Hoạt động"
                    })
                    .OrderBy(c => c.ten)
                    .ToList();

                dgvCategories.DataSource = categories;

                // Update status
                lblStatus.Text = $"Tổng số: {categories.Count} danh mục";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách danh mục: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCategories_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCategories.CurrentRow != null && !_isAddMode)
            {
                _selectedCategoryId = dgvCategories.CurrentRow.Cells["colId"].Value?.ToString();
                LoadCategoryDetail(_selectedCategoryId);
            }
        }

        private void LoadCategoryDetail(string categoryId)
        {
            if (string.IsNullOrEmpty(categoryId))
                return;

            try
            {
                var category = _context.DanhMucs.Find(categoryId);

                if (category != null)
                {
                    txtCategoryId.Text = category.id;
                    txtCategoryName.Text = category.ten;

                    // Load số lượng sản phẩm
                    int productCount = _context.SanPhamDanhMucs
                        .Count(sp => sp.danhMucId == categoryId && !sp.isDelete);
                    
                    lblProductCount.Text = $"Số sản phẩm: {productCount}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết danh mục: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetFormMode(true);
            _isAddMode = true;
            ClearForm();
            txtCategoryId.Text = "Tự động tạo";
            txtCategoryName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCategories.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần chỉnh sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetFormMode(true);
            _isAddMode = false;
            txtCategoryName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCategories.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xem danh mục có sản phẩm không
            int productCount = _context.SanPhamDanhMucs
                .Count(sp => sp.danhMucId == _selectedCategoryId && !sp.isDelete);

            if (productCount > 0)
            {
                MessageBox.Show(
                    $"Không thể xóa danh mục này vì có {productCount} sản phẩm đang sử dụng!\n\n" +
                    "Vui lòng xóa hoặc chuyển các sản phẩm sang danh mục khác trước.",
                    "Không thể xóa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa danh mục '{txtCategoryName.Text}'?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var category = _context.DanhMucs.Find(_selectedCategoryId);
                    if (category != null)
                    {
                        category.isDelete = true;
                        _context.SaveChanges();

                        MessageBox.Show("Xóa danh mục thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadCategories();
                        ClearForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa danh mục: {ex.Message}", "Lỗi",
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
                    bool exists = _context.DanhMucs
                        .Any(c => c.ten.ToLower() == txtCategoryName.Text.Trim().ToLower() && !c.isDelete);

                    if (exists)
                    {
                        MessageBox.Show("Tên danh mục đã tồn tại!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCategoryName.Focus();
                        return;
                    }

                    // Thêm mới
                    var newCategory = new DanhMuc
                    {
                        id = Guid.NewGuid().ToString(),
                        ten = txtCategoryName.Text.Trim(),
                        isDelete = false
                    };

                    _context.DanhMucs.Add(newCategory);
                    _context.SaveChanges();

                    MessageBox.Show("Thêm danh mục thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Kiểm tra trùng tên (trừ chính nó)
                    bool exists = _context.DanhMucs
                        .Any(c => c.id != _selectedCategoryId && 
                             c.ten.ToLower() == txtCategoryName.Text.Trim().ToLower() && 
                             !c.isDelete);

                    if (exists)
                    {
                        MessageBox.Show("Tên danh mục đã tồn tại!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCategoryName.Focus();
                        return;
                    }

                    // Cập nhật
                    var category = _context.DanhMucs.Find(_selectedCategoryId);
                    if (category != null)
                    {
                        category.ten = txtCategoryName.Text.Trim();
                        _context.SaveChanges();

                        MessageBox.Show("Cập nhật danh mục thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                LoadCategories();
                SetFormMode(false);
                _isAddMode = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu danh mục: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetFormMode(false);
            _isAddMode = false;

            if (dgvCategories.CurrentRow != null)
            {
                LoadCategoryDetail(_selectedCategoryId);
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
                    LoadCategories();
                    return;
                }

                var categories = _context.DanhMucs
                    .Where(c => !c.isDelete && c.ten.ToLower().Contains(searchText))
                    .Select(c => new
                    {
                        c.id,
                        c.ten,
                        SoLuongSanPham = c.SanPhamDanhMucs.Count(sp => !sp.isDelete),
                        TrangThai = c.isDelete ? "Không hoạt động" : "Hoạt động"
                    })
                    .OrderBy(c => c.ten)
                    .ToList();

                dgvCategories.DataSource = categories;
                lblStatus.Text = $"Tìm thấy: {categories.Count} danh mục";
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
            LoadCategories();
            ClearForm();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xuất Excel đang được phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategoryName.Focus();
                return false;
            }

            if (txtCategoryName.Text.Trim().Length < 2)
            {
                MessageBox.Show("Tên danh mục phải có ít nhất 2 ký tự!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategoryName.Focus();
                return false;
            }

            return true;
        }

        private void SetFormMode(bool isEditMode)
        {
            // Enable/disable input fields
            txtCategoryName.Enabled = isEditMode;
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
            dgvCategories.Enabled = !isEditMode;
        }

        private void ClearForm()
        {
            txtCategoryId.Clear();
            txtCategoryName.Clear();
            lblProductCount.Text = "Số sản phẩm: 0";
            _selectedCategoryId = null;
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

        // Event handler cho Enter key trong txtCategoryName
        private void txtCategoryName_KeyPress(object sender, KeyPressEventArgs e)
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
