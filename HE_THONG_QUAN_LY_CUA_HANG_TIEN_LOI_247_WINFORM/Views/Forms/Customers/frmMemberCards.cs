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
    public partial class frmMemberCards : Form
    {
        private readonly CustomerController _customerController;
        private KhachHang _selectedCustomer;
        private TheThanhVien _currentMemberCard;

        public frmMemberCards()
        {
            InitializeComponent();
            _customerController = new CustomerController();
            CustomizeInterface();
        }
        private void CustomizeInterface()
        {
            // Style GridView
            dgvMemberCards.BorderStyle = BorderStyle.None;
            dgvMemberCards.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvMemberCards.GridColor = Color.FromArgb(230, 230, 230);
            dgvMemberCards.RowHeadersVisible = false;
            dgvMemberCards.EnableHeadersVisualStyles = false;
            dgvMemberCards.ColumnHeadersHeight = 40;
            dgvMemberCards.RowTemplate.Height = 40;

            dgvMemberCards.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185); // Xanh Dương
            dgvMemberCards.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMemberCards.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvMemberCards.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            dgvMemberCards.DefaultCellStyle.SelectionBackColor = Color.FromArgb(211, 233, 252); // Xanh Nhạt
            dgvMemberCards.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvMemberCards.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvMemberCards.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

            // Căn chỉnh cột
            colPoints.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colIssueDate.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colRank.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void frmMemberCards_Load(object sender, EventArgs e)
        {
            LoadCustomersWithMemberCards();
            InitializeMemberRanks();
        }

        /// <summary>
        /// Khởi tạo các hạng thẻ
        /// </summary>
        private void InitializeMemberRanks()
        {
            var ranks = new List<string> { "Đồng", "Bạc", "Vàng" };
            cmbRank.DataSource = new List<string>(ranks);
            
            var filterRanks = new List<string> { "Tất cả" };
            filterRanks.AddRange(ranks);
            cmbFilterRank.DataSource = filterRanks;
        }

        /// <summary>
        /// Load danh sách khách hàng có thẻ thành viên
        /// </summary>
        private void LoadCustomersWithMemberCards()
        {
            try
            {
                var customers = _customerController.GetAllCustomers();
                DisplayMemberCards(customers, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hiển thị danh sách thẻ thành viên lên DataGridView
        /// </summary>
        private void DisplayMemberCards(List<KhachHang> customers, string filterRank = null)
        {
            dgvMemberCards.Rows.Clear();
            
            foreach (var customer in customers)
            {
                var memberCard = _customerController.GetMemberCard(customer.id);
                
                // Nếu có filter rank, chỉ hiển thị khách hàng có thẻ và đúng hạng
                if (!string.IsNullOrEmpty(filterRank) && filterRank != "Tất cả")
                {
                    if (memberCard == null || memberCard.hang != filterRank)
                        continue;
                }
                
                dgvMemberCards.Rows.Add(
                    customer.id,                                    // colCustomerId
                    customer.hoTen,                                 // colCustomerName
                    customer.soDienThoai,                          // colPhone
                    memberCard?.hang ?? "Chưa có",                 // colRank
                    memberCard?.diemTichLuy ?? 0,                  // colPoints
                    memberCard?.ngayCap.ToString("dd/MM/yyyy") ?? "" // colIssueDate
                );
            }
        }

        /// <summary>
        /// Chọn khách hàng
        /// </summary>
        private void OnCustomerSelected(string customerId)
        {
            try
            {
                _selectedCustomer = _customerController.GetCustomerById(customerId);
                if (_selectedCustomer != null)
                {
                    LoadMemberCardInfo(customerId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load thông tin thẻ thành viên
        /// </summary>
        private void LoadMemberCardInfo(string customerId)
        {
            try
            {
                _currentMemberCard = _customerController.GetMemberCard(customerId);
                
                txtCustomerId.Text = _selectedCustomer.id;
                txtCustomerName.Text = _selectedCustomer.hoTen;
                txtPhone.Text = _selectedCustomer.soDienThoai;
                
                if (_currentMemberCard != null)
                {
                    txtPoints.Text = _currentMemberCard.diemTichLuy.ToString();
                    cmbRank.SelectedItem = _currentMemberCard.hang;
                    dtpIssueDate.Value = _currentMemberCard.ngayCap;
                }
                else
                {
                    // Chưa có thẻ, tính toán từ lịch sử mua hàng
                    var detail = _customerController.GetCustomerDetail(customerId);
                    if (detail != null)
                    {
                        var points = _customerController.CalculateLoyaltyPoints(detail.TotalPurchase);
                        var rank = _customerController.DetermineMemberRank(points);
                        
                        txtPoints.Text = points.ToString();
                        cmbRank.SelectedItem = rank;
                        dtpIssueDate.Value = DateTime.Now;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cập nhật thẻ thành viên
        /// </summary>
        private void UpdateMemberCard()
        {
            try
            {
                if (_selectedCustomer == null)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng.", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var memberCard = new TheThanhVien
                {
                    khachHangId = _selectedCustomer.id,
                    hang = cmbRank.SelectedItem.ToString(),
                    diemTichLuy = int.Parse(txtPoints.Text),
                    ngayCap = dtpIssueDate.Value
                };

                var result = _customerController.UpdateMemberCard(memberCard);
                
                if (result.success)
                {
                    MessageBox.Show(result.message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCustomersWithMemberCards();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show(result.message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cộng điểm thủ công
        /// </summary>
        private void AddPoints(int points)
        {
            try
            {
                if (_currentMemberCard == null)
                {
                    MessageBox.Show("Khách hàng chưa có thẻ thành viên.", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _currentMemberCard.diemTichLuy += points;
                
                // Cập nhật hạng thẻ dựa trên điểm mới
                _currentMemberCard.hang = _customerController.DetermineMemberRank(_currentMemberCard.diemTichLuy);

                var result = _customerController.UpdateMemberCard(_currentMemberCard);
                
                if (result.success)
                {
                    MessageBox.Show($"Đã cộng {points} điểm thành công.", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadMemberCardInfo(_selectedCustomer.id);
                    LoadCustomersWithMemberCards();
                }
                else
                {
                    MessageBox.Show(result.message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tìm kiếm thẻ thành viên
        /// </summary>
        private void SearchMemberCards(string keyword)
        {
            try
            {
                var customers = _customerController.SearchCustomers(keyword);
                
                dgvMemberCards.Rows.Clear();
                
                foreach (var customer in customers)
                {
                    var memberCard = _customerController.GetMemberCard(customer.id);
                    // Chỉ hiển thị khách hàng đã có thẻ khi search
                    if (memberCard != null)
                    {
                        dgvMemberCards.Rows.Add(
                            customer.id,
                            customer.hoTen,
                            customer.soDienThoai,
                            memberCard.hang,
                            memberCard.diemTichLuy,
                            memberCard.ngayCap.ToString("dd/MM/yyyy")
                        );
                    }
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
        private void FilterByRank(string rank)
        {
            try
            {
                var customers = _customerController.GetAllCustomers();
                DisplayMemberCards(customers, rank);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xem lịch sử tích điểm
        /// </summary>
        private void ViewPointsHistory()
        {
            try
            {
                if (_selectedCustomer == null)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng.", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var detail = _customerController.GetCustomerDetail(_selectedCustomer.id);
                
                if (detail != null)
                {
                    MessageBox.Show(
                        $"Lịch sử tích điểm của {detail.Customer.hoTen}\n\n" +
                        $"Tổng tiền đã mua: {detail.TotalPurchase:N0} VND\n" +
                        $"Điểm tích lũy hiện tại: {detail.MemberCard?.diemTichLuy ?? 0}\n" +
                        $"Hạng thẻ: {detail.MemberCard?.hang ?? "Chưa có"}",
                        "Lịch sử tích điểm",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xóa dữ liệu trên form
        /// </summary>
        private void ClearForm()
        {
            _selectedCustomer = null;
            _currentMemberCard = null;
            txtCustomerId.Clear();
            txtCustomerName.Clear();
            txtPhone.Clear();
            txtPoints.Clear();
            txtAddPoints.Text = "0";
            cmbRank.SelectedIndex = 0;
            dtpIssueDate.Value = DateTime.Now;
        }

        #region Event Handlers

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchMemberCards(txtSearch.Text);
        }

        private void cmbFilterRank_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFilterRank.SelectedItem != null)
            {
                FilterByRank(cmbFilterRank.SelectedItem.ToString());
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCustomersWithMemberCards();
            ClearForm();
        }

        private void dgvMemberCards_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMemberCards.SelectedRows.Count > 0)
            {
                var row = dgvMemberCards.SelectedRows[0];
                var customerId = row.Cells["colCustomerId"].Value?.ToString();
                if (!string.IsNullOrEmpty(customerId))
                {
                    OnCustomerSelected(customerId);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateMemberCard();
        }

        private void btnAddPoints_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtAddPoints.Text, out int points))
            {
                if (points > 0)
                {
                    AddPoints(points);
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập số điểm lớn hơn 0.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập số điểm hợp lệ.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnViewHistory_Click(object sender, EventArgs e)
        {
            ViewPointsHistory();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
