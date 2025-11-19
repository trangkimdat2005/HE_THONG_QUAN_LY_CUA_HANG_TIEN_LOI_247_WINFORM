using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models; // Đảm bảo dùng đúng Namespace chứa DTO
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Products
{
    public partial class frmProducts : Form
    {
        private readonly ProductController _productController;
        private readonly AppDbContext _context;
        private string _selectedProductId;
        private bool _isAddMode = false;
        private string _imagePath = string.Empty;
        private bool _isDataLoaded = false;


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
        public frmProducts()
        {
            InitializeComponent();
            _productController = new ProductController();
            _context = new AppDbContext();

            SetPlaceholder(txtSearch, "Nhập tên hoặc mã sản phẩm để tìm...");

            //// --- ĐĂNG KÝ SỰ KIỆN THỦ CÔNG (Đảm bảo hoạt động) ---
            //this.Load += frmProducts_Load;
            //if (btnAdd != null) this.btnAdd.Click += btnAdd_Click;
            //if (btnEdit != null) this.btnEdit.Click += btnEdit_Click;
            //if (btnDelete != null) this.btnDelete.Click += btnDelete_Click;
            //if (btnSave != null) this.btnSave.Click += btnSave_Click;
            //if (btnCancel != null) this.btnCancel.Click += btnCancel_Click;
            //if (btnSearch != null) this.btnSearch.Click += btnSearch_Click;
            //if (btnRefresh != null) this.btnRefresh.Click += btnRefresh_Click;
            //if (btnBrowseImage != null) this.btnBrowseImage.Click += btnBrowseImage_Click;
            //if (btnClearImage != null) this.btnClearImage.Click += btnClearImage_Click;
            if (txtSearch != null)
            {
                txtSearch.TextChanged += (s, e) => { LoadProducts(); };
            }

            if (txtSearch != null) txtSearch.KeyPress += (s, e) => { if (e.KeyChar == 13) { LoadProducts(); e.Handled = true; } };

            if (dgvProducts != null) this.dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;
        }

        #region Form Load & Initialization

        private async void frmProducts_Load(object sender, EventArgs e)
        {
            try
            {
                if (lblTitle != null) lblTitle.Text = "ĐANG TẢI DỮ LIỆU...";
                DisableControlsWhileLoading();
                await LoadDataAsync();
                SetFormMode(false);
                _isDataLoaded = true;
                if (lblTitle != null) lblTitle.Text = "QUẢN LÝ SẢN PHẨM";
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

        private void DisableControlsWhileLoading()
        {
            if (panelSearch != null) panelSearch.Enabled = false;
            if (panelButtons != null) panelButtons.Enabled = false;
            if (dgvProducts != null) dgvProducts.Enabled = false;
        }

        private void EnableControlsAfterLoading()
        {
            if (panelSearch != null) panelSearch.Enabled = true;
            if (panelButtons != null) panelButtons.Enabled = true;
            if (dgvProducts != null) dgvProducts.Enabled = true;
        }

        #endregion

        #region Data Loading

        private async Task LoadDataAsync()
        {
            await Task.Run(() =>
            {
                if (this.IsDisposed || this.Disposing) return;
                this.Invoke((MethodInvoker)delegate
                {
                    LoadBrands();
                    LoadCategories();
                    LoadStatuses(); // <--- Quan trọng: Load danh sách trạng thái
                    LoadProducts();
                });
            });
        }

        private void LoadBrands()
        {
            try
            {
                var brands = _context.NhanHieux.Where(b => !b.isDelete).OrderBy(b => b.ten).ToList();
                var filterBrands = new List<NhanHieu> { new NhanHieu { id = "", ten = "-- Tất cả --" } };
                filterBrands.AddRange(brands);

                if (cmbBrand != null)
                {
                    cmbBrand.DataSource = filterBrands;
                    cmbBrand.DisplayMember = "ten";
                    cmbBrand.ValueMember = "id";
                }
                if (cmbBrandDetail != null)
                {
                    cmbBrandDetail.DataSource = new List<NhanHieu>(brands);
                    cmbBrandDetail.DisplayMember = "ten";
                    cmbBrandDetail.ValueMember = "id";
                }
            }
            catch { }
        }

        private void LoadCategories()
        {
            try
            {
                var cats = _context.DanhMucs.Where(c => !c.isDelete).OrderBy(c => c.ten).ToList();
                var filterCats = new List<DanhMuc> { new DanhMuc { id = "", ten = "-- Tất cả --" } };
                filterCats.AddRange(cats);

                // Combo tìm kiếm
                if (cmbCategory != null)
                {
                    cmbCategory.DataSource = filterCats;
                    cmbCategory.DisplayMember = "ten";
                    cmbCategory.ValueMember = "id";
                }

                // Combo chi tiết (SỬA LẠI TÊN BIẾN CHO ĐÚNG Ô DANH MỤC)
                // Giả sử ô Danh mục chi tiết tên là cmbCategoryDetail
                if (cmbCategoryDetail != null)
                {
                    cmbCategoryDetail.DataSource = new List<DanhMuc>(cats);
                    cmbCategoryDetail.DisplayMember = "ten";
                    cmbCategoryDetail.ValueMember = "id";
                }
            }
            catch { }
        }

        private void LoadStatuses()
        {
            // Tạo list object cho trạng thái (Không dùng Entity SanPhamDonVi ở đây)
            var statusList = new List<object>
            {
                new { Value = false, Text = "Đang kinh doanh" },
                new { Value = true, Text = "Ngừng kinh doanh" }
            };

            // Giả sử ô Trạng thái chi tiết tên là cmbStatusDetail
            if (cmbStatusDetail != null)
            {
                cmbStatusDetail.DataSource = statusList;
                cmbStatusDetail.DisplayMember = "Text";
                cmbStatusDetail.ValueMember = "Value";
            }
        }

        private void LoadProducts()
        {
            try
            {
                string keyword = (txtSearch != null && !string.IsNullOrWhiteSpace(txtSearch.Text)) ? txtSearch.Text : null;
                string brandId = (cmbBrand != null && cmbBrand.SelectedValue != null && !string.IsNullOrEmpty(cmbBrand.SelectedValue.ToString()))
                    ? cmbBrand.SelectedValue.ToString() : null;
                string categoryId = (cmbCategory != null && cmbCategory.SelectedValue != null && !string.IsNullOrEmpty(cmbCategory.SelectedValue.ToString()))
                    ? cmbCategory.SelectedValue.ToString() : null;

                var products = _productController.FilterProducts(keyword, brandId, categoryId);

                if (dgvProducts != null)
                {
                    dgvProducts.AutoGenerateColumns = false;
                    dgvProducts.DataSource = products;

                    // Mapping cột thủ công để tránh lỗi
                    foreach (DataGridViewColumn col in dgvProducts.Columns)
                    {
                        if (col.Name == "Id" || col.HeaderText.Contains("Mã")) col.DataPropertyName = "Id";
                        else if (col.Name == "Ten" || col.HeaderText.Contains("Tên")) col.DataPropertyName = "Ten";
                        else if (col.Name == "NhanHieu" || col.HeaderText.Contains("Nhãn")) col.DataPropertyName = "NhanHieu";
                        else if (col.Name == "DanhMuc" || col.HeaderText.Contains("Danh")) col.DataPropertyName = "DanhMuc";
                        else if (col.Name == "MoTa" || col.HeaderText.Contains("Mô")) col.DataPropertyName = "MoTa";
                        else if (col.Name == "GiaBan" || col.HeaderText.Contains("Giá"))
                        {
                            col.DataPropertyName = "GiaBan";
                            col.DefaultCellStyle.Format = "N0";
                            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                        else if (col.Name == "DonVi" || col.HeaderText.Contains("Đơn")) col.DataPropertyName = "DonVi";
                        else if (col.Name == "TrangThai" || col.HeaderText.Contains("Trạng")) col.DataPropertyName = "TrangThai";
                    }
                }
                if (this.Visible) this.Text = $"Quản lý sản phẩm - Tổng: {products.Count}";
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi hiển thị danh sách: {ex.Message}"); }
        }

        private void LoadProductDetail(string productId)
        {
            if (string.IsNullOrEmpty(productId)) return;
            try
            {
                // Lấy chi tiết sản phẩm (bao gồm cả bảng giá SanPhamDonVi)
                var p = _productController.GetProductById(productId);
                if (p != null)
                {
                    if (txtProductId != null) txtProductId.Text = p.id;
                    if (txtProductName != null) txtProductName.Text = p.ten;

                    if (p.nhanHieuId != null && cmbBrandDetail != null)
                        cmbBrandDetail.SelectedValue = p.nhanHieuId;

                    if (txtDescription != null) txtDescription.Text = p.moTa;

                    // Load Danh mục
                    var catId = _productController.GetProductCategoryId(productId);
                    if (cmbCategoryDetail != null) // Sửa đúng tên biến Danh mục
                    {
                        if (!string.IsNullOrEmpty(catId)) cmbCategoryDetail.SelectedValue = catId;
                        else cmbCategoryDetail.SelectedIndex = -1;
                    }

                    // Load Trạng thái (isDelete)
                    if (cmbStatusDetail != null)
                        cmbStatusDetail.SelectedValue = p.isDelete;

                    // --- XỬ LÝ HIỂN THỊ GIÁ TIỀN ---
                    if (p.SanPhamDonVis != null && p.SanPhamDonVis.Count > 0)
                    {
                        // Lấy giá của đơn vị cơ bản (hệ số quy đổi nhỏ nhất)
                        var donViCoBan = p.SanPhamDonVis
                                            .Where(dv => !dv.isDelete)
                                            .OrderBy(dv => dv.heSoQuyDoi)
                                            .FirstOrDefault();

                        // Giả sử TextBox giá tiền tên là txtPrice (hoặc txtGiaBan)
                        if (txtPrice != null)
                        {
                            txtPrice.Text = donViCoBan != null ? donViCoBan.giaBan.ToString("N0") : "0";
                        }
                    }
                    else
                    {
                        if (txtPrice != null) txtPrice.Text = "0";
                    }

                    if (picProduct != null) picProduct.Image = null;
                    _imagePath = string.Empty;
                }
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi tải chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        #endregion

        #region CRUD Operations (Add/Update/Delete)

        private void AddProduct()
        {
            if (!ValidateInput()) return;
            try
            {
                var newProduct = new SanPham
                {
                    ten = txtProductName.Text.Trim(),
                    nhanHieuId = cmbBrandDetail.SelectedValue.ToString(),
                    moTa = txtDescription != null ? txtDescription.Text.Trim() : ""
                };
                // Lấy categoryId từ đúng ô Danh mục
                string categoryId = cmbCategoryDetail != null ? cmbCategoryDetail.SelectedValue?.ToString() : null;

                var (success, message, product) = _productController.AddProduct(newProduct, categoryId);

                if (success)
                {
                    MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts();
                    SetFormMode(false);
                    _isAddMode = false;
                }
                else MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void UpdateProduct()
        {
            if (!ValidateInput()) return;
            try
            {
                string currentId = txtProductId.Text.Trim();
                var updatedProduct = new SanPham
                {
                    id = currentId,
                    ten = txtProductName.Text.Trim(),
                    nhanHieuId = cmbBrandDetail.SelectedValue.ToString(),
                    moTa = txtDescription != null ? txtDescription.Text.Trim() : ""
                };
                string categoryId = cmbCategoryDetail != null ? cmbCategoryDetail.SelectedValue?.ToString() : null;

                var (success, message) = _productController.UpdateProduct(updatedProduct, categoryId);

                if (success)
                {
                    MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts();
                    SetFormMode(false);
                    _isAddMode = false;
                }
                else MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi cập nhật: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void DeleteProduct()
        {
            if (string.IsNullOrEmpty(_selectedProductId))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này? (Giá bán cũng sẽ bị ẩn)", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var (success, message) = _productController.DeleteProduct(_selectedProductId);
                if (success)
                {
                    MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProducts();
                    ClearForm();
                }
                else MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Event Handlers

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetFormMode(true);
            _isAddMode = true;
            ClearForm();
            if (txtProductId != null) txtProductId.Text = _productController.GenerateNewProductId();
            if (txtProductName != null) txtProductName.Focus();
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
            if (txtProductName != null) txtProductName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e) => DeleteProduct();

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

        private void btnSearch_Click(object sender, EventArgs e) => LoadProducts();

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (txtSearch != null) txtSearch.Clear();
            if (cmbBrand != null && cmbBrand.Items.Count > 0) cmbBrand.SelectedIndex = 0;
            if (cmbCategory != null && cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;
            LoadProducts();
            ClearForm();
        }

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow != null && !_isAddMode && _isDataLoaded)
            {
                // Sử dụng đúng DTO mới: ProductDetailDto
                var selectedItem = dgvProducts.CurrentRow.DataBoundItem as ProductDetailDto;
                if (selectedItem != null)
                {
                    _selectedProductId = selectedItem.Id;
                    LoadProductDetail(_selectedProductId);

                    if (btnEdit != null) btnEdit.Enabled = true;
                    if (btnDelete != null) btnDelete.Enabled = true;
                }
            }
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _imagePath = openFileDialog.FileName;
                        if (picProduct != null) picProduct.Image = Image.FromFile(_imagePath);
                    }
                    catch { }
                }
            }
        }

        private void btnClearImage_Click(object sender, EventArgs e)
        {
            if (picProduct != null) picProduct.Image = null;
            _imagePath = string.Empty;
        }

        #endregion

        #region Helper Methods

        private bool ValidateInput()
        {
            if (txtProductName != null && string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Nhập tên sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductName.Focus();
                return false;
            }
            if (cmbBrandDetail != null && cmbBrandDetail.SelectedValue == null)
            {
                MessageBox.Show("Chọn nhãn hiệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbBrandDetail.Focus();
                return false;
            }
            return true;
        }

        private void SetFormMode(bool isEdit)
        {
            if (txtProductName != null) txtProductName.Enabled = isEdit;
            if (cmbBrandDetail != null) cmbBrandDetail.Enabled = isEdit;
            // Bật tắt các combo chi tiết
            if (cmbCategoryDetail != null) cmbCategoryDetail.Enabled = isEdit;
            if (cmbStatusDetail != null) cmbStatusDetail.Enabled = isEdit;

            if (txtDescription != null) txtDescription.Enabled = isEdit;
            if (txtPrice != null) txtPrice.Enabled = false; // Giá thường không sửa ở đây mà sửa ở form quản lý giá riêng

            if (btnBrowseImage != null) btnBrowseImage.Enabled = isEdit;
            if (btnClearImage != null) btnClearImage.Enabled = isEdit;
            if (btnSave != null) btnSave.Enabled = isEdit;
            if (btnCancel != null) btnCancel.Enabled = isEdit;

            if (btnAdd != null) btnAdd.Enabled = !isEdit;
            if (btnEdit != null) btnEdit.Enabled = !isEdit && dgvProducts.CurrentRow != null;
            if (btnDelete != null) btnDelete.Enabled = !isEdit && dgvProducts.CurrentRow != null;

            if (panelSearch != null) panelSearch.Enabled = !isEdit;
            if (dgvProducts != null) dgvProducts.Enabled = !isEdit;
            if (txtProductId != null) txtProductId.Enabled = false;
        }

        private void ClearForm()
        {
            if (txtProductId != null) txtProductId.Clear();
            if (txtProductName != null) txtProductName.Clear();
            if (txtDescription != null) txtDescription.Clear();
            if (txtPrice != null) txtPrice.Text = "";

            if (cmbBrandDetail != null) cmbBrandDetail.SelectedIndex = -1;
            if (cmbCategoryDetail != null) cmbCategoryDetail.SelectedIndex = -1;
            if (cmbStatusDetail != null) cmbStatusDetail.SelectedIndex = -1;

            if (picProduct != null) picProduct.Image = null;
            _imagePath = string.Empty;
            _selectedProductId = null;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (picProduct != null && picProduct.Image != null)
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