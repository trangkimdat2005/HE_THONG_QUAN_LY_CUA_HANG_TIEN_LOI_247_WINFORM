using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Products
{
    /// <summary>
    /// Form quản lý sản phẩm với Controller Pattern
    /// Form -> ProductController -> ProductService -> AppDbContext
    /// </summary>
    public partial class frmProducts : Form
    {
        private readonly ProductController _productController;
        private readonly AppDbContext _context;
        private string _selectedProductId;
        private bool _isAddMode = false;
        private string _imagePath = string.Empty;
        private bool _isDataLoaded = false;

        public frmProducts()
        {
            InitializeComponent();
            _productController = new ProductController();
            _context = new AppDbContext();
            RegisterEnterKeyHandlers();
        }

        #region Form Load & Initialization

        private async void frmProducts_Load(object sender, EventArgs e)
        {
            try
            {
                lblTitle.Text = "ĐANG TẢI DỮ LIỆU...";
                DisableControlsWhileLoading();
                await LoadDataAsync();
                SetFormMode(false);
                _isDataLoaded = true;
                lblTitle.Text = "QUẢN LÝ SẢN PHẨM";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnableControlsAfterLoading();
            }
        }

        private void RegisterEnterKeyHandlers()
        {
            txtSearch.KeyPress += (s, e) => { if (e.KeyChar == 13) { LoadProducts(); e.Handled = true; } };
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

        #endregion

        #region Data Loading

        private async Task LoadDataAsync()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    LoadBrands();
                    LoadCategories();
                    LoadProducts();
                });
            });
        }

        private void LoadBrands()
        {
            var brands = _context.NhanHieux.Where(b => !b.isDelete).OrderBy(b => b.ten).ToList();

            var filterBrands = new List<NhanHieu> { new NhanHieu { id = "", ten = "-- Tất cả --" } };
            filterBrands.AddRange(brands);
            cmbBrand.DataSource = filterBrands;
            cmbBrand.DisplayMember = "ten";
            cmbBrand.ValueMember = "id";

            cmbBrandDetail.DataSource = new List<NhanHieu>(brands);
            cmbBrandDetail.DisplayMember = "ten";
            cmbBrandDetail.ValueMember = "id";
        }

        private void LoadCategories()
        {
            var cats = _context.DanhMucs.Where(c => !c.isDelete).OrderBy(c => c.ten).ToList();

            var filterCats = new List<DanhMuc> { new DanhMuc { id = "", ten = "-- Tất cả --" } };
            filterCats.AddRange(cats);
            cmbCategory.DataSource = filterCats;
            cmbCategory.DisplayMember = "ten";
            cmbCategory.ValueMember = "id";

            cmbStatusDetail.DataSource = new List<DanhMuc>(cats);
            cmbStatusDetail.DisplayMember = "ten";
            cmbStatusDetail.ValueMember = "id";
        }

        private void LoadProducts()
        {
            try
            {
                string keyword = string.IsNullOrWhiteSpace(txtSearch.Text) ? null : txtSearch.Text;
                string brandId = (cmbBrand.SelectedValue != null && !string.IsNullOrEmpty(cmbBrand.SelectedValue.ToString()))
                    ? cmbBrand.SelectedValue.ToString() : null;
                string categoryId = (cmbCategory.SelectedValue != null && !string.IsNullOrEmpty(cmbCategory.SelectedValue.ToString()))
                    ? cmbCategory.SelectedValue.ToString() : null;

                var products = _productController.FilterProducts(keyword, brandId, categoryId);

                var displayList = new List<object>();
                foreach (var p in products)
                {
                    string catName = "";
                    string catId = _productController.GetProductCategoryId(p.id);
                    if (!string.IsNullOrEmpty(catId))
                    {
                        var cat = _context.DanhMucs.Find(catId);
                        if (cat != null) catName = cat.ten;
                    }

                    string brandName = "";
                    if (!string.IsNullOrEmpty(p.nhanHieuId))
                    {
                        var brand = _context.NhanHieux.Find(p.nhanHieuId);
                        if (brand != null) brandName = brand.ten;
                    }

                    displayList.Add(new
                    {
                        p.id,
                        p.ten,
                        NhanHieu = brandName,
                        DanhMuc = catName,
                        p.moTa,
                        isDelete = p.isDelete ? "Ngừng kinh doanh" : "Đang kinh doanh"
                    });
                }

                dgvProducts.DataSource = displayList;
                this.Text = $"Quản lý sản phẩm - Tổng: {displayList.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị danh sách: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductDetail(string productId)
        {
            if (string.IsNullOrEmpty(productId)) return;
            try
            {
                var p = _productController.GetProductById(productId);
                if (p != null)
                {
                    txtProductId.Text = p.id;
                    txtProductName.Text = p.ten;
                    cmbBrandDetail.SelectedValue = p.nhanHieuId;
                    txtDescription.Text = p.moTa;

                    var catId = _productController.GetProductCategoryId(productId);
                    if (!string.IsNullOrEmpty(catId)) cmbStatusDetail.SelectedValue = catId;
                    else cmbStatusDetail.SelectedIndex = -1;

                    picProduct.Image = null;
                    _imagePath = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region CRUD Operations

        private void AddProduct()
        {
            if (!ValidateInput()) return;

            try
            {
                var newProduct = new SanPham
                {
                    ten = txtProductName.Text.Trim(),
                    nhanHieuId = cmbBrandDetail.SelectedValue.ToString(),
                    moTa = txtDescription.Text.Trim()
                };
                string categoryId = cmbStatusDetail.SelectedValue?.ToString();

                var (success, message, product) = _productController.AddProduct(newProduct, categoryId);

                if (success)
                {
                    MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts();
                    SetFormMode(false);
                    _isAddMode = false;
                }
                else
                {
                    MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateProduct()
        {
            if (!ValidateInput()) return;

            try
            {
                string currentId = txtProductId.Text.Trim();

                if (string.IsNullOrEmpty(currentId))
                {
                    MessageBox.Show("Không xác định được mã sản phẩm để sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var updatedProduct = new SanPham
                {
                    id = currentId,
                    ten = txtProductName.Text.Trim(),
                    nhanHieuId = cmbBrandDetail.SelectedValue.ToString(),
                    moTa = txtDescription.Text.Trim()
                };
                string categoryId = cmbStatusDetail.SelectedValue?.ToString();

                var (success, message) = _productController.UpdateProduct(updatedProduct, categoryId);

                if (success)
                {
                    MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts();
                    SetFormMode(false);
                    _isAddMode = false;
                }
                else
                {
                    MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteProduct()
        {
            if (string.IsNullOrEmpty(_selectedProductId))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var (success, message) = _productController.DeleteProduct(_selectedProductId);
                if (success)
                {
                    MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Validation & UI Helpers

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Nhập tên sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductName.Focus();
                return false;
            }
            if (cmbBrandDetail.SelectedValue == null)
            {
                MessageBox.Show("Chọn nhãn hiệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbBrandDetail.Focus();
                return false;
            }
            return true;
        }

        private void SetFormMode(bool isEdit)
        {
            txtProductName.Enabled = isEdit;
            cmbBrandDetail.Enabled = isEdit;
            cmbStatusDetail.Enabled = isEdit;
            txtDescription.Enabled = isEdit;
            btnBrowseImage.Enabled = isEdit;
            btnClearImage.Enabled = isEdit;
            btnSave.Enabled = isEdit;
            btnCancel.Enabled = isEdit;

            btnAdd.Enabled = !isEdit;
            btnEdit.Enabled = !isEdit && dgvProducts.CurrentRow != null;
            btnDelete.Enabled = !isEdit && dgvProducts.CurrentRow != null;
            btnExport.Enabled = !isEdit;

            panelSearch.Enabled = !isEdit;
            dgvProducts.Enabled = !isEdit;
            txtProductId.Enabled = false;
        }

        private void ClearForm()
        {
            txtProductId.Clear();
            txtProductName.Clear();
            txtDescription.Clear();
            cmbBrandDetail.SelectedIndex = -1;
            cmbStatusDetail.SelectedIndex = -1;
            picProduct.Image = null;
            _imagePath = string.Empty;
            _selectedProductId = null;
        }

        #endregion

        #region Event Handlers

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow != null && !_isAddMode && _isDataLoaded)
            {
                if (dgvProducts.Columns.Contains("id"))
                    _selectedProductId = dgvProducts.CurrentRow.Cells["id"].Value?.ToString();
                else if (dgvProducts.Columns.Contains("colId"))
                    _selectedProductId = dgvProducts.CurrentRow.Cells["colId"].Value?.ToString();
                else
                    _selectedProductId = dgvProducts.CurrentRow.Cells[0].Value?.ToString();

                LoadProductDetail(_selectedProductId);
                
                // Update button states
                btnEdit.Enabled = !_isAddMode;
                btnDelete.Enabled = !_isAddMode;
            }
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Reserved for future features
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetFormMode(true);
            _isAddMode = true;
            ClearForm();
            txtProductId.Text = _productController.GenerateNewProductId();
            txtProductName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedProductId))
            {
                MessageBox.Show("Chọn sản phẩm cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetFormMode(true);
            _isAddMode = false;
            txtProductName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteProduct();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_isAddMode) AddProduct();
            else UpdateProduct();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetFormMode(false);
            _isAddMode = false;
            if (!string.IsNullOrEmpty(_selectedProductId)) LoadProductDetail(_selectedProductId);
            else ClearForm();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            if (cmbBrand.Items.Count > 0) cmbBrand.SelectedIndex = 0;
            if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
            LoadProducts();
            ClearForm();
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

        #endregion

        #region Cleanup

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (picProduct.Image != null)
            {
                picProduct.Image.Dispose();
                picProduct.Image = null;
            }
            _context?.Dispose();
            _productController?.Dispose();
            base.OnFormClosing(e);
        }

        #endregion
    }
}