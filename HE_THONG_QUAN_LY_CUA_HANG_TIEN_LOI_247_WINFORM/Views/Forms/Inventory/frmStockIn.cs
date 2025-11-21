using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Utils; // <-- Thêm using cho UserSession

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.Forms.Inventory
{
    public partial class frmStockIn : Form
    {
        private readonly StockInController _stockInController;
        private List<ImportDetailItem> _detailList;
        private decimal _totalAmount = 0;

        private class ImportDetailItem
        {
            public string SanPhamDonViId { get; set; }
            public string SanPham { get; set; }
            public int SoLuong { get; set; }
            public decimal DonGia { get; set; }
            public DateTime? HanSuDung { get; set; }
            public decimal ThanhTien { get; set; }
        }

        public frmStockIn()
        {
            InitializeComponent();
            _stockInController = new StockInController();
            _detailList = new List<ImportDetailItem>();

            SetupDataGridView();
            this.Load += frmStockIn_Load;

            // Đăng ký sự kiện - KHÔNG COMMENT OUT
            if (btnAdd != null) btnAdd.Click += btnAdd_Click;
            if (btnSave != null) btnSave.Click += btnSave_Click;
            if (btnCancel != null) btnCancel.Click += btnCancel_Click;
            if (dgvDetail != null) dgvDetail.CellContentClick += dgvDetail_CellContentClick;
            
            // Auto-load NCC khi chọn sản phẩm
            if (cboProduct != null)
            {
                cboProduct.SelectedIndexChanged += cboProduct_SelectedIndexChanged;
            }
        }

        private void frmStockIn_Load(object sender, EventArgs e)
        {
            try
            {
                LoadSuppliers();
                LoadEmployees();
                LoadProducts();

                // Set ngày mặc định
                dtpDate.Value = DateTime.Now;
                dtpExpiry.Value = DateTime.Now.AddMonths(6);

                // ✅ TỰ ĐỘNG CHỌN NHÂN VIÊN ĐANG ĐĂNG NHẬP
                SetCurrentEmployee();

                UpdateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo form: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tự động chọn nhân viên đang đăng nhập và disable ComboBox
        /// </summary>
        private void SetCurrentEmployee()
        {
            try
            {
                var session = UserSession.Instance;
                
                if (!session.IsLoggedIn || string.IsNullOrEmpty(session.EmployeeId))
                {
                    MessageBox.Show("Không tìm thấy thông tin nhân viên đăng nhập!\nVui lòng đăng nhập lại.", 
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboStaff != null)
                {
                    cboStaff.SelectedValue = session.EmployeeId;
                    
                    cboStaff.Enabled = false;
                    
                    cboStaff.BackColor = Color.FromArgb(240, 240, 240); 
                    
                    ToolTip tooltip = new ToolTip();
                    tooltip.SetToolTip(cboStaff, 
                        $"Nhân viên: {session.EmployeeName}\n" +
                        $"Chức vụ: {session.Position}\n" +
                        $"(Tự động lấy từ tài khoản đăng nhập)");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thiết lập nhân viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            dgvDetail.BackgroundColor = Color.White;
            dgvDetail.BorderStyle = BorderStyle.None;
            dgvDetail.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDetail.GridColor = Color.FromArgb(230, 230, 230);
            dgvDetail.RowHeadersVisible = false;
            dgvDetail.EnableHeadersVisualStyles = false;

            dgvDetail.ColumnHeadersHeight = 45;
            dgvDetail.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvDetail.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDetail.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            dgvDetail.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            dgvDetail.RowTemplate.Height = 40;
            dgvDetail.DefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 245, 255);
            dgvDetail.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvDetail.DefaultCellStyle.Font = new Font("Segoe UI", 9.75F);
            dgvDetail.DefaultCellStyle.Padding = new Padding(5, 0, 0, 0);

            if (colQty != null) colQty.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (colPrice != null) colPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            if (colTotal != null) colTotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void LoadSuppliers()
        {
            try
            {
                var suppliers = _stockInController.GetAllSuppliers();
                cboSupplier.DataSource = suppliers;
                cboSupplier.DisplayMember = "ten";
                cboSupplier.ValueMember = "id";
                cboSupplier.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load nhà cung cấp: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadEmployees()
        {
            try
            {
                var employees = _stockInController.GetAllEmployees();
                cboStaff.DataSource = employees;
                cboStaff.DisplayMember = "hoTen";
                cboStaff.ValueMember = "id";
                cboStaff.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load nhân viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                var products = _stockInController.GetAllProductUnits();
                cboProduct.DataSource = products;
                cboProduct.DisplayMember = "Ten";
                cboProduct.ValueMember = "Id";
                cboProduct.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load sản phẩm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboProduct.SelectedValue == null || cboProduct.SelectedIndex == -1)
                return;

            try
            {
                string sanPhamDonViId = cboProduct.SelectedValue.ToString();
                
                // Lấy nhà cung cấp gần nhất cho sản phẩm này
                string supplierId = _stockInController.GetLastSupplierByProduct(sanPhamDonViId);
                
                if (!string.IsNullOrEmpty(supplierId) && cboSupplier != null)
                {
                    cboSupplier.SelectedValue = supplierId;
                }
            }
            catch
            {
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboSupplier.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn nhà cung cấp trước khi thêm sản phẩm!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboSupplier.Focus();
                    return;
                }

                if (cboStaff.SelectedValue == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin nhân viên!\nVui lòng đăng nhập lại.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (cboProduct.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboProduct.Focus();
                    return;
                }

                if (txtQty.Value <= 0)
                {
                    MessageBox.Show("Số lượng phải lớn hơn 0!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQty.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPrice.Text))
                {
                    MessageBox.Show("Vui lòng nhập đơn giá nhập!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPrice.Focus();
                    return;
                }

                decimal donGia = 0;
                if (!decimal.TryParse(txtPrice.Text, out donGia) || donGia <= 0)
                {
                    MessageBox.Show("Đơn giá phải là số và lớn hơn 0!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPrice.Focus();
                    return;
                }

                // Validation: Hạn sử dụng phải lớn hơn ngày hiện tại
                if (dtpExpiry.Value.Date <= DateTime.Now.Date)
                {
                    MessageBox.Show("Hạn sử dụng phải lớn hơn ngày hiện tại!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpExpiry.Focus();
                    return;
                }

                // Kiểm tra sản phẩm đã tồn tại trong danh sách chưa
                string sanPhamDonViId = cboProduct.SelectedValue.ToString();
                if (_detailList.Any(d => d.SanPhamDonViId == sanPhamDonViId))
                {
                    MessageBox.Show("Sản phẩm này đã có trong phiếu nhập!\nVui lòng chọn sản phẩm khác hoặc xóa sản phẩm cũ trước.", 
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Thêm vào danh sách
                var item = new ImportDetailItem
                {
                    SanPhamDonViId = sanPhamDonViId,
                    SanPham = cboProduct.Text,
                    SoLuong = (int)txtQty.Value,
                    DonGia = donGia,
                    HanSuDung = dtpExpiry.Value,
                    ThanhTien = (int)txtQty.Value * donGia
                };

                _detailList.Add(item);
                RefreshGrid();
                UpdateTotal();
                ClearInput();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm sản phẩm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshGrid()
        {
            dgvDetail.Rows.Clear();
            foreach (var item in _detailList)
            {
                dgvDetail.Rows.Add(
                    item.SanPham,
                    item.SoLuong,
                    $"{item.DonGia:N0} đ",
                    item.HanSuDung?.ToString("dd/MM/yyyy") ?? "",
                    $"{item.ThanhTien:N0} đ",
                    "Xóa"
                );
            }
        }

        private void UpdateTotal()
        {
            _totalAmount = _detailList.Sum(d => d.ThanhTien);
            lblTotalMoney.Text = $"{_totalAmount:N0} đ";
        }

        private void ClearInput()
        {
            cboProduct.SelectedIndex = -1;
            txtQty.Value = 1;
            txtPrice.Clear();
            dtpExpiry.Value = DateTime.Now.AddMonths(6);
            cboProduct.Focus();
        }

        private void dgvDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == colDelete.Index && e.RowIndex >= 0)
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _detailList.RemoveAt(e.RowIndex);
                    RefreshGrid();
                    UpdateTotal();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboSupplier.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboSupplier.Focus();
                    return;
                }

                if (cboStaff.SelectedValue == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin nhân viên!\nVui lòng đăng nhập lại.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_detailList.Count == 0)
                {
                    MessageBox.Show("Vui lòng thêm ít nhất 1 sản phẩm vào phiếu nhập!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboProduct.Focus();
                    return;
                }

                var chiTietList = _detailList.Select(d => new ChiTietPhieuNhap
                {
                    sanPhamDonViId = d.SanPhamDonViId,
                    soLuong = d.SoLuong,
                    donGia = d.DonGia,
                    tongTien = d.ThanhTien,
                    hanSuDung = d.HanSuDung.HasValue ? d.HanSuDung.Value : DateTime.Now.AddMonths(6),  
                    isDelete = false
                }).ToList();

                // Lưu phiếu nhập
                var (success, message, phieuNhapId) = _stockInController.CreateImportReceipt(
                    cboSupplier.SelectedValue.ToString(),
                    cboStaff.SelectedValue.ToString(),
                    dtpDate.Value,
                    chiTietList
                );

                if (success)
                {
                    var session = UserSession.Instance;
                    MessageBox.Show(
                        $" Lưu phiếu nhập thành công!\n\n" +
                        $"Mã phiếu: {phieuNhapId}\n" +
                        $"Người nhập: {session.EmployeeName}\n" +
                        $"Tổng tiền: {_totalAmount:N0} đ",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"✗ {message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"✗ Lỗi lưu phiếu nhập:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_detailList.Count > 0)
            {
                if (MessageBox.Show("Bạn có chắc muốn hủy? Dữ liệu chưa lưu sẽ mất!", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _stockInController?.Dispose();
            base.OnFormClosing(e);
        }
    }
}