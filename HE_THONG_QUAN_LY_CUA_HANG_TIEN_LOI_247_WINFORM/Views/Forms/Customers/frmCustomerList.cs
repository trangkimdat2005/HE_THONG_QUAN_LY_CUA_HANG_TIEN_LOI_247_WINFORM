using Guna.UI2.AnimatorNS;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Customers
{
    /// <summary>
    /// Form Danh sách khách hàng - Hiển thị tổng quan, thống kê, tìm kiếm nâng cao
    /// </summary>
    public partial class frmCustomerList : Form
    {
        private readonly CustomerController _customerController;
        private List<KhachHang> _allCustomers;
        private KhachHang _selectedCustomer;

        public frmCustomerList()
        {
            InitializeComponent();
            _customerController = new CustomerController();
        }

        private void frmCustomerList_Load(object sender, EventArgs e)
        {
            LoadCustomers();
            InitializeFilters();
        }

        /// <summary>
        /// Khởi tạo các bộ lọc
        /// </summary>
        private void InitializeFilters()
        {
            // Thiết lập ComboBox cho trạng thái
            var statuses = new List<string> { "Tất cả", "Active", "Inactive" };
            cmbStatus.DataSource = statuses;
            
            // Thiết lập ComboBox cho hạng thẻ
            var memberRanks = new List<string> { "Tất cả", "Đồng", "Bạc", "Vàng" };
            cmbMemberRank.DataSource = memberRanks;
        }

        /// <summary>
        /// Load danh sách khách hàng
        /// </summary>
        private void LoadCustomers()
        {
            try
            {
                _allCustomers = _customerController.GetAllCustomers();
                DisplayCustomers(_allCustomers);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hiển thị danh sách khách hàng
        /// </summary>
        private void DisplayCustomers(List<KhachHang> customers)
        {
            try
            {
                dgvCustomers.Rows.Clear(); //Xóa dữ liệu cũ
                
                // Pre-load tất cả thẻ thành viên và lịch sử để tránh N+1 query problem
                var customerIds = customers.Select(c => c.id).ToList();
                var allMemberCards = new Dictionary<string, TheThanhVien>();
                var allPurchaseTotals = new Dictionary<string, decimal>();
                
                foreach (var customerId in customerIds)
                {
                    var memberCard = _customerController.GetMemberCard(customerId);
                    if (memberCard != null)
                    {
                        allMemberCards[customerId] = memberCard;
                    }
                    
                    // Lấy toàn bộ lịch sử mua hàng (từ ngày đầu tiên đến hiện tại)
                    var purchaseHistory = _customerController.GetPurchaseHistoryByDate(
                        customerId, 
                        DateTime.MinValue, 
                        DateTime.Now);
                    allPurchaseTotals[customerId] = purchaseHistory.Sum(p => p.tongTien);
                }
                
                // Bind dữ liệu vào DataGridView
                foreach (var customer in customers)
                {
                    allMemberCards.TryGetValue(customer.id, out var memberCard);
                    allPurchaseTotals.TryGetValue(customer.id, out var totalPurchase);
                    
                    dgvCustomers.Rows.Add(
                        customer.id,                              // colId
                        customer.hoTen,                           // colName  
                        customer.soDienThoai,                     // colPhone
                        customer.email,                           // colEmail
                        customer.diaChi,                          // colAddress
                        customer.ngayDangKy.ToString("dd/MM/yyyy"), // colRegisterDate
                        customer.trangThai,                       // colStatus
                        memberCard?.hang ?? "Chưa có",            // colMemberRank
                        memberCard?.diemTichLuy ?? 0,             // colPoints
                        totalPurchase.ToString("N0") + " VNĐ"     // colTotalPurchase
                    );
                }
                
                // Hiển thị thống kê
                DisplayStatistics(customers);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hiển thị thống kê
        /// </summary>
        private void DisplayStatistics(List<KhachHang> customers)
        {
            try
            {
                var totalCustomers = customers.Count;
                var activeCustomers = customers.Count(c => c.trangThai == "Active");
                
                // Đếm theo hạng thẻ
                var goldMembers = 0;
                var silverMembers = 0;
                var bronzeMembers = 0;

                foreach (var customer in customers)
                {
                    var memberCard = _customerController.GetMemberCard(customer.id);
                    if (memberCard != null)
                    {
                        switch (memberCard.hang)
                        {
                            case "Vàng":
                                goldMembers++;
                                break;
                            case "Bạc":
                                silverMembers++;
                                break;
                            case "Đồng":
                                bronzeMembers++;
                                break;
                        }
                    }
                }

                // Hiển thị thống kê
                lblTotalCustomers.Text = totalCustomers.ToString();
                lblActiveCustomers.Text = activeCustomers.ToString();
                lblGoldMembers.Text = goldMembers.ToString();
                lblSilverMembers.Text = silverMembers.ToString();
                lblBronzeMembers.Text = bronzeMembers.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tìm kiếm khách hàng
        /// </summary>
        private void SearchCustomers(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    DisplayCustomers(_allCustomers);
                }
                else
                {
                    var results = _customerController.SearchCustomers(keyword);
                    DisplayCustomers(results);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Lọc theo trạng thái
        /// </summary>
        private void FilterByStatus(string status)
        {
            try
            {
                if (status == "Tất cả")
                {
                    DisplayCustomers(_allCustomers);
                }
                else
                {
                    var filtered = _allCustomers.Where(c => c.trangThai == status).ToList();
                    DisplayCustomers(filtered);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Lọc theo hạng thẻ
        /// </summary>
        private void FilterByMemberRank(string rank)
        {
            try
            {
                if (rank == "Tất cả")
                {
                    DisplayCustomers(_allCustomers);
                }
                else
                {
                    var filtered = _allCustomers.Where(c =>
                    {
                        var memberCard = _customerController.GetMemberCard(c.id);
                        return memberCard != null && memberCard.hang == rank;
                    }).ToList();
                    
                    DisplayCustomers(filtered);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hiển thị khách hàng VIP
        /// </summary>
        private void ShowVIPCustomers()
        {
            try
            {
                var vipCustomers = _customerController.GetVIPCustomers();
                DisplayCustomers(vipCustomers);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xem chi tiết khách hàng
        /// </summary>
        private void ViewCustomerDetail()
        {
            try
            {
                if (_selectedCustomer == null)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng.", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Mở form CRUD để xem/sửa thông tin
                var frmDetail = new frmCustomers();
                frmDetail.ShowDialog();
                
                // Refresh lại danh sách sau khi đóng form
                LoadCustomers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xem lịch sử mua hàng
        /// </summary>
        private void ViewPurchaseHistory()
        {
            try
            {
                if (_selectedCustomer == null)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng.", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var frmHistory = new frmPurchaseHistory(_selectedCustomer.id);
                frmHistory.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xuất danh sách
        /// </summary>
        private void ExportList()
        {
            try
            {
                // TODO: Implement xuất Excel/PDF
                MessageBox.Show("Chức năng xuất danh sách đang được phát triển.", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Refresh danh sách
        /// </summary>
        private void RefreshList()
        {
            LoadCustomers();
        }

        /// <summary>
        /// Chọn khách hàng từ DataGridView
        /// </summary>
        private void OnCustomerSelected(string customerId)
        {
            _selectedCustomer = _allCustomers.FirstOrDefault(c => c.id == customerId);
        }

        #region Event Handlers

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchCustomers(txtSearch.Text);
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SearchCustomers(txtSearch.Text);
                e.Handled = true;
            }
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStatus.SelectedItem != null)
            {
                FilterByStatus(cmbStatus.SelectedItem.ToString());
            }
        }

        private void cmbMemberRank_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMemberRank.SelectedItem != null)
            {
                FilterByMemberRank(cmbMemberRank.SelectedItem.ToString());
            }
        }

        private void btnVIPCustomers_Click(object sender, EventArgs e)
        {
            ShowVIPCustomers();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportList();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void btnViewDetail_Click(object sender, EventArgs e)
        {
            ViewCustomerDetail();
        }

        private void btnViewHistory_Click(object sender, EventArgs e)
        {
            ViewPurchaseHistory();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count > 0)
            {
                var row = dgvCustomers.SelectedRows[0];
                var customerId = row.Cells["colId"].Value?.ToString();
                if (!string.IsNullOrEmpty(customerId))
                {
                    OnCustomerSelected(customerId);
                }
            }
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Kiểm tra click hợp lệ
            if (e.RowIndex >= 0 && dgvCustomers.Columns[e.ColumnIndex].Name == "colActions")
            {
                var customerId = dgvCustomers.Rows[e.RowIndex].Cells["colId"].Value?.ToString();

                if (!string.IsNullOrEmpty(customerId))
                {
                    // Lấy thông tin khách hàng
                    var customer = _customerController.GetCustomerById(customerId);

                    if (customer != null)
                    {
                        // Set selected customer và mở form chi tiết
                        _selectedCustomer = customer;
                        ViewCustomerDetail();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin khách hàng!", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // Reload lại danh sách nếu cần
                        LoadCustomers();
                    }
                }
            }
        }


        #endregion
    }
}
