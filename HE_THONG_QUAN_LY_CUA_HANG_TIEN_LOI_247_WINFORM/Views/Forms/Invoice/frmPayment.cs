using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Bills;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Invoice
{
    public partial class frmPayment : Form
    {
        public decimal TotalAmount { get; set; }
        public decimal CustomerPay { get; private set; }
        public decimal ReturnAmount { get; private set; }
        public string Note { get; private set; }
        public string PaymentMethod { get; private set; } = "Tiền mặt";

        public frmPayment(decimal totalAmount)
        {
            InitializeComponent();
            this.TotalAmount = totalAmount;
        }

        // Constructor m?c đ?nh cho Designer (n?u c?n)
        public frmPayment() : this(0) { }

        private void frmPayment_Load(object sender, EventArgs e)
        {
            lblTotalAmount.Text = TotalAmount.ToString("N0") + " đ";
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            // M?c đ?nh ch?n ti?n m?t
            HighlightButton(btnCash);

            // T?o các nút g?i ? ti?n
            GenerateSuggestionButtons();

            txtCustomerPay.Focus();
        }

        private void GenerateSuggestionButtons()
        {
            flowSuggestions.Controls.Clear();

            var suggestions = new List<decimal>();
            suggestions.Add(TotalAmount); // Chính xác

            if (TotalAmount % 10000 != 0) suggestions.Add(Math.Ceiling(TotalAmount / 10000) * 10000);
            if (TotalAmount % 50000 != 0) suggestions.Add(Math.Ceiling(TotalAmount / 50000) * 50000);
            if (TotalAmount % 100000 != 0) suggestions.Add(Math.Ceiling(TotalAmount / 100000) * 100000);
            if (TotalAmount % 500000 != 0) suggestions.Add(Math.Ceiling(TotalAmount / 500000) * 500000);

            suggestions = suggestions.Distinct().OrderBy(x => x).ToList();

            foreach (var amount in suggestions)
            {
                Button btn = new Button();
                btn.Text = amount.ToString("N0") + " đ";
                btn.Size = new Size(100, 35);
                btn.FlatStyle = FlatStyle.Flat;
                btn.BackColor = Color.White;
                btn.ForeColor = Color.Black;
                btn.Cursor = Cursors.Hand;
                btn.Click += (s, e) => { txtCustomerPay.Text = amount.ToString("N0"); };
                flowSuggestions.Controls.Add(btn);
            }
        }

        private void HighlightButton(Button activeBtn)
        {
            // Reset style
            btnCash.BackColor = Color.White;
            btnCash.ForeColor = Color.Black;
            btnTransfer.BackColor = Color.White;
            btnTransfer.ForeColor = Color.Black;

            // Set active style (Màu xanh ch? đ?o)
            activeBtn.BackColor = Color.FromArgb(0, 150, 136);
            activeBtn.ForeColor = Color.White;
        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            HighlightButton(btnCash);
            PaymentMethod = "Tiền mặt";
            lblRightHeader.Text = "Thanh Toán Tiền Mặt";
            txtCustomerPay.Enabled = true;
            txtCustomerPay.Focus();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            HighlightButton(btnTransfer);
            PaymentMethod = "Chuyển khoản";
            lblRightHeader.Text = "Thanh Toán Chuyển Khoản";
            // Chuy?n kho?n th? m?c đ?nh khách tr? đúng s? ti?n
            txtCustomerPay.Text = TotalAmount.ToString("N0");
            txtCustomerPay.Enabled = false; // Không cho s?a
        }

        private void txtCustomerPay_TextChanged(object sender, EventArgs e)
        {
            CalculateChange();
        }

        private void CalculateChange()
        {
            if (string.IsNullOrEmpty(txtCustomerPay.Text))
            {
                lblChangeAmount.Text = "Vui lòng nhập tiền khách đưa";
                lblChangeAmount.ForeColor = Color.Black;
                return;
            }

            try
            {
                // X? l? chu?i ti?n t? (b? d?u ph?y, ch? đ)
                string cleanText = txtCustomerPay.Text.Replace(",", "").Replace(".", "").Replace(" đ", "").Trim();
                decimal pay = decimal.Parse(cleanText);

                decimal change = pay - TotalAmount;

                if (change >= 0)
                {
                    lblChangeAmount.Text = change.ToString("N0") + " đ";
                    lblChangeAmount.ForeColor = Color.FromArgb(39, 174, 96); // Xanh lá
                }
                else
                {
                    lblChangeAmount.Text = "Thiếu " + Math.Abs(change).ToString("N0") + " đ";
                    lblChangeAmount.ForeColor = Color.Red;
                }
            }
            catch
            {
                lblChangeAmount.Text = "Số tiền không hợp lí";
            }
        }

        // Format textbox ti?n khi nh?p
        private void txtCustomerPay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtCustomerPay.Text))
            {
                MessageBox.Show("Vui lòng nhập số tiền khách đưa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Clean and parse the customer payment amount
            // Remove formatting characters that might be present: commas, dots, đ symbol, spaces
            string cleanText = txtCustomerPay.Text.Replace(",", "").Replace(".", "").Replace(" đ", "").Replace("đ", "").Trim();
            
            if (!decimal.TryParse(cleanText, out decimal pay))
            {
                MessageBox.Show("Số tiền khách đưa không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (pay < TotalAmount)
            {
                MessageBox.Show("Khách chưa trả đủ tiền!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CustomerPay = pay;
            ReturnAmount = pay - TotalAmount;
            Note = txtNote.Text;

            try
            {
                // Use pattern matching 'is' to check type and assign variable in one step
                if (this.Owner is frmInvoiceDetails parent)
                {
                    parent.SaveInvoiceFromPayment(CustomerPay, PaymentMethod, Note);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // Handle case where Owner is NOT frmInvoiceDetails (e.g., during testing)
                    MessageBox.Show("Form cha không hợp lệ (Owner is null or wrong type).");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu hoá đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}