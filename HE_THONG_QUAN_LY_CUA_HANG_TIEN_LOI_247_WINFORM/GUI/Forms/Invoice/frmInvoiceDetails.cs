using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Bills
{
    public partial class frmInvoiceDetails : Form
    {
        private InvoiceController _invoiceController;
        private CustomerController _customerController;
        private ProductController _productController;
        private CategoryController _categoryController;
        
        private string _invoiceId;
        private List<ChiTietHoaDon> _invoiceDetails;
        private bool _isEditMode = false;

        public frmInvoiceDetails()
        {
            InitializeComponent();
            _invoiceController = new InvoiceController();
            _customerController = new CustomerController();
            _productController = new ProductController();
            _categoryController = new CategoryController();
            _invoiceDetails = new List<ChiTietHoaDon>();
        }

        public frmInvoiceDetails(string invoiceId) : this()
        {
            _invoiceId = invoiceId;
            _isEditMode = true;
        }

        private void frmInvoiceDetails_Load(object sender, EventArgs e)
        {
            InitializeUI();
            LoadCustomers();
            LoadCategories();
            LoadProducts();

            if (_isEditMode && !string.IsNullOrEmpty(_invoiceId))
            {
                LoadInvoiceData();
            }
        }

        private void InitializeUI()
        {
            // Setup DataGridViews
            dgvInvoiceDetails.AutoGenerateColumns = false;
            dgvProducts.AutoGenerateColumns = false;
            
            // Note: PlaceholderText is not available in .NET Framework 4.7.2
            // Consider adding placeholder text manually using GotFocus/LostFocus events if needed
            
            // Disable payment button initially
            btnPayment.Enabled = false;
        }

        private void LoadCustomers()
        {
            try
            {
                var customers = _customerController.GetAllCustomers();
                
                cmbCustomer.DataSource = customers;
                cmbCustomer.DisplayMember = "hoTen";
                cmbCustomer.ValueMember = "id";
                
                if (customers.Count > 0)
                {
                    cmbCustomer.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách khách hàng: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategories()
        {
            try
            {
                var categories = _categoryController.GetAllCategories();
                
                var categoryList = new List<object>();
                categoryList.Add(new { id = "", ten = "Tất cả" });
                
                foreach (var cat in categories)
                {
                    categoryList.Add(new { id = cat.id, ten = cat.ten });
                }
                
                cmbCategory.DataSource = categoryList;
                cmbCategory.DisplayMember = "ten";
                cmbCategory.ValueMember = "id";
                cmbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh mục: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts(string keyword = null, string categoryId = null)
        {
            try
            {
                var products = _productController.FilterProducts(keyword, null, categoryId);
                
                dgvProducts.Rows.Clear();
                foreach (var product in products)
                {
                    dgvProducts.Rows.Add(
                        product.Ten,
                        product.GiaBan.ToString("N0") + " đ"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải sản phẩm: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInvoiceData()
        {
            try
            {
                var invoice = _invoiceController.GetInvoiceById(_invoiceId);
                if (invoice == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Load invoice info
                lblInvoiceId.Text = invoice.id;
                cmbCustomer.SelectedValue = invoice.khachHangId;

                // Load invoice details
                _invoiceDetails = _invoiceController.GetInvoiceDetails(_invoiceId);
                DisplayInvoiceDetails();
                CalculateTotal();

                // Disable editing if already paid
                if (invoice.trangThai == "Đã thanh toán")
                {
                    SetReadOnlyMode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải hóa đơn: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayInvoiceDetails()
        {
            dgvInvoiceDetails.Rows.Clear();
            
            foreach (var detail in _invoiceDetails)
            {
                dgvInvoiceDetails.Rows.Add(
                    detail.sanPhamDonViId, // You might want to load product name instead
                    detail.soLuong,
                    detail.donGia.ToString("N0") + " đ",
                    (detail.soLuong * detail.donGia).ToString("N0") + " đ"
                );
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow != null)
            {
                var productName = dgvProducts.CurrentRow.Cells["colProductName"].Value?.ToString();
                var priceStr = dgvProducts.CurrentRow.Cells["colPrice"].Value?.ToString();
                
                if (string.IsNullOrEmpty(productName)) return;
                
                // Remove " đ" and parse price
                var price = decimal.Parse(priceStr.Replace(" đ", "").Replace(",", ""));
                
                // Check if product already exists in invoice
                var existingRow = -1;
                for (int i = 0; i < dgvInvoiceDetails.Rows.Count; i++)
                {
                    if (dgvInvoiceDetails.Rows[i].Cells["colProduct"].Value?.ToString() == productName)
                    {
                        existingRow = i;
                        break;
                    }
                }

                if (existingRow >= 0)
                {
                    // Increase quantity
                    var currentQty = int.Parse(dgvInvoiceDetails.Rows[existingRow].Cells["colQuantity"].Value.ToString());
                    dgvInvoiceDetails.Rows[existingRow].Cells["colQuantity"].Value = currentQty + 1;
                    
                    var newTotal = (currentQty + 1) * price;
                    dgvInvoiceDetails.Rows[existingRow].Cells["colTotal"].Value = newTotal.ToString("N0") + " đ";
                }
                else
                {
                    // Add new product
                    dgvInvoiceDetails.Rows.Add(
                        productName,
                        1,
                        priceStr,
                        price.ToString("N0") + " đ"
                    );
                }

                CalculateTotal();
            }
        }

        private void btnRemoveProduct_Click(object sender, EventArgs e)
        {
            if (dgvInvoiceDetails.CurrentRow != null)
            {
                dgvInvoiceDetails.Rows.Remove(dgvInvoiceDetails.CurrentRow);
                CalculateTotal();
            }
        }

        private void dgvInvoiceDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvInvoiceDetails.Columns["colQuantity"].Index)
            {
                UpdateRowTotal(e.RowIndex);
                CalculateTotal();
            }
        }

        private void UpdateRowTotal(int rowIndex)
        {
            try
            {
                var row = dgvInvoiceDetails.Rows[rowIndex];
                var quantity = int.Parse(row.Cells["colQuantity"].Value?.ToString() ?? "0");
                var priceStr = row.Cells["colUnitPrice"].Value?.ToString() ?? "0";
                var price = decimal.Parse(priceStr.Replace(" đ", "").Replace(",", ""));
                
                var total = quantity * price;
                row.Cells["colTotal"].Value = total.ToString("N0") + " đ";
            }
            catch { }
        }

        private void CalculateTotal()
        {
            decimal subtotal = 0;
            
            foreach (DataGridViewRow row in dgvInvoiceDetails.Rows)
            {
                if (row.Cells["colTotal"].Value != null)
                {
                    var totalStr = row.Cells["colTotal"].Value.ToString();
                    subtotal += decimal.Parse(totalStr.Replace(" đ", "").Replace(",", ""));
                }
            }

            // Get discount
            decimal discount = 0;
            if (decimal.TryParse(txtDiscount.Text, out discount))
            {
                if (discount > subtotal)
                {
                    discount = subtotal;
                    txtDiscount.Text = discount.ToString("N0");
                }
            }

            var total = subtotal - discount;

            lblSubtotal.Text = subtotal.ToString("N0") + " đ";
            lblDiscountValue.Text = discount.ToString("N0") + " đ";
            lblTotal.Text = total.ToString("N0") + " đ";

            // Enable payment button if there are items
            btnPayment.Enabled = dgvInvoiceDetails.Rows.Count > 0;
        }

        private void btnApplyDiscount_Click(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProducts(txtSearch.Text);
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            var categoryId = cmbCategory.SelectedValue?.ToString();
            LoadProducts(txtSearch.Text, categoryId);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn hủy?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            ProcessPayment();
        }

        private void ProcessPayment()
        {
            try
            {
                // Validate
                if (cmbCustomer.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dgvInvoiceDetails.Rows.Count == 0)
                {
                    MessageBox.Show("Vui lòng thêm sản phẩm vào hóa đơn!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Create invoice object
                var invoice = new HoaDon
                {
                    khachHangId = cmbCustomer.SelectedValue.ToString(),
                    nhanVienId = "NV001", // TODO: Get from logged-in user
                    ngayLap = DateTime.Now,
                    trangThai = "Chờ thanh toán"
                };

                // Create invoice details (TODO: Get actual product IDs)
                var details = new List<ChiTietHoaDon>();
                // ... populate details from DataGridView

                // Save invoice
                var createResult = _invoiceController.CreateInvoice(invoice, details);
                bool success = createResult.Item1;
                string message = createResult.Item2;
                string invoiceId = createResult.Item3;

                if (!success)
                {
                    MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Show payment dialog
                var frmPayment = new frmPayment(invoiceId);
                var paymentResult = frmPayment.ShowDialog();

                if (paymentResult == DialogResult.OK)
                {
                    MessageBox.Show("Thanh toán thành công!", "Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetReadOnlyMode()
        {
            cmbCustomer.Enabled = false;
            dgvProducts.Enabled = false;
            txtDiscount.Enabled = false;
            btnApplyDiscount.Enabled = false;
            btnPayment.Enabled = false;
            
            // Remove delete column from invoice details
            if (dgvInvoiceDetails.Columns.Contains("colRemove"))
            {
                dgvInvoiceDetails.Columns["colRemove"].Visible = false;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _invoiceController?.Dispose();
            _productController?.Dispose();
            _categoryController?.Dispose();
        }
    }

    // Payment dialog form
    public class frmPayment : Form
    {
        private InvoiceController _invoiceController;
        private string _invoiceId;
        private ComboBox cmbPaymentMethod;
        private TextBox txtAmount;
        private Label lblTotal;
        private Button btnConfirm;
        private Button btnCancel;

        public frmPayment(string invoiceId)
        {
            _invoiceId = invoiceId;
            _invoiceController = new InvoiceController();
            InitializeComponent();
            LoadPaymentMethods();
            LoadInvoiceTotal();
        }

        private void InitializeComponent()
        {
            this.Text = "Thanh toán";
            this.Size = new Size(450, 320);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            var pnlTop = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.FromArgb(41, 128, 185) };
            var lblTitle = new Label { Text = "💳 THANH TOÁN HÓA ĐƠN", Location = new Point(20, 15), AutoSize = true, Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.White };
            pnlTop.Controls.Add(lblTitle);

            var lblPaymentMethod = new Label { Text = "Phương thức thanh toán:", Location = new Point(30, 90), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            cmbPaymentMethod = new ComboBox { Location = new Point(220, 87), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };

            var lblAmountLabel = new Label { Text = "Tổng tiền:", Location = new Point(30, 130), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            lblTotal = new Label { Location = new Point(220, 128), AutoSize = true, Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.FromArgb(231, 76, 60) };

            var lblAmountPaid = new Label { Text = "Số tiền thanh toán:", Location = new Point(30, 170), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtAmount = new TextBox { Location = new Point(220, 167), Width = 200, Font = new Font("Segoe UI", 10) };

            btnConfirm = new Button { Text = "✓ Xác nhận", Location = new Point(140, 230), Size = new Size(130, 40), BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            btnCancel = new Button { Text = "✗ Hủy", Location = new Point(280, 230), Size = new Size(100, 40), BackColor = Color.FromArgb(231, 76, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

            btnConfirm.Click += btnConfirm_Click;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            this.Controls.AddRange(new Control[] { pnlTop, lblPaymentMethod, cmbPaymentMethod, lblAmountLabel, lblTotal, lblAmountPaid, txtAmount, btnConfirm, btnCancel });
        }

        private void LoadPaymentMethods()
        {
            try
            {
                var methods = _invoiceController.GetPaymentMethods();
                cmbPaymentMethod.DataSource = methods;
                cmbPaymentMethod.DisplayMember = "tenKenh";
                cmbPaymentMethod.ValueMember = "id";
            }
            catch { }
        }

        private void LoadInvoiceTotal()
        {
            try
            {
                var invoice = _invoiceController.GetInvoiceById(_invoiceId);
                if (invoice != null)
                {
                    lblTotal.Text = (invoice.tongTien ?? 0).ToString("N0") + " đ";
                    txtAmount.Text = (invoice.tongTien ?? 0).ToString("N0");
                }
            }
            catch { }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbPaymentMethod.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn phương thức thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtAmount.Text.Replace(",", ""), out decimal amount) || amount <= 0)
                {
                    MessageBox.Show("Vui lòng nhập số tiền hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var paymentMethodId = cmbPaymentMethod.SelectedValue.ToString();
                var paymentResult = _invoiceController.ProcessPayment(_invoiceId, paymentMethodId, amount, "Thanh toán hóa đơn");
                bool success = paymentResult.Item1;
                string message = paymentResult.Item2;

                if (success)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _invoiceController?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
