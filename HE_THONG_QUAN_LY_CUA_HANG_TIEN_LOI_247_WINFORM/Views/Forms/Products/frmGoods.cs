using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

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

            if (dgvGood != null) this.dgvGood.SelectionChanged += dgvGoods_SelectionChanged;
            if (txtSearch != null) txtSearch.KeyPress += (s, e) => { if (e.KeyChar == 13) { btnSearch_Click(s, e); e.Handled = true; } };
            if (txtProductName != null) txtProductName.KeyPress += (s, e) => { if (e.KeyChar == 13) { btnSave_Click(s, e); e.Handled = true; } };

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

            if (cmbBrand != null) cmbBrand.SelectionChangeCommitted += (s, e) => { LoadGoods(); };
            if (cmbCategory != null) cmbCategory.SelectionChangeCommitted += (s, e) => { LoadGoods(); };

            this.Load += frmGoods_Load;
        }

        private void frmGoods_Load(object sender, EventArgs e)
        {
            try
            {
                if (dgvGood != null) dgvGood.AutoGenerateColumns = false;

                SetupDataGridViewColumns();

                LoadBrands();
                LoadCategories();
                LoadGoods();
                SetFormMode(false);

                if (dgvGood.Rows.Count > 0)
                {
                    dgvGood.Rows[0].Selected = true;
                    dgvGood.CurrentCell = dgvGood.Rows[0].Cells[0];

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
            if (dgvGood == null) return;

            dgvGood.Columns.Clear();

            dgvGood.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "Mã hàng hoá", Name = "ColId", ReadOnly = true });
            dgvGood.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Ten", HeaderText = "Tên hàng hoá", Name = "ColTen", ReadOnly = true });
            dgvGood.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NhanHieu", HeaderText = "Nhãn hiệu", Name = "ColNhanHieu", ReadOnly = true });
            dgvGood.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DanhMuc", HeaderText = "Danh Mục", Name = "ColDanhMuc", ReadOnly = true });

            dgvGood.AutoGenerateColumns = false;
            dgvGood.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        #endregion

        #region Load Data

        private void LoadGoods()
        {
            try
            {
                string keyword = txtSearch?.Text.Trim();
                string brandId = cmbBrand?.SelectedValue?.ToString();
                string categoryId = cmbCategory?.SelectedValue?.ToString();

                if (brandId == string.Empty) brandId = null;
                if (categoryId == string.Empty) categoryId = null;

                var goods = _goodsController.GetAllGoods(keyword, brandId, categoryId);
                dgvGood.DataSource = goods;

                if (dgvGood != null && dgvGood.Columns.Count > 0)
                {
                    foreach (DataGridViewColumn col in dgvGood.Columns)
                    {
                        if (col.HeaderText.ToLower().Contains("mã")) col.DataPropertyName = "Id";
                        else if (col.HeaderText.ToLower().Contains("tên")) col.DataPropertyName = "Ten";
                        else if (col.HeaderText.ToLower().Contains("nhãn hiệu")) col.DataPropertyName = "NhanHieu";
                        else if (col.HeaderText.ToLower().Contains("danh mục")) col.DataPropertyName = "DanhMuc";
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

                var filterBrands = new List<NhanHieu>
                {
                    new NhanHieu { id = "", ten = "-- Tất cả --" }
                };
                filterBrands.AddRange(brands);

                if (cmbBrand != null)
                {
                    cmbBrand.DataSource = new List<NhanHieu>(filterBrands);
                    cmbBrand.DisplayMember = "ten";
                    cmbBrand.ValueMember = "id";
                    cmbBrand.SelectedIndex = 0;
                }

                if (cmbBrandDetail != null)
                {
                    var detailBrands = new List<NhanHieu>(brands);
                    cmbBrandDetail.DataSource = detailBrands;
                    cmbBrandDetail.DisplayMember = "ten";
                    cmbBrandDetail.ValueMember = "id";
                    cmbBrandDetail.SelectedIndex = -1;
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

                var filterCategories = new List<DanhMuc>
                {
                    new DanhMuc { id = "", ten = "-- Tất cả --" }
                };
                filterCategories.AddRange(categories);

                if (cmbCategory != null)
                {
                    cmbCategory.DataSource = new List<DanhMuc>(filterCategories);
                    cmbCategory.DisplayMember = "ten";
                    cmbCategory.ValueMember = "id";
                    cmbCategory.SelectedIndex = 0;
                }

                if (cmbCategoryDetail != null)
                {
                    var detailCategories = new List<DanhMuc>(categories);
                    cmbCategoryDetail.DataSource = detailCategories;
                    cmbCategoryDetail.DisplayMember = "ten";
                    cmbCategoryDetail.ValueMember = "id";
                    cmbCategoryDetail.SelectedIndex = -1;
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
            if (dgvGood?.CurrentRow == null) return null;

            foreach (DataGridViewColumn col in dgvGood.Columns)
            {
                if (col.DataPropertyName == "Id" || col.HeaderText.ToLower().Contains("mã"))
                {
                    return dgvGood.CurrentRow.Cells[col.Index].Value?.ToString();
                }
            }

            return null;
        }

        #endregion

        #region Selection Changed

        private void dgvGoods_SelectionChanged(object sender, EventArgs e)
        {
            if (_isAddMode) return;

            if (dgvGood == null || dgvGood.CurrentRow == null || dgvGood.CurrentRow.Index == -1)
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
                    SetPlaceholder(txtGoodId, "");
                    txtGoodId.Text = good.id;
                    txtGoodId.ForeColor = System.Drawing.Color.Black;

                    txtProductName.Text = good.ten;

                    if (cmbBrandDetail != null && !string.IsNullOrEmpty(good.nhanHieuId))
                        cmbBrandDetail.SelectedValue = good.nhanHieuId;
                    else if (cmbBrandDetail != null)
                        cmbBrandDetail.SelectedIndex = -1;

                    string categoryId = _goodsController.GetGoodCategoryId(goodId);
                    if (cmbCategoryDetail != null && !string.IsNullOrEmpty(categoryId))
                        cmbCategoryDetail.SelectedValue = categoryId;
                    else if (cmbCategoryDetail != null)
                        cmbCategoryDetail.SelectedIndex = -1;

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

                SetPlaceholder(txtGoodId, ""); 
                txtGoodId.Text = newId;
                txtGoodId.ForeColor = System.Drawing.Color.Blue;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể sinh mã tự động: {ex.Message}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SetPlaceholder(txtGoodId, "Lỗi sinh mã");
                txtGoodId.Text = "";
                txtGoodId.ForeColor = System.Drawing.Color.Red;
            }

            txtProductName.Focus();
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
            txtProductName.Focus();

            if (txtGoodId != null)
            {
                txtGoodId.ForeColor = System.Drawing.Color.Black;
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
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên hàng hóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductName.Focus();
                return;
            }

            if (cmbBrandDetail.SelectedValue == null || cmbBrandDetail.SelectedValue.ToString() == string.Empty)
            {
                MessageBox.Show("Vui lòng chọn nhãn hiệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbBrandDetail.Focus();
                return;
            }

            try
            {
                var categoryId = cmbCategoryDetail.SelectedValue?.ToString();

                var goodToSave = new SanPham
                {
                    id = _isAddMode ? txtGoodId.Text : _selectedGoodId,
                    ten = txtProductName.Text.Trim(),
                    nhanHieuId = cmbBrandDetail.SelectedValue.ToString(),
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
            if (cmbBrand != null) cmbBrand.SelectedIndex = 0;
            if (cmbCategory != null) cmbCategory.SelectedIndex = 0;
            LoadGoods();
            ClearForm();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng đang phát triển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Helpers

        private void SetFormMode(bool isEdit)
        {
            txtProductName.Enabled = isEdit;
            cmbBrandDetail.Enabled = isEdit;
            cmbCategoryDetail.Enabled = isEdit;

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
            cmbBrand.Enabled = !isEdit;
            cmbCategory.Enabled = !isEdit;

            dgvGood.Enabled = !isEdit;
            txtGoodId.Enabled = false; 
        }

        private void ClearForm()
        {
            txtGoodId.Clear();
            SetPlaceholder(txtGoodId, "Mã hàng hóa");
            txtGoodId.ForeColor = System.Drawing.Color.Gray;

            txtProductName.Clear();

            if (cmbBrandDetail != null)
            {
                cmbBrandDetail.SelectedIndex = -1;
            }
            if (cmbCategoryDetail != null)
            {
                cmbCategoryDetail.SelectedIndex = -1;
            }

            _selectedGoodId = null;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _goodsController?.Dispose();
            base.OnFormClosing(e);
        }
        #endregion
    }
}