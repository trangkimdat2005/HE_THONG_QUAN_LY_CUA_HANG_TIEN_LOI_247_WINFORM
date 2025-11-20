using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices; // <--- 1. Thêm thư viện này
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Inventory
{
    public partial class frmInventoryMain : Form
    {
        private readonly StockInController _stockInController;

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
        public frmInventoryMain()
        {
            InitializeComponent();
            _stockInController = new StockInController();

            SetPlaceholder(txtSearch, "Nhập mã phiếu hoặc tên nhà cung cấp...");

            this.Load += frmInventoryMain_Load;
            //if (btnSearch != null) btnSearch.Click += btnSearch_Click;
            //if (btnAdd != null) btnAdd.Click += btnAdd_Click;
            //if (btnDetail != null) btnDetail.Click += btnDetail_Click;
            if (btnDelete != null) btnDelete.Click += btnDelete_Click;

            if (txtSearch != null)
            {
                txtSearch.TextChanged += (s, e) => { PerformSearch(); };
                txtSearch.KeyPress += (s, e) =>
                {
                    if (e.KeyChar == (char)Keys.Enter)
                    {
                        PerformSearch();
                        e.Handled = true;
                    }
                };
            }
        }

        private void frmInventoryMain_Load(object sender, EventArgs e)
        {
            try
            {
                if (dgvImportList != null)
                {
                    dgvImportList.AutoGenerateColumns = false;
                }
                LoadImportReceipts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load danh sách phiếu nhập
        private void LoadImportReceipts()
        {
            try
            {
                var receipts = _stockInController.GetAllImportReceipts();
                dgvImportList.DataSource = receipts;

                MapDataGridViewColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Map cột DataGridView
        private void MapDataGridViewColumns()
        {
            if (dgvImportList.Columns.Count == 0) return;

            if (dgvImportList.Columns["colId"] != null)
                dgvImportList.Columns["colId"].DataPropertyName = "Id";

            if (dgvImportList.Columns["colSupplier"] != null)
                dgvImportList.Columns["colSupplier"].DataPropertyName = "NhaCungCap";

            if (dgvImportList.Columns["colDate"] != null)
            {
                dgvImportList.Columns["colDate"].DataPropertyName = "NgayNhap";
                dgvImportList.Columns["colDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (dgvImportList.Columns["colTotal"] != null)
            {
                dgvImportList.Columns["colTotal"].DataPropertyName = "TongTien";
                dgvImportList.Columns["colTotal"].DefaultCellStyle.Format = "#,##0 đ";
                dgvImportList.Columns["colTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvImportList.Columns["colStatus"] != null)
                dgvImportList.Columns["colStatus"].DataPropertyName = "NhanVien";
        }

        // Tìm kiếm phiếu nhập
        private void PerformSearch()
        {
            try
            {
                string keyword = txtSearch.Text.Trim();

                if (string.IsNullOrEmpty(keyword))
                {
                    LoadImportReceipts();
                    return;
                }

                var searchResults = _stockInController.SearchImportReceipts(keyword);
                dgvImportList.DataSource = searchResults;
                MapDataGridViewColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        // Mở form tạo phiếu nhập mới
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var frmStockIn = new Views.Forms.Inventory.frmStockIn();

                DialogResult result = frmStockIn.ShowDialog();

                if (result == DialogResult.OK)
                {
                    LoadImportReceipts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form nhập hàng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xem chi tiết phiếu nhập
        private void btnDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvImportList.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn phiếu nhập để xem chi tiết!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string phieuNhapId = GetCurrentReceiptId();
                if (string.IsNullOrEmpty(phieuNhapId))
                {
                    MessageBox.Show("Không thể xác định mã phiếu nhập!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var details = _stockInController.GetImportReceiptDetails(phieuNhapId);

                string message = $"Chi tiết phiếu nhập: {phieuNhapId}\n\n";
                if (details is System.Collections.IList list)
                {
                    foreach (var item in list)
                    {
                        var itemType = item.GetType();
                        var sanPham = itemType.GetProperty("SanPham")?.GetValue(item);
                        var soLuong = itemType.GetProperty("SoLuong")?.GetValue(item);
                        var donGia = itemType.GetProperty("DonGia")?.GetValue(item);
                        var thanhTien = itemType.GetProperty("ThanhTien")?.GetValue(item);

                        message += $"{sanPham} - SL: {soLuong} - Giá: {donGia:N0}đ - Tổng: {thanhTien:N0}đ\n";
                    }
                }

                MessageBox.Show(message, "Chi tiết phiếu nhập",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xem chi tiết: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvImportList.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn phiếu nhập cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string phieuNhapId = GetCurrentReceiptId();
                if (string.IsNullOrEmpty(phieuNhapId)) return;

                // 3. Xác nhận xóa
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu nhập này?",
                    "Cảnh báo quan trọng",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    // 4. Gọi Controller xóa
                    var result = _stockInController.DeleteImportReceipt(phieuNhapId);

                    if (result.success)
                    {
                        MessageBox.Show(result.message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadImportReceipts(); // Tải lại danh sách
                    }
                    else
                    {
                        MessageBox.Show(result.message, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Lấy ID phiếu nhập hiện tại
        private string GetCurrentReceiptId()
        {
            if (dgvImportList.CurrentRow == null) return null;

            var dataItem = dgvImportList.CurrentRow.DataBoundItem;
            if (dataItem != null)
            {
                var prop = dataItem.GetType().GetProperty("Id");
                if (prop != null) return prop.GetValue(dataItem)?.ToString();
            }

            if (dgvImportList.Columns["colId"] != null && dgvImportList.CurrentRow.Cells["colId"].Value != null)
                return dgvImportList.CurrentRow.Cells["colId"].Value.ToString();

            return null;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _stockInController?.Dispose();
            base.OnFormClosing(e);
        }
    }
}