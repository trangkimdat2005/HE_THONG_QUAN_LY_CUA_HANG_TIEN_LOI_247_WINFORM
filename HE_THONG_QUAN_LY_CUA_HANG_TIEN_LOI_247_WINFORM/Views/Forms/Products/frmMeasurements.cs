using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Products
{
    public partial class frmMeasurements : Form
    {
        private readonly MeasurementController _measurementController;
        private string _selectedUnitId;
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
        public frmMeasurements()
        {
            InitializeComponent();
            _measurementController = new MeasurementController();

            SetPlaceholder(txtSearch, "Nhập tên hoặc mã danh mục để tìm...");

            //this.Load += frmMeasurements_Load;
            //if (btnAdd != null) this.btnAdd.Click += btnAdd_Click;
            //if (btnEdit != null) this.btnEdit.Click += btnEdit_Click;
            //if (btnDelete != null) this.btnDelete.Click += btnDelete_Click;
            //if (btnSave != null) this.btnSave.Click += btnSave_Click;
            //if (btnCancel != null) this.btnCancel.Click += btnCancel_Click;
            //if (btnSearch != null) this.btnSearch.Click += btnSearch_Click;
            //if (btnRefresh != null) this.btnRefresh.Click += btnRefresh_Click;
            //if (btnExport != null) this.btnExport.Click += btnExport_Click;

            //if (dgvUnits != null) this.dgvUnits.SelectionChanged += dgvUnits_SelectionChanged;

            if (txtSearch != null)
            {
                txtSearch.TextChanged += (s, e) => { btnSearch_Click(null, null); };
                txtSearch.KeyPress += (s, e) => { if (e.KeyChar == 13) { btnSearch_Click(s, e); e.Handled = true; } };
            }
            if (txtUnitName != null) txtUnitName.KeyPress += (s, e) => { if (e.KeyChar == 13) { txtSymbol?.Focus(); e.Handled = true; } };
            if (txtSymbol != null) txtSymbol.KeyPress += (s, e) => { if (e.KeyChar == 13) { btnSave_Click(s, e); e.Handled = true; } };



        }

        private void frmMeasurements_Load(object sender, EventArgs e)
        {
            try
            {
                if (dgvUnits != null) dgvUnits.AutoGenerateColumns = false;
                LoadUnits();
                SetFormMode(false);
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi tải form: {ex.Message}"); }
        }

        private void LoadUnits()
        {
            try
            {
                var units = _measurementController.GetAllMeasurements();
                dgvUnits.DataSource = units;

                foreach (DataGridViewColumn col in dgvUnits.Columns)
                {
                    if (col.Name == "Id" || col.HeaderText.Contains("Mã"))
                        col.DataPropertyName = "Id";

                    else if (col.Name == "Ten" || col.HeaderText.Contains("Tên"))
                        col.DataPropertyName = "Ten";
                    else if (col.Name == "KyHieu" || col.HeaderText.Contains("Ký hiệu") || col.HeaderText.Contains("Kí hiệu") || col.HeaderText.Contains("Kí Hiệu"))
                        col.DataPropertyName = "KyHieu";
                }

                if (units is IList list) lblStatus.Text = $"Tổng số: {list.Count} đơn vị";
            }
            catch (Exception ex) { MessageBox.Show($"Lỗi hiển thị: {ex.Message}"); }
        }

        private string GetCurrentId()
        {
            if (dgvUnits.CurrentRow == null) return null;

            var dataItem = dgvUnits.CurrentRow.DataBoundItem;
            if (dataItem != null)
            {
                var prop = dataItem.GetType().GetProperty("Id");
                if (prop != null) return prop.GetValue(dataItem)?.ToString();
            }

            if (dgvUnits.ColumnCount > 0 && dgvUnits.CurrentRow.Cells[0].Value != null)
                return dgvUnits.CurrentRow.Cells[0].Value.ToString();

            return null;
        }

        private void dgvUnits_SelectionChanged(object sender, EventArgs e)
        {
            if (!_isAddMode)
            {
                string id = GetCurrentId();
                if (!string.IsNullOrEmpty(id))
                {
                    _selectedUnitId = id;
                    LoadUnitDetail(id);
                }
            }
        }

        private void LoadUnitDetail(string unitId)
        {
            if (string.IsNullOrEmpty(unitId)) return;
            try
            {
                var unit = _measurementController.GetMeasurementById(unitId);
                if (unit != null)
                {
                    txtUnitId.Text = unit.id;
                    txtUnitName.Text = unit.ten;
                    txtSymbol.Text = unit.kyHieu;

                    int count = _measurementController.GetProductCount(unitId);
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
                txtUnitId.Text = _measurementController.GenerateNewMeasurementId();
            }
            catch
            {
                txtUnitId.Text = "Tự động tạo";
            }

            txtUnitName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string id = GetCurrentId();
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Vui lòng chọn đơn vị!");
                return;
            }

            _selectedUnitId = id;
            SetFormMode(true);
            _isAddMode = false;
            txtUnitName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = GetCurrentId();
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Vui lòng chọn đơn vị!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var (success, message) = _measurementController.DeleteMeasurement(id);
                if (success)
                {
                    MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUnits();
                    ClearForm();
                }
                else MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                if (_isAddMode)
                {
                    var newUnit = new DonViDoLuong
                    {
                        id = txtUnitId.Text.Trim(), // Lấy mã đã sinh
                        ten = txtUnitName.Text.Trim(),
                        kyHieu = txtSymbol.Text.Trim(),
                        isDelete = false
                    };
                    var (success, message, _) = _measurementController.AddMeasurement(newUnit);

                    if (success)
                    {
                        MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUnits();
                        SetFormMode(false);
                    }
                    else MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var updatedUnit = new DonViDoLuong
                    {
                        id = _selectedUnitId,
                        ten = txtUnitName.Text.Trim(),
                        kyHieu = txtSymbol.Text.Trim()
                    };
                    var (success, message) = _measurementController.UpdateMeasurement(updatedUnit);

                    if (success)
                    {
                        MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUnits();
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
            if (!string.IsNullOrEmpty(currentId)) LoadUnitDetail(currentId);
            else ClearForm();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var list = _measurementController.SearchMeasurements(txtSearch.Text.Trim());
            dgvUnits.DataSource = list;
            if (list is IList l) lblStatus.Text = $"Tìm thấy: {l.Count}";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadUnits();
            ClearForm();
        }

        private void btnExport_Click(object sender, EventArgs e) => MessageBox.Show("Đang phát triển!");

        #endregion

        #region Helpers
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtUnitName.Text))
            {
                MessageBox.Show("Nhập tên đơn vị!");
                txtUnitName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSymbol.Text))
            {
                MessageBox.Show("Nhập ký hiệu!");
                txtSymbol.Focus();
                return false;
            }
            return true;
        }

        private void SetFormMode(bool isEdit)
        {
            txtUnitName.Enabled = isEdit;
            txtSymbol.Enabled = isEdit;
            btnSave.Enabled = isEdit;
            btnCancel.Enabled = isEdit;

            btnAdd.Enabled = !isEdit;
            btnEdit.Enabled = !isEdit;
            btnDelete.Enabled = !isEdit;
            btnSearch.Enabled = !isEdit;
            btnRefresh.Enabled = !isEdit;
            btnExport.Enabled = !isEdit;

            dgvUnits.Enabled = !isEdit;
            txtUnitId.Enabled = false; // Khóa ID
        }

        private void ClearForm()
        {
            txtUnitId.Clear();
            txtUnitName.Clear();
            txtSymbol.Clear();
            lblProductCount.Text = "Số sản phẩm: 0";
            _selectedUnitId = null;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _measurementController?.Dispose();
            base.OnFormClosing(e);
        }
        #endregion
    }
}