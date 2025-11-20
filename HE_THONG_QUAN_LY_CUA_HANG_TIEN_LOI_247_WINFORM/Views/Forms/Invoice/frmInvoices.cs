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
            CustomizeInterface();
        }

        private void CustomizeInterface()
        {
            dgvInvoices.BorderStyle = BorderStyle.None;
            dgvInvoices.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvInvoices.GridColor = Color.FromArgb(230, 230, 230);
            dgvInvoices.RowHeadersVisible = false;
            dgvInvoices.EnableHeadersVisualStyles = false;
            dgvInvoices.ColumnHeadersHeight = 40;
            dgvInvoices.RowTemplate.Height = 40;

            dgvInvoices.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvInvoices.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvInvoices.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvInvoices.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            dgvInvoices.DefaultCellStyle.SelectionBackColor = Color.FromArgb(211, 233, 252);
            dgvInvoices.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvInvoices.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvInvoices.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

            colTotalAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void frmInvoices_Load(object sender, EventArgs e)
        {
            InitializeUI();
            LoadInvoices();
        }

        private void InitializeUI()
        {
            dtpFromDate.Value = DateTime.Now.AddMonths(-1);
            dtpToDate.Value = DateTime.Now;
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new object[] { "Tất cả", "Chưa thanh toán", "Đã thanh toán" });
            cmbStatus.SelectedIndex = 0;
        }

        private void LoadInvoices()
        {
            try { var invoices = _invoiceController.GetAllInvoices(); DisplayInvoices(invoices); } catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void DisplayInvoices(List<HoaDon> invoices)
        {
            dgvInvoices.Rows.Clear();
            foreach (var i in invoices)
            {
                int idx = dgvInvoices.Rows.Add(i.id, i.ngayLap.ToString("dd/MM/yyyy HH:mm"), i.NhanVien?.hoTen ?? "N/A", i.KhachHang?.hoTen ?? "Khách lẻ", (i.tongTien ?? 0).ToString("N0") + " đ", i.trangThai);

                var cell = dgvInvoices.Rows[idx].Cells["colStatus"];
                if (i.trangThai == "Đã thanh toán") { cell.Style.ForeColor = Color.Green; cell.Style.Font = new Font("Segoe UI", 10F, FontStyle.Bold); }
                else if (i.trangThai == "Chưa thanh toán") { cell.Style.ForeColor = Color.Red; cell.Style.Font = new Font("Segoe UI", 10F, FontStyle.Bold); }
            }
            lblTotalInvoices.Text = $"Tổng số: {invoices.Count} hóa đơn";
        }

        private void btnSearch_Click(object sender, EventArgs e) => SearchInvoices();

        private void SearchInvoices()
        {
            try { DisplayInvoices(_invoiceController.SearchInvoices(txtSearch.Text, dtpFromDate.Value, dtpToDate.Value, cmbStatus.Text)); } catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnCreateNew_Click(object sender, EventArgs e) 
        { 
            var form = new frmInvoiceDetails();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadInvoices(); // Refresh if invoice was created and paid
            }
        }
        
        private void btnView_Click(object sender, EventArgs e) 
        { 
            if (!string.IsNullOrEmpty(_selectedInvoiceId)) 
            { 
                var form = new frmInvoiceDetails(_selectedInvoiceId);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadInvoices(); // Refresh if invoice was updated/paid
                }
            } 
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedInvoiceId)) return;
            if (MessageBox.Show("Xóa", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var res = _invoiceController.DeleteInvoice(_selectedInvoiceId);
                MessageBox.Show(res.Item2); if (res.Item1) LoadInvoices();
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e) => LoadInvoices();
        private void dgvInvoices_SelectionChanged(object sender, EventArgs e) { if (dgvInvoices.CurrentRow != null) _selectedInvoiceId = dgvInvoices.CurrentRow.Cells["colInvoiceId"].Value?.ToString(); }
        private void dgvInvoices_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { if (e.RowIndex >= 0) btnView_Click(null, null); }
    }
}