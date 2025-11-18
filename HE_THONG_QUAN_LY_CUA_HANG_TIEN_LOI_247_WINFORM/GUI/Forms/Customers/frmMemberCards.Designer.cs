namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Customers
{
    partial class frmMemberCards
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
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cmbFilterRank = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.dgvMemberCards = new System.Windows.Forms.DataGridView();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.btnViewHistory = new System.Windows.Forms.Button();
            this.btnAddPoints = new System.Windows.Forms.Button();
            this.txtAddPoints = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.dtpIssueDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbRank = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPoints = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCustomerId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.colCustomerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPoints = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIssueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlTop.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMemberCards)).BeginInit();
            this.pnlRight.SuspendLayout();
            this.groupBoxActions.SuspendLayout();
            this.groupBoxInfo.SuspendLayout();
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
            this.pnlTop.Size = new System.Drawing.Size(1400, 60);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(326, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ THẺ THÀNH VIÊN";
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.White;
            this.pnlSearch.Controls.Add(this.btnRefresh);
            this.pnlSearch.Controls.Add(this.cmbFilterRank);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Controls.Add(this.lblSearch);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 60);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(10);
            this.pnlSearch.Size = new System.Drawing.Size(1400, 70);
            this.pnlSearch.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(880, 20);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 35);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cmbFilterRank
            // 
            this.cmbFilterRank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterRank.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbFilterRank.FormattingEnabled = true;
            this.cmbFilterRank.Location = new System.Drawing.Point(670, 25);
            this.cmbFilterRank.Name = "cmbFilterRank";
            this.cmbFilterRank.Size = new System.Drawing.Size(180, 25);
            this.cmbFilterRank.TabIndex = 4;
            this.cmbFilterRank.SelectedIndexChanged += new System.EventHandler(this.cmbFilterRank_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label1.Location = new System.Drawing.Point(580, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Lọc hạng thẻ:";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(430, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 35);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtSearch.Location = new System.Drawing.Point(100, 25);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(320, 25);
            this.txtSearch.TabIndex = 1;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblSearch.Location = new System.Drawing.Point(20, 28);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(63, 17);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Tìm kiếm:";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 130);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.pnlLeft);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.pnlRight);
            this.splitContainer.Size = new System.Drawing.Size(1400, 550);
            this.splitContainer.SplitterDistance = 850;
            this.splitContainer.TabIndex = 2;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.dgvMemberCards);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(10);
            this.pnlLeft.Size = new System.Drawing.Size(850, 550);
            this.pnlLeft.TabIndex = 0;
            // 
            // dgvMemberCards
            // 
            this.dgvMemberCards.AllowUserToAddRows = false;
            this.dgvMemberCards.AllowUserToDeleteRows = false;
            this.dgvMemberCards.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMemberCards.BackgroundColor = System.Drawing.Color.White;
            this.dgvMemberCards.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMemberCards.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMemberCards.ColumnHeadersHeight = 40;
            this.dgvMemberCards.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCustomerId,
            this.colCustomerName,
            this.colPhone,
            this.colRank,
            this.colPoints,
            this.colIssueDate});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMemberCards.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMemberCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMemberCards.EnableHeadersVisualStyles = false;
            this.dgvMemberCards.Location = new System.Drawing.Point(10, 10);
            this.dgvMemberCards.MultiSelect = false;
            this.dgvMemberCards.Name = "dgvMemberCards";
            this.dgvMemberCards.ReadOnly = true;
            this.dgvMemberCards.RowHeadersVisible = false;
            this.dgvMemberCards.RowTemplate.Height = 35;
            this.dgvMemberCards.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMemberCards.Size = new System.Drawing.Size(830, 530);
            this.dgvMemberCards.TabIndex = 0;
            this.dgvMemberCards.SelectionChanged += new System.EventHandler(this.dgvMemberCards_SelectionChanged);
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.White;
            this.pnlRight.Controls.Add(this.groupBoxActions);
            this.pnlRight.Controls.Add(this.groupBoxInfo);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(0, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(10);
            this.pnlRight.Size = new System.Drawing.Size(546, 550);
            this.pnlRight.TabIndex = 0;
            // 
            // groupBoxActions
            // 
            this.groupBoxActions.Controls.Add(this.btnViewHistory);
            this.groupBoxActions.Controls.Add(this.btnAddPoints);
            this.groupBoxActions.Controls.Add(this.txtAddPoints);
            this.groupBoxActions.Controls.Add(this.label8);
            this.groupBoxActions.Controls.Add(this.btnSave);
            this.groupBoxActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxActions.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxActions.Location = new System.Drawing.Point(10, 370);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxActions.Size = new System.Drawing.Size(526, 170);
            this.groupBoxActions.TabIndex = 1;
            this.groupBoxActions.TabStop = false;
            this.groupBoxActions.Text = "Thao tác";
            // 
            // btnViewHistory
            // 
            this.btnViewHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnViewHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewHistory.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnViewHistory.ForeColor = System.Drawing.Color.White;
            this.btnViewHistory.Location = new System.Drawing.Point(280, 110);
            this.btnViewHistory.Name = "btnViewHistory";
            this.btnViewHistory.Size = new System.Drawing.Size(220, 40);
            this.btnViewHistory.TabIndex = 4;
            this.btnViewHistory.Text = "Xem lịch sử tích điểm";
            this.btnViewHistory.UseVisualStyleBackColor = false;
            this.btnViewHistory.Click += new System.EventHandler(this.btnViewHistory_Click);
            // 
            // btnAddPoints
            // 
            this.btnAddPoints.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnAddPoints.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPoints.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAddPoints.ForeColor = System.Drawing.Color.White;
            this.btnAddPoints.Location = new System.Drawing.Point(280, 45);
            this.btnAddPoints.Name = "btnAddPoints";
            this.btnAddPoints.Size = new System.Drawing.Size(220, 35);
            this.btnAddPoints.TabIndex = 3;
            this.btnAddPoints.Text = "Cộng điểm";
            this.btnAddPoints.UseVisualStyleBackColor = false;
            this.btnAddPoints.Click += new System.EventHandler(this.btnAddPoints_Click);
            // 
            // txtAddPoints
            // 
            this.txtAddPoints.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtAddPoints.Location = new System.Drawing.Point(140, 50);
            this.txtAddPoints.Name = "txtAddPoints";
            this.txtAddPoints.Size = new System.Drawing.Size(120, 25);
            this.txtAddPoints.TabIndex = 2;
            this.txtAddPoints.Text = "0";
            this.txtAddPoints.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label8.Location = new System.Drawing.Point(20, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(125, 17);
            this.label8.TabIndex = 1;
            this.label8.Text = "Số điểm cộng thêm:";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(20, 110);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(240, 40);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Cập nhật thông tin thẻ";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Controls.Add(this.dtpIssueDate);
            this.groupBoxInfo.Controls.Add(this.label7);
            this.groupBoxInfo.Controls.Add(this.cmbRank);
            this.groupBoxInfo.Controls.Add(this.label6);
            this.groupBoxInfo.Controls.Add(this.txtPoints);
            this.groupBoxInfo.Controls.Add(this.label5);
            this.groupBoxInfo.Controls.Add(this.txtPhone);
            this.groupBoxInfo.Controls.Add(this.label4);
            this.groupBoxInfo.Controls.Add(this.txtCustomerName);
            this.groupBoxInfo.Controls.Add(this.label3);
            this.groupBoxInfo.Controls.Add(this.txtCustomerId);
            this.groupBoxInfo.Controls.Add(this.label2);
            this.groupBoxInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxInfo.Location = new System.Drawing.Point(10, 10);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxInfo.Size = new System.Drawing.Size(526, 360);
            this.groupBoxInfo.TabIndex = 0;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "Thông tin thẻ thành viên";
            // 
            // dtpIssueDate
            // 
            this.dtpIssueDate.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtpIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIssueDate.Location = new System.Drawing.Point(150, 305);
            this.dtpIssueDate.Name = "dtpIssueDate";
            this.dtpIssueDate.Size = new System.Drawing.Size(350, 25);
            this.dtpIssueDate.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label7.Location = new System.Drawing.Point(20, 308);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "Ngày cấp:";
            // 
            // cmbRank
            // 
            this.cmbRank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRank.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbRank.FormattingEnabled = true;
            this.cmbRank.Location = new System.Drawing.Point(150, 260);
            this.cmbRank.Name = "cmbRank";
            this.cmbRank.Size = new System.Drawing.Size(350, 25);
            this.cmbRank.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label6.Location = new System.Drawing.Point(20, 263);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "Hạng thẻ:";
            // 
            // txtPoints
            // 
            this.txtPoints.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtPoints.Location = new System.Drawing.Point(150, 215);
            this.txtPoints.Name = "txtPoints";
            this.txtPoints.ReadOnly = true;
            this.txtPoints.Size = new System.Drawing.Size(350, 25);
            this.txtPoints.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label5.Location = new System.Drawing.Point(20, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Điểm tích lũy:";
            // 
            // txtPhone
            // 
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtPhone.Location = new System.Drawing.Point(150, 170);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.ReadOnly = true;
            this.txtPhone.Size = new System.Drawing.Size(350, 25);
            this.txtPhone.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label4.Location = new System.Drawing.Point(20, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Số điện thoại:";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtCustomerName.Location = new System.Drawing.Point(150, 125);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(350, 25);
            this.txtCustomerName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label3.Location = new System.Drawing.Point(20, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tên khách hàng:";
            // 
            // txtCustomerId
            // 
            this.txtCustomerId.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtCustomerId.Location = new System.Drawing.Point(150, 80);
            this.txtCustomerId.Name = "txtCustomerId";
            this.txtCustomerId.ReadOnly = true;
            this.txtCustomerId.Size = new System.Drawing.Size(350, 25);
            this.txtCustomerId.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label2.Location = new System.Drawing.Point(20, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mã khách hàng:";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 680);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(10);
            this.pnlBottom.Size = new System.Drawing.Size(1400, 60);
            this.pnlBottom.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1270, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 35);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // colCustomerId
            // 
            this.colCustomerId.DataPropertyName = "id";
            this.colCustomerId.HeaderText = "Mã KH";
            this.colCustomerId.Name = "colCustomerId";
            this.colCustomerId.ReadOnly = true;
            // 
            // colCustomerName
            // 
            this.colCustomerName.DataPropertyName = "hoTen";
            this.colCustomerName.HeaderText = "Tên khách hàng";
            this.colCustomerName.Name = "colCustomerName";
            this.colCustomerName.ReadOnly = true;
            // 
            // colPhone
            // 
            this.colPhone.DataPropertyName = "soDienThoai";
            this.colPhone.HeaderText = "Số điện thoại";
            this.colPhone.Name = "colPhone";
            this.colPhone.ReadOnly = true;
            // 
            // colRank
            // 
            this.colRank.DataPropertyName = "hang";
            this.colRank.HeaderText = "Hạng thẻ";
            this.colRank.Name = "colRank";
            this.colRank.ReadOnly = true;
            // 
            // colPoints
            // 
            this.colPoints.DataPropertyName = "diemTichLuy";
            this.colPoints.HeaderText = "Điểm tích lũy";
            this.colPoints.Name = "colPoints";
            this.colPoints.ReadOnly = true;
            // 
            // colIssueDate
            // 
            this.colIssueDate.DataPropertyName = "ngayCap";
            this.colIssueDate.HeaderText = "Ngày cấp";
            this.colIssueDate.Name = "colIssueDate";
            this.colIssueDate.ReadOnly = true;
            // 
            // frmMemberCards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 740);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmMemberCards";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý thẻ thành viên";
            this.Load += new System.EventHandler(this.frmMemberCards_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMemberCards)).EndInit();
            this.pnlRight.ResumeLayout(false);
            this.groupBoxActions.ResumeLayout(false);
            this.groupBoxActions.PerformLayout();
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ComboBox cmbFilterRank;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.DataGridView dgvMemberCards;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCustomerId;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPoints;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbRank;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpIssueDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBoxActions;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtAddPoints;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnAddPoints;
        private System.Windows.Forms.Button btnViewHistory;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRank;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIssueDate;
    }
}