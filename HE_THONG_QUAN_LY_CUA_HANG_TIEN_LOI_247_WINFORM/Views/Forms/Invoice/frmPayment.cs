using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Invoice
{
    public partial class frmPayment : Form
    {
        // Properties để truyền dữ liệu ra ngoài
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

        // Constructor mặc định cho Designer (nếu cần)
        public frmPayment() : this(0) { }

        private void frmPayment_Load(object sender, EventArgs e)
        {
            // Set dữ liệu ban đầu
            lblTotalAmount.Text = TotalAmount.ToString("N0") + " đ";
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            // Mặc định chọn tiền mặt
            HighlightButton(btnCash);

            // Tạo các nút gợi ý tiền
            GenerateSuggestionButtons();

            txtCustomerPay.Focus();
        }

        private void GenerateSuggestionButtons()
        {
            flowSuggestions.Controls.Clear();

            // Các mệnh giá gợi ý: Chính xác, Tròn chục, Tròn trăm, Tròn 500
            var suggestions = new List<decimal>();
            suggestions.Add(TotalAmount); // Chính xác

            // Logic gợi ý đơn giản (Làm tròn lên)
            if (TotalAmount % 10000 != 0) suggestions.Add(Math.Ceiling(TotalAmount / 10000) * 10000);
            if (TotalAmount % 50000 != 0) suggestions.Add(Math.Ceiling(TotalAmount / 50000) * 50000);
            if (TotalAmount % 100000 != 0) suggestions.Add(Math.Ceiling(TotalAmount / 100000) * 100000);
            if (TotalAmount % 500000 != 0) suggestions.Add(Math.Ceiling(TotalAmount / 500000) * 500000);

            // Lọc trùng và sắp xếp
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

            // Set active style (Màu xanh chủ đạo)
            activeBtn.BackColor = Color.FromArgb(0, 150, 136);
            activeBtn.ForeColor = Color.White;
        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            HighlightButton(btnCash);
            PaymentMethod = "Tiền mặt";
            lblRightHeader.Text = "💵 Thanh Toán Tiền Mặt";
            txtCustomerPay.Enabled = true;
            txtCustomerPay.Focus();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            HighlightButton(btnTransfer);
            PaymentMethod = "Chuyển khoản";
            lblRightHeader.Text = "💳 Thanh Toán Chuyển Khoản";
            // Chuyển khoản thì mặc định khách trả đúng số tiền
            txtCustomerPay.Text = TotalAmount.ToString("N0");
            txtCustomerPay.Enabled = false; // Không cho sửa
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
                // Xử lý chuỗi tiền tệ (bỏ dấu phẩy, chữ đ)
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
                lblChangeAmount.Text = "Số tiền không hợp lệ";
            }
        }

        // Format textbox tiền khi nhập
        private void txtCustomerPay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            // Validate lại trước khi đóng
            string cleanText = txtCustomerPay.Text.Replace(",", "").Replace(".", "").Replace(" đ", "").Trim();
            decimal pay = 0;

            if (!decimal.TryParse(cleanText, out pay))
            {
                MessageBox.Show("Số tiền khách đưa không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (pay < TotalAmount)
            {
                MessageBox.Show("Khách chưa trả đủ tiền!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lưu dữ liệu
            CustomerPay = pay;
            ReturnAmount = pay - TotalAmount;
            Note = txtNote.Text;

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