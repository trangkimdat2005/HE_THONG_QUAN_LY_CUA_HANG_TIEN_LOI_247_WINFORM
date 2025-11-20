using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Products
{
    public partial class frmProducts : Form
    {
        private readonly ProductController _productController;
        private readonly AppDbContext _context;
        private string _imagePath = string.Empty;
        private readonly ProductController _controller;
        private AppDbContext _context;
        private string _selectedId;
        private bool _isAddMode = false;
        private bool _isFirstLoad = true; // Để kiểm tra load lần đầu

        public frmProducts()
        {
            InitializeComponent();
            _controller = new ProductController();
            _context = new AppDbContext();
            
            // Wire up events
            this.Load += frmProducts_Load;
            this.Activated += frmProducts_Activated;
            dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;
            
            cmbBrand.SelectionChangeCommitted += (s, e) => LoadProducts();
            cmbCategory.SelectionChangeCommitted += (s, e) => LoadProducts();
        }

        private void frmProducts_Load(object sender, EventArgs e)
        {
            try
            {
                LoadComboBoxes();
                LoadProducts();
                ToggleEditMode(false);
                _isFirstLoad = false; // Đã load xong lần đầu
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmProducts_Activated(object sender, EventArgs e)
        {
            // Refresh ComboBoxes khi quay lại từ form khác
            if (!_isAddMode && !_isFirstLoad)
            {
                RefreshComboBoxes();
            }
        }

        #region Initialize & Refresh

        private void LoadComboBoxes()
        {
            try
            {
                // Brands (filter)
                var brands = _context.NhanHieux.Where(x => !x.isDelete).OrderBy(x => x.ten).ToList();
                brands.Insert(0, new NhanHieu { id = "", ten = "-- Tất cả --" });
                cmbBrand.DataSource = brands;
                cmbBrand.DisplayMember = "ten";
                cmbBrand.ValueMember = "id";

                // Categories (filter)
                var categories = _context.DanhMucs.Where(x => !x.isDelete).OrderBy(x => x.ten).ToList();
                categories.Insert(0, new DanhMuc { id = "", ten = "-- Tất cả --" });
                cmbCategory.DataSource = categories;
                cmbCategory.DisplayMember = "ten";
                cmbCategory.ValueMember = "id";

                // Goods (detail panel)
                var goods = _controller.GetAllGoods();
                cmbGoodDetail.DataSource = goods;
                cmbGoodDetail.DisplayMember = "ten";
                cmbGoodDetail.ValueMember = "id";
                cmbGoodDetail.SelectedIndex = -1;

                // Units (detail panel)
                var units = _controller.GetAllUnits();
                cmbUnitDetail.DataSource = units;
                cmbUnitDetail.DisplayMember = "ten";
                cmbUnitDetail.ValueMember = "id";
                cmbUnitDetail.SelectedIndex = -1;

                // Status
                cmbStatusDetail.DataSource = new[]
                {
                    new { Value = "Available", Text = "Có sẵn" },
                    new { Value = "Unavailable", Text = "Không có sẵn" }
                };
                cmbStatusDetail.DisplayMember = "Text";
                cmbStatusDetail.ValueMember = "Value";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load combobox: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshComboBoxes()
        {
            try
            {
                // Dispose context cũ và tạo mới để lấy dữ liệu fresh
                _context?.Dispose();
                _context = new AppDbContext();

                // Lưu giá trị đang chọn
                var selectedBrandId = cmbBrand.SelectedValue?.ToString();
                var selectedCategoryId = cmbCategory.SelectedValue?.ToString();
                var selectedGoodId = cmbGoodDetail.SelectedValue?.ToString();
                var selectedUnitId = cmbUnitDetail.SelectedValue?.ToString();

                // Reload Brands
                var brands = _context.NhanHieux.Where(x => !x.isDelete).OrderBy(x => x.ten).ToList();
                brands.Insert(0, new NhanHieu { id = "", ten = "-- Tất cả --" });
                cmbBrand.DataSource = brands;
                cmbBrand.DisplayMember = "ten";
                cmbBrand.ValueMember = "id";
                if (!string.IsNullOrEmpty(selectedBrandId))
                    cmbBrand.SelectedValue = selectedBrandId;

                // Reload Categories
                var categories = _context.DanhMucs.Where(x => !x.isDelete).OrderBy(x => x.ten).ToList();
                categories.Insert(0, new DanhMuc { id = "", ten = "-- Tất cả --" });
                cmbCategory.DataSource = categories;
                cmbCategory.DisplayMember = "ten";
                cmbCategory.ValueMember = "id";
                if (!string.IsNullOrEmpty(selectedCategoryId))
                    cmbCategory.SelectedValue = selectedCategoryId;

                // Reload Goods
                var goods = _controller.GetAllGoods();
                cmbGoodDetail.DataSource = goods;
                cmbGoodDetail.DisplayMember = "ten";
                cmbGoodDetail.ValueMember = "id";
                if (!string.IsNullOrEmpty(selectedGoodId))
                    cmbGoodDetail.SelectedValue = selectedGoodId;
                else
                    cmbGoodDetail.SelectedIndex = -1;

                // Reload Units
                var units = _controller.GetAllUnits();
                cmbUnitDetail.DataSource = units;
                cmbUnitDetail.DisplayMember = "ten";
                cmbUnitDetail.ValueMember = "id";
                if (!string.IsNullOrEmpty(selectedUnitId))
                    cmbUnitDetail.SelectedValue = selectedUnitId;
                else
                    cmbUnitDetail.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error RefreshComboBoxes: {ex.Message}");
            }
        }

        #endregion

        #region Load Data

        private void LoadProducts()
        {
            try
            {
                var keyword = string.IsNullOrWhiteSpace(txtSearch.Text) ? null : txtSearch.Text;
                var brandId = cmbBrand.SelectedValue?.ToString();
                var categoryId = cmbCategory.SelectedValue?.ToString();

                if (brandId == "") brandId = null;
                if (categoryId == "") categoryId = null;

                var products = _controller.FilterProducts(keyword, brandId, categoryId);
                
                // Sử dụng các cột đã thiết kế trong Designer
                dgvProducts.AutoGenerateColumns = false;
                dgvProducts.DataSource = products;
                
                this.Text = $"Quản lý sản phẩm - Tổng: {products.Count}";

                // CHỈ hiển thị MessageBox khi:
                // 1. Load lần đầu (_isFirstLoad = true)
                // 2. Không có dữ liệu
                // 3. KHÔNG đang ở chế độ Add
                if (_isFirstLoad && products.Count == 0 && !_isAddMode)
                {
                    MessageBox.Show("Không có dữ liệu sản phẩm đơn vị.\n\nVui lòng:\n1. Thêm Hàng hóa ở frmGoods\n2. Thêm Đơn vị ở frmMeasurements\n3. Quay lại đây thêm Sản phẩm đơn vị (Hàng hóa + Đơn vị + Giá)", 
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDetail(string id)
        {
            if (string.IsNullOrEmpty(id)) return;

            try
            {
                var item = _controller.GetProductUnitById(id);
                if (item != null)
                {
                    cmbGoodDetail.SelectedValue = item.sanPhamId;
                    cmbUnitDetail.SelectedValue = item.donViId;
                    txtPrice.Text = item.giaBan.ToString("N0");
                    cmbStatusDetail.SelectedValue = item.trangThai;
                    txtDescription.Text = item.SanPham?.moTa;
                    picProduct.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region CRUD

        private void SaveProduct()
        {
            if (!ValidateInput()) return;

            try
            {
                var productUnit = new SanPhamDonVi
                {
                    id = _isAddMode ? null : _selectedId,
                    sanPhamId = cmbGoodDetail.SelectedValue.ToString(),
                    donViId = cmbUnitDetail.SelectedValue.ToString(),
                    heSoQuyDoi = 1,
                    giaBan = decimal.Parse(txtPrice.Text.Replace(",", "")),
                    trangThai = cmbStatusDetail.SelectedValue.ToString()
                };

                if (_isAddMode)
                {
                    var (success, message, result) = _controller.AddProductUnit(productUnit);
                    if (success)
                    {
                        MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadProducts();
                        ToggleEditMode(false);
                        _isAddMode = false;
                        if (result != null)
                        {
                            _selectedId = result.id;
                            SelectRow(result.id);
                        }
                    }
                    else
                    {
                        MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    var (success, message) = _controller.UpdateProductUnit(productUnit);
                    if (success)
                    {
                        MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadProducts();
                        ToggleEditMode(false);
                        LoadDetail(_selectedId);
                    }
                    else
                    {
                        MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteProduct()
        {
            if (string.IsNullOrEmpty(_selectedId))
            {
                MessageBox.Show("Chọn sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var (success, message) = _controller.DeleteProductUnit(_selectedId);
                if (success)
                {
                    MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts();
                    ClearDetail();
                }
                else
                {
                    MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateInput()
        {
            if (cmbGoodDetail.SelectedValue == null)
            {
                MessageBox.Show("Chọn hàng hóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbGoodDetail.Focus();
                return false;
            }

            if (cmbUnitDetail.SelectedValue == null)
            {
                MessageBox.Show("Chọn đơn vị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbUnitDetail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrice.Text) || !decimal.TryParse(txtPrice.Text.Replace(",", ""), out var price) || price < 0)
            {
                MessageBox.Show("Giá bán không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }

            return true;
        }

        #endregion

        #region Button Events

        private void btnAdd_Click(object sender, EventArgs e)
        {
            RefreshComboBoxes();
            ToggleEditMode(true);
            _isAddMode = true;
            
            // Focus vào ComboBox hàng hóa
            if (cmbGoodDetail.Items.Count > 0)
                cmbGoodDetail.Focus();
            else
                MessageBox.Show("Chưa có hàng hóa!\nVui lòng vào frmGoods để thêm hàng hóa trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_selectedId))
            {
                RefreshComboBoxes();
                ToggleEditMode(true);
                _isAddMode = false;
            }
            else
            {
                MessageBox.Show("Chọn sản phẩm cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) => DeleteProduct();

        private void btnSave_Click(object sender, EventArgs e) => SaveProduct();

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ToggleEditMode(false);
            _isAddMode = false;
            if (!string.IsNullOrEmpty(_selectedId))
                LoadDetail(_selectedId);
            else
                ClearDetail();
        }

        private void btnSearch_Click(object sender, EventArgs e) => LoadProducts();

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            if (cmbBrand.Items.Count > 0) cmbBrand.SelectedIndex = 0;
            if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
            RefreshComboBoxes();
            LoadProducts();
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        picProduct.Image = Image.FromFile(dialog.FileName);
                    }
                    catch { }
                }
            }
        }

        private void btnClearImage_Click(object sender, EventArgs e)
        {
            if (picProduct.Image != null)
            {
                picProduct.Image.Dispose();
                picProduct.Image = null;
            }
        }

        #endregion

        #region Grid Events

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (_isAddMode || dgvProducts.CurrentRow == null) return;

            _selectedId = dgvProducts.CurrentRow.Cells["colId"].Value?.ToString();
            if (!string.IsNullOrEmpty(_selectedId))
                LoadDetail(_selectedId);
        }

        #endregion

        #region Helpers

        private void ToggleEditMode(bool isEdit)
        {
            // Detail controls
            cmbGoodDetail.Enabled = isEdit;
            cmbUnitDetail.Enabled = isEdit;
            txtPrice.Enabled = isEdit;
            cmbStatusDetail.Enabled = isEdit;
            txtDescription.Enabled = isEdit;

            // Buttons
            btnSave.Visible = isEdit;
            btnCancel.Visible = isEdit;
            btnAdd.Enabled = !isEdit;
            btnEdit.Enabled = !isEdit && dgvProducts.CurrentRow != null;
            btnDelete.Enabled = !isEdit && dgvProducts.CurrentRow != null;
            btnExport.Enabled = !isEdit;

            // Search panel
            panelSearch.Enabled = !isEdit;
            dgvProducts.Enabled = !isEdit;

            if (isEdit && string.IsNullOrEmpty(_selectedId))
            {
                _isAddMode = true;
                ClearDetail();
            }
        }

        private void ClearDetail()
        {
            cmbGoodDetail.SelectedIndex = -1;
            cmbUnitDetail.SelectedIndex = -1;
            txtPrice.Clear();
            if (cmbStatusDetail.Items.Count > 0) cmbStatusDetail.SelectedIndex = 0;
            txtDescription.Clear();
            if (picProduct.Image != null)
            {
                picProduct.Image.Dispose();
                picProduct.Image = null;
            }
            _selectedId = null;
        }

        private void SelectRow(string id)
        {
            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                if (row.Cells["colId"].Value?.ToString() == id)
                {
                    row.Selected = true;
                    dgvProducts.CurrentCell = row.Cells[0];
                    dgvProducts.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }

        #endregion

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (picProduct?.Image != null)
            {
                picProduct.Image.Dispose();
                picProduct.Image = null;
            }
            _context?.Dispose();
            _controller?.Dispose();
            base.OnFormClosing(e);
        }
    }
}