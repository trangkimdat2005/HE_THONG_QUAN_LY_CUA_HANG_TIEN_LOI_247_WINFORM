using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.Forms.Products
{
    public partial class frmGoods : Form
    {
        private readonly GoodsController _goodsController;
        private string _selectedGoodId;
        private bool _isAddMode = false;

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

        public frmGoods()
        {
            InitializeComponent();
            _goodsController = new GoodsController();

            SetPlaceholder(txtSearch, "Nhập tên hoặc mã hàng hóa để tìm...");

            if (dgvGoods != null) this.dgvGoods.SelectionChanged += dgvGoods_SelectionChanged;
            if (txtSearch != null) txtSearch.KeyPress += (s, e) => { if (e.KeyChar == 13) { btnSearch_Click(s, e); e.Handled = true; } };
            if (txtName != null) txtName.KeyPress += (s, e) => { if (e.KeyChar == 13) { btnSave_Click(s, e); e.Handled = true; } };

            if (btnAdd != null) btnAdd.Click += btnAdd_Click;
            if (btnEdit != null) btnEdit.Click += btnEdit_Click;
            if (btnDelete != null) btnDelete.Click += btnDelete_Click;
            if (btnSave != null) btnSave.Click += btnSave_Click;
            if (btnCancel != null) btnCancel.Click += btnCancel_Click;
            if (btnSearch != null) btnSearch.Click += btnSearch_Click;
            if (btnRefresh != null) btnRefresh.Click += btnRefresh_Click;
            if (btnExport != null) btnExport.Click += btnExport_Click;


            if (txtSearch != null)
            {
                txtSearch.TextChanged += (s, e) => { btnSearch_Click(null, null); };
                txtSearch.KeyPress += (s, e) => { if (e.KeyChar == 13) { btnSearch_Click(s, e); e.Handled = true; } };
            }

            if (cmbBrandFilter != null) cmbBrandFilter.SelectionChangeCommitted += (s, e) => { LoadGoods(); };
            if (cmbCategoryFilter != null) cmbCategoryFilter.SelectionChangeCommitted += (s, e) => { LoadGoods(); };

            this.Load += frmGoods_Load;
        }

        private void frmGoods_Load(object sender, EventArgs e)
        {
            try
            {
                if (dgvGoods != null) dgvGoods.AutoGenerateColumns = false;

                SetupDataGridViewColumns();

                LoadBrands();
                LoadCategories();
                LoadGoods();
                SetFormMode(false);

                if (dgvGoods.Rows.Count > 0)
                {
                    dgvGoods.Rows[0].Selected = true;
                    dgvGoods.CurrentCell = dgvGoods.Rows[0].Cells[0];

                    string firstId = GetCurrentId();
                    if (!string.IsNullOrEmpty(firstId))
                    {
                        _selectedGoodId = firstId;
                        LoadGoodDetail(firstId);
                    }
                }
                else
                {
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải form: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Setup DataGridView

        private void SetupDataGridViewColumns()
        {
            if (dgvGoods == null) return;

            dgvGoods.Columns.Clear();

            dgvGoods.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "Mã hàng hoá", Name = "colId", ReadOnly = true });
            dgvGoods.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Ten", HeaderText = "Tên hàng hoá", Name = "colName", ReadOnly = true });
            dgvGoods.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NhanHieu", HeaderText = "Nhãn hiệu", Name = "colBrand", ReadOnly = true });
            dgvGoods.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DanhMuc", HeaderText = "Danh Mục", Name = "colCategory", ReadOnly = true });
            dgvGoods.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MoTa", HeaderText = "Mô tả", Name = "moTa", ReadOnly = true });

            dgvGoods.AutoGenerateColumns = false;
            dgvGoods.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        #endregion

        #region Load Data

        private void LoadGoods()
        {
            try
            {
                string keyword = txtSearch?.Text.Trim();
                string brandId = cmbBrandFilter?.SelectedValue?.ToString();
                string categoryId = cmbCategoryFilter?.SelectedValue?.ToString();

                if (brandId == string.Empty) brandId = null;
                if (categoryId == string.Empty) categoryId = null;

                var goods = _goodsController.GetAllGoods(keyword, brandId, categoryId);
                dgvGoods.DataSource = goods;

                if (dgvGoods != null && dgvGoods.Columns.Count > 0)
                {
                    foreach (DataGridViewColumn col in dgvGoods.Columns)
                    {
                        if (col.HeaderText.ToLower().Contains("mã")) col.DataPropertyName = "Id";
                        else if (col.HeaderText.ToLower().Contains("tên")) col.DataPropertyName = "Ten";
                        else if (col.HeaderText.ToLower().Contains("nhãn hiệu")) col.DataPropertyName = "NhanHieu";
                        else if (col.HeaderText.ToLower().Contains("danh mục")) col.DataPropertyName = "DanhMuc";
                        else if (col.HeaderText.ToLower().Contains("mô tả")) col.DataPropertyName = "MoTa";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBrands()
        {
            try
            {
                var brands = _goodsController.GetAllBrands();

                // Tạo list riêng cho filter combobox
                var filterBrands = new List<NhanHieu>
                {
                    new NhanHieu { id = "", ten = "-- Tất cả --" }
                };
                filterBrands.AddRange(brands);

                if (cmbBrandFilter != null)
                {
                    cmbBrandFilter.DataSource = null;
                    cmbBrandFilter.DataSource = filterBrands;
                    cmbBrandFilter.DisplayMember = "ten";
                    cmbBrandFilter.ValueMember = "id";
                    cmbBrandFilter.SelectedIndex = 0;
                }

                // Detail combobox - không set SelectedIndex
                if (cmbBrand != null)
                {
                    var detailBrands = new List<NhanHieu>(brands);
                    cmbBrand.DataSource = null;
                    cmbBrand.DataSource = detailBrands;
                    cmbBrand.DisplayMember = "ten";
                    cmbBrand.ValueMember = "id";
                    cmbBrand.SelectedIndex = -1; // Không chọn gì
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải nhãn hiệu: {ex.Message}\n{ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategories()
        {
            try
            {
                var categories = _goodsController.GetAllCategories();

                // Tạo list riêng cho filter combobox
                var filterCategories = new List<DanhMuc>
                {
                    new DanhMuc { id = "", ten = "-- Tất cả --" }
                };
                filterCategories.AddRange(categories);

                if (cmbCategoryFilter != null)
                {
                    cmbCategoryFilter.DataSource = null;
                    cmbCategoryFilter.DataSource = filterCategories;
                    cmbCategoryFilter.DisplayMember = "ten";
                    cmbCategoryFilter.ValueMember = "id";
                    cmbCategoryFilter.SelectedIndex = 0;
                }

                // Detail combobox - không set SelectedIndex
                if (cmbCategory != null)
                {
                    var detailCategories = new List<DanhMuc>(categories);
                    cmbCategory.DataSource = null;
                    cmbCategory.DataSource = detailCategories;
                    cmbCategory.DisplayMember = "ten";
                    cmbCategory.ValueMember = "id";
                    cmbCategory.SelectedIndex = -1; // Không chọn gì
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh mục: {ex.Message}\n{ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Get Current ID

        private string GetCurrentId()
        {
            if (dgvGoods?.CurrentRow == null) return null;

            foreach (DataGridViewColumn col in dgvGoods.Columns)
            {
                if (col.DataPropertyName == "Id" || col.HeaderText.ToLower().Contains("mã"))
                {
                    return dgvGoods.CurrentRow.Cells[col.Index].Value?.ToString();
                }
            }

            return null;
        }

        #endregion

        #region Selection Changed

        private void dgvGoods_SelectionChanged(object sender, EventArgs e)
        {
            if (_isAddMode) return;

            if (dgvGoods == null || dgvGoods.CurrentRow == null || dgvGoods.CurrentRow.Index == -1)
            {
                ClearForm();
                return;
            }

            string id = GetCurrentId();
            if (!string.IsNullOrEmpty(id))
            {
                _selectedGoodId = id;
                LoadGoodDetail(id);
            }
            else
            {
                ClearForm();
            }
        }

        private void LoadGoodDetail(string goodId)
        {
            if (string.IsNullOrEmpty(goodId)) return;
            try
            {
                var good = _goodsController.GetGoodById(goodId);
                if (good != null)
                {
                    SetPlaceholder(txtId, "");
                    txtId.Text = good.id;
                    txtId.ForeColor = System.Drawing.Color.Black;

                    txtName.Text = good.ten;
                    
                    // Xử lý mô tả
                    txtDescription.Text = good.moTa ?? string.Empty;

                    if (cmbBrand != null && !string.IsNullOrEmpty(good.nhanHieuId))
                        cmbBrand.SelectedValue = good.nhanHieuId;
                    else if (cmbBrand != null)
                        cmbBrand.SelectedIndex = -1;

                    string categoryId = _goodsController.GetGoodCategoryId(goodId);
                    if (cmbCategory != null && !string.IsNullOrEmpty(categoryId))
                        cmbCategory.SelectedValue = categoryId;
                    else if (cmbCategory != null)
                        cmbCategory.SelectedIndex = -1;

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

            try
            {
                string newId = _goodsController.GenerateNewProductId();

                SetPlaceholder(txtId, "");
                txtId.Text = newId;
                txtId.ForeColor = System.Drawing.Color.Blue;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể sinh mã tự động: {ex.Message}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SetPlaceholder(txtId, "Lỗi sinh mã");
                txtId.Text = "";
                txtId.ForeColor = System.Drawing.Color.Red;
            }

            txtName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string id = GetCurrentId();
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Vui lòng chọn hàng hóa cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _selectedGoodId = id;
            SetFormMode(true);
            _isAddMode = false;
            txtName.Focus();

            if (txtId != null)
            {
                txtId.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = GetCurrentId();
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Vui lòng chọn hàng hóa cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa hàng hóa này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var (success, message) = _goodsController.DeleteGood(id);
                if (success)
                {
                    MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadGoods();
                    ClearForm();
                }
                else
                    MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên hàng hóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            if (cmbBrand.SelectedValue == null || cmbBrand.SelectedValue.ToString() == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn nhãn hiệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbBrand.Focus();
                return;
            }

            try
            {
                var categoryId = cmbCategory.SelectedValue?.ToString();

                var goodToSave = new SanPham
                {
                    id = _isAddMode ? txtId.Text : _selectedGoodId,
                    ten = txtName.Text.Trim(),
                    nhanHieuId = cmbBrand.SelectedValue.ToString(),
                    moTa = txtDescription.Text.Trim()
                };

                (bool success, string message, string productId) result;

                if (_isAddMode)
                {
                    result = _goodsController.AddGood(goodToSave, categoryId);
                }
                else
                {
                    var (success, message) = _goodsController.UpdateGood(goodToSave, categoryId);
                    result = (success, message, goodToSave.id);
                }

                if (result.success)
                {
                    MessageBox.Show(result.message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadGoods();
                    SetFormMode(false);
                    _isAddMode = false;

                    if (!string.IsNullOrEmpty(result.productId))
                    {
                        _selectedGoodId = result.productId;
                        LoadGoodDetail(result.productId);
                    }
                }
                else
                    MessageBox.Show(result.message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetFormMode(false);
            _isAddMode = false;

            if (!string.IsNullOrEmpty(_selectedGoodId))
                LoadGoodDetail(_selectedGoodId);
            else
                ClearForm();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SetFormMode(false);
            LoadGoods();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SetFormMode(false);
            if (txtSearch != null) txtSearch.Clear();
            if (cmbBrandFilter != null) cmbBrandFilter.SelectedIndex = 0;
            if (cmbCategoryFilter != null) cmbCategoryFilter.SelectedIndex = 0;
            LoadGoods();
            ClearForm();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel(dgvGoods);
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
                    sfd.FileName = "DanhSachHangHoa_" + DateTime.Now.ToString("ddMMyy_Hmmss") + ".xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (var workbook = new ClosedXML.Excel.XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Hàng Hóa");

                            // Header
                            for (int i = 0; i < dgv.Columns.Count; i++)
                            {
                                worksheet.Cell(1, i + 1).Value = dgv.Columns[i].HeaderText;
                                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                                worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
                            }

                            // Data
                            for (int i = 0; i < dgv.Rows.Count; i++)
                            {
                                for (int j = 0; j < dgv.Columns.Count; j++)
                                {
                                    var cellValue = dgv.Rows[i].Cells[j].Value;
                                    worksheet.Cell(i + 2, j + 1).Value = cellValue != null ? cellValue.ToString() : "";
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

        #endregion

        #region Helpers

        private void SetFormMode(bool isEdit)
        {
            txtName.Enabled = isEdit;
            txtDescription.Enabled = isEdit;
            cmbBrand.Enabled = isEdit;
            cmbCategory.Enabled = isEdit;

            btnSave.Visible = isEdit;
            btnCancel.Visible = isEdit;

            if (isEdit)
            {
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
            }


            btnAdd.Enabled = !isEdit;
            btnEdit.Enabled = !isEdit;
            btnDelete.Enabled = !isEdit;
            btnExport.Enabled = !isEdit;

            btnSearch.Enabled = !isEdit;
            btnRefresh.Enabled = !isEdit;
            txtSearch.Enabled = !isEdit;
            cmbBrandFilter.Enabled = !isEdit;
            cmbCategoryFilter.Enabled = !isEdit;

            dgvGoods.Enabled = !isEdit;
            txtId.Enabled = false;
        }

        private void ClearForm()
        {
            txtId.Clear();
            SetPlaceholder(txtId, "Mã hàng hóa");
            txtId.ForeColor = System.Drawing.Color.Gray;

            txtName.Clear();
            txtDescription.Clear();

            if (cmbBrand != null)
            {
                cmbBrand.SelectedIndex = -1;
            }
            if (cmbCategory != null)
            {
                cmbCategory.SelectedIndex = -1;
            }

            _selectedGoodId = null;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _goodsController?.Dispose();
            base.OnFormClosing(e);
        }
        #endregion

        private void lblDescription_Click(object sender, EventArgs e)
        {

        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExport_Click_1(object sender, EventArgs e)
        {

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Excel Files|*.xlsx;*.xls"; // Chỉ cho chọn file Excel
            dialog.Title = "Chọn file Excel danh sách sản phẩm";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Đổi con trỏ chuột thành hình xoay xoay để người dùng biết đang chạy
                this.Cursor = Cursors.WaitCursor;

                try
                {
                    // 1. Khởi tạo Service
                    var excelService = new HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Services.ExcelImportService();

                    // 2. Gọi hàm nhập Sản Phẩm
                    var result = excelService.ImportSanPham(dialog.FileName);

                    this.Cursor = Cursors.Default; // Trả lại con trỏ chuột bình thường

                    // 3. Xử lý kết quả trả về
                    if (result.IsSuccess)
                    {
                        MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // GỌI HÀM LOAD LẠI DỮ LIỆU Ở ĐÂY
                        // Ví dụ: LoadData(); hoặc btnRefresh_Click(null, null);
                        // Bạn thay bằng tên hàm load dữ liệu thực tế của form bạn nhé
                        LoadGoods();
                    }
                    else
                    {
                        // Nếu có lỗi chi tiết thì hiện ra
                        if (result.ErrorLogs != null && result.ErrorLogs.Count > 0)
                        {
                            // Ghép các dòng lỗi lại để hiện thông báo (lấy tối đa 15 lỗi đầu cho đỡ dài)
                            string errorDetails = string.Join("\n", result.ErrorLogs.Take(15));

                            if (result.ErrorLogs.Count > 15)
                                errorDetails += $"\n... và {result.ErrorLogs.Count - 15} lỗi khác.";

                            MessageBox.Show(result.Message + "\n\nCHI TIẾT LỖI:\n" + errorDetails,
                                            "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            // Lỗi chung chung (ví dụ file rỗng)
                            MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Lỗi Application: " + ex.Message, "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}