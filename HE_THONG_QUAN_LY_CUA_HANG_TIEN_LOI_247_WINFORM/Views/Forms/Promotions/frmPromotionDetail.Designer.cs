namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Promotions
{
    partial class frmPromotionDetail
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.groupBoxCodes = new System.Windows.Forms.GroupBox();
            this.btnAddCode = new System.Windows.Forms.Button();
            this.dgvPromotionCodes = new System.Windows.Forms.DataGridView();
            this.colCodeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUsageCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCodeStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxCondition = new System.Windows.Forms.GroupBox();
            this.numMaxValue = new System.Windows.Forms.NumericUpDown();
            this.lblMaxValue = new System.Windows.Forms.Label();
            this.cmbDiscountType = new System.Windows.Forms.ComboBox();
            this.lblDiscountType = new System.Windows.Forms.Label();
            this.numMinValue = new System.Windows.Forms.NumericUpDown();
            this.lblMinValue = new System.Windows.Forms.Label();
            this.txtCondition = new System.Windows.Forms.TextBox();
            this.lblCondition = new System.Windows.Forms.Label();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.txtPromotionName = new System.Windows.Forms.TextBox();
            this.lblPromotionName = new System.Windows.Forms.Label();
            this.txtPromotionId = new System.Windows.Forms.TextBox();
            this.lblPromotionId = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.groupBoxCodes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPromotionCodes)).BeginInit();
            this.groupBoxCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinValue)).BeginInit();
            this.groupBoxInfo.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1000, 60);
            this.panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(430, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "CHI TIẾT CHƯƠNG TRÌNH KHUYẾN MÃI";
            // 
            // panelMain
            // 
            this.panelMain.AutoScroll = true;
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.groupBoxCodes);
            this.panelMain.Controls.Add(this.groupBoxCondition);
            this.panelMain.Controls.Add(this.groupBoxInfo);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 60);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20);
            this.panelMain.Size = new System.Drawing.Size(1000, 590);
            this.panelMain.TabIndex = 1;
            // 
            // groupBoxCodes
            // 
            this.groupBoxCodes.Controls.Add(this.btnAddCode);
            this.groupBoxCodes.Controls.Add(this.dgvPromotionCodes);
            this.groupBoxCodes.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxCodes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxCodes.Location = new System.Drawing.Point(20, 520);
            this.groupBoxCodes.Name = "groupBoxCodes";
            this.groupBoxCodes.Size = new System.Drawing.Size(960, 250);
            this.groupBoxCodes.TabIndex = 2;
            this.groupBoxCodes.TabStop = false;
            this.groupBoxCodes.Text = "Danh sách mã khuyến mãi";
            this.groupBoxCodes.Visible = false;
            // 
            // btnAddCode
            // 
            this.btnAddCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAddCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAddCode.ForeColor = System.Drawing.Color.White;
            this.btnAddCode.Location = new System.Drawing.Point(820, 25);
            this.btnAddCode.Name = "btnAddCode";
            this.btnAddCode.Size = new System.Drawing.Size(120, 35);
            this.btnAddCode.TabIndex = 1;
            this.btnAddCode.Text = "Thêm mã";
            this.btnAddCode.UseVisualStyleBackColor = false;
            this.btnAddCode.Click += new System.EventHandler(this.btnAddCode_Click);
            // 
            // dgvPromotionCodes
            // 
            this.dgvPromotionCodes.AllowUserToAddRows = false;
            this.dgvPromotionCodes.AllowUserToDeleteRows = false;
            this.dgvPromotionCodes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPromotionCodes.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPromotionCodes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPromotionCodes.ColumnHeadersHeight = 35;
            this.dgvPromotionCodes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCodeId,
            this.colCode,
            this.colValue,
            this.colUsageCount,
            this.colCodeStatus});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(233)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPromotionCodes.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPromotionCodes.EnableHeadersVisualStyles = false;
            this.dgvPromotionCodes.Location = new System.Drawing.Point(20, 70);
            this.dgvPromotionCodes.Name = "dgvPromotionCodes";
            this.dgvPromotionCodes.ReadOnly = true;
            this.dgvPromotionCodes.RowHeadersVisible = false;
            this.dgvPromotionCodes.RowTemplate.Height = 30;
            this.dgvPromotionCodes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPromotionCodes.Size = new System.Drawing.Size(920, 160);
            this.dgvPromotionCodes.TabIndex = 0;
            // 
            // colCodeId
            // 
            this.colCodeId.DataPropertyName = "id";
            this.colCodeId.HeaderText = "Mã";
            this.colCodeId.Name = "colCodeId";
            this.colCodeId.ReadOnly = true;
            this.colCodeId.Visible = false;
            // 
            // colCode
            // 
            this.colCode.DataPropertyName = "code";
            this.colCode.HeaderText = "Mã khuyến mãi";
            this.colCode.Name = "colCode";
            this.colCode.ReadOnly = true;
            // 
            // colValue
            // 
            this.colValue.DataPropertyName = "giaTri";
            this.colValue.HeaderText = "Giá trị";
            this.colValue.Name = "colValue";
            this.colValue.ReadOnly = true;
            // 
            // colUsageCount
            // 
            this.colUsageCount.DataPropertyName = "soLanSuDung";
            this.colUsageCount.HeaderText = "Số lần sử dụng";
            this.colUsageCount.Name = "colUsageCount";
            this.colUsageCount.ReadOnly = true;
            // 
            // colCodeStatus
            // 
            this.colCodeStatus.DataPropertyName = "trangThai";
            this.colCodeStatus.HeaderText = "Trạng thái";
            this.colCodeStatus.Name = "colCodeStatus";
            this.colCodeStatus.ReadOnly = true;
            // 
            // groupBoxCondition
            // 
            this.groupBoxCondition.Controls.Add(this.numMaxValue);
            this.groupBoxCondition.Controls.Add(this.lblMaxValue);
            this.groupBoxCondition.Controls.Add(this.cmbDiscountType);
            this.groupBoxCondition.Controls.Add(this.lblDiscountType);
            this.groupBoxCondition.Controls.Add(this.numMinValue);
            this.groupBoxCondition.Controls.Add(this.lblMinValue);
            this.groupBoxCondition.Controls.Add(this.txtCondition);
            this.groupBoxCondition.Controls.Add(this.lblCondition);
            this.groupBoxCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxCondition.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxCondition.Location = new System.Drawing.Point(20, 320);
            this.groupBoxCondition.Name = "groupBoxCondition";
            this.groupBoxCondition.Size = new System.Drawing.Size(960, 200);
            this.groupBoxCondition.TabIndex = 1;
            this.groupBoxCondition.TabStop = false;
            this.groupBoxCondition.Text = "Điều kiện áp dụng";
            // 
            // numMaxValue
            // 
            this.numMaxValue.DecimalPlaces = 2;
            this.numMaxValue.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.numMaxValue.Location = new System.Drawing.Point(620, 150);
            this.numMaxValue.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numMaxValue.Name = "numMaxValue";
            this.numMaxValue.Size = new System.Drawing.Size(300, 25);
            this.numMaxValue.TabIndex = 7;
            // 
            // lblMaxValue
            // 
            this.lblMaxValue.AutoSize = true;
            this.lblMaxValue.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblMaxValue.Location = new System.Drawing.Point(490, 153);
            this.lblMaxValue.Name = "lblMaxValue";
            this.lblMaxValue.Size = new System.Drawing.Size(97, 17);
            this.lblMaxValue.TabIndex = 6;
            this.lblMaxValue.Text = "Giá trị tối đa:";
            // 
            // cmbDiscountType
            // 
            this.cmbDiscountType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDiscountType.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbDiscountType.FormattingEnabled = true;
            this.cmbDiscountType.Location = new System.Drawing.Point(150, 150);
            this.cmbDiscountType.Name = "cmbDiscountType";
            this.cmbDiscountType.Size = new System.Drawing.Size(300, 25);
            this.cmbDiscountType.TabIndex = 5;
            // 
            // lblDiscountType
            // 
            this.lblDiscountType.AutoSize = true;
            this.lblDiscountType.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblDiscountType.Location = new System.Drawing.Point(20, 153);
            this.lblDiscountType.Name = "lblDiscountType";
            this.lblDiscountType.Size = new System.Drawing.Size(77, 17);
            this.lblDiscountType.TabIndex = 4;
            this.lblDiscountType.Text = "Giảm theo:";
            // 
            // numMinValue
            // 
            this.numMinValue.DecimalPlaces = 2;
            this.numMinValue.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.numMinValue.Location = new System.Drawing.Point(150, 110);
            this.numMinValue.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numMinValue.Name = "numMinValue";
            this.numMinValue.Size = new System.Drawing.Size(300, 25);
            this.numMinValue.TabIndex = 3;
            // 
            // lblMinValue
            // 
            this.lblMinValue.AutoSize = true;
            this.lblMinValue.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblMinValue.Location = new System.Drawing.Point(20, 113);
            this.lblMinValue.Name = "lblMinValue";
            this.lblMinValue.Size = new System.Drawing.Size(120, 17);
            this.lblMinValue.TabIndex = 2;
            this.lblMinValue.Text = "Giá trị tối thiệu:";
            // 
            // txtCondition
            // 
            this.txtCondition.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtCondition.Location = new System.Drawing.Point(150, 30);
            this.txtCondition.Multiline = true;
            this.txtCondition.Name = "txtCondition";
            this.txtCondition.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCondition.Size = new System.Drawing.Size(770, 60);
            this.txtCondition.TabIndex = 1;
            // 
            // lblCondition
            // 
            this.lblCondition.AutoSize = true;
            this.lblCondition.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblCondition.Location = new System.Drawing.Point(20, 33);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(74, 17);
            this.lblCondition.TabIndex = 0;
            this.lblCondition.Text = "Điều kiện:";
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Controls.Add(this.txtDescription);
            this.groupBoxInfo.Controls.Add(this.lblDescription);
            this.groupBoxInfo.Controls.Add(this.dtpEndDate);
            this.groupBoxInfo.Controls.Add(this.lblEndDate);
            this.groupBoxInfo.Controls.Add(this.dtpStartDate);
            this.groupBoxInfo.Controls.Add(this.lblStartDate);
            this.groupBoxInfo.Controls.Add(this.cmbType);
            this.groupBoxInfo.Controls.Add(this.lblType);
            this.groupBoxInfo.Controls.Add(this.txtPromotionName);
            this.groupBoxInfo.Controls.Add(this.lblPromotionName);
            this.groupBoxInfo.Controls.Add(this.txtPromotionId);
            this.groupBoxInfo.Controls.Add(this.lblPromotionId);
            this.groupBoxInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxInfo.Location = new System.Drawing.Point(20, 20);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(960, 300);
            this.groupBoxInfo.TabIndex = 0;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "Thông tin chương trình";
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtDescription.Location = new System.Drawing.Point(150, 200);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(770, 80);
            this.txtDescription.TabIndex = 11;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblDescription.Location = new System.Drawing.Point(20, 203);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(48, 17);
            this.lblDescription.TabIndex = 10;
            this.lblDescription.Text = "Mô tả:";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(620, 150);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(300, 25);
            this.dtpEndDate.TabIndex = 9;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblEndDate.Location = new System.Drawing.Point(490, 153);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(105, 17);
            this.lblEndDate.TabIndex = 8;
            this.lblEndDate.Text = "Ngày kết thúc:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(150, 150);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(300, 25);
            this.dtpStartDate.TabIndex = 7;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblStartDate.Location = new System.Drawing.Point(20, 153);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(101, 17);
            this.lblStartDate.TabIndex = 6;
            this.lblStartDate.Text = "Ngày bắt đầu:";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(150, 110);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(770, 25);
            this.cmbType.TabIndex = 5;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblType.Location = new System.Drawing.Point(20, 113);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(39, 17);
            this.lblType.TabIndex = 4;
            this.lblType.Text = "Loại:";
            // 
            // txtPromotionName
            // 
            this.txtPromotionName.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtPromotionName.Location = new System.Drawing.Point(150, 70);
            this.txtPromotionName.Name = "txtPromotionName";
            this.txtPromotionName.Size = new System.Drawing.Size(770, 25);
            this.txtPromotionName.TabIndex = 3;
            // 
            // lblPromotionName
            // 
            this.lblPromotionName.AutoSize = true;
            this.lblPromotionName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblPromotionName.Location = new System.Drawing.Point(20, 73);
            this.lblPromotionName.Name = "lblPromotionName";
            this.lblPromotionName.Size = new System.Drawing.Size(118, 17);
            this.lblPromotionName.TabIndex = 2;
            this.lblPromotionName.Text = "Tên chương trình:";
            // 
            // txtPromotionId
            // 
            this.txtPromotionId.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtPromotionId.Location = new System.Drawing.Point(150, 30);
            this.txtPromotionId.Name = "txtPromotionId";
            this.txtPromotionId.ReadOnly = true;
            this.txtPromotionId.Size = new System.Drawing.Size(770, 25);
            this.txtPromotionId.TabIndex = 1;
            // 
            // lblPromotionId
            // 
            this.lblPromotionId.AutoSize = true;
            this.lblPromotionId.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblPromotionId.Location = new System.Drawing.Point(20, 33);
            this.lblPromotionId.Name = "lblPromotionId";
            this.lblPromotionId.Size = new System.Drawing.Size(115, 17);
            this.lblPromotionId.TabIndex = 0;
            this.lblPromotionId.Text = "Mã chương trình:";
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.White;
            this.panelButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 650);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(1000, 70);
            this.panelButtons.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(520, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 40);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(183)))), ((int)(((byte)(255)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(330, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 40);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmPromotionDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 720);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPromotionDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết chương trình khuyến mãi";
            this.Load += new System.EventHandler(this.frmPromotionDetail_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.groupBoxCodes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPromotionCodes)).EndInit();
            this.groupBoxCondition.ResumeLayout(false);
            this.groupBoxCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinValue)).EndInit();
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.TextBox txtPromotionId;
        private System.Windows.Forms.Label lblPromotionId;
        private System.Windows.Forms.TextBox txtPromotionName;
        private System.Windows.Forms.Label lblPromotionName;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.GroupBox groupBoxCondition;
        private System.Windows.Forms.TextBox txtCondition;
        private System.Windows.Forms.Label lblCondition;
        private System.Windows.Forms.NumericUpDown numMinValue;
        private System.Windows.Forms.Label lblMinValue;
        private System.Windows.Forms.ComboBox cmbDiscountType;
        private System.Windows.Forms.Label lblDiscountType;
        private System.Windows.Forms.NumericUpDown numMaxValue;
        private System.Windows.Forms.Label lblMaxValue;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBoxCodes;
        private System.Windows.Forms.DataGridView dgvPromotionCodes;
        private System.Windows.Forms.Button btnAddCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUsageCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodeStatus;
    }
}