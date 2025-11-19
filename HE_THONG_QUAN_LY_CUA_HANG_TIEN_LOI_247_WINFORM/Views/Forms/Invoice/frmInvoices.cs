using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Bills
{
    public partial class frmInvoices : Form
    {
        private InvoiceController _invoiceController;
        private string _selectedInvoiceId;

        public frmInvoices()
        {
            InitializeComponent();
            _invoiceController = new InvoiceController();
        }

        private void frmInvoices_Load(object sender, EventArgs e)
        {
            InitializeUI();
            LoadInvoices();
        }

        private void InitializeUI()
        {
            // Setup date pickers
            dtpFromDate.Value = DateTime.Now.AddMonths(-1);
            dtpToDate.Value = DateTime.Now;

            // Setup status filter
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new object[] { "Tất cả", "Chờ thanh toán", "Đang thanh toán", "Đã thanh toán" });
            cmbStatus.SelectedIndex = 0;

            // Setup DataGridView
            dgvInvoices.AutoGenerateColumns = false;
            dgvInvoices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInvoices.MultiSelect = false;
        }

        private void LoadInvoices()
        {
            try
            {
                var invoices = _invoiceController.GetAllInvoices();
                DisplayInvoices(invoices);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách hóa đơn: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayInvoices(List<HoaDon> invoices)
        {
            dgvInvoices.Rows.Clear();

            foreach (var invoice in invoices)
            {
                int rowIndex = dgvInvoices.Rows.Add(
                    invoice.id,
                    invoice.ngayLap.ToString("dd/MM/yyyy HH:mm"),
                    invoice.NhanVien?.hoTen ?? "N/A",
                    invoice.KhachHang?.hoTen ?? "Khách lẻ",
                    (invoice.tongTien ?? 0).ToString("N0") + " đ",
                    invoice.trangThai
                );

                // Set status badge color
                var statusCell = dgvInvoices.Rows[rowIndex].Cells["colStatus"];
                switch (invoice.trangThai)
                {
                    case "Đã thanh toán":
                        statusCell.Style.BackColor = Color.FromArgb(76, 175, 80); // Green
                        statusCell.Style.ForeColor = Color.White;
                        break;
                    case "Chưa thanh toán":
                        statusCell.Style.BackColor = Color.FromArgb(228, 8, 10); //Red
                        statusCell.Style.ForeColor = Color.Black;
                        break;
               
                }
            }

            lblTotalInvoices.Text = $"Tổng số: {invoices.Count} hóa đơn";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchInvoices();
        }

        private void SearchInvoices()
        {
            try
            {
                var keyword = txtSearch.Text.Trim();
                var fromDate = dtpFromDate.Value.Date;
                var toDate = dtpToDate.Value.Date.AddDays(1).AddSeconds(-1);
                var status = cmbStatus.SelectedItem?.ToString();

                var invoices = _invoiceController.SearchInvoices(keyword, fromDate, toDate, status);
                DisplayInvoices(invoices);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreateNew_Click(object sender, EventArgs e)
        {
            var frmDetail = new frmInvoiceDetails();
            frmDetail.ShowDialog();
            LoadInvoices(); // Refresh after creating
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ViewSelectedInvoice();
        }

        private void ViewSelectedInvoice()
        {
            if (string.IsNullOrEmpty(_selectedInvoiceId))
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để xem!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var frmDetail = new frmInvoiceDetails(_selectedInvoiceId);
            frmDetail.ShowDialog();
            LoadInvoices(); // Refresh after viewing
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedInvoice();
        }

        private void DeleteSelectedInvoice()
        {
            if (string.IsNullOrEmpty(_selectedInvoiceId))
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để xóa!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var deleteResult = _invoiceController.DeleteInvoice(_selectedInvoiceId);
                bool success = deleteResult.Item1;
                string message = deleteResult.Item2;
                
                MessageBox.Show(message, success ? "Thành công" : "Lỗi", 
                    MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (success)
                {
                    LoadInvoices();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadInvoices();
        }

        private void dgvInvoices_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvInvoices.CurrentRow != null && dgvInvoices.CurrentRow.Index >= 0)
            {
                _selectedInvoiceId = dgvInvoices.CurrentRow.Cells["colInvoiceId"].Value?.ToString();
            }
        }

        private void dgvInvoices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ViewSelectedInvoice();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _invoiceController?.Dispose();
        }
    }
}
