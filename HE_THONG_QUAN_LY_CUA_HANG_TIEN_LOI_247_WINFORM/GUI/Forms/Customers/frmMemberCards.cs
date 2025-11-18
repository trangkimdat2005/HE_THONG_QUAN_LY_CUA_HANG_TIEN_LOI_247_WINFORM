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
                
                var displayList = new List<dynamic>();
                foreach (var customer in customers)
                {
                    var memberCard = _customerController.GetMemberCard(customer.id);
                    displayList.Add(new
                    {
                        id = customer.id,
                        hoTen = customer.hoTen,
                        soDienThoai = customer.soDienThoai,
                        Rank = memberCard?.hang ?? "Chưa có",
                        Points = memberCard?.diemTichLuy ?? 0,
                        IssueDate = memberCard?.ngayCap.ToString("dd/MM/yyyy") ?? ""
                    });
                }

                dgvMemberCards.DataSource = displayList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                
                var displayList = new List<dynamic>();
                foreach (var customer in customers)
                {
                    var memberCard = _customerController.GetMemberCard(customer.id);
                    if (memberCard != null)
                    {
                        displayList.Add(new
                        {
                            id = customer.id,
                            hoTen = customer.hoTen,
                            soDienThoai = customer.soDienThoai,
                            Rank = memberCard.hang,
                            Points = memberCard.diemTichLuy,
                            IssueDate = memberCard.ngayCap.ToString("dd/MM/yyyy")
                        });
                    }
                }

                dgvMemberCards.DataSource = displayList;
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
                
                var displayList = new List<dynamic>();
                foreach (var customer in customers)
                {
                    var memberCard = _customerController.GetMemberCard(customer.id);
                    if (memberCard != null)
                    {
                        if (rank == "Tất cả" || memberCard.hang == rank)
                        {
                            displayList.Add(new
                            {
                                id = customer.id,
                                hoTen = customer.hoTen,
                                soDienThoai = customer.soDienThoai,
                                Rank = memberCard.hang,
                                Points = memberCard.diemTichLuy,
                                IssueDate = memberCard.ngayCap.ToString("dd/MM/yyyy")
                            });
                        }
                    }
                }

                dgvMemberCards.DataSource = displayList;
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
