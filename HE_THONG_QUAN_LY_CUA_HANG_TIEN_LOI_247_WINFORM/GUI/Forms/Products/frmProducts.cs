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

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Products
{
    public partial class frmProducts : Form
    {
        private AppDbContext _context;
        private string _selectedProductId;
        private bool _isAddMode = false;
        private string _imagePath = string.Empty;
        private bool _isDataLoaded = false;

        public frmProducts()
        {
            InitializeComponent();
            _context = new AppDbContext();
        }

        private async void frmProducts_Load(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị loading message
                dgvProducts.DataSource = null;
                lblTitle.Text = "QUẢN LÝ SẢN PHẨM - Đang tải dữ liệu...";

                // Disable controls trong khi loading
                DisableControlsWhileLoading();

                // Load data async để không block UI
                await LoadDataAsync();

                SetFormMode(false);
                _isDataLoaded = true;

                lblTitle.Text = "QUẢN LÝ SẢN PHẨM";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblTitle.Text = "QUẢN LÝ SẢN PHẨM - Lỗi tải dữ liệu";
            }
            finally
            {
                EnableControlsAfterLoading();
            }
        }

        private void DisableControlsWhileLoading()
        {
            panelSearch.Enabled = false;
            panelButtons.Enabled = false;
            dgvProducts.Enabled = false;
        }

        private void EnableControlsAfterLoading()
        {
            panelSearch.Enabled = true;
            panelButtons.Enabled = true;
            dgvProducts.Enabled = true;
        }

        private async Task LoadDataAsync()
        {
            // Load all data in parallel
            await Task.Run(() =>
            {
                // Load brands synchronously in background thread
                this.Invoke((MethodInvoker)delegate
                {
                    LoadBrands();
                });
            });

            await Task.Run(() =>
            {
                // Load categories synchronously in background thread
                this.Invoke((MethodInvoker)delegate
                {
                    LoadCategories();
                });
            });

            await Task.Run(() =>
            {
                // Load products synchronously in background thread
                this.Invoke((MethodInvoker)delegate
                {
                    LoadProducts();
                });
            });
        }

        private void LoadBrands()
        {
            try
            {
                var brands = _context.NhanHieux
                    .Where(b => !b.isDelete)
                    .OrderBy(b => b.ten)
                    .ToList();

                // Load for search filter
                var brandListWithAll = new List<NhanHieu> { new NhanHieu { id = "", ten = "-- Tất cả --" } };
                brandListWithAll.AddRange(brands);

                cmbBrand.DataSource = brandListWithAll;
                cmbBrand.DisplayMember = "ten";
                cmbBrand.ValueMember = "id";

                // Load for detail form (clone list to avoid data binding issues)
                cmbBrandDetail.DataSource = brands.ToList();
                cmbBrandDetail.DisplayMember = "ten";
                cmbBrandDetail.ValueMember = "id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhãn hiệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategories()
        {
            try
            {
                var categories = _context.DanhMucs
                    .Where(c => !c.isDelete)
                    .OrderBy(c => c.ten)
                    .ToList();

                var categoryListWithAll = new List<DanhMuc> { new DanhMuc { id = "", ten = "-- Tất cả --" } };
                categoryListWithAll.AddRange(categories);

                cmbCategory.DataSource = categoryListWithAll;
                cmbCategory.DisplayMember = "ten";
                cmbCategory.ValueMember = "id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách danh mục: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                var query = _context.SanPhams
                    .Where(p => !p.isDelete)
                    .AsQueryable();

                // Apply search filter
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    string searchText = txtSearch.Text.ToLower();
                    query = query.Where(p => p.ten.ToLower().Contains(searchText));
                }

                // Apply brand filter
                if (cmbBrand.SelectedValue != null && !string.IsNullOrEmpty(cmbBrand.SelectedValue.ToString()))
                {
                    string brandId = cmbBrand.SelectedValue.ToString();
                    if (!string.IsNullOrEmpty(brandId))
                    {
                        query = query.Where(p => p.nhanHieuId == brandId);
                    }
                }

                // Apply category filter
                if (cmbCategory.SelectedValue != null && !string.IsNullOrEmpty(cmbCategory.SelectedValue.ToString()))
                {
                    string categoryId = cmbCategory.SelectedValue.ToString();
                    if (!string.IsNullOrEmpty(categoryId))
                    {
                        query = query.Where(p => p.SanPhamDanhMucs.Any(spdm => spdm.danhMucId == categoryId && !spdm.isDelete));
                    }
                }

                // Take only 1000 records max to prevent performance issues
                var products = query
                    .Take(1000)
                    .Select(p => new
                    {
                        p.id,
                        p.ten,
                        NhanHieu = p.NhanHieu.ten,
                        DanhMuc = p.SanPhamDanhMucs
                            .Where(spdm => !spdm.isDelete)
                            .Select(spdm => spdm.DanhMuc.ten)
                            .FirstOrDefault() ?? "",
                        p.moTa,
                        isDelete = p.isDelete ? "Ngừng kinh doanh" : "Đang kinh doanh"
                    })
                    .OrderBy(p => p.ten)
                    .ToList();

                dgvProducts.DataSource = products;

                // Update status label
                this.Text = $"Quản lý sản phẩm - Tổng số: {products.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách sản phẩm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow != null && !_isAddMode && _isDataLoaded)
            {
                _selectedProductId = dgvProducts.CurrentRow.Cells["colId"].Value?.ToString();
                LoadProductDetail(_selectedProductId);
            }
        }

        private void LoadProductDetail(string productId)
        {
            if (string.IsNullOrEmpty(productId))
                return;

            try
            {
                var product = _context.SanPhams
                    .FirstOrDefault(p => p.id == productId);

                if (product != null)
                {
                    txtProductId.Text = product.id;
                    txtProductName.Text = product.ten;
                    cmbBrandDetail.SelectedValue = product.nhanHieuId;
                    txtDescription.Text = product.moTa ?? "";

                    // Clear image for now (can be implemented later with actual image storage)
                    picProduct.Image = null;
                    _imagePath = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết sản phẩm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetFormMode(true);
            _isAddMode = true;
            ClearForm();
            txtProductId.Text = "Tự động tạo";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần chỉnh sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetFormMode(true);
            _isAddMode = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa sản phẩm này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var product = _context.SanPhams.Find(_selectedProductId);
                    if (product != null)
                    {
                        product.isDelete = true;
                        _context.SaveChanges();
                        MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadProducts();
                        ClearForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa sản phẩm: {ex.Message}", "Lỗi",
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
                    // Add new product
                    var newProduct = new SanPham
                    {
                        id = Guid.NewGuid().ToString(),
                        ten = txtProductName.Text.Trim(),
                        nhanHieuId = cmbBrandDetail.SelectedValue.ToString(),
                        moTa = txtDescription.Text.Trim(),
                        isDelete = false
                    };

                    _context.SanPhams.Add(newProduct);
                    _context.SaveChanges();

                    MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Update existing product
                    var product = _context.SanPhams.Find(_selectedProductId);
                    if (product != null)
                    {
                        product.ten = txtProductName.Text.Trim();
                        product.nhanHieuId = cmbBrandDetail.SelectedValue.ToString();
                        product.moTa = txtDescription.Text.Trim();

                        _context.SaveChanges();

                        MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                LoadProducts();
                SetFormMode(false);
                _isAddMode = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu sản phẩm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetFormMode(false);
            _isAddMode = false;
            if (dgvProducts.CurrentRow != null)
            {
                LoadProductDetail(_selectedProductId);
            }
            else
            {
                ClearForm();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbBrand.SelectedIndex = 0;
            cmbCategory.SelectedIndex = 0;
            LoadProducts();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xuất Excel đang được phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Chọn ảnh sản phẩm";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _imagePath = openFileDialog.FileName;
                        picProduct.Image = Image.FromFile(_imagePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi tải ảnh: {ex.Message}", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnClearImage_Click(object sender, EventArgs e)
        {
            picProduct.Image = null;
            _imagePath = string.Empty;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductName.Focus();
                return false;
            }

            if (cmbBrandDetail.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhãn hiệu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbBrandDetail.Focus();
                return false;
            }

            return true;
        }

        private void SetFormMode(bool isEditMode)
        {
            // Enable/disable input fields
            txtProductName.Enabled = isEditMode;
            cmbBrandDetail.Enabled = isEditMode;
            txtDescription.Enabled = isEditMode;
            btnBrowseImage.Enabled = isEditMode;
            btnClearImage.Enabled = isEditMode;
            btnSave.Enabled = isEditMode;
            btnCancel.Enabled = isEditMode;

            // Enable/disable action buttons
            btnAdd.Enabled = !isEditMode;
            btnEdit.Enabled = !isEditMode;
            btnDelete.Enabled = !isEditMode;
            btnExport.Enabled = !isEditMode;

            // Enable/disable grid selection
            dgvProducts.Enabled = !isEditMode;
        }

        private void ClearForm()
        {
            txtProductId.Clear();
            txtProductName.Clear();
            txtDescription.Clear();
            cmbBrandDetail.SelectedIndex = -1;
            picProduct.Image = null;
            _imagePath = string.Empty;
            _selectedProductId = null;
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
