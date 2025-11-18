namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Customers
{
    partial class frmPurchaseHistory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlCustomerInfo = new System.Windows.Forms.Panel();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCustomers = new System.Windows.Forms.ComboBox();
            this.lblSelectCustomer = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.btnFilter = new System.Windows.Forms.Button();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlStatistics = new System.Windows.Forms.Panel();
            this.pnlTotalAmount = new System.Windows.Forms.Panel();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlTotalCount = new System.Windows.Forms.Panel();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.dgvPurchaseHistory = new System.Windows.Forms.DataGridView();
            this.colInvoiceId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaymentMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmployee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colActions = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            this.pnlCustomerInfo.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.pnlStatistics.SuspendLayout();
            this.pnlTotalAmount.SuspendLayout();
            this.pnlTotalCount.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseHistory)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1200, 60);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(241, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "LỊCH SỬ MUA HÀNG";
            // 
            // pnlCustomerInfo
            // 
            this.pnlCustomerInfo.BackColor = System.Drawing.Color.White;
            this.pnlCustomerInfo.Controls.Add(this.lblPhone);
            this.pnlCustomerInfo.Controls.Add(this.lblCustomerName);
            this.pnlCustomerInfo.Controls.Add(this.label2);
            this.pnlCustomerInfo.Controls.Add(this.label1);
            this.pnlCustomerInfo.Controls.Add(this.cmbCustomers);
            this.pnlCustomerInfo.Controls.Add(this.lblSelectCustomer);
            this.pnlCustomerInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCustomerInfo.Location = new System.Drawing.Point(0, 60);
            this.pnlCustomerInfo.Name = "pnlCustomerInfo";
            this.pnlCustomerInfo.Padding = new System.Windows.Forms.Padding(10);
            this.pnlCustomerInfo.Size = new System.Drawing.Size(1200, 80);
            this.pnlCustomerInfo.TabIndex = 1;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblPhone.Location = new System.Drawing.Point(850, 40);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(0, 19);
            this.lblPhone.TabIndex = 5;
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCustomerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.lblCustomerName.Location = new System.Drawing.Point(550, 40);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(0, 19);
            this.lblCustomerName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label2.Location = new System.Drawing.Point(750, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Số điện thoại:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label1.Location = new System.Drawing.Point(450, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tên khách hàng:";
            // 
            // cmbCustomers
            // 
            this.cmbCustomers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomers.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbCustomers.FormattingEnabled = true;
            this.cmbCustomers.Location = new System.Drawing.Point(130, 35);
            this.cmbCustomers.Name = "cmbCustomers";
            this.cmbCustomers.Size = new System.Drawing.Size(300, 25);
            this.cmbCustomers.TabIndex = 1;
            this.cmbCustomers.SelectedIndexChanged += new System.EventHandler(this.cmbCustomers_SelectedIndexChanged);
            // 
            // lblSelectCustomer
            // 
            this.lblSelectCustomer.AutoSize = true;
            this.lblSelectCustomer.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblSelectCustomer.Location = new System.Drawing.Point(20, 38);
            this.lblSelectCustomer.Name = "lblSelectCustomer";
            this.lblSelectCustomer.Size = new System.Drawing.Size(104, 17);
            this.lblSelectCustomer.TabIndex = 0;
            this.lblSelectCustomer.Text = "Chọn khách hàng:";
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.Controls.Add(this.btnFilter);
            this.pnlFilter.Controls.Add(this.dtpToDate);
            this.pnlFilter.Controls.Add(this.label4);
            this.pnlFilter.Controls.Add(this.dtpFromDate);
            this.pnlFilter.Controls.Add(this.label3);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 140);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(10);
            this.pnlFilter.Size = new System.Drawing.Size(1200, 70);
            this.pnlFilter.TabIndex = 2;
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.Location = new System.Drawing.Point(620, 20);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(100, 35);
            this.btnFilter.TabIndex = 4;
            this.btnFilter.Text = "Lọc";
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // dtpToDate
            // 
            this.dtpToDate.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(420, 25);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(180, 25);
            this.dtpToDate.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label4.Location = new System.Drawing.Point(340, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Đến ngày:";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(100, 25);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(180, 25);
            this.dtpFromDate.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label3.Location = new System.Drawing.Point(20, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Từ ngày:";
            // 
            // pnlStatistics
            // 
            this.pnlStatistics.BackColor = System.Drawing.Color.White;
            this.pnlStatistics.Controls.Add(this.pnlTotalAmount);
            this.pnlStatistics.Controls.Add(this.pnlTotalCount);
            this.pnlStatistics.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStatistics.Location = new System.Drawing.Point(0, 210);
            this.pnlStatistics.Name = "pnlStatistics";
            this.pnlStatistics.Padding = new System.Windows.Forms.Padding(10);
            this.pnlStatistics.Size = new System.Drawing.Size(1200, 90);
            this.pnlStatistics.TabIndex = 3;
            // 
            // pnlTotalAmount
            // 
            this.pnlTotalAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.pnlTotalAmount.Controls.Add(this.lblTotalAmount);
            this.pnlTotalAmount.Controls.Add(this.label6);
            this.pnlTotalAmount.Location = new System.Drawing.Point(620, 15);
            this.pnlTotalAmount.Name = "pnlTotalAmount";
            this.pnlTotalAmount.Size = new System.Drawing.Size(560, 60);
            this.pnlTotalAmount.TabIndex = 1;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTotalAmount.ForeColor = System.Drawing.Color.White;
            this.lblTotalAmount.Location = new System.Drawing.Point(0, 25);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(560, 35);
            this.lblTotalAmount.TabIndex = 1;
            this.lblTotalAmount.Text = "0 VNĐ";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(560, 25);
            this.label6.TabIndex = 0;
            this.label6.Text = "TỔNG TIỀN";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTotalCount
            // 
            this.pnlTotalCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.pnlTotalCount.Controls.Add(this.lblTotalCount);
            this.pnlTotalCount.Controls.Add(this.label5);
            this.pnlTotalCount.Location = new System.Drawing.Point(20, 15);
            this.pnlTotalCount.Name = "pnlTotalCount";
            this.pnlTotalCount.Size = new System.Drawing.Size(560, 60);
            this.pnlTotalCount.TabIndex = 0;
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalCount.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTotalCount.ForeColor = System.Drawing.Color.White;
            this.lblTotalCount.Location = new System.Drawing.Point(0, 25);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(560, 35);
            this.lblTotalCount.TabIndex = 1;
            this.lblTotalCount.Text = "0";
            this.lblTotalCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(560, 25);
            this.label5.TabIndex = 0;
            this.label5.Text = "SỐ LẦN MUA HÀNG";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.dgvPurchaseHistory);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 300);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(10);
            this.pnlMain.Size = new System.Drawing.Size(1200, 330);
            this.pnlMain.TabIndex = 4;
            // 
            // dgvPurchaseHistory
            // 
            this.dgvPurchaseHistory.AllowUserToAddRows = false;
            this.dgvPurchaseHistory.AllowUserToDeleteRows = false;
            this.dgvPurchaseHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPurchaseHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvPurchaseHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPurchaseHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPurchaseHistory.ColumnHeadersHeight = 40;
            this.dgvPurchaseHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colInvoiceId,
            this.colDate,
            this.colTotalAmount,
            this.colPaymentMethod,
            this.colEmployee,
            this.colActions});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPurchaseHistory.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPurchaseHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPurchaseHistory.EnableHeadersVisualStyles = false;
            this.dgvPurchaseHistory.Location = new System.Drawing.Point(10, 10);
            this.dgvPurchaseHistory.MultiSelect = false;
            this.dgvPurchaseHistory.Name = "dgvPurchaseHistory";
            this.dgvPurchaseHistory.ReadOnly = true;
            this.dgvPurchaseHistory.RowHeadersVisible = false;
            this.dgvPurchaseHistory.RowTemplate.Height = 35;
            this.dgvPurchaseHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPurchaseHistory.Size = new System.Drawing.Size(1180, 310);
            this.dgvPurchaseHistory.TabIndex = 0;
            this.dgvPurchaseHistory.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPurchaseHistory_CellClick);
            // 
            // colInvoiceId
            // 
            this.colInvoiceId.DataPropertyName = "id";
            this.colInvoiceId.HeaderText = "Mã hóa đơn";
            this.colInvoiceId.Name = "colInvoiceId";
            this.colInvoiceId.ReadOnly = true;
            // 
            // colDate
            // 
            this.colDate.DataPropertyName = "ngayMua";
            this.colDate.HeaderText = "Ngày mua";
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            // 
            // colTotalAmount
            // 
            this.colTotalAmount.DataPropertyName = "tongTien";
            this.colTotalAmount.HeaderText = "Tổng tiền";
            this.colTotalAmount.Name = "colTotalAmount";
            this.colTotalAmount.ReadOnly = true;
            // 
            // colPaymentMethod
            // 
            this.colPaymentMethod.DataPropertyName = "phuongThucThanhToan";
            this.colPaymentMethod.HeaderText = "Phương thức thanh toán";
            this.colPaymentMethod.Name = "colPaymentMethod";
            this.colPaymentMethod.ReadOnly = true;
            // 
            // colEmployee
            // 
            this.colEmployee.DataPropertyName = "nhanVien";
            this.colEmployee.HeaderText = "Nhân viên";
            this.colEmployee.Name = "colEmployee";
            this.colEmployee.ReadOnly = true;
            // 
            // colActions
            // 
            this.colActions.HeaderText = "Thao tác";
            this.colActions.Name = "colActions";
            this.colActions.ReadOnly = true;
            this.colActions.Text = "Chi tiết";
            this.colActions.UseColumnTextForButtonValue = true;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Controls.Add(this.btnExport);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 630);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(10);
            this.pnlBottom.Size = new System.Drawing.Size(1200, 60);
            this.pnlBottom.TabIndex = 5;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1070, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 35);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(20, 15);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(120, 35);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Xuất báo cáo";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // frmPurchaseHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 690);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlStatistics);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlCustomerInfo);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmPurchaseHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lịch sử mua hàng";
            this.Load += new System.EventHandler(this.frmPurchaseHistory_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlCustomerInfo.ResumeLayout(false);
            this.pnlCustomerInfo.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlStatistics.ResumeLayout(false);
            this.pnlTotalAmount.ResumeLayout(false);
            this.pnlTotalCount.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseHistory)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlCustomerInfo;
        private System.Windows.Forms.ComboBox cmbCustomers;
        private System.Windows.Forms.Label lblSelectCustomer;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Panel pnlStatistics;
        private System.Windows.Forms.Panel pnlTotalCount;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlTotalAmount;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.DataGridView dgvPurchaseHistory;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaymentMethod;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmployee;
        private System.Windows.Forms.DataGridViewButtonColumn colActions;
    }
}