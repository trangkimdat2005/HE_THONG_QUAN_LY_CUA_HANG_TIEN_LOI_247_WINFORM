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
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Customers
{
    public partial class frmCustomers : Form
    {
        private readonly CustomerController _customerController;
        private KhachHang _selectedCustomer;

        public frmCustomers()
        {
            InitializeComponent();
            _customerController = new CustomerController();
        }

        private void frmCustomers_Load(object sender, EventArgs e)
        {
            LoadCustomers();
            InitializeForm();
        }

        /// <summary>
        /// Khởi tạo các control trên form
        /// </summary>
        private void InitializeForm()
        {
            // Thiết lập trạng thái
            var statuses = new List<string> { "Active", "Inactive" };
            cmbStatus.DataSource = statuses;
            cmbStatus.SelectedIndex = 0;
            
            // Set default date
            dtpBirthDate.Value = DateTime.Now.AddYears(-20);
        }

        /// <summary>
        /// Load danh sách khách hàng
        /// </summary>
        private void LoadCustomers()
        {
            try
            {
                var customers = _customerController.GetAllCustomers();
                dgvCustomers.DataSource = customers;
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
                var customers = _customerController.SearchCustomers(keyword);
                dgvCustomers.DataSource = customers;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Thêm khách hàng mới
        /// </summary>
        private void AddCustomer()
        {
            try
            {
                if (!ValidateInput())
                    return;

                var customer = new KhachHang
                {
                    hoTen = txtCustomerName.Text.Trim(),
                    soDienThoai = txtPhone.Text.Trim(),
                    email = txtEmail.Text.Trim(),
                    diaChi = txtAddress.Text.Trim(),
                    gioiTinh = rbMale.Checked,
                    ngayDangKy = DateTime.Now,
                    trangThai = cmbStatus.SelectedItem.ToString()
                };

                var result = _customerController.AddCustomer(customer);
                
                if (result.success)
                {
                    MessageBox.Show(result.message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCustomers();
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
        /// Cập nhật thông tin khách hàng
        /// </summary>
        private void UpdateCustomer()
        {
            try
            {
                if (_selectedCustomer == null)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng cần cập nhật.", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateInput())
                    return;

                _selectedCustomer.hoTen = txtCustomerName.Text.Trim();
                _selectedCustomer.soDienThoai = txtPhone.Text.Trim();
                _selectedCustomer.email = txtEmail.Text.Trim();
                _selectedCustomer.diaChi = txtAddress.Text.Trim();
                _selectedCustomer.gioiTinh = rbMale.Checked;
                _selectedCustomer.trangThai = cmbStatus.SelectedItem.ToString();

                var result = _customerController.UpdateCustomer(_selectedCustomer);
                
                if (result.success)
                {
                    MessageBox.Show(result.message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCustomers();
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
        /// Xóa khách hàng
        /// </summary>
        private void DeleteCustomer()
        {
            try
            {
                if (_selectedCustomer == null)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng cần xóa.", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirmResult = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa khách hàng '{_selectedCustomer.hoTen}'?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmResult == DialogResult.Yes)
                {
                    var result = _customerController.DeleteCustomer(_selectedCustomer.id);
                    
                    if (result.success)
                    {
                        MessageBox.Show(result.message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCustomers();
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show(result.message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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

                var detail = _customerController.GetCustomerDetail(_selectedCustomer.id);
                
                if (detail != null)
                {
                    MessageBox.Show(
                        $"Khách hàng: {detail.Customer.hoTen}\n" +
                        $"Tổng tiền đã mua: {detail.TotalPurchase:N0} VND\n" +
                        $"Số lần mua: {detail.PurchaseCount}\n" +
                        $"Điểm tích lũy: {detail.MemberCard?.diemTichLuy ?? 0}\n" +
                        $"Hạng thẻ: {detail.MemberCard?.hang ?? "Chưa có"}",
                        "Thông tin chi tiết",
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
            txtCustomerId.Clear();
            txtCustomerName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            rbMale.Checked = true;
            dtpBirthDate.Value = DateTime.Now.AddYears(-20);
            cmbStatus.SelectedIndex = 0;
        }

        /// <summary>
        /// Xem khách hàng VIP
        /// </summary>
        private void ViewVIPCustomers()
        {
            try
            {
                var vipCustomers = _customerController.GetVIPCustomers();
                dgvCustomers.DataSource = vipCustomers;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Validate input
        /// </summary>
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng.", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Load thông tin khách hàng lên form
        /// </summary>
        private void LoadCustomerToForm(KhachHang customer)
        {
            txtCustomerId.Text = customer.id;
            txtCustomerName.Text = customer.hoTen;
            txtPhone.Text = customer.soDienThoai;
            txtEmail.Text = customer.email;
            txtAddress.Text = customer.diaChi;
            
            if (customer.gioiTinh)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;
            
            // dtpBirthDate.Value = DateTime.Now; // Model không có ngaySinh
            cmbStatus.SelectedItem = customer.trangThai;
        }

        #region Event Handlers

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchCustomers(txtSearch.Text);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCustomers();
            ClearForm();
        }

        private void btnVIPCustomers_Click(object sender, EventArgs e)
        {
            ViewVIPCustomers();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddCustomer();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateCustomer();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteCustomer();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnViewDetail_Click(object sender, EventArgs e)
        {
            ViewCustomerDetail();
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
                    _selectedCustomer = _customerController.GetCustomerById(customerId);
                    if (_selectedCustomer != null)
                    {
                        LoadCustomerToForm(_selectedCustomer);
                    }
                }
            }
        }

        #endregion
    }
}
