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
        // Properties đ? truy?n d? li?u ra ngoài
        public decimal TotalAmount { get; set; }
        public decimal CustomerPay { get; private set; }
        public decimal ReturnAmount { get; private set; }
        public string Note { get; private set; }
        public string PaymentMethod { get; private set; } = "Ti?n m?t";

        public frmPayment(decimal totalAmount)
        {
            InitializeComponent();
            this.TotalAmount = totalAmount;
        }

        // Constructor m?c đ?nh cho Designer (n?u c?n)
        public frmPayment() : this(0) { }

        private void frmPayment_Load(object sender, EventArgs e)
        {
            // Set d? li?u ban đ?u
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

            // Các m?nh giá g?i ?: Chính xác, Tr?n ch?c, Tr?n trăm, Tr?n 500
            var suggestions = new List<decimal>();
            suggestions.Add(TotalAmount); // Chính xác

            // Logic g?i ? đơn gi?n (Làm tr?n lên)
            if (TotalAmount % 10000 != 0) suggestions.Add(Math.Ceiling(TotalAmount / 10000) * 10000);
            if (TotalAmount % 50000 != 0) suggestions.Add(Math.Ceiling(TotalAmount / 50000) * 50000);
            if (TotalAmount % 100000 != 0) suggestions.Add(Math.Ceiling(TotalAmount / 100000) * 100000);
            if (TotalAmount % 500000 != 0) suggestions.Add(Math.Ceiling(TotalAmount / 500000) * 500000);

            // L?c trùng và s?p x?p
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
            PaymentMethod = "Ti?n m?t";
            lblRightHeader.Text = "?? Thanh Toán Ti?n M?t";
            txtCustomerPay.Enabled = true;
            txtCustomerPay.Focus();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            HighlightButton(btnTransfer);
            PaymentMethod = "Chuy?n kho?n";
            lblRightHeader.Text = "?? Thanh Toán Chuy?n Kho?n";
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
                lblChangeAmount.Text = "Vui l?ng nh?p ti?n khách đưa";
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
                    lblChangeAmount.Text = "Thi?u " + Math.Abs(change).ToString("N0") + " đ";
                    lblChangeAmount.ForeColor = Color.Red;
                }
            }
            catch
            {
                lblChangeAmount.Text = "S? ti?n không h?p l?";
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
            // Validate l?i trư?c khi đóng
            string cleanText = txtCustomerPay.Text.Replace(",", "").Replace(".", "").Replace(" đ", "").Trim();
            decimal pay = 0;

            if (!decimal.TryParse(cleanText, out pay))
            {
                MessageBox.Show("S? ti?n khách đưa không h?p l?!", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (pay < TotalAmount)
            {
                MessageBox.Show("Khách chưa tr? đ? ti?n!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lưu d? li?u
            CustomerPay = pay;
            ReturnAmount = pay - TotalAmount;
            Note = txtNote.Text;

            // If Owner is frmInvoiceDetails, call its save method to persist invoice + payment
            try
            {
                if (this.Owner is frmInvoiceDetails parent)
                {
                    parent.SaveInvoiceFromPayment(CustomerPay, PaymentMethod, Note);
                    // parent will close this form's owner when done; ensure we set DialogResult accordingly
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i khi lưu hoá đơn t? form thanh toán: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // fallback: just close with OK
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}