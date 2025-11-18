using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Customers
{
    public partial class frmPurchaseHistory : Form
    {
        private readonly CustomerController _customerController;
        private string _customerId;

        public frmPurchaseHistory()
        {
            InitializeComponent();
            _customerController = new CustomerController();
        }

        public frmPurchaseHistory(string customerId) : this()
        {
            _customerId = customerId;
        }

        private void frmPurchaseHistory_Load(object sender, EventArgs e)
        {
            LoadCustomerInfo();
            if (!string.IsNullOrEmpty(_customerId))
            {
                LoadPurchaseHistory();
            }
        }

        /// <summary>
        /// Load thông tin khách hàng
        /// </summary>
        private void LoadCustomerInfo()
        {
            try
            {
                if (string.IsNullOrEmpty(_customerId))
                {
                    LoadAllCustomers();
                    cmbCustomers.Visible = true;
                    lblSelectCustomer.Visible = true;
                }
                else
                {
                    var customer = _customerController.GetCustomerById(_customerId);
                    if (customer != null)
                    {
                        lblCustomerName.Text = customer.hoTen;
                        lblPhone.Text = customer.soDienThoai;
                        cmbCustomers.Visible = false;
                        lblSelectCustomer.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load tất cả khách hàng vào ComboBox
        /// </summary>
        private void LoadAllCustomers()
        {
            try
            {
                var customers = _customerController.GetAllCustomers();
                cmbCustomers.DataSource = customers;
                cmbCustomers.DisplayMember = "hoTen";
                cmbCustomers.ValueMember = "id";
                
                if (customers.Count > 0)
                {
                    cmbCustomers.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load lịch sử mua hàng
        /// </summary>
        private void LoadPurchaseHistory()
        {
            try
            {
                if (string.IsNullOrEmpty(_customerId))
                    return;

                var purchaseHistory = _customerController.GetPurchaseHistory(_customerId);
                
                dgvPurchaseHistory.DataSource = purchaseHistory;
                
                // Tính tổng
                var totalAmount = purchaseHistory.Sum(p => p.tongTien);
                var totalCount = purchaseHistory.Count;
                
                lblTotalAmount.Text = $"{totalAmount:N0} VNĐ";
                lblTotalCount.Text = totalCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Lọc theo thời gian
        /// </summary>
        private void FilterByDateRange(DateTime fromDate, DateTime toDate)
        {
            try
            {
                if (string.IsNullOrEmpty(_customerId))
                    return;

                var purchaseHistory = _customerController.GetPurchaseHistory(_customerId);
                
                // Lọc theo ngày
                var filtered = purchaseHistory
                    .Where(p => p.ngayMua >= fromDate && p.ngayMua <= toDate)
                    .ToList();
                
                dgvPurchaseHistory.DataSource = filtered;
                
                // Tính tổng
                var totalAmount = filtered.Sum(p => p.tongTien);
                var totalCount = filtered.Count;
                
                lblTotalAmount.Text = $"{totalAmount:N0} VNĐ";
                lblTotalCount.Text = totalCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xem chi tiết hóa đơn
        /// </summary>
        private void ViewInvoiceDetail(string invoiceId)
        {
            try
            {
                // TODO: Mở form chi tiết hóa đơn
                MessageBox.Show($"Xem chi tiết hóa đơn: {invoiceId}", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xuất báo cáo
        /// </summary>
        private void ExportReport()
        {
            try
            {
                if (string.IsNullOrEmpty(_customerId))
                {
                    MessageBox.Show("Vui lòng chọn khách hàng.", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // TODO: Implement xuất báo cáo Excel/PDF
                MessageBox.Show("Chức năng xuất báo cáo đang được phát triển.", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Thay đổi khách hàng được chọn
        /// </summary>
        private void OnCustomerChanged(string newCustomerId)
        {
            _customerId = newCustomerId;
            LoadPurchaseHistory();
            
            var customer = _customerController.GetCustomerById(newCustomerId);
            if (customer != null)
            {
                lblCustomerName.Text = customer.hoTen;
                lblPhone.Text = customer.soDienThoai;
            }
        }

        #region Event Handlers

        private void cmbCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCustomers.SelectedValue != null)
            {
                var customerId = cmbCustomers.SelectedValue.ToString();
                OnCustomerChanged(customerId);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            FilterByDateRange(dtpFromDate.Value, dtpToDate.Value);
        }

        private void dgvPurchaseHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvPurchaseHistory.Columns["colActions"].Index)
            {
                var invoiceId = dgvPurchaseHistory.Rows[e.RowIndex].Cells["colInvoiceId"].Value?.ToString();
                if (!string.IsNullOrEmpty(invoiceId))
                {
                    ViewInvoiceDetail(invoiceId);
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportReport();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
