using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Products
{
    public partial class frmBrands : Form
    {
        private readonly BrandController _brandController;
        private string _selectedBrandId;
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
        public frmBrands()
        {
            InitializeComponent();
            _brandController = new BrandController();

            SetPlaceholder(txtSearch, "Nhập tên hoặc mã nhãn hiệu để tìm...");

            //this.Load += frmBrands_Load;
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

            if (dgvBrands != null) this.dgvBrands.SelectionChanged += dgvBrands_SelectionChanged;
            if (txtSearch != null) txtSearch.KeyPress += (s, e) => { if (e.KeyChar == 13) { btnSearch_Click(s, e); e.Handled = true; } };
            if (txtBrandName != null) txtBrandName.KeyPress += (s, e) => { if (e.KeyChar == 13) { btnSave_Click(s, e); e.Handled = true; } };

            this.VisibleChanged += (s, e) => {
                if (this.Visible) LoadBrands();
            };
        }

        private void frmBrands_Load(object sender, EventArgs e)
        {
            try
            {
                if (dgvBrands != null) dgvBrands.AutoGenerateColumns = false;
                LoadBrands();
                SetFormMode(false);
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi tải form: {ex.Message}"); }
        }

        private void LoadBrands()
        {
            try
            {
                var brands = _brandController.GetAllBrands();
                dgvBrands.DataSource = brands;

                // Mapping cột thủ công
                foreach (DataGridViewColumn col in dgvBrands.Columns)
                {
                    if (col.HeaderText.ToLower().Contains("mã")) col.DataPropertyName = "Id";
                    else if (col.HeaderText.ToLower().Contains("tên")) col.DataPropertyName = "Ten";
                    else if (col.HeaderText.ToLower().Contains("số lượng")) col.DataPropertyName = "SoLuongSanPham";
                }

                if (brands is IList list) lblStatus.Text = $"Tổng số: {list.Count} nhãn hiệu";
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi hiển thị: {ex.Message}"); }
        }

        // --- HÀM QUAN TRỌNG: Lấy ID an toàn ---
        private string GetCurrentId()
        {
            if (dgvBrands.CurrentRow == null) return null;

            var dataItem = dgvBrands.CurrentRow.DataBoundItem;
            if (dataItem != null)
            {
                var prop = dataItem.GetType().GetProperty("Id");
                if (prop != null) return prop.GetValue(dataItem)?.ToString();
            }

            // Fallback
            if (dgvBrands.ColumnCount > 0 && dgvBrands.CurrentRow.Cells[0].Value != null)
                return dgvBrands.CurrentRow.Cells[0].Value.ToString();

            return null;
        }

        private void dgvBrands_SelectionChanged(object sender, EventArgs e)
        {
            if (!_isAddMode)
            {
                string id = GetCurrentId();
                if (!string.IsNullOrEmpty(id))
                {
                    _selectedBrandId = id;
                    LoadBrandDetail(id);
                }
            }
        }

        private void LoadBrandDetail(string brandId)
        {
            if (string.IsNullOrEmpty(brandId)) return;
            try
            {
                var brand = _brandController.GetBrandById(brandId);
                if (brand != null)
                {
                    txtBrandId.Text = brand.id;
                    txtBrandName.Text = brand.ten;
                    int count = _brandController.GetProductCount(brandId);
                    lblProductCount.Text = $"Số sản phẩm: {count}";
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

            try
            {
                txtBrandId.Text = _brandController.GenerateNewBrandId();
            }
            catch
            {
                txtBrandId.Text = "Tự động tạo";
            }

            txtBrandName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string id = GetCurrentId();
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Vui lòng chọn nhãn hiệu!");
                return;
            }

            _selectedBrandId = id;
            SetFormMode(true);
            _isAddMode = false;
            txtBrandName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = GetCurrentId();
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Vui lòng chọn nhãn hiệu!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var (success, message) = _brandController.DeleteBrand(id);
                if (success)
                {
                    MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBrands();
                    ClearForm();
                }
                else MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBrandName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhãn hiệu!");
                txtBrandName.Focus();
                return;
            }

            try
            {
                if (_isAddMode)
                {
                    var newBrand = new NhanHieu
                    {
                        id = txtBrandId.Text.Trim(),
                        ten = txtBrandName.Text.Trim(),
                        isDelete = false
                    };
                    var (success, message, _) = _brandController.AddBrand(newBrand);

                    if (success)
                    {
                        MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBrands();
                        SetFormMode(false);
                    }
                    else MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var updateBrand = new NhanHieu { id = _selectedBrandId, ten = txtBrandName.Text.Trim() };
                    var (success, message) = _brandController.UpdateBrand(updateBrand);

                    if (success)
                    {
                        MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBrands();
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
            if (!string.IsNullOrEmpty(currentId)) LoadBrandDetail(currentId);
            else ClearForm();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var list = _brandController.SearchBrands(txtSearch.Text.Trim());
            dgvBrands.DataSource = list;
            if (list is IList l) lblStatus.Text = $"Tìm thấy: {l.Count}";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadBrands();
            ClearForm();
        }

        private void btnExport_Click(object sender, EventArgs e) => ExportToExcel(dgvBrands);

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
                    sfd.FileName = "DanhSachNhanHieu_" + DateTime.Now.ToString("ddMMyyy_HHmmss") + ".xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (var workbook = new ClosedXML.Excel.XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Nhãn Hiệu");

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
            txtBrandName.Enabled = isEdit;
            btnSave.Enabled = isEdit;
            btnCancel.Enabled = isEdit;

            btnAdd.Enabled = !isEdit;
            btnEdit.Enabled = !isEdit;
            btnDelete.Enabled = !isEdit;
            btnSearch.Enabled = !isEdit;
            btnRefresh.Enabled = !isEdit;
            btnExport.Enabled = !isEdit;

            dgvBrands.Enabled = !isEdit;
            txtBrandId.Enabled = false; // Khóa ID
        }

        private void ClearForm()
        {
            txtBrandId.Clear();
            txtBrandName.Clear();
            lblProductCount.Text = "Số sản phẩm: 0";
            _selectedBrandId = null;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _brandController?.Dispose();
            base.OnFormClosing(e);
        }
        #endregion
    }
}