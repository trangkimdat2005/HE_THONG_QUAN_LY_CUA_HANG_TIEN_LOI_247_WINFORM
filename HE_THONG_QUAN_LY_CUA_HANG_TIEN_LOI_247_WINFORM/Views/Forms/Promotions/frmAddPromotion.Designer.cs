namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Promotions
{
    partial class frmAddPromotion
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tlpRoot = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.grpValue = new System.Windows.Forms.GroupBox();
            this.lblUsageLimit = new System.Windows.Forms.Label();
            this.txtUsageLimit = new System.Windows.Forms.TextBox();
            this.lblMinOrder = new System.Windows.Forms.Label();
            this.txtMinOrder = new System.Windows.Forms.TextBox();
            this.lblMaxDiscount = new System.Windows.Forms.Label();
            this.txtMaxDiscount = new System.Windows.Forms.TextBox();
            this.lblDiscountValue = new System.Windows.Forms.Label();
            this.txtDiscountValue = new System.Windows.Forms.TextBox();
            this.lblDiscountType = new System.Windows.Forms.Label();
            this.cboDiscountType = new System.Windows.Forms.ComboBox();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.pnlCode = new System.Windows.Forms.Panel();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.btnGenerateCode = new System.Windows.Forms.Button();
            this.lblCode = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.grpScope = new System.Windows.Forms.GroupBox();
            this.rdoProduct = new System.Windows.Forms.RadioButton();
            this.rdoCategory = new System.Windows.Forms.RadioButton();
            this.rdoAll = new System.Windows.Forms.RadioButton();
            this.grpSettings = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.tlpRoot.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.grpValue.SuspendLayout();
            this.grpInfo.SuspendLayout();
            this.pnlCode.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.grpScope.SuspendLayout();
            this.grpSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1200, 60);
            this.panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(220, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "THÊM KHUYẾN MÃI";
            // 
            // tlpRoot
            // 
            this.tlpRoot.ColumnCount = 2;
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tlpRoot.Controls.Add(this.pnlLeft, 0, 0);
            this.tlpRoot.Controls.Add(this.pnlRight, 1, 0);
            this.tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpRoot.Location = new System.Drawing.Point(0, 60);
            this.tlpRoot.Name = "tlpRoot";
            this.tlpRoot.Padding = new System.Windows.Forms.Padding(10);
            this.tlpRoot.RowCount = 1;
            this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpRoot.Size = new System.Drawing.Size(1200, 640);
            this.tlpRoot.TabIndex = 1;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.grpValue);
            this.pnlLeft.Controls.Add(this.grpInfo);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(13, 13);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.pnlLeft.Size = new System.Drawing.Size(820, 614);
            this.pnlLeft.TabIndex = 0;
            // 
            // grpValue
            // 
            this.grpValue.Controls.Add(this.lblUsageLimit);
            this.grpValue.Controls.Add(this.txtUsageLimit);
            this.grpValue.Controls.Add(this.lblMinOrder);
            this.grpValue.Controls.Add(this.txtMinOrder);
            this.grpValue.Controls.Add(this.lblMaxDiscount);
            this.grpValue.Controls.Add(this.txtMaxDiscount);
            this.grpValue.Controls.Add(this.lblDiscountValue);
            this.grpValue.Controls.Add(this.txtDiscountValue);
            this.grpValue.Controls.Add(this.lblDiscountType);
            this.grpValue.Controls.Add(this.cboDiscountType);
            this.grpValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpValue.Location = new System.Drawing.Point(0, 260);
            this.grpValue.Name = "grpValue";
            this.grpValue.Padding = new System.Windows.Forms.Padding(15, 20, 15, 15);
            this.grpValue.Size = new System.Drawing.Size(810, 354);
            this.grpValue.TabIndex = 1;
            this.grpValue.TabStop = false;
            this.grpValue.Text = "Giá trị & Điều kiện";
            // 
            // lblUsageLimit
            // 
            this.lblUsageLimit.AutoSize = true;
            this.lblUsageLimit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUsageLimit.Location = new System.Drawing.Point(20, 180);
            this.lblUsageLimit.Name = "lblUsageLimit";
            this.lblUsageLimit.Size = new System.Drawing.Size(114, 15);
            this.lblUsageLimit.TabIndex = 9;
            this.lblUsageLimit.Text = "Tổng số lần sử dụng";
            // 
            // txtUsageLimit
            // 
            this.txtUsageLimit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUsageLimit.Location = new System.Drawing.Point(20, 205);
            this.txtUsageLimit.Name = "txtUsageLimit";
            this.txtUsageLimit.Size = new System.Drawing.Size(200, 25);
            this.txtUsageLimit.TabIndex = 8;
            // 
            // lblMinOrder
            // 
            this.lblMinOrder.AutoSize = true;
            this.lblMinOrder.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMinOrder.Location = new System.Drawing.Point(250, 110);
            this.lblMinOrder.Name = "lblMinOrder";
            this.lblMinOrder.Size = new System.Drawing.Size(106, 15);
            this.lblMinOrder.TabIndex = 7;
            this.lblMinOrder.Text = "Đơn hàng tối thiểu";
            // 
            // txtMinOrder
            // 
            this.txtMinOrder.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMinOrder.Location = new System.Drawing.Point(250, 135);
            this.txtMinOrder.Name = "txtMinOrder";
            this.txtMinOrder.Size = new System.Drawing.Size(200, 25);
            this.txtMinOrder.TabIndex = 6;
            // 
            // lblMaxDiscount
            // 
            this.lblMaxDiscount.AutoSize = true;
            this.lblMaxDiscount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMaxDiscount.Location = new System.Drawing.Point(20, 110);
            this.lblMaxDiscount.Name = "lblMaxDiscount";
            this.lblMaxDiscount.Size = new System.Drawing.Size(68, 15);
            this.lblMaxDiscount.TabIndex = 5;
            this.lblMaxDiscount.Text = "Giảm tối đa";
            // 
            // txtMaxDiscount
            // 
            this.txtMaxDiscount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaxDiscount.Location = new System.Drawing.Point(20, 135);
            this.txtMaxDiscount.Name = "txtMaxDiscount";
            this.txtMaxDiscount.Size = new System.Drawing.Size(200, 25);
            this.txtMaxDiscount.TabIndex = 4;
            // 
            // lblDiscountValue
            // 
            this.lblDiscountValue.AutoSize = true;
            this.lblDiscountValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDiscountValue.Location = new System.Drawing.Point(250, 40);
            this.lblDiscountValue.Name = "lblDiscountValue";
            this.lblDiscountValue.Size = new System.Drawing.Size(68, 15);
            this.lblDiscountValue.TabIndex = 3;
            this.lblDiscountValue.Text = "Giá trị giảm";
            // 
            // txtDiscountValue
            // 
            this.txtDiscountValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDiscountValue.Location = new System.Drawing.Point(250, 65);
            this.txtDiscountValue.Name = "txtDiscountValue";
            this.txtDiscountValue.Size = new System.Drawing.Size(200, 25);
            this.txtDiscountValue.TabIndex = 2;
            // 
            // lblDiscountType
            // 
            this.lblDiscountType.AutoSize = true;
            this.lblDiscountType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDiscountType.Location = new System.Drawing.Point(20, 40);
            this.lblDiscountType.Name = "lblDiscountType";
            this.lblDiscountType.Size = new System.Drawing.Size(90, 15);
            this.lblDiscountType.TabIndex = 1;
            this.lblDiscountType.Text = "Hình thức giảm";
            // 
            // cboDiscountType
            // 
            this.cboDiscountType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDiscountType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboDiscountType.FormattingEnabled = true;
            this.cboDiscountType.Items.AddRange(new object[] {
            "Phần trăm (%)",
            "Giảm tiền (VND)"});
            this.cboDiscountType.Location = new System.Drawing.Point(20, 65);
            this.cboDiscountType.Name = "cboDiscountType";
            this.cboDiscountType.Size = new System.Drawing.Size(200, 25);
            this.cboDiscountType.TabIndex = 0;
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.pnlCode);
            this.grpInfo.Controls.Add(this.lblCode);
            this.grpInfo.Controls.Add(this.txtDescription);
            this.grpInfo.Controls.Add(this.lblDescription);
            this.grpInfo.Controls.Add(this.txtName);
            this.grpInfo.Controls.Add(this.lblName);
            this.grpInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpInfo.Location = new System.Drawing.Point(0, 0);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Padding = new System.Windows.Forms.Padding(15, 20, 15, 15);
            this.grpInfo.Size = new System.Drawing.Size(810, 260);
            this.grpInfo.TabIndex = 0;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "Thông tin Chương trình & Điều kiện";
            // 
            // pnlCode
            // 
            this.pnlCode.Controls.Add(this.txtCode);
            this.pnlCode.Controls.Add(this.btnGenerateCode);
            this.pnlCode.Location = new System.Drawing.Point(20, 215);
            this.pnlCode.Name = "pnlCode";
            this.pnlCode.Size = new System.Drawing.Size(760, 30);
            this.pnlCode.TabIndex = 5;
            // 
            // txtCode
            // 
            this.txtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCode.Location = new System.Drawing.Point(0, 0);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(760, 25);
            this.txtCode.TabIndex = 0;
            // 
            // btnGenerateCode
            // 
            this.btnGenerateCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(183)))), ((int)(((byte)(255)))));
            this.btnGenerateCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateCode.ForeColor = System.Drawing.Color.White;
            this.btnGenerateCode.Location = new System.Drawing.Point(620, 0);
            this.btnGenerateCode.Name = "btnGenerateCode";
            this.btnGenerateCode.Size = new System.Drawing.Size(140, 30);
            this.btnGenerateCode.TabIndex = 1;
            this.btnGenerateCode.Text = "Tạo mã ngẫu nhiên";
            this.btnGenerateCode.UseVisualStyleBackColor = false;
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCode.Location = new System.Drawing.Point(20, 195);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(90, 15);
            this.lblCode.TabIndex = 4;
            this.lblCode.Text = "Mã Khuyến Mãi";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.Location = new System.Drawing.Point(20, 105);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(760, 80);
            this.txtDescription.TabIndex = 3;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDescription.Location = new System.Drawing.Point(20, 85);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(38, 15);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Mô tả";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.Location = new System.Drawing.Point(20, 55);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(760, 25);
            this.txtName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblName.Location = new System.Drawing.Point(20, 30);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(97, 15);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Tên chương trình";
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.grpScope);
            this.pnlRight.Controls.Add(this.grpSettings);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(839, 13);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.pnlRight.Size = new System.Drawing.Size(348, 614);
            this.pnlRight.TabIndex = 1;
            // 
            // grpScope
            // 
            this.grpScope.Controls.Add(this.rdoProduct);
            this.grpScope.Controls.Add(this.rdoCategory);
            this.grpScope.Controls.Add(this.rdoAll);
            this.grpScope.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpScope.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpScope.Location = new System.Drawing.Point(10, 290);
            this.grpScope.Name = "grpScope";
            this.grpScope.Padding = new System.Windows.Forms.Padding(15, 20, 15, 15);
            this.grpScope.Size = new System.Drawing.Size(338, 160);
            this.grpScope.TabIndex = 1;
            this.grpScope.TabStop = false;
            this.grpScope.Text = "Phạm vi áp dụng";
            // 
            // rdoProduct
            // 
            this.rdoProduct.AutoSize = true;
            this.rdoProduct.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rdoProduct.Location = new System.Drawing.Point(20, 95);
            this.rdoProduct.Name = "rdoProduct";
            this.rdoProduct.Size = new System.Drawing.Size(114, 19);
            this.rdoProduct.TabIndex = 2;
            this.rdoProduct.Text = "Sản phẩm cụ thể";
            this.rdoProduct.UseVisualStyleBackColor = true;
            // 
            // rdoCategory
            // 
            this.rdoCategory.AutoSize = true;
            this.rdoCategory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rdoCategory.Location = new System.Drawing.Point(20, 65);
            this.rdoCategory.Name = "rdoCategory";
            this.rdoCategory.Size = new System.Drawing.Size(116, 19);
            this.rdoCategory.TabIndex = 1;
            this.rdoCategory.Text = "Danh mục cụ thể";
            this.rdoCategory.UseVisualStyleBackColor = true;
            // 
            // rdoAll
            // 
            this.rdoAll.AutoSize = true;
            this.rdoAll.Checked = true;
            this.rdoAll.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rdoAll.Location = new System.Drawing.Point(20, 35);
            this.rdoAll.Name = "rdoAll";
            this.rdoAll.Size = new System.Drawing.Size(119, 19);
            this.rdoAll.TabIndex = 0;
            this.rdoAll.TabStop = true;
            this.rdoAll.Text = "Toàn bộ cửa hàng";
            this.rdoAll.UseVisualStyleBackColor = true;
            // 
            // grpSettings
            // 
            this.grpSettings.Controls.Add(this.btnCancel);
            this.grpSettings.Controls.Add(this.btnSave);
            this.grpSettings.Controls.Add(this.dtpEndDate);
            this.grpSettings.Controls.Add(this.lblEndDate);
            this.grpSettings.Controls.Add(this.dtpStartDate);
            this.grpSettings.Controls.Add(this.lblStartDate);
            this.grpSettings.Controls.Add(this.cboStatus);
            this.grpSettings.Controls.Add(this.lblStatus);
            this.grpSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpSettings.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpSettings.Location = new System.Drawing.Point(10, 0);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Padding = new System.Windows.Forms.Padding(15, 20, 15, 15);
            this.grpSettings.Size = new System.Drawing.Size(338, 290);
            this.grpSettings.TabIndex = 0;
            this.grpSettings.TabStop = false;
            this.grpSettings.Text = "Cài đặt";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(120, 235);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(183)))), ((int)(((byte)(255)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(20, 235);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "dd/MM/yyyy";
            this.dtpEndDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.Location = new System.Drawing.Point(20, 195);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(200, 25);
            this.dtpEndDate.TabIndex = 5;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEndDate.Location = new System.Drawing.Point(20, 175);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(81, 15);
            this.lblEndDate.TabIndex = 4;
            this.lblEndDate.Text = "Ngày kết thúc";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CustomFormat = "dd/MM/yyyy";
            this.dtpStartDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(20, 135);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(200, 25);
            this.dtpStartDate.TabIndex = 3;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStartDate.Location = new System.Drawing.Point(20, 115);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(78, 15);
            this.lblStartDate.TabIndex = 2;
            this.lblStartDate.Text = "Ngày bắt đầu";
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Items.AddRange(new object[] {
            "Hoạt động",
            "Tạm dừng"});
            this.cboStatus.Location = new System.Drawing.Point(20, 75);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(200, 25);
            this.cboStatus.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.Location = new System.Drawing.Point(20, 50);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(59, 15);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Trạng thái";
            // 
            // frmAddPromotion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.tlpRoot);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmAddPromotion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm khuyến mãi";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.tlpRoot.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.grpValue.ResumeLayout(false);
            this.grpValue.PerformLayout();
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            this.pnlCode.ResumeLayout(false);
            this.pnlCode.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.grpScope.ResumeLayout(false);
            this.grpScope.PerformLayout();
            this.grpSettings.ResumeLayout(false);
            this.grpSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel tlpRoot;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.GroupBox grpValue;
        private System.Windows.Forms.Label lblUsageLimit;
        private System.Windows.Forms.TextBox txtUsageLimit;
        private System.Windows.Forms.Label lblMinOrder;
        private System.Windows.Forms.TextBox txtMinOrder;
        private System.Windows.Forms.Label lblMaxDiscount;
        private System.Windows.Forms.TextBox txtMaxDiscount;
        private System.Windows.Forms.Label lblDiscountValue;
        private System.Windows.Forms.TextBox txtDiscountValue;
        private System.Windows.Forms.Label lblDiscountType;
        private System.Windows.Forms.ComboBox cboDiscountType;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.Panel pnlCode;
        private System.Windows.Forms.Button btnGenerateCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.GroupBox grpScope;
        private System.Windows.Forms.RadioButton rdoProduct;
        private System.Windows.Forms.RadioButton rdoCategory;
        private System.Windows.Forms.RadioButton rdoAll;
        private System.Windows.Forms.GroupBox grpSettings;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label lblStatus;
    }
}