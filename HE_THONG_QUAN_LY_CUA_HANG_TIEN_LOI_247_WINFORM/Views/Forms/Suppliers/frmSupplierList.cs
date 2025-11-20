using ClosedXML.Excel;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Suppliers
{
    public partial class frmSupplierList : Form
    {
        private readonly IQuanLyServices _services;
        private string _selectedSupplierId;
        private bool _isAddMode = false;

        public frmSupplierList()
        {
            InitializeComponent();
            _services = new QuanLyServices();
            CustomizeInterface();
        }

        private void CustomizeInterface()
        {
            dgvSuppliers.BorderStyle = BorderStyle.None;
            dgvSuppliers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvSuppliers.GridColor = Color.FromArgb(230, 230, 230);
            dgvSuppliers.RowHeadersVisible = false;
            dgvSuppliers.EnableHeadersVisualStyles = false;
            dgvSuppliers.ColumnHeadersHeight = 40;
            dgvSuppliers.RowTemplate.Height = 40;

            dgvSuppliers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvSuppliers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSuppliers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvSuppliers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSuppliers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            dgvSuppliers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(211, 233, 252);
            dgvSuppliers.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvSuppliers.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvSuppliers.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

            // Gán các sự kiện ràng buộc nhập liệu từ EventService
            BatRangBuoc();
        }

        private void BatRangBuoc()
        {
            // Tên: Không được nhập số
            txtSupplierName.KeyPress += EventService.TextBox_KhongNhapSo_KeyPress;

            // SĐT: Chỉ nhập số
            //txtPhone.KeyPress += EventService.TextBox_KhongNhapChu_KeyPress;
            txtPhone.Validating += EventService.TextBox_SoDienThoai_Validating;

            // Mã số thuế: Chỉ nhập số
            txtTaxCode.KeyPress += EventService.TextBox_KhongNhapChu_KeyPress;

            // Email: Validate định dạng khi rời khỏi ô
            txtEmail.Validating += EventService.TextBox_Email_Validating;
        }

        private void frmSupplierList_Load(object sender, EventArgs e)
        {
            try
            {
                SetupDataGridView();
                LoadSuppliers();
                SetFormMode(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            dgvSuppliers.Columns.Clear();
            dgvSuppliers.AutoGenerateColumns = false;

            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                HeaderText = "Mã NCC",
                DataPropertyName = "id",
                Width = 100
            });

            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colName",
                HeaderText = "Tên nhà cung cấp",
                DataPropertyName = "ten",
                Width = 250,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPhone",
                HeaderText = "Số điện thoại",
                DataPropertyName = "soDienThoai",
                Width = 120
            });

            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colEmail",
                HeaderText = "Email",
                DataPropertyName = "email",
                Width = 180
            });

            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAddress",
                HeaderText = "Địa chỉ",
                DataPropertyName = "diaChi",
                Width = 200
            });

            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTaxCode",
                HeaderText = "Mã số thuế",
                DataPropertyName = "maSoThue",
                Width = 120
            });

            dgvSuppliers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "Trạng thái",
                DataPropertyName = "TrangThai",
                Width = 120
            });
        }

        private void LoadSuppliers()
        {
            try
            {
                var suppliers = _services.GetList<NhaCungCap>()
                    .Select(s => new
                    {
                        s.id,
                        s.ten,
                        s.soDienThoai,
                        s.email,
                        s.diaChi,
                        s.maSoThue,
                        TrangThai = s.isDelete ? "Không hoạt động" : "Hoạt động"
                    })
                    .OrderBy(s => s.ten)
                    .ToList();

                dgvSuppliers.DataSource = suppliers;
                lblStatus.Text = $"Tổng số: {suppliers.Count} nhà cung cấp";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSuppliers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSuppliers.CurrentRow != null && !_isAddMode)
            {
                _selectedSupplierId = dgvSuppliers.CurrentRow.Cells["colId"].Value?.ToString();
                LoadSupplierDetail(_selectedSupplierId);
            }
        }

        private void LoadSupplierDetail(string supplierId)
        {
            if (string.IsNullOrEmpty(supplierId)) return;

            try
            {
                var supplier = _services.GetById<NhaCungCap>(supplierId);

                if (supplier != null)
                {
                    txtSupplierId.Text = supplier.id;
                    txtSupplierName.Text = supplier.ten;
                    txtPhone.Text = supplier.soDienThoai;
                    txtEmail.Text = supplier.email;
                    txtAddress.Text = supplier.diaChi;
                    txtTaxCode.Text = supplier.maSoThue;

                    // Đếm số phiếu nhập (Client-side filtering vì Generic Service trả về List)
                    var listPhieuNhap = _services.GetList<PhieuNhap>();
                    int receiptCount = listPhieuNhap.Count(pn => pn.nhaCungCapId == supplierId);
                    lblReceiptCount.Text = $"Số phiếu nhập: {receiptCount}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải chi tiết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetFormMode(true);
            _isAddMode = true;
            ClearForm();

            txtSupplierId.Text = _services.GenerateNewId<NhaCungCap>("NCC", 7);
            txtSupplierName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetFormMode(true);
            _isAddMode = false;
            txtSupplierName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = dgvSuppliers.CurrentRow.Cells["colName"].Value.ToString();

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa nhà cung cấp '{name}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    var supplier = _services.GetById<NhaCungCap>(_selectedSupplierId);
                    if (supplier == null) return;

                    // Thử xóa cứng trước
                    if (_services.HardDelete(supplier))
                    {
                        MessageBox.Show("Đã xóa vĩnh viễn nhà cung cấp!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Nếu thất bại (do FK), chuyển sang xóa mềm
                        if (_services.SoftDelete(supplier))
                        {
                            MessageBox.Show("Không thể xóa vĩnh viễn do có dữ liệu liên quan.\nĐã chuyển sang trạng thái 'Ngưng hoạt động' (Xóa mềm).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    LoadSuppliers();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateInput()
        {
            // 1. Kiểm tra rỗng (Trừ Email, ID tự sinh)
            if (!EventService.KiemTraRong(
                (txtSupplierName, "Vui lòng nhập tên nhà cung cấp"),
                (txtPhone, "Vui lòng nhập số điện thoại"),
                (txtAddress, "Vui lòng nhập địa chỉ"),
                (txtTaxCode, "Vui lòng nhập mã số thuế")
            )) return false;

            // 3. Validate Mã số thuế (Đúng 12 số)
            if (txtTaxCode.Text.Trim().Length != 12 || !long.TryParse(txtTaxCode.Text.Trim(), out _))
            {
                MessageBox.Show("Mã số thuế không hợp lệ!\nYêu cầu: Phải đủ 12 chữ số.", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTaxCode.Focus();
                return false;
            }

            // 4. Validate Email (Nếu có nhập)
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(txtEmail.Text.Trim());
                    if (addr.Address != txtEmail.Text.Trim()) throw new FormatException();
                }
                catch
                {
                    MessageBox.Show("Email không hợp lệ!", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                var listSuppliers = _services.GetList<NhaCungCap>();
                string currentName = txtSupplierName.Text.Trim();

                if (_isAddMode)
                {
                    // Check trùng tên khi thêm mới
                    if (listSuppliers.Any(s => s.ten.Equals(currentName, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show("Tên nhà cung cấp đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSupplierName.Focus();
                        return;
                    }

                    var newSupplier = new NhaCungCap
                    {
                        id = _services.GenerateNewId<NhaCungCap>("NCC",7), 
                        ten = currentName,
                        soDienThoai = txtPhone.Text.Trim(),
                        email = txtEmail.Text.Trim(),
                        diaChi = txtAddress.Text.Trim(),
                        maSoThue = txtTaxCode.Text.Trim(),
                        isDelete = false
                    };

                    if (_services.Add(newSupplier))
                    {
                        MessageBox.Show("Thêm mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // Check trùng tên khi sửa (trừ chính nó)
                    if (listSuppliers.Any(s => s.id != _selectedSupplierId && s.ten.Equals(currentName, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageBox.Show("Tên nhà cung cấp đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSupplierName.Focus();
                        return;
                    }

                    var supplier = _services.GetById<NhaCungCap>(_selectedSupplierId);
                    if (supplier != null)
                    {
                        supplier.ten = currentName;
                        supplier.soDienThoai = txtPhone.Text.Trim();
                        supplier.email = txtEmail.Text.Trim();
                        supplier.diaChi = txtAddress.Text.Trim();
                        supplier.maSoThue = txtTaxCode.Text.Trim();

                        if (_services.Update(supplier))
                        {
                            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                LoadSuppliers();
                SetFormMode(false);
                _isAddMode = false;
                if (!string.IsNullOrEmpty(_selectedSupplierId)) LoadSupplierDetail(_selectedSupplierId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetFormMode(false);
            _isAddMode = false;
            if (!string.IsNullOrEmpty(_selectedSupplierId))
            {
                LoadSupplierDetail(_selectedSupplierId);
            }
            else
            {
                ClearForm();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            var listAll = _services.GetList<NhaCungCap>();

            var result = listAll.Where(s =>
                s.ten.ToLower().Contains(keyword) ||
                s.soDienThoai.Contains(keyword) ||
                s.maSoThue.Contains(keyword)
            ).Select(s => new
            {
                s.id,
                s.ten,
                s.soDienThoai,
                s.email,
                s.diaChi,
                s.maSoThue,
                TrangThai = s.isDelete ? "Không hoạt động" : "Hoạt động"
            }).ToList();

            dgvSuppliers.DataSource = result;
            lblStatus.Text = $"Tìm thấy: {result.Count} nhà cung cấp";
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadSuppliers();
            ClearForm();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvSuppliers.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Workbook|*.xlsx";
                    sfd.FileName = "DanhSachNhaCungCap.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("NhaCungCap");

                            // Header
                            for (int i = 0; i < dgvSuppliers.Columns.Count; i++)
                            {
                                worksheet.Cell(1, i + 1).Value = dgvSuppliers.Columns[i].HeaderText;
                                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                                worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightBlue;
                                worksheet.Cell(1, i + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            }

                            // Data
                            for (int i = 0; i < dgvSuppliers.Rows.Count; i++)
                            {
                                for (int j = 0; j < dgvSuppliers.Columns.Count; j++)
                                {
                                    var cell = worksheet.Cell(i + 2, j + 1);
                                    cell.Value = dgvSuppliers.Rows[i].Cells[j].Value?.ToString();
                                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                }
                            }

                            worksheet.Columns().AdjustToContents();
                            workbook.SaveAs(sfd.FileName);
                        }
                        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetFormMode(bool isEditMode)
        {
            txtSupplierName.Enabled = isEditMode;
            txtPhone.Enabled = isEditMode;
            txtEmail.Enabled = isEditMode;
            txtAddress.Enabled = isEditMode;
            txtTaxCode.Enabled = isEditMode;

            btnSave.Enabled = isEditMode;
            btnCancel.Enabled = isEditMode;

            btnAdd.Enabled = !isEditMode;
            btnEdit.Enabled = !isEditMode;
            btnDelete.Enabled = !isEditMode;
            btnSearch.Enabled = !isEditMode;
            btnRefresh.Enabled = !isEditMode;
            btnExport.Enabled = !isEditMode;

            dgvSuppliers.Enabled = !isEditMode;
        }

        private void ClearForm()
        {
            txtSupplierId.Clear();
            txtSupplierName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtTaxCode.Clear();
            lblReceiptCount.Text = "Số phiếu nhập: 0";
            _selectedSupplierId = null;
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(sender, e);
                e.Handled = true;
            }
        }

        private void txtSupplierName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPhone.Focus();
                e.Handled = true;
            }
        }
    }
}