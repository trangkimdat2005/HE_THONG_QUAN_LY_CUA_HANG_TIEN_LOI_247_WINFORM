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
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Config;

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
            lblRightHeader.Text = "THANH TOÁN TIỀN MẶT";
            
            // Ẩn QR Code panel
            pnlQRCode.Visible = false;
            
            // Hiện lại các control cho tiền mặt
            lblCustomerPay.Visible = true;
            txtCustomerPay.Visible = true;
            lblSuggestions.Visible = true;
            flowSuggestions.Visible = true;
            lblChangeLabel.Visible = true;
            lblChangeAmount.Visible = true;
            lblNote.Visible = true;
            txtNote.Visible = true;
            
            txtCustomerPay.Enabled = true;
            txtCustomerPay.Clear();
            txtCustomerPay.Focus();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            HighlightButton(btnTransfer);
            PaymentMethod = "Chuyển khoản";
            lblRightHeader.Text = "THANH TOÁN CHUYỂN KHOẢN";

            // Ẩn các control cho tiền mặt
            lblCustomerPay.Visible = false;
            txtCustomerPay.Visible = false;
            lblSuggestions.Visible = false;
            flowSuggestions.Visible = false;
            lblChangeLabel.Visible = false;
            lblChangeAmount.Visible = false;
            lblNote.Visible = false;
            txtNote.Visible = false;
            
            // Hiện QR Code panel
            pnlQRCode.Visible = true;
            
            // Set số tiền khách đưa = tổng tiền
            txtCustomerPay.Text = TotalAmount.ToString("N0");
            
            //// Hiển thị thông tin ngân hàng
            //lblBankInfo.Text = $"Ngân hàng: {PaymentConfig.BankName}\r\n" +
            //                  $"STK: {PaymentConfig.AccountNumber}\r\n" +
            //                  $"Chủ TK: {PaymentConfig.AccountName}";
            
            // Tạo QR Code
            GenerateQRCode();
        }

        /// <summary>
        /// Tạo QR Code theo chuẩn VietQR
        /// </summary>
        private void GenerateQRCode()
        {
            try
            {
                // Tạo mô tả giao dịch
                string description = $"Thanh toan don hang {DateTime.Now:ddMMyyyyHHmmss}";
                
                // Sử dụng PaymentConfig để tạo URL
                string qrUrl = PaymentConfig.GenerateQRUrl(TotalAmount, description);
                
                // Download và hiển thị QR code
                using (var webClient = new System.Net.WebClient())
                {
                    byte[] imageBytes = webClient.DownloadData(qrUrl);
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        picQRCode.Image = Image.FromStream(ms);
                    }
                }
                
                lblQRInstruction.Text = "✅ Quét mã QR bằng ứng dụng Mobile Banking để thanh toán\n" +
                                       $"💰 Số tiền: {TotalAmount.ToString("N0")} VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Không thể tạo mã QR!\n\n{ex.Message}\n\nVui lòng chuyển khoản thủ công theo thông tin bên trên.",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                
                // Hiển thị placeholder
                ShowQRPlaceholder();
            }
        }

        /// <summary>
        /// Hiển thị placeholder khi không tạo được QR
        /// </summary>
        private void ShowQRPlaceholder()
        {
            var bmp = new Bitmap(200, 200);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                
                // Vẽ border
                g.DrawRectangle(new Pen(Color.LightGray, 2), 0, 0, 199, 199);
                
                // Vẽ text
                g.DrawString("QR Code\nđang tải...\n\nVui lòng chuyển khoản\ntheo thông tin trên", 
                    new Font("Segoe UI", 10, FontStyle.Bold), 
                    Brushes.Gray, 
                    new RectangleF(0, 0, 200, 200),
                    new StringFormat { 
                        Alignment = StringAlignment.Center, 
                        LineAlignment = StringAlignment.Center 
                    });
            }
            picQRCode.Image = bmp;
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
            // Validate for cash payment
            if (PaymentMethod == "Tiền mặt")
            {
                if (string.IsNullOrWhiteSpace(txtCustomerPay.Text))
                {
                    MessageBox.Show("Vui lòng nhập số tiền khách đưa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

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
            }
            else
            {
                // Chuyển khoản - Xác nhận đã nhận được tiền
                var result = MessageBox.Show(
                    $"✅ Xác nhận đã nhận được {TotalAmount.ToString("N0")} đ qua chuyển khoản?\n\n" +
                    $"🏦 Ngân hàng: {PaymentConfig.BankName}\n" +
                    $"📱 STK: {PaymentConfig.AccountNumber}\n\n" +
                    "Lưu ý: Vui lòng kiểm tra kỹ trước khi xác nhận!",
                    "Xác nhận thanh toán chuyển khoản",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes)
                {
                    return;
                }

                CustomerPay = TotalAmount;
                ReturnAmount = 0;
            }

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

        private void pnlQRCode_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}