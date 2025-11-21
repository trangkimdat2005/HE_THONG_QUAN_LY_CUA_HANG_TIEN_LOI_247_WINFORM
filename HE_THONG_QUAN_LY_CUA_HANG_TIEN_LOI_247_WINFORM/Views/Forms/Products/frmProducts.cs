using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Products
{
    public partial class frmProducts : Form
    {
        private readonly ProductController _controller;
        private AppDbContext _context;
        private string _selectedId;
        private bool _isAddMode = false;

        // === API cho Placeholder (giống frmGoods) ===
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
            _controller = new ProductController();
            _context = new AppDbContext();

            // Set placeholder
            SetPlaceholder(txtSearch, "Nhập tên hoặc mã hàng hóa để tìm...");

            // === GÁN SỰ KIỆN (theo pattern frmGoods/frmMeasurements) ===
            this.Load += frmProducts_Load;
            this.Activated += frmProducts_Activated; // Reload khi form được activate lại
            
            // Selection Changed (giống frmGoods)
            if (dgvProducts != null) dgvProducts.SelectionChanged += dgvProducts_SelectionChanged;
            
            // === SEARCH EVENTS - GIỐNG FRMGOODS ===
            // TextChanged: Tự động search khi gõ
            if (txtSearch != null)
            {
                txtSearch.TextChanged += (s, e) => { btnSearch_Click(null, null); };
            }
            
            // Enter key: Search
            if (txtSearch != null)
            {
                txtSearch.KeyPress += (s, e) =>
                {
                    if (e.KeyChar == 13)
                    {
                        btnSearch_Click(s, e);
                        e.Handled = true;
                    }
                };
            }

            // Search & Filter buttons
            if (btnSearch != null) btnSearch.Click += btnSearch_Click;
            if (btnRefresh != null) btnRefresh.Click += btnRefresh_Click;
            
            // Filter ComboBoxes - auto reload khi chọn
            if (cmbBrand != null) cmbBrand.SelectionChangeCommitted += (s, e) => LoadData();
            if (cmbCategory != null) cmbCategory.SelectionChangeCommitted += (s, e) => LoadData();

            // CRUD Buttons
            if (btnAdd != null) btnAdd.Click += btnAdd_Click;
            if (btnEdit != null) btnEdit.Click += btnEdit_Click;
            if (btnDelete != null) btnDelete.Click += btnDelete_Click;
            if (btnSave != null) btnSave.Click += btnSave_Click;
            if (btnCancel != null) btnCancel.Click += btnCancel_Click;
            if (btnExport != null) btnExport.Click += btnExport_Click;

            // Image Buttons
            if (btnBrowseImage != null) btnBrowseImage.Click += btnBrowseImage_Click;
            if (btnClearImage != null) btnClearImage.Click += btnClearImage_Click;
        }

        private void frmProducts_Load(object sender, EventArgs e)
        {
            try
            {
                // Setup DataGridView
                if (dgvProducts != null)
                {
                    dgvProducts.AutoGenerateColumns = false;
                    SetupDataGridViewColumns();
                }

                LoadFilterComboBoxes();
                LoadDetailComboBoxes();
                LoadData();
                SetFormMode(false);

                // Auto-select dòng đầu tiên (giống frmGoods)
                if (dgvProducts.Rows.Count > 0)
                {
                    dgvProducts.Rows[0].Selected = true;
                    dgvProducts.CurrentCell = dgvProducts.Rows[0].Cells[GetFirstVisibleColumnIndex()];

                    string firstId = GetCurrentId();
                    if (!string.IsNullOrEmpty(firstId))
                    {
                        _selectedId = firstId;
                        LoadProductDetail(firstId);
                    }
                }
                else
                {
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // === EVENT KHI FORM ĐƯỢC ACTIVATE LẠI ===
        private void frmProducts_Activated(object sender, EventArgs e)
        {
            // Reload ComboBox hàng hóa để cập nhật dữ liệu mới từ frmGoods
            if (!_isAddMode)
            {
                ReloadDetailComboBoxes();
            }
        }

        #region Setup DataGridView

        private void SetupDataGridViewColumns()
        {
            if (dgvProducts == null) return;

            // ẨN cột colId (Mã sản phẩm đơn vị)
            if (dgvProducts.Columns.Contains("colId"))
            {
                dgvProducts.Columns["colId"].Visible = false; // ẨN HOÀN TOÀN
                dgvProducts.Columns["colId"].DataPropertyName = "Id"; // Vẫn cần bind để lấy ID
            }

            // Mapping các cột hiển thị
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

            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        // Helper: Lấy index của cột visible đầu tiên
        private int GetFirstVisibleColumnIndex()
        {
            foreach (DataGridViewColumn col in dgvProducts.Columns)
            {
                if (col.Visible) return col.Index;
            }
            return 0;
        }

        #endregion

        #region Load Data & Logic

        private void LoadFilterComboBoxes()
        {
            try
            {
                // Filter ComboBox cho nhãn hiệu (có "Tất cả")
                var brands = _context.NhanHieux.Where(x => !x.isDelete).OrderBy(x => x.ten).ToList();
                var filterBrands = new System.Collections.Generic.List<NhanHieu>
                {
                    new NhanHieu { id = "", ten = "-- Tất cả --" }
                };
                filterBrands.AddRange(brands);

                if (cmbBrand != null)
                {
                    cmbBrand.DataSource = null;
                    cmbBrand.DataSource = filterBrands;
                    cmbBrand.DisplayMember = "ten";
                    cmbBrand.ValueMember = "id";
                    cmbBrand.SelectedIndex = 0;
                }

                // Filter ComboBox cho danh mục (có "Tất cả")
                var categories = _context.DanhMucs.Where(x => !x.isDelete).OrderBy(x => x.ten).ToList();
                var filterCategories = new System.Collections.Generic.List<DanhMuc>
                {
                    new DanhMuc { id = "", ten = "-- Tất cả --" }
                };
                filterCategories.AddRange(categories);

                if (cmbCategory != null)
                {
                    cmbCategory.DataSource = null;
                    cmbCategory.DataSource = filterCategories;
                    cmbCategory.DisplayMember = "ten";
                    cmbCategory.ValueMember = "id";
                    cmbCategory.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải bộ lọc: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDetailComboBoxes()
        {
            try
            {
                // ComboBox hàng hóa cho form nhập liệu
                var goods = _controller.GetAllGoods();
                if (cmbGoodDetail != null)
                {
                    var currentValue = cmbGoodDetail.SelectedValue; // Lưu giá trị hiện tại

                    cmbGoodDetail.DataSource = null;
                    cmbGoodDetail.DataSource = goods;
                    cmbGoodDetail.DisplayMember = "ten";
                    cmbGoodDetail.ValueMember = "id";
                    
                    // Khôi phục giá trị đã chọn (nếu có)
                    if (currentValue != null)
                    {
                        cmbGoodDetail.SelectedValue = currentValue;
                    }
                    else
                    {
                        cmbGoodDetail.SelectedIndex = -1;
                    }
                }

                // ComboBox đơn vị
                var units = _controller.GetAllUnits();
                if (cmbUnitDetail != null)
                {
                    var currentValue = cmbUnitDetail.SelectedValue;

                    cmbUnitDetail.DataSource = null;
                    cmbUnitDetail.DataSource = units;
                    cmbUnitDetail.DisplayMember = "ten";
                    cmbUnitDetail.ValueMember = "id";
                    
                    if (currentValue != null)
                    {
                        cmbUnitDetail.SelectedValue = currentValue;
                    }
                    else
                    {
                        cmbUnitDetail.SelectedIndex = -1;
                    }
                }

                // ComboBox trạng thái
                if (cmbStatusDetail != null)
                {
                    cmbStatusDetail.DataSource = new[]
                    {
                        new { Value = "Available", Text = "Đang kinh doanh" },
                        new { Value = "Unavailable", Text = "Ngừng kinh doanh" }
                    };
                    cmbStatusDetail.DisplayMember = "Text";
                    cmbStatusDetail.ValueMember = "Value";
                    cmbStatusDetail.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu nhập liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Reload ComboBox mà không reset selection (dùng khi form activated)
        private void ReloadDetailComboBoxes()
        {
            try
            {
                // Lưu giá trị hiện tại
                var currentGoodValue = cmbGoodDetail?.SelectedValue;
                var currentUnitValue = cmbUnitDetail?.SelectedValue;

                // Reload ComboBox hàng hóa
                var goods = _controller.GetAllGoods();
                if (cmbGoodDetail != null)
                {
                    cmbGoodDetail.DataSource = null;
                    cmbGoodDetail.DataSource = goods;
                    cmbGoodDetail.DisplayMember = "ten";
                    cmbGoodDetail.ValueMember = "id";
                    
                    // Khôi phục giá trị
                    if (currentGoodValue != null)
                        cmbGoodDetail.SelectedValue = currentGoodValue;
                    else
                        cmbGoodDetail.SelectedIndex = -1;
                }

                // Reload ComboBox đơn vị
                var units = _controller.GetAllUnits();
                if (cmbUnitDetail != null)
                {
                    cmbUnitDetail.DataSource = null;
                    cmbUnitDetail.DataSource = units;
                    cmbUnitDetail.DisplayMember = "ten";
                    cmbUnitDetail.ValueMember = "id";
                    
                    if (currentUnitValue != null)
                        cmbUnitDetail.SelectedValue = currentUnitValue;
                    else
                        cmbUnitDetail.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi reload ComboBox: {ex.Message}");
            }
        }

        private void LoadData()
        {
            try
            {
                string keyword = txtSearch?.Text.Trim();
                string brandId = cmbBrand?.SelectedValue?.ToString();
                string categoryId = cmbCategory?.SelectedValue?.ToString();

                // Xử lý filter "Tất cả" (empty string → null)
                if (brandId == "") brandId = null;
                if (categoryId == "") categoryId = null;

                var productList = _controller.FilterProducts(keyword, brandId, categoryId);

                dgvProducts.DataSource = productList;

                this.Text = $"Quản lý sản phẩm - Tổng: {productList.Count} bản ghi";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Get Current ID

        private string GetCurrentId()
        {
            if (dgvProducts?.CurrentRow == null) return null;

            var dataItem = dgvProducts.CurrentRow.DataBoundItem as ProductDetailDto;
            if (dataItem != null)
            {
                return dataItem.Id; // Đây là ID của SanPhamDonVi (SPDV0001, SPDV0002...)
            }

            // Fallback: lấy từ cột colId (ẩn)
            if (dgvProducts.Columns.Contains("colId") && dgvProducts.CurrentRow.Cells["colId"].Value != null)
                return dgvProducts.CurrentRow.Cells["colId"].Value.ToString();

            return null;
        }

        #endregion

        #region Selection Changed (giống frmGoods)

        private void dgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (_isAddMode) return;

            if (dgvProducts == null || dgvProducts.CurrentRow == null || dgvProducts.CurrentRow.Index == -1)
            {
                ClearForm();
                return;
            }

            string id = GetCurrentId();
            if (!string.IsNullOrEmpty(id))
            {
                _selectedId = id;
                LoadProductDetail(id);
            }
            else
            {
                ClearForm();
            }
        }

        private void LoadProductDetail(string productId)
        {
            if (string.IsNullOrEmpty(productId)) return;

            try
            {
                var item = dgvProducts.CurrentRow?.DataBoundItem as ProductDetailDto;

                if (item != null)
                {
                    // Binding dữ liệu vào ComboBox
                    if (cmbGoodDetail != null && !string.IsNullOrEmpty(item.SanPhamId))
                        cmbGoodDetail.SelectedValue = item.SanPhamId;
                    else if (cmbGoodDetail != null)
                        cmbGoodDetail.SelectedIndex = -1;

                    if (cmbUnitDetail != null && !string.IsNullOrEmpty(item.DonViId))
                        cmbUnitDetail.SelectedValue = item.DonViId;
                    else if (cmbUnitDetail != null)
                        cmbUnitDetail.SelectedIndex = -1;

                    // Giá bán
                    if (txtPrice != null)
                        txtPrice.Text = item.GiaBan.ToString("N0");

                    // Trạng thái
                    if (cmbStatusDetail != null && !string.IsNullOrEmpty(item.TrangThai))
                        cmbStatusDetail.SelectedValue = item.TrangThai;
                    else if (cmbStatusDetail != null)
                        cmbStatusDetail.SelectedIndex = 0;

                    // Clear ảnh
                    if (picProduct != null)
                        picProduct.Image = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region CRUD Actions

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetFormMode(true);
            _isAddMode = true;
            ClearForm();

            // Hiển thị ID tự động sẽ sinh (giống frmGoods/frmMeasurements)
            try
            {
                string newId = _controller.GenerateNewProductUnitId();
                // Có thể hiển thị newId lên một label hoặc tooltip để user biết
                this.Text = $"Quản lý sản phẩm - Thêm mới (Mã tự động: {newId})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể sinh mã tự động: {ex.Message}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Focus vào combobox hàng hóa
            if (cmbGoodDetail != null && cmbGoodDetail.Items.Count > 0)
                cmbGoodDetail.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string id = GetCurrentId();
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _selectedId = id;
            SetFormMode(true);
            _isAddMode = false;
            
            if (cmbGoodDetail != null)
                cmbGoodDetail.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = GetCurrentId();
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var (success, message) = _controller.DeleteProductUnit(id);
                if (success)
                {
                    MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearForm();
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
                    id = _isAddMode ? null : _selectedId, // Null khi Add - Controller sẽ tự sinh
                    sanPhamId = cmbGoodDetail.SelectedValue.ToString(),
                    donViId = cmbUnitDetail.SelectedValue.ToString(),
                    giaBan = decimal.Parse(txtPrice.Text.Replace(",", "").Replace(".", "")),
                    trangThai = cmbStatusDetail.SelectedValue?.ToString() ?? "Available",
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
                    SetFormMode(false);
                    _isAddMode = false;

                    // Tự động select sản phẩm vừa thêm/sửa
                    if (!string.IsNullOrEmpty(result?.id))
                    {
                        _selectedId = result.id;
                        LoadProductDetail(result.id);
                    }
                }
                else
                {
                    MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetFormMode(false);
            _isAddMode = false;

            if (!string.IsNullOrEmpty(_selectedId))
                LoadProductDetail(_selectedId);
            else
                ClearForm();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SetFormMode(false);
            LoadData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SetFormMode(false);
            if (txtSearch != null) txtSearch.Clear();
            if (cmbBrand != null) cmbBrand.SelectedIndex = 0;
            if (cmbCategory != null) cmbCategory.SelectedIndex = 0;
            LoadData();
            ClearForm();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel(dgvProducts);
        }

        private void ExportToExcel(DataGridView dgv)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Workbook|*.xlsx";
                    sfd.FileName = "DanhSachSanPham_" + DateTime.Now.ToString("ddMMyy_Hmmss") + ".xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (var workbook = new ClosedXML.Excel.XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Danh Sách");

                            for (int i = 0; i < dgv.Columns.Count; i++)
                            {
                                if (dgv.Columns[i].Visible)
                                {
                                    worksheet.Cell(1, i + 1).Value = dgv.Columns[i].HeaderText;
                                    worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                                    worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
                                }
                            }

                            for (int i = 0; i < dgv.Rows.Count; i++)
                            {
                                int excelCol = 1;
                                for (int j = 0; j < dgv.Columns.Count; j++)
                                {
                                    if (dgv.Columns[j].Visible)
                                    {
                                        var cellValue = dgv.Rows[i].Cells[j].Value;
                                        worksheet.Cell(i + 2, excelCol).Value = cellValue != null ? cellValue.ToString() : "";
                                        excelCol++;
                                    }
                                }
                            }

                            worksheet.Columns().AdjustToContents();
                            workbook.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show("Xuất file Excel thành công!\nĐường dẫn: " + sfd.FileName, 
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (picProduct != null)
                            picProduct.Image = Image.FromFile(dialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi tải ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnClearImage_Click(object sender, EventArgs e)
        {
            if (picProduct != null && picProduct.Image != null)
                picProduct.Image = null;
        }

        #endregion

        #region Helpers

        private bool ValidateInput()
        {
            if (cmbGoodDetail == null || cmbGoodDetail.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Hàng hóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbGoodDetail?.Focus();
                return false;
            }

            if (cmbUnitDetail == null || cmbUnitDetail.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Đơn vị tính!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbUnitDetail?.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrice?.Text))
            {
                MessageBox.Show("Vui lòng nhập Giá bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice?.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text.Replace(",", "").Replace(".", ""), out decimal price) || price < 0)
            {
                MessageBox.Show("Giá bán không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice?.Focus();
                return false;
            }

            return true;
        }

        private void SetFormMode(bool isEdit)
        {
            // Vùng nhập liệu
            if (cmbGoodDetail != null) cmbGoodDetail.Enabled = isEdit;
            if (cmbUnitDetail != null) cmbUnitDetail.Enabled = isEdit;
            if (txtPrice != null) txtPrice.Enabled = isEdit;
            if (cmbStatusDetail != null) cmbStatusDetail.Enabled = isEdit;
            if (btnBrowseImage != null) btnBrowseImage.Enabled = isEdit;
            if (btnClearImage != null) btnClearImage.Enabled = isEdit;

            // Nút Lưu/Hủy
            if (btnSave != null)
            {
                btnSave.Visible = isEdit;
                btnSave.Enabled = isEdit;
            }
            if (btnCancel != null)
            {
                btnCancel.Visible = isEdit;
                btnCancel.Enabled = isEdit;
            }

            // Nút chức năng chính
            if (btnAdd != null) btnAdd.Enabled = !isEdit;
            if (btnEdit != null) btnEdit.Enabled = !isEdit;
            if (btnDelete != null) btnDelete.Enabled = !isEdit;
            if (btnExport != null) btnExport.Enabled = !isEdit;

            // Vùng tìm kiếm và lọc
            if (btnSearch != null) btnSearch.Enabled = !isEdit;
            if (btnRefresh != null) btnRefresh.Enabled = !isEdit;
            if (txtSearch != null) txtSearch.Enabled = !isEdit;
            if (cmbBrand != null) cmbBrand.Enabled = !isEdit;
            if (cmbCategory != null) cmbCategory.Enabled = !isEdit;

            // Grid
            if (dgvProducts != null) dgvProducts.Enabled = !isEdit;
        }

        private void ClearForm()
        {
            _selectedId = null;

            if (cmbGoodDetail != null) cmbGoodDetail.SelectedIndex = -1;
            if (cmbUnitDetail != null) cmbUnitDetail.SelectedIndex = -1;
            if (txtPrice != null) txtPrice.Clear();
            if (cmbStatusDetail != null && cmbStatusDetail.Items.Count > 0) cmbStatusDetail.SelectedIndex = 0;
            if (picProduct != null) picProduct.Image = null;
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