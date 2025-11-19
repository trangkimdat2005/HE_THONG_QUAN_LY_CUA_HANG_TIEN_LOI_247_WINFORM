namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Suppliers
{
    partial class frmSupplierList
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
            this.panelSearch = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.dgvSuppliers = new System.Windows.Forms.DataGridView();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.groupBoxDetail = new System.Windows.Forms.GroupBox();
            this.lblReceiptCount = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtTaxCode = new System.Windows.Forms.TextBox();
            this.lblTaxCode = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.txtSupplierId = new System.Windows.Forms.TextBox();
            this.lblSupplierName = new System.Windows.Forms.Label();
            this.lblSupplierId = new System.Windows.Forms.Label();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTaxCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelTop.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelContainer.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuppliers)).BeginInit();
            this.panelDetail.SuspendLayout();
            this.groupBoxDetail.SuspendLayout();
            this.panelStatus.SuspendLayout();
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
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(308, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ NHÀ CUNG CẤP";
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.White;
            this.panelSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSearch.Controls.Add(this.btnRefresh);
            this.panelSearch.Controls.Add(this.btnSearch);
            this.panelSearch.Controls.Add(this.txtSearch);
            this.panelSearch.Controls.Add(this.lblSearch);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 60);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Padding = new System.Windows.Forms.Padding(10);
            this.panelSearch.Size = new System.Drawing.Size(1200, 70);
            this.panelSearch.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(750, 20);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 35);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(630, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 35);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(180, 25);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(420, 25);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblSearch.Location = new System.Drawing.Point(15, 28);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(120, 17);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Tên nhà cung cấp:";
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.White;
            this.panelButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelButtons.Controls.Add(this.btnExport);
            this.panelButtons.Controls.Add(this.btnDelete);
            this.panelButtons.Controls.Add(this.btnEdit);
            this.panelButtons.Controls.Add(this.btnAdd);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelButtons.Location = new System.Drawing.Point(0, 130);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(1200, 60);
            this.panelButtons.TabIndex = 2;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(390, 10);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(120, 40);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Xuất Excel";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(270, 10);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(110, 40);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(140, 10);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(120, 40);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Chỉnh sửa";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(10, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 40);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Thêm mới";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.panelGrid);
            this.panelContainer.Controls.Add(this.panelDetail);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 190);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(1200, 490);
            this.panelContainer.TabIndex = 3;
            // 
            // panelGrid
            // 
            this.panelGrid.Controls.Add(this.dgvSuppliers);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(0, 0);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Padding = new System.Windows.Forms.Padding(20);
            this.panelGrid.Size = new System.Drawing.Size(849, 490);
            this.panelGrid.TabIndex = 0;
            // 
            // dgvSuppliers
            // 
            this.dgvSuppliers.AllowUserToAddRows = false;
            this.dgvSuppliers.AllowUserToDeleteRows = false;
            this.dgvSuppliers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSuppliers.BackgroundColor = System.Drawing.Color.White;
            this.dgvSuppliers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSuppliers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSuppliers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSuppliers.ColumnHeadersHeight = 40;
            this.dgvSuppliers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colName,
            this.colPhone,
            this.colEmail,
            this.colAddress,
            this.colTaxCode,
            this.colStatus});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(233)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSuppliers.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSuppliers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSuppliers.EnableHeadersVisualStyles = false;
            this.dgvSuppliers.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.dgvSuppliers.Location = new System.Drawing.Point(20, 20);
            this.dgvSuppliers.MultiSelect = false;
            this.dgvSuppliers.Name = "dgvSuppliers";
            this.dgvSuppliers.ReadOnly = true;
            this.dgvSuppliers.RowHeadersVisible = false;
            this.dgvSuppliers.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.dgvSuppliers.RowTemplate.Height = 40;
            this.dgvSuppliers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSuppliers.Size = new System.Drawing.Size(809, 450);
            this.dgvSuppliers.TabIndex = 0;
            this.dgvSuppliers.SelectionChanged += new System.EventHandler(this.dgvSuppliers_SelectionChanged);
            // 
            // panelDetail
            // 
            this.panelDetail.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelDetail.Controls.Add(this.groupBoxDetail);
            this.panelDetail.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelDetail.Location = new System.Drawing.Point(849, 0);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Padding = new System.Windows.Forms.Padding(10);
            this.panelDetail.Size = new System.Drawing.Size(351, 490);
            this.panelDetail.TabIndex = 1;
            // 
            // groupBoxDetail
            // 
            this.groupBoxDetail.Controls.Add(this.lblReceiptCount);
            this.groupBoxDetail.Controls.Add(this.btnCancel);
            this.groupBoxDetail.Controls.Add(this.btnSave);
            this.groupBoxDetail.Controls.Add(this.txtTaxCode);
            this.groupBoxDetail.Controls.Add(this.lblTaxCode);
            this.groupBoxDetail.Controls.Add(this.txtAddress);
            this.groupBoxDetail.Controls.Add(this.lblAddress);
            this.groupBoxDetail.Controls.Add(this.txtEmail);
            this.groupBoxDetail.Controls.Add(this.lblEmail);
            this.groupBoxDetail.Controls.Add(this.txtPhone);
            this.groupBoxDetail.Controls.Add(this.lblPhone);
            this.groupBoxDetail.Controls.Add(this.txtSupplierName);
            this.groupBoxDetail.Controls.Add(this.txtSupplierId);
            this.groupBoxDetail.Controls.Add(this.lblSupplierName);
            this.groupBoxDetail.Controls.Add(this.lblSupplierId);
            this.groupBoxDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDetail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxDetail.Location = new System.Drawing.Point(10, 10);
            this.groupBoxDetail.Name = "groupBoxDetail";
            this.groupBoxDetail.Size = new System.Drawing.Size(331, 470);
            this.groupBoxDetail.TabIndex = 0;
            this.groupBoxDetail.TabStop = false;
            this.groupBoxDetail.Text = "Chi tiết nhà cung cấp";
            // 
            // lblReceiptCount
            // 
            this.lblReceiptCount.AutoSize = true;
            this.lblReceiptCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblReceiptCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblReceiptCount.Location = new System.Drawing.Point(20, 350);
            this.lblReceiptCount.Name = "lblReceiptCount";
            this.lblReceiptCount.Size = new System.Drawing.Size(112, 17);
            this.lblReceiptCount.TabIndex = 14;
            this.lblReceiptCount.Text = "Số phiếu nhập: 0";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(210, 390);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 40);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(60, 390);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(130, 40);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtTaxCode
            // 
            this.txtTaxCode.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtTaxCode.Location = new System.Drawing.Point(129, 300);
            this.txtTaxCode.Name = "txtTaxCode";
            this.txtTaxCode.Size = new System.Drawing.Size(191, 25);
            this.txtTaxCode.TabIndex = 11;
            // 
            // lblTaxCode
            // 
            this.lblTaxCode.AutoSize = true;
            this.lblTaxCode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTaxCode.Location = new System.Drawing.Point(7, 303);
            this.lblTaxCode.Name = "lblTaxCode";
            this.lblTaxCode.Size = new System.Drawing.Size(81, 17);
            this.lblTaxCode.TabIndex = 10;
            this.lblTaxCode.Text = "Mã số thuế:";
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtAddress.Location = new System.Drawing.Point(129, 230);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(191, 50);
            this.txtAddress.TabIndex = 9;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblAddress.Location = new System.Drawing.Point(6, 233);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(55, 17);
            this.lblAddress.TabIndex = 8;
            this.lblAddress.Text = "Địa chỉ:";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtEmail.Location = new System.Drawing.Point(129, 185);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(191, 25);
            this.txtEmail.TabIndex = 7;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblEmail.Location = new System.Drawing.Point(6, 188);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(46, 17);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email:";
            // 
            // txtPhone
            // 
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtPhone.Location = new System.Drawing.Point(129, 140);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(191, 25);
            this.txtPhone.TabIndex = 5;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblPhone.Location = new System.Drawing.Point(7, 143);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(94, 17);
            this.lblPhone.TabIndex = 4;
            this.lblPhone.Text = "Số điện thoại:";
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtSupplierName.Location = new System.Drawing.Point(129, 95);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.Size = new System.Drawing.Size(191, 25);
            this.txtSupplierName.TabIndex = 3;
            this.txtSupplierName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSupplierName_KeyPress);
            // 
            // txtSupplierId
            // 
            this.txtSupplierId.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtSupplierId.Location = new System.Drawing.Point(129, 50);
            this.txtSupplierId.Name = "txtSupplierId";
            this.txtSupplierId.ReadOnly = true;
            this.txtSupplierId.Size = new System.Drawing.Size(191, 25);
            this.txtSupplierId.TabIndex = 2;
            // 
            // lblSupplierName
            // 
            this.lblSupplierName.AutoSize = true;
            this.lblSupplierName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblSupplierName.Location = new System.Drawing.Point(6, 98);
            this.lblSupplierName.Name = "lblSupplierName";
            this.lblSupplierName.Size = new System.Drawing.Size(120, 17);
            this.lblSupplierName.TabIndex = 1;
            this.lblSupplierName.Text = "Tên nhà cung cấp:";
            // 
            // lblSupplierId
            // 
            this.lblSupplierId.AutoSize = true;
            this.lblSupplierId.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblSupplierId.Location = new System.Drawing.Point(6, 53);
            this.lblSupplierId.Name = "lblSupplierId";
            this.lblSupplierId.Size = new System.Drawing.Size(117, 17);
            this.lblSupplierId.TabIndex = 0;
            this.lblSupplierId.Text = "Mã nhà cung cấp:";
            // 
            // panelStatus
            // 
            this.panelStatus.BackColor = System.Drawing.Color.White;
            this.panelStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStatus.Controls.Add(this.lblStatus);
            this.panelStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatus.Location = new System.Drawing.Point(0, 680);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(1200, 35);
            this.panelStatus.TabIndex = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(10, 8);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(73, 17);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Tổng số: 0";
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "TrangThai";
            this.colStatus.HeaderText = "Trạng thái";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // colTaxCode
            // 
            this.colTaxCode.DataPropertyName = "maSoThue";
            this.colTaxCode.HeaderText = "Mã số thuế";
            this.colTaxCode.Name = "colTaxCode";
            this.colTaxCode.ReadOnly = true;
            // 
            // colAddress
            // 
            this.colAddress.DataPropertyName = "diaChi";
            this.colAddress.HeaderText = "Địa chỉ";
            this.colAddress.Name = "colAddress";
            this.colAddress.ReadOnly = true;
            // 
            // colEmail
            // 
            this.colEmail.DataPropertyName = "email";
            this.colEmail.HeaderText = "Email";
            this.colEmail.Name = "colEmail";
            this.colEmail.ReadOnly = true;
            // 
            // colPhone
            // 
            this.colPhone.DataPropertyName = "soDienThoai";
            this.colPhone.HeaderText = "Số điện thoại";
            this.colPhone.Name = "colPhone";
            this.colPhone.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colName.DataPropertyName = "ten";
            this.colName.HeaderText = "Tên nhà cung cấp";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colId
            // 
            this.colId.DataPropertyName = "id";
            this.colId.HeaderText = "Mã NCC";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            // 
            // frmSupplierList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 715);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSupplierList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý nhà cung cấp";
            this.Load += new System.EventHandler(this.frmSupplierList_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelContainer.ResumeLayout(false);
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuppliers)).EndInit();
            this.panelDetail.ResumeLayout(false);
            this.groupBoxDetail.ResumeLayout(false);
            this.groupBoxDetail.PerformLayout();
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.DataGridView dgvSuppliers;
        private System.Windows.Forms.Panel panelDetail;
        private System.Windows.Forms.GroupBox groupBoxDetail;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtSupplierName;
        private System.Windows.Forms.TextBox txtSupplierId;
        private System.Windows.Forms.Label lblSupplierName;
        private System.Windows.Forms.Label lblSupplierId;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblReceiptCount;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtTaxCode;
        private System.Windows.Forms.Label lblTaxCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTaxCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
    }
}