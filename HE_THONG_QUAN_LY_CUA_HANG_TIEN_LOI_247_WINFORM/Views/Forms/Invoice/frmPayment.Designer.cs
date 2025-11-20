namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Invoice
{
    partial class frmPayment
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.grpMethod = new System.Windows.Forms.GroupBox();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.btnCash = new System.Windows.Forms.Button();
            this.pnlTotalBox = new System.Windows.Forms.Panel();
            this.lblVatNote = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblTotalLabel = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnComplete = new System.Windows.Forms.Button();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.lblChangeAmount = new System.Windows.Forms.Label();
            this.lblChangeLabel = new System.Windows.Forms.Label();
            this.flowSuggestions = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSuggestions = new System.Windows.Forms.Label();
            this.txtCustomerPay = new System.Windows.Forms.TextBox();
            this.lblCustomerPay = new System.Windows.Forms.Label();
            this.lblRightHeader = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.grpMethod.SuspendLayout();
            this.pnlTotalBox.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(900, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(277, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "THANH TOÁN ĐƠN HÀNG";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 50);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer.Panel1.Controls.Add(this.pnlLeft);
            this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer.Panel2.Controls.Add(this.pnlRight);
            this.splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(10);
            this.splitContainer.Size = new System.Drawing.Size(900, 511);
            this.splitContainer.SplitterDistance = 440;
            this.splitContainer.TabIndex = 1;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.txtDate);
            this.pnlLeft.Controls.Add(this.lblDate);
            this.pnlLeft.Controls.Add(this.grpMethod);
            this.pnlLeft.Controls.Add(this.pnlTotalBox);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(10, 10);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(420, 491);
            this.pnlLeft.TabIndex = 0;
            // 
            // txtDate
            // 
            this.txtDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDate.Enabled = false;
            this.txtDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDate.Location = new System.Drawing.Point(10, 350);
            this.txtDate.Name = "txtDate";
            this.txtDate.ReadOnly = true;
            this.txtDate.Size = new System.Drawing.Size(400, 25);
            this.txtDate.TabIndex = 3;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDate.ForeColor = System.Drawing.Color.DimGray;
            this.lblDate.Location = new System.Drawing.Point(10, 325);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(103, 19);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "Ngày giao dịch:";
            // 
            // grpMethod
            // 
            this.grpMethod.Controls.Add(this.btnTransfer);
            this.grpMethod.Controls.Add(this.btnCash);
            this.grpMethod.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpMethod.Location = new System.Drawing.Point(0, 160);
            this.grpMethod.Name = "grpMethod";
            this.grpMethod.Size = new System.Drawing.Size(420, 150);
            this.grpMethod.TabIndex = 1;
            this.grpMethod.TabStop = false;
            this.grpMethod.Text = "Chọn Phương Thức Thanh Toán:";
            // 
            // btnTransfer
            // 
            this.btnTransfer.BackColor = System.Drawing.Color.White;
            this.btnTransfer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransfer.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnTransfer.ForeColor = System.Drawing.Color.Black;
            this.btnTransfer.Location = new System.Drawing.Point(20, 90);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(380, 45);
            this.btnTransfer.TabIndex = 1;
            this.btnTransfer.Text = "💳  Chuyển khoản / QR Code";
            this.btnTransfer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTransfer.UseVisualStyleBackColor = false;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // btnCash
            // 
            this.btnCash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnCash.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCash.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCash.ForeColor = System.Drawing.Color.White;
            this.btnCash.Location = new System.Drawing.Point(20, 35);
            this.btnCash.Name = "btnCash";
            this.btnCash.Size = new System.Drawing.Size(380, 45);
            this.btnCash.TabIndex = 0;
            this.btnCash.Text = "💵  Tiền mặt";
            this.btnCash.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCash.UseVisualStyleBackColor = false;
            this.btnCash.Click += new System.EventHandler(this.btnCash_Click);
            // 
            // pnlTotalBox
            // 
            this.pnlTotalBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(250)))), ((int)(((byte)(240)))));
            this.pnlTotalBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTotalBox.Controls.Add(this.lblVatNote);
            this.pnlTotalBox.Controls.Add(this.lblTotalAmount);
            this.pnlTotalBox.Controls.Add(this.lblTotalLabel);
            this.pnlTotalBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTotalBox.Location = new System.Drawing.Point(0, 0);
            this.pnlTotalBox.Name = "pnlTotalBox";
            this.pnlTotalBox.Size = new System.Drawing.Size(420, 140);
            this.pnlTotalBox.TabIndex = 0;
            // 
            // lblVatNote
            // 
            this.lblVatNote.AutoSize = true;
            this.lblVatNote.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblVatNote.ForeColor = System.Drawing.Color.DimGray;
            this.lblVatNote.Location = new System.Drawing.Point(135, 100);
            this.lblVatNote.Name = "lblVatNote";
            this.lblVatNote.Size = new System.Drawing.Size(158, 15);
            this.lblVatNote.TabIndex = 2;
            this.lblVatNote.Text = "Đã bao gồm VAT và giảm giá";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblTotalAmount.Location = new System.Drawing.Point(10, 40);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(400, 55);
            this.lblTotalAmount.TabIndex = 1;
            this.lblTotalAmount.Text = "0 đ";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalLabel
            // 
            this.lblTotalLabel.AutoSize = true;
            this.lblTotalLabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalLabel.ForeColor = System.Drawing.Color.DimGray;
            this.lblTotalLabel.Location = new System.Drawing.Point(15, 15);
            this.lblTotalLabel.Name = "lblTotalLabel";
            this.lblTotalLabel.Size = new System.Drawing.Size(256, 20);
            this.lblTotalLabel.TabIndex = 0;
            this.lblTotalLabel.Text = "$ TỔNG CỘNG PHẢI THANH TOÁN";
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.btnCancel);
            this.pnlRight.Controls.Add(this.btnComplete);
            this.pnlRight.Controls.Add(this.txtNote);
            this.pnlRight.Controls.Add(this.lblNote);
            this.pnlRight.Controls.Add(this.lblChangeAmount);
            this.pnlRight.Controls.Add(this.lblChangeLabel);
            this.pnlRight.Controls.Add(this.flowSuggestions);
            this.pnlRight.Controls.Add(this.lblSuggestions);
            this.pnlRight.Controls.Add(this.txtCustomerPay);
            this.pnlRight.Controls.Add(this.lblCustomerPay);
            this.pnlRight.Controls.Add(this.lblRightHeader);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(10, 10);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(436, 491);
            this.pnlRight.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(0, 440);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(430, 45);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "ⓧ  Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnComplete
            // 
            this.btnComplete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnComplete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnComplete.FlatAppearance.BorderSize = 0;
            this.btnComplete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnComplete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnComplete.ForeColor = System.Drawing.Color.White;
            this.btnComplete.Location = new System.Drawing.Point(0, 385);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(430, 45);
            this.btnComplete.TabIndex = 9;
            this.btnComplete.Text = "✔  Hoàn Tất Giao Dịch";
            this.btnComplete.UseVisualStyleBackColor = false;
            this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
            // 
            // txtNote
            // 
            this.txtNote.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNote.Location = new System.Drawing.Point(0, 310);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(430, 60);
            this.txtNote.TabIndex = 8;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNote.Location = new System.Drawing.Point(0, 285);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(59, 19);
            this.lblNote.TabIndex = 7;
            this.lblNote.Text = "Ghi chú:";
            // 
            // lblChangeAmount
            // 
            this.lblChangeAmount.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblChangeAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblChangeAmount.Location = new System.Drawing.Point(0, 240);
            this.lblChangeAmount.Name = "lblChangeAmount";
            this.lblChangeAmount.Size = new System.Drawing.Size(430, 30);
            this.lblChangeAmount.TabIndex = 6;
            this.lblChangeAmount.Text = "0 đ";
            this.lblChangeAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblChangeLabel
            // 
            this.lblChangeLabel.AutoSize = true;
            this.lblChangeLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblChangeLabel.Location = new System.Drawing.Point(0, 215);
            this.lblChangeLabel.Name = "lblChangeLabel";
            this.lblChangeLabel.Size = new System.Drawing.Size(176, 19);
            this.lblChangeLabel.TabIndex = 5;
            this.lblChangeLabel.Text = "Tiền Thối Lại (Trả Khách):";
            // 
            // flowSuggestions
            // 
            this.flowSuggestions.Location = new System.Drawing.Point(0, 150);
            this.flowSuggestions.Name = "flowSuggestions";
            this.flowSuggestions.Size = new System.Drawing.Size(430, 50);
            this.flowSuggestions.TabIndex = 4;
            // 
            // lblSuggestions
            // 
            this.lblSuggestions.AutoSize = true;
            this.lblSuggestions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSuggestions.ForeColor = System.Drawing.Color.DimGray;
            this.lblSuggestions.Location = new System.Drawing.Point(0, 130);
            this.lblSuggestions.Name = "lblSuggestions";
            this.lblSuggestions.Size = new System.Drawing.Size(90, 15);
            this.lblSuggestions.TabIndex = 3;
            this.lblSuggestions.Text = "Gợi ý mệnh giá:";
            // 
            // txtCustomerPay
            // 
            this.txtCustomerPay.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtCustomerPay.Location = new System.Drawing.Point(0, 80);
            this.txtCustomerPay.Name = "txtCustomerPay";
            this.txtCustomerPay.Size = new System.Drawing.Size(430, 32);
            this.txtCustomerPay.TabIndex = 2;
            this.txtCustomerPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCustomerPay.TextChanged += new System.EventHandler(this.txtCustomerPay_TextChanged);
            this.txtCustomerPay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCustomerPay_KeyPress);
            // 
            // lblCustomerPay
            // 
            this.lblCustomerPay.AutoSize = true;
            this.lblCustomerPay.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCustomerPay.Location = new System.Drawing.Point(0, 55);
            this.lblCustomerPay.Name = "lblCustomerPay";
            this.lblCustomerPay.Size = new System.Drawing.Size(136, 19);
            this.lblCustomerPay.TabIndex = 1;
            this.lblCustomerPay.Text = "Tiền Khách Đưa (*):";
            // 
            // lblRightHeader
            // 
            this.lblRightHeader.AutoSize = true;
            this.lblRightHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRightHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblRightHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblRightHeader.Location = new System.Drawing.Point(0, 0);
            this.lblRightHeader.Name = "lblRightHeader";
            this.lblRightHeader.Size = new System.Drawing.Size(232, 25);
            this.lblRightHeader.TabIndex = 0;
            this.lblRightHeader.Text = "THANH TOÁN TIỀN MẶT";
            // 
            // frmPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 561);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thanh Toán";
            this.Load += new System.EventHandler(this.frmPayment_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.grpMethod.ResumeLayout(false);
            this.pnlTotalBox.ResumeLayout(false);
            this.pnlTotalBox.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlTotalBox;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblTotalLabel;
        private System.Windows.Forms.Label lblVatNote;
        private System.Windows.Forms.GroupBox grpMethod;
        private System.Windows.Forms.Button btnCash;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblRightHeader;
        private System.Windows.Forms.Label lblCustomerPay;
        private System.Windows.Forms.TextBox txtCustomerPay;
        private System.Windows.Forms.Label lblSuggestions;
        private System.Windows.Forms.FlowLayoutPanel flowSuggestions;
        private System.Windows.Forms.Label lblChangeLabel;
        private System.Windows.Forms.Label lblChangeAmount;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Button btnComplete;
        private System.Windows.Forms.Button btnCancel;
    }
}