using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Customers
{
    public partial class frmPurchaseHistory : Form
    {
        private readonly CustomerController _customerController;
        private string _customerId;
        private bool _isLoading = false; // Flag để tránh trigger event khi đang load
        private DateTime _fromDate = DateTime.Now.AddMonths(-6); // Mặc định lấy 6 tháng gần nhất
        private DateTime _toDate = DateTime.Now;

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
                _isLoading = true; // Bắt đầu loading
                
                var customers = _customerController.GetAllCustomers();
                
                // Create a simple list to avoid EF proxy issues
                var customerList = customers.Select(c => new
                {
                    id = c.id,
                    hoTen = c.hoTen
                }).ToList();
                
                cmbCustomers.DataSource = customerList;
                cmbCustomers.DisplayMember = "hoTen";
                cmbCustomers.ValueMember = "id";
                
                _isLoading = false; // Kết thúc loading
                
                if (customerList.Count > 0)
                {
                    cmbCustomers.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                _isLoading = false;
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

                var purchaseHistory = _customerController.GetPurchaseHistoryByDate(_customerId, _fromDate, _toDate);
                
                DisplayPurchaseHistory(purchaseHistory);
                
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
        /// Hiển thị lịch sử mua hàng lên DataGridView
        /// </summary>
        private void DisplayPurchaseHistory(List<LichSuMuaHang> purchaseHistory)
        {
            dgvPurchaseHistory.Rows.Clear();
            
            foreach (var item in purchaseHistory)
            {
                dgvPurchaseHistory.Rows.Add(
                    item.hoaDonId,                           // colInvoiceId
                    item.ngayMua.ToString("dd/MM/yyyy"),     // colDate
                    item.tongTien.ToString("N0") + " VNĐ",   // colTotalAmount
                    "Tiền mặt",                              // colPaymentMethod (có thể join với bảng payment)
                    ""                                       // colEmployee (có thể join với bảng HoaDon)
                );
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
            // Bỏ qua nếu đang loading hoặc SelectedValue chưa sẵn sàng
            if (_isLoading || cmbCustomers.SelectedValue == null)
                return;

            // Kiểm tra kiểu của SelectedValue trước khi convert
            if (cmbCustomers.SelectedValue is string customerId) 
            {
                OnCustomerChanged(customerId);
            }
        }
        private void FilterByDateRange(DateTime fromDate, DateTime toDate)
        {
            try
            {
                if (string.IsNullOrEmpty(_customerId))
                    return;

                // Cập nhật date range
                _fromDate = fromDate;
                _toDate = toDate;

                // Gọi Controller để lấy danh sách ĐÃ ĐƯỢC LỌC
                var filteredList = _customerController.GetPurchaseHistoryByDate(_customerId, fromDate, toDate);

                // Hiển thị
                DisplayPurchaseHistory(filteredList);

                // Cập nhật tổng (Logic tính tổng đơn giản này để ở View cũng chấp nhận được, 
                // hoặc tạo DTO trả về cả List và TotalAmount từ Controller thì tốt hơn)
                lblTotalAmount.Text = $"{filteredList.Sum(p => p.tongTien):N0} VNĐ";
                lblTotalCount.Text = filteredList.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
