using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Products
{
    public partial class frmProducts : Form
    {
        private readonly ProductController _controller;
        private AppDbContext _context;
        private string _selectedId;
        private bool _isAddMode = false;

        public frmProducts()
        {
            InitializeComponent();
            _controller = new ProductController();
            _context = new AppDbContext();

            // --- 1. GÁN SỰ KIỆN THỦ CÔNG (Đảm bảo nút bấm ăn sự kiện) ---
            this.Load += frmProducts_Load;
            dgvProducts.CellClick += dgvProducts_CellClick;

            // Search & Filter
            btnSearch.Click += btnSearch_Click;
            btnRefresh.Click += btnRefresh_Click;
            cmbBrand.SelectionChangeCommitted += (s, e) => LoadData();
            cmbCategory.SelectionChangeCommitted += (s, e) => LoadData();

            // CRUD Buttons
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;
            btnExport.Click += (s, e) => MessageBox.Show("Tính năng đang phát triển");

            // Image Buttons
            btnBrowseImage.Click += btnBrowseImage_Click;
            btnClearImage.Click += btnClearImage_Click;
        }

        private void frmProducts_Load(object sender, EventArgs e)
        {
            try
            {
                // --- 2. KHẮC PHỤC LỖI GIAO DIỆN BẰNG CODE ---
                FixUI_Logic();

                LoadFilterComboBoxes();
                LoadDetailComboBoxes();
                LoadData();
                ToggleEditMode(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo: {ex.Message}");
            }
        }

        // === HÀM QUAN TRỌNG ĐỂ SỬA LỖI "KHÔNG ẤN ĐƯỢC" ===
        private void FixUI_Logic()
        {
            try
            {
                // === DEBUG: Kiểm tra buttons có tồn tại không ===
                System.Diagnostics.Debug.WriteLine("=== CHECKING BUTTONS ===");
                System.Diagnostics.Debug.WriteLine($"btnAdd: {(btnAdd != null ? "OK" : "NULL")}");
                System.Diagnostics.Debug.WriteLine($"btnEdit: {(btnEdit != null ? "OK" : "NULL")}");
                System.Diagnostics.Debug.WriteLine($"btnDelete: {(btnDelete != null ? "OK" : "NULL")}");
                System.Diagnostics.Debug.WriteLine($"btnSave: {(btnSave != null ? "OK" : "NULL")}");
                
                // === FIX 1: Đảm bảo buttons luôn enabled ===
                if (btnAdd != null)
                {
                    btnAdd.Enabled = true;
                    btnAdd.Visible = true;
                    System.Diagnostics.Debug.WriteLine($"btnAdd Enabled: {btnAdd.Enabled}, Visible: {btnAdd.Visible}");
                }
                
                if (btnEdit != null)
                {
                    btnEdit.Enabled = true;
                    btnEdit.Visible = true;
                }
                
                if (btnDelete != null)
                {
                    btnDelete.Enabled = true;
                    btnDelete.Visible = true;
                }

                // === FIX 2: BringToFront các panel chứa buttons ===
                // Quan trọng: Đưa panel buttons lên trên để không bị che
                if (panelTop != null) panelTop.BringToFront();
                if (panelSearch != null) panelSearch.BringToFront();
                if (panelButtons != null)
                {
                    panelButtons.BringToFront();
                    panelButtons.Enabled = true;
                    panelButtons.Visible = true;
                    System.Diagnostics.Debug.WriteLine($"panelButtons: Enabled={panelButtons.Enabled}, Visible={panelButtons.Visible}");
                }
                if (panelDetail != null) panelDetail.BringToFront();

                // === FIX 3: Mapping GridView columns ===
                if (dgvProducts.Columns.Contains("colId")) 
                    dgvProducts.Columns["colId"].DataPropertyName = "Id";
                if (dgvProducts.Columns.Contains("colProductName")) 
                    dgvProducts.Columns["colProductName"].DataPropertyName = "TenSanPham";
                if (dgvProducts.Columns.Contains("colBrand")) 
                    dgvProducts.Columns["colBrand"].DataPropertyName = "NhanHieu";
                if (dgvProducts.Columns.Contains("colCategory")) 
                    dgvProducts.Columns["colCategory"].DataPropertyName = "DanhMuc";
                if (dgvProducts.Columns.Contains("donVi")) 
                    dgvProducts.Columns["donVi"].DataPropertyName = "DonVi";
                if (dgvProducts.Columns.Contains("giaBan"))
                {
                    dgvProducts.Columns["giaBan"].DataPropertyName = "GiaBan";
                    dgvProducts.Columns["giaBan"].DefaultCellStyle.Format = "N0";
                }
                if (dgvProducts.Columns.Contains("trangThai")) 
                    dgvProducts.Columns["trangThai"].DataPropertyName = "TrangThai";
                
                System.Diagnostics.Debug.WriteLine("=== FIX UI COMPLETE ===");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR in FixUI_Logic: {ex.Message}");
                MessageBox.Show($"Lỗi khởi tạo UI: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #region Load Data & Logic

        private void LoadFilterComboBoxes()
        {
            var brands = _context.NhanHieux.Where(x => !x.isDelete).OrderBy(x => x.ten).ToList();
            brands.Insert(0, new NhanHieu { id = "", ten = "-- Tất cả --" });
            cmbBrand.DataSource = brands;
            cmbBrand.DisplayMember = "ten";
            cmbBrand.ValueMember = "id";
            cmbBrand.SelectedIndex = 0;

            var categories = _context.DanhMucs.Where(x => !x.isDelete).OrderBy(x => x.ten).ToList();
            categories.Insert(0, new DanhMuc { id = "", ten = "-- Tất cả --" });
            cmbCategory.DataSource = categories;
            cmbCategory.DisplayMember = "ten";
            cmbCategory.ValueMember = "id";
            cmbCategory.SelectedIndex = 0;
        }

        private void LoadDetailComboBoxes()
        {
            try
            {
                var goods = _controller.GetAllGoods(); // Trả về List object {id, ten}
                cmbGoodDetail.DataSource = goods;
                cmbGoodDetail.DisplayMember = "ten";
                cmbGoodDetail.ValueMember = "id";
                cmbGoodDetail.SelectedIndex = -1;

                var units = _controller.GetAllUnits();
                cmbUnitDetail.DataSource = units;
                cmbUnitDetail.DisplayMember = "ten";
                cmbUnitDetail.ValueMember = "id";
                cmbUnitDetail.SelectedIndex = -1;

                cmbStatusDetail.DataSource = new[]
                {
                    new { Value = "Available", Text = "Đang kinh doanh" }, // Value phải khớp database
                    new { Value = "Unavailable", Text = "Ngừng kinh doanh" }
                };
                cmbStatusDetail.DisplayMember = "Text";
                cmbStatusDetail.ValueMember = "Value";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu nhập liệu: " + ex.Message);
            }
        }

        private void LoadData()
        {
            try
            {
                string keyword = txtSearch.Text.Trim();
                string brandId = cmbBrand.SelectedValue?.ToString();
                string categoryId = cmbCategory.SelectedValue?.ToString();

                if (brandId == "") brandId = null;
                if (categoryId == "") categoryId = null;

                var productList = _controller.FilterProducts(keyword, brandId, categoryId);

                dgvProducts.AutoGenerateColumns = false;
                dgvProducts.DataSource = productList;

                this.Text = $"Quản lý sản phẩm - Tổng: {productList.Count} bản ghi";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách: {ex.Message}");
            }
        }

        #endregion

        #region Grid Events

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || _isAddMode) return;

            try
            {
                var item = dgvProducts.Rows[e.RowIndex].DataBoundItem as ProductDetailDto;

                if (item != null)
                {
                    _selectedId = item.Id;

                    // Binding dữ liệu vào Combobox
                    // Vì ValueMember là ID, gán SelectedValue bằng ID là nó tự nhảy text
                    cmbGoodDetail.SelectedValue = item.SanPhamId;
                    cmbUnitDetail.SelectedValue = item.DonViId;

                    txtPrice.Text = item.GiaBan.ToString("N0");

                    // Binding trạng thái
                    // Cần check xem item.TrangThai có khớp với Value trong datasource không
                    cmbStatusDetail.SelectedValue = item.TrangThai;

                    picProduct.Image = null;

                    ToggleEditMode(false); // Enable nút Sửa/Xóa
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị chi tiết: " + ex.Message);
            }
        }

        #endregion

        #region Buttons Events

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ResetInputs();
            ToggleEditMode(true);
            _isAddMode = true;

            // Focus để nhập liệu ngay
            if (cmbGoodDetail.Items.Count > 0) cmbGoodDetail.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedId))
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa trên lưới!");
                return;
            }
            ToggleEditMode(true);
            _isAddMode = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedId)) return;

            if (MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này không?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var (success, message) = _controller.DeleteProductUnit(_selectedId);
                if (success)
                {
                    MessageBox.Show(message);
                    LoadData();
                    ResetInputs();
                    ToggleEditMode(false); // Reset trạng thái nút
                }
                else
                {
                    MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                var model = new SanPhamDonVi
                {
                    id = _isAddMode ? null : _selectedId,
                    sanPhamId = cmbGoodDetail.SelectedValue.ToString(),
                    donViId = cmbUnitDetail.SelectedValue.ToString(),
                    giaBan = decimal.Parse(txtPrice.Text.Replace(",", "").Replace(".", "")),
                    trangThai = cmbStatusDetail.SelectedValue != null ? cmbStatusDetail.SelectedValue.ToString() : "Available",
                    heSoQuyDoi = 1,
                    isDelete = false
                };

                bool success;
                string message;
                SanPhamDonVi result = null;

                if (_isAddMode)
                    (success, message, result) = _controller.AddProductUnit(model);
                else
                    (success, message) = _controller.UpdateProductUnit(model);

                if (success)
                {
                    MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ToggleEditMode(false);
                    _isAddMode = false;
                    ResetInputs();
                }
                else
                {
                    MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu dữ liệu: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ToggleEditMode(false);
            _isAddMode = false;
            if (string.IsNullOrEmpty(_selectedId)) ResetInputs();
        }

        private void btnSearch_Click(object sender, EventArgs e) => LoadData();

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbBrand.SelectedIndex = 0;
            cmbCategory.SelectedIndex = 0;
            LoadData();
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try { picProduct.Image = Image.FromFile(dialog.FileName); } catch { }
                }
            }
        }

        private void btnClearImage_Click(object sender, EventArgs e)
        {
            if (picProduct.Image != null) picProduct.Image = null;
        }

        #endregion

        #region Helpers

        private bool ValidateInput()
        {
            if (cmbGoodDetail.SelectedIndex == -1) { MessageBox.Show("Vui lòng chọn Hàng hóa!"); return false; }
            if (cmbUnitDetail.SelectedIndex == -1) { MessageBox.Show("Vui lòng chọn Đơn vị tính!"); return false; }
            if (string.IsNullOrWhiteSpace(txtPrice.Text)) { MessageBox.Show("Vui lòng nhập Giá bán!"); return false; }
            if (!decimal.TryParse(txtPrice.Text.Replace(",", "").Replace(".", ""), out decimal price) || price < 0)
            {
                MessageBox.Show("Giá bán không hợp lệ!");
                return false;
            }
            return true;
        }

        private void ResetInputs()
        {
            _selectedId = null;
            cmbGoodDetail.SelectedIndex = -1;
            cmbUnitDetail.SelectedIndex = -1;
            txtPrice.Clear();
            txtDescription.Clear();
            picProduct.Image = null;
            if (cmbStatusDetail.Items.Count > 0) cmbStatusDetail.SelectedIndex = 0;
        }

        // Logic khóa/mở nút bấm
        private void ToggleEditMode(bool isEditing)
        {
            // Vùng nhập liệu
            cmbGoodDetail.Enabled = isEditing;
            cmbUnitDetail.Enabled = isEditing;
            txtPrice.Enabled = isEditing;
            cmbStatusDetail.Enabled = isEditing;

            // Nút bấm
            btnSave.Visible = isEditing;
            btnCancel.Visible = isEditing;

            // Nút chức năng chính
            btnAdd.Enabled = !isEditing; // Đang sửa thì không được thêm

            // Nút Sửa/Xóa chỉ sáng khi KHÔNG đang sửa VÀ đã chọn 1 dòng
            bool hasSelection = !string.IsNullOrEmpty(_selectedId);
            btnEdit.Enabled = !isEditing && hasSelection;
            btnDelete.Enabled = !isEditing && hasSelection;

            // Grid
            dgvProducts.Enabled = !isEditing;
            panelSearch.Enabled = !isEditing;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _controller?.Dispose();
            _context?.Dispose();
            base.OnFormClosing(e);
        }

        #endregion
    }
}