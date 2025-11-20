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
        private AppDbContext _context; // Dùng riêng để load Brand/Category filter
        private string _selectedId;    // ID của SanPhamDonVi (bảng trung gian)
        private bool _isAddMode = false;

        public frmProducts()
        {
            InitializeComponent();
            _controller = new ProductController();
            _context = new AppDbContext();

            // --- GÁN SỰ KIỆN THỦ CÔNG (DO DESIGNER THIẾU) ---
            // Form Events
            this.Load += frmProducts_Load;

            // Grid Events
            dgvProducts.CellClick += dgvProducts_CellClick;

            // Search Panel Events
            btnSearch.Click += btnSearch_Click;
            btnRefresh.Click += btnRefresh_Click;
            cmbBrand.SelectionChangeCommitted += (s, e) => LoadData();
            cmbCategory.SelectionChangeCommitted += (s, e) => LoadData();

            // CRUD Buttons Events
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;

            // Image Buttons Events
            btnBrowseImage.Click += (s, e) =>
            {
                // Logic chọn ảnh (đã có ở code trước nhưng chưa có event)
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        try { picProduct.Image = Image.FromFile(dialog.FileName); } catch { }
                    }
                }
            };

            btnClearImage.Click += (s, e) =>
            {
                if (picProduct.Image != null) picProduct.Image = null;
            };
        }

        private void frmProducts_Load(object sender, EventArgs e)
        {
            try
            {
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

        #region 1. Load Data & ComboBox

        // Load Combobox cho thanh tìm kiếm (Brand, Category)
        private void LoadFilterComboBoxes()
        {
            // Load Brands
            var brands = _context.NhanHieux.Where(x => !x.isDelete).OrderBy(x => x.ten).ToList();
            brands.Insert(0, new NhanHieu { id = "", ten = "-- Tất cả --" });
            cmbBrand.DataSource = brands;
            cmbBrand.DisplayMember = "ten";
            cmbBrand.ValueMember = "id";
            cmbBrand.SelectedIndex = 0;

            // Load Categories
            var categories = _context.DanhMucs.Where(x => !x.isDelete).OrderBy(x => x.ten).ToList();
            categories.Insert(0, new DanhMuc { id = "", ten = "-- Tất cả --" });
            cmbCategory.DataSource = categories;
            cmbCategory.DisplayMember = "ten";
            cmbCategory.ValueMember = "id";
            cmbCategory.SelectedIndex = 0;
        }

        // Load Combobox cho phần nhập liệu (Hàng hóa, Đơn vị) - Lấy từ Controller
        private void LoadDetailComboBoxes()
        {
            try
            {
                // Hàng hóa (Lấy từ SanPham)
                var goods = _controller.GetAllGoods();
                cmbGoodDetail.DataSource = goods;
                cmbGoodDetail.DisplayMember = "ten";
                cmbGoodDetail.ValueMember = "id";
                cmbGoodDetail.SelectedIndex = -1;

                // Đơn vị (Lấy từ DonViDoLuong)
                var units = _controller.GetAllUnits();
                cmbUnitDetail.DataSource = units;
                cmbUnitDetail.DisplayMember = "ten";
                cmbUnitDetail.ValueMember = "id";
                cmbUnitDetail.SelectedIndex = -1;

                // Trạng thái (Fix cứng)
                cmbStatusDetail.DataSource = new[]
                {
                    new { Value = "Đang kinh doanh", Text = "Đang kinh doanh" },
                    new { Value = "Ngừng kinh doanh", Text = "Ngừng kinh doanh" }
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

                // Gọi Controller lấy list DTO
                var productList = _controller.FilterProducts(keyword, brandId, categoryId);

                // Gán datasource
                dgvProducts.AutoGenerateColumns = false; // Quan trọng: Không tự tạo cột
                dgvProducts.DataSource = productList;

                this.Text = $"Quản lý sản phẩm - Tổng: {productList.Count} bản ghi";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách: {ex.Message}");
            }
        }

        #endregion

        #region 2. Binding Data (Grid -> Inputs)

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || _isAddMode) return;

            try
            {
                var item = dgvProducts.Rows[e.RowIndex].DataBoundItem as ProductDetailDto;

                if (item != null)
                {
                    _selectedId = item.Id; 

                    cmbGoodDetail.SelectedValue = item.SanPhamId;
                    cmbUnitDetail.SelectedValue = item.DonViId;

                    txtPrice.Text = item.GiaBan.ToString("N0");
                    cmbStatusDetail.SelectedValue = item.TrangThai;

                    // Lưu ý: Hiện tại DTO không có ảnh, nên ta bỏ qua hoặc set null
                    picProduct.Image = null;

                    ToggleEditMode(false); // Đảm bảo nút bấm đúng trạng thái
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị chi tiết: " + ex.Message);
            }
        }

        #endregion

        #region 3. CRUD Operations

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                // Tạo model từ input
                var model = new SanPhamDonVi
                {
                    id = _isAddMode ? null : _selectedId,
                    sanPhamId = cmbGoodDetail.SelectedValue.ToString(),
                    donViId = cmbUnitDetail.SelectedValue.ToString(),
                    giaBan = decimal.Parse(txtPrice.Text.Replace(",", "").Replace(".", "")),
                    trangThai = cmbStatusDetail.SelectedValue.ToString(),
                    heSoQuyDoi = 1, // Mặc định là 1 nếu giao diện không có chỗ nhập
                    isDelete = false
                };

                bool success;
                string message;
                SanPhamDonVi result = null;

                if (_isAddMode)
                {
                    (success, message, result) = _controller.AddProductUnit(model);
                }
                else
                {
                    (success, message) = _controller.UpdateProductUnit(model);
                }

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
                }
                else
                {
                    MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ResetInputs();
            ToggleEditMode(true);
            _isAddMode = true;
            cmbGoodDetail.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedId))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!");
                return;
            }
            ToggleEditMode(true);
            _isAddMode = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ToggleEditMode(false);
            _isAddMode = false;
            // Nếu hủy khi đang thêm mới thì xóa trắng
            if (string.IsNullOrEmpty(_selectedId)) ResetInputs();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbBrand.SelectedIndex = 0;
            cmbCategory.SelectedIndex = 0;
            LoadData();
        }

        #endregion

        #region Helpers

        private bool ValidateInput()
        {
            if (cmbGoodDetail.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Hàng hóa!");
                return false;
            }
            if (cmbUnitDetail.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Đơn vị tính!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Vui lòng nhập Giá bán!");
                return false;
            }

            // Check số
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

        private void ToggleEditMode(bool enable)
        {
            // Panel nhập liệu
            cmbGoodDetail.Enabled = enable;
            cmbUnitDetail.Enabled = enable;
            txtPrice.Enabled = enable;
            cmbStatusDetail.Enabled = enable;
            txtDescription.Enabled = false; // Thường mô tả đi theo Hàng hóa (bảng SanPham), ở đây chỉ view

            // Buttons
            btnSave.Visible = enable;
            btnCancel.Visible = enable;

            btnAdd.Enabled = !enable;
            btnEdit.Enabled = !enable && !string.IsNullOrEmpty(_selectedId);
            btnDelete.Enabled = !enable && !string.IsNullOrEmpty(_selectedId);

            // Grid & Search
            dgvProducts.Enabled = !enable;
            panelSearch.Enabled = !enable;
        }

        // Clean up
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _controller?.Dispose();
            _context?.Dispose();
            base.OnFormClosing(e);
        }

        #endregion
    }
}