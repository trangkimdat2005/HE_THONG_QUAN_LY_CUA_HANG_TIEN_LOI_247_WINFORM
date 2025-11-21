using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection; // Cần cái này để dùng Reflection lấy ID
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Products
{
    public partial class frmCategorys : Form
    {
        private readonly CategoryController _categoryController;
        private string _selectedCategoryId;
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
        public frmCategorys()
        {
            InitializeComponent();
            _categoryController = new CategoryController();

            SetPlaceholder(txtSearch, "Nhập tên hoặc mã ký hiệu để tìm...");

            //// --- ĐĂNG KÝ SỰ KIỆN THỦ CÔNG (Đảm bảo nút luôn chạy) ---
            //this.Load += frmCategorys_Load;
            //if (btnAdd != null) this.btnAdd.Click += btnAdd_Click;
            //if (btnEdit != null) this.btnEdit.Click += btnEdit_Click;
            //if (btnDelete != null) this.btnDelete.Click += btnDelete_Click;
            //if (btnSave != null) this.btnSave.Click += btnSave_Click;
            //if (btnCancel != null) this.btnCancel.Click += btnCancel_Click;
            //if (btnSearch != null) this.btnSearch.Click += btnSearch_Click;
            //if (btnRefresh != null) this.btnRefresh.Click += btnRefresh_Click;
            //if (btnExport != null) this.btnExport.Click += btnExport_Click;
            if (txtSearch != null)
            {
                txtSearch.TextChanged += (s, e) => { btnSearch_Click(null, null); };
            }

            if (dgvCategories != null) this.dgvCategories.SelectionChanged += dgvCategories_SelectionChanged;
            if (txtSearch != null) txtSearch.KeyPress += (s, e) => { if (e.KeyChar == 13) { btnSearch_Click(s, e); e.Handled = true; } };
            if (txtCategoryName != null) txtCategoryName.KeyPress += (s, e) => { if (e.KeyChar == 13) { btnSave_Click(s, e); e.Handled = true; } };

            this.VisibleChanged += (s, e) => {
                if (this.Visible) LoadCategories();
            };
        }

        private void frmCategorys_Load(object sender, EventArgs e)
        {
            try
            {
                if (dgvCategories != null) dgvCategories.AutoGenerateColumns = false;
                LoadCategories();
                SetFormMode(false);
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi tải form: {ex.Message}"); }
        }

        private void LoadCategories()
        {
            try
            {
                var categories = _categoryController.GetAllCategories();
                dgvCategories.DataSource = categories;

                if (dgvCategories.Columns.Count >= 3)
                {
                    dgvCategories.Columns[0].DataPropertyName = "Id";

                    dgvCategories.Columns[1].DataPropertyName = "Ten";

                    dgvCategories.Columns[2].DataPropertyName = "SoLuongSanPham";

                    dgvCategories.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }

                if (categories is IList list) lblStatus.Text = $"Tổng số: {list.Count} danh mục";
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi hiển thị: {ex.Message}"); }
        }
        // --- HÀM QUAN TRỌNG: Lấy ID an toàn từ dòng đang chọn ---
        private string GetCurrentId()
        {
            if (dgvCategories.CurrentRow == null) return null;

            // Cách 1: Lấy từ DataBoundItem (Dynamic Object)
            var dataItem = dgvCategories.CurrentRow.DataBoundItem;
            if (dataItem != null)
            {
                var prop = dataItem.GetType().GetProperty("Id");
                if (prop != null) return prop.GetValue(dataItem)?.ToString();
            }

            // Cách 2: Lấy từ ô Cell (Fallback)
            if (dgvCategories.Columns.Contains("Id") && dgvCategories.CurrentRow.Cells["Id"].Value != null)
                return dgvCategories.CurrentRow.Cells["Id"].Value.ToString();

            // Lấy cột đầu tiên
            if (dgvCategories.ColumnCount > 0 && dgvCategories.CurrentRow.Cells[0].Value != null)
                return dgvCategories.CurrentRow.Cells[0].Value.ToString();

            return null;
        }

        private void dgvCategories_SelectionChanged(object sender, EventArgs e)
        {
            if (!_isAddMode)
            {
                string id = GetCurrentId();
                if (!string.IsNullOrEmpty(id))
                {
                    _selectedCategoryId = id;
                    LoadCategoryDetail(id);
                }
            }
        }

        private void LoadCategoryDetail(string categoryId)
        {
            if (string.IsNullOrEmpty(categoryId)) return;
            try
            {
                var category = _categoryController.GetCategoryById(categoryId);
                if (category != null)
                {
                    txtCategoryId.Text = category.id;
                    txtCategoryName.Text = category.ten;

                    if (dgvCategories.CurrentRow != null)
                    {
                        var val = dgvCategories.CurrentRow.Cells[2].Value;
                        lblProductCount.Text = $"Số sản phẩm: {val}";
                    }
                }
            }
            catch { }
        }

        #region CRUD Actions

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetFormMode(true);
            _isAddMode = true;
            ClearForm();

            // Sinh mã tự động
            try
            {
                txtCategoryId.Text = _categoryController.GenerateNewCategoryId();
            }
            catch
            {
                txtCategoryId.Text = "Tự động tạo";
            }

            txtCategoryName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string id = GetCurrentId(); // Lấy ID trực tiếp
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Vui lòng chọn danh mục!");
                return;
            }

            _selectedCategoryId = id;
            SetFormMode(true);
            _isAddMode = false;
            txtCategoryName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = GetCurrentId(); // Lấy ID trực tiếp
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Vui lòng chọn danh mục!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var (success, message) = _categoryController.DeleteCategory(id);
                if (success)
                {
                    MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategories();
                    ClearForm();
                }
                else MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục!");
                txtCategoryName.Focus();
                return;
            }

            try
            {
                if (_isAddMode)
                {
                    var newCat = new DanhMuc
                    {
                        id = txtCategoryId.Text.Trim(),
                        ten = txtCategoryName.Text.Trim(),
                        isDelete = false
                    };
                    var (success, message, _) = _categoryController.AddCategory(newCat);

                    if (success)
                    {
                        MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCategories();
                        SetFormMode(false);
                    }
                    else MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var updateCat = new DanhMuc { id = _selectedCategoryId, ten = txtCategoryName.Text.Trim() };
                    var (success, message) = _categoryController.UpdateCategory(updateCat);

                    if (success)
                    {
                        MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCategories();
                        SetFormMode(false);
                    }
                    else MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi hệ thống: " + ex.Message); }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetFormMode(false);
            _isAddMode = false;
            string currentId = GetCurrentId();
            if (!string.IsNullOrEmpty(currentId)) LoadCategoryDetail(currentId);
            else ClearForm();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var list = _categoryController.SearchCategories(txtSearch.Text.Trim());
            dgvCategories.DataSource = list;
            if (list is IList l) lblStatus.Text = $"Tìm thấy: {l.Count}";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadCategories();
            ClearForm();
        }

        private void btnExport_Click(object sender, EventArgs e) => ExportToExcel(dgvCategories);

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
                    sfd.FileName = "DanhSachDanhMuc_" + DateTime.Now.ToString("ddMMyy_Hmmss") + ".xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (var workbook = new ClosedXML.Excel.XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Danh Mục");

                            for (int i = 0; i < dgv.Columns.Count; i++)
                            {
                                worksheet.Cell(1, i + 1).Value = dgv.Columns[i].HeaderText;
                                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                                worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGray;
                            }

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
            txtCategoryName.Enabled = isEdit;
            btnSave.Enabled = isEdit;
            btnCancel.Enabled = isEdit;

            btnAdd.Enabled = !isEdit;
            btnEdit.Enabled = !isEdit;
            btnDelete.Enabled = !isEdit;
            btnSearch.Enabled = !isEdit;
            btnRefresh.Enabled = !isEdit;
            btnExport.Enabled = !isEdit;

            dgvCategories.Enabled = !isEdit;
            txtCategoryId.Enabled = false;
        }

        private void ClearForm()
        {
            txtCategoryId.Clear();
            txtCategoryName.Clear();
            lblProductCount.Text = "Số sản phẩm: 0";
            _selectedCategoryId = null;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _categoryController?.Dispose();
            base.OnFormClosing(e);
        }
        #endregion
    }
}