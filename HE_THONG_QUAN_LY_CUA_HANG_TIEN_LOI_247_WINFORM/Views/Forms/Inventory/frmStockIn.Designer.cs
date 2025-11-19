namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.Forms.Inventory
{
    partial class frmStockIn
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            this.lblSection1 = new System.Windows.Forms.Label();
            this.pnlInfo = new Guna.UI2.WinForms.Guna2Panel();
            this.cboStaff = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboSupplier = new Guna.UI2.WinForms.Guna2ComboBox();
            this.dtpImportDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSection2 = new System.Windows.Forms.Label();
            this.pnlInput = new Guna.UI2.WinForms.Guna2Panel();
            this.dtpExpiryDate = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.cboProduct = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnAddProduct = new Guna.UI2.WinForms.Guna2Button();
            this.txtPrice = new Guna.UI2.WinForms.Guna2TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtQuantity = new Guna.UI2.WinForms.Guna2TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvStockInDetail = new Guna.UI2.WinForms.Guna2DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colExpiry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pnlFooter = new Guna.UI2.WinForms.Guna2Panel();
            this.btnImportExcel = new Guna.UI2.WinForms.Guna2Button();
            this.btnChooseFile = new Guna.UI2.WinForms.Guna2Button();
            this.lblFileName = new System.Windows.Forms.Label();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.pnlInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockInDetail)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.lblSection1);
            this.pnlMain.Controls.Add(this.pnlInfo);
            this.pnlMain.Controls.Add(this.lblSection2);
            this.pnlMain.Controls.Add(this.pnlInput);
            this.pnlMain.Controls.Add(this.dgvStockInDetail);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Size = new System.Drawing.Size(1000, 600);
            this.pnlMain.TabIndex = 5;
            // 
            // lblSection1
            // 
            this.lblSection1.AutoSize = true;
            this.lblSection1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSection1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.lblSection1.Location = new System.Drawing.Point(15, 10);
            this.lblSection1.Name = "lblSection1";
            this.lblSection1.Size = new System.Drawing.Size(233, 30);
            this.lblSection1.TabIndex = 0;
            this.lblSection1.Text = "Thông tin Phiếu Nhập";
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.cboStaff);
            this.pnlInfo.Controls.Add(this.label8);
            this.pnlInfo.Controls.Add(this.cboSupplier);
            this.pnlInfo.Controls.Add(this.dtpImportDate);
            this.pnlInfo.Controls.Add(this.label3);
            this.pnlInfo.Controls.Add(this.label2);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInfo.Location = new System.Drawing.Point(20, 40);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(960, 85);
            this.pnlInfo.TabIndex = 1;
            // 
            // cboStaff
            // 
            this.cboStaff.BackColor = System.Drawing.Color.Transparent;
            this.cboStaff.BorderRadius = 4;
            this.cboStaff.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboStaff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStaff.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboStaff.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboStaff.FocusedState.Parent = this.cboStaff;
            this.cboStaff.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboStaff.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboStaff.HoverState.Parent = this.cboStaff;
            this.cboStaff.ItemHeight = 30;
            this.cboStaff.ItemsAppearance.Parent = this.cboStaff;
            this.cboStaff.Location = new System.Drawing.Point(340, 35);
            this.cboStaff.Name = "cboStaff";
            this.cboStaff.ShadowDecoration.Parent = this.cboStaff;
            this.cboStaff.Size = new System.Drawing.Size(300, 36);
            this.cboStaff.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.DimGray;
            this.label8.Location = new System.Drawing.Point(337, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 17);
            this.label8.TabIndex = 2;
            this.label8.Text = "Nhân viên nhập";
            // 
            // cboSupplier
            // 
            this.cboSupplier.BackColor = System.Drawing.Color.Transparent;
            this.cboSupplier.BorderRadius = 4;
            this.cboSupplier.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSupplier.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboSupplier.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboSupplier.FocusedState.Parent = this.cboSupplier;
            this.cboSupplier.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboSupplier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboSupplier.HoverState.Parent = this.cboSupplier;
            this.cboSupplier.ItemHeight = 30;
            this.cboSupplier.ItemsAppearance.Parent = this.cboSupplier;
            this.cboSupplier.Location = new System.Drawing.Point(0, 35);
            this.cboSupplier.Name = "cboSupplier";
            this.cboSupplier.ShadowDecoration.Parent = this.cboSupplier;
            this.cboSupplier.Size = new System.Drawing.Size(320, 36);
            this.cboSupplier.TabIndex = 1;
            // 
            // dtpImportDate
            // 
            this.dtpImportDate.BorderRadius = 4;
            this.dtpImportDate.CheckedState.Parent = this.dtpImportDate;
            this.dtpImportDate.FillColor = System.Drawing.Color.White;
            this.dtpImportDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpImportDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpImportDate.HoverState.Parent = this.dtpImportDate;
            this.dtpImportDate.Location = new System.Drawing.Point(660, 35);
            this.dtpImportDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpImportDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpImportDate.Name = "dtpImportDate";
            this.dtpImportDate.ShadowDecoration.Parent = this.dtpImportDate;
            this.dtpImportDate.Size = new System.Drawing.Size(300, 36);
            this.dtpImportDate.TabIndex = 5;
            this.dtpImportDate.Value = new System.DateTime(2023, 11, 19, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(657, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ngày nhập";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(-3, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Nhà cung cấp";
            // 
            // lblSection2
            // 
            this.lblSection2.AutoSize = true;
            this.lblSection2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSection2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.lblSection2.Location = new System.Drawing.Point(15, 140);
            this.lblSection2.Name = "lblSection2";
            this.lblSection2.Size = new System.Drawing.Size(246, 30);
            this.lblSection2.TabIndex = 2;
            this.lblSection2.Text = "Chi Tiết Hàng Hoá Nhập";
            // 
            // pnlInput
            // 
            this.pnlInput.Controls.Add(this.dtpExpiryDate);
            this.pnlInput.Controls.Add(this.label9);
            this.pnlInput.Controls.Add(this.cboProduct);
            this.pnlInput.Controls.Add(this.btnAddProduct);
            this.pnlInput.Controls.Add(this.txtPrice);
            this.pnlInput.Controls.Add(this.label6);
            this.pnlInput.Controls.Add(this.txtQuantity);
            this.pnlInput.Controls.Add(this.label5);
            this.pnlInput.Controls.Add(this.label4);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInput.Location = new System.Drawing.Point(20, 125);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(960, 100);
            this.pnlInput.TabIndex = 3;
            // 
            // dtpExpiryDate
            // 
            this.dtpExpiryDate.BorderRadius = 4;
            this.dtpExpiryDate.CheckedState.Parent = this.dtpExpiryDate;
            this.dtpExpiryDate.FillColor = System.Drawing.Color.White;
            this.dtpExpiryDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiryDate.HoverState.Parent = this.dtpExpiryDate;
            this.dtpExpiryDate.Location = new System.Drawing.Point(660, 60);
            this.dtpExpiryDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpExpiryDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpExpiryDate.Name = "dtpExpiryDate";
            this.dtpExpiryDate.ShadowDecoration.Parent = this.dtpExpiryDate;
            this.dtpExpiryDate.Size = new System.Drawing.Size(150, 36);
            this.dtpExpiryDate.TabIndex = 7;
            this.dtpExpiryDate.Value = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.DimGray;
            this.label9.Location = new System.Drawing.Point(657, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 17);
            this.label9.TabIndex = 6;
            this.label9.Text = "Hạn sử dụng";
            // 
            // cboProduct
            // 
            this.cboProduct.BackColor = System.Drawing.Color.Transparent;
            this.cboProduct.BorderRadius = 4;
            this.cboProduct.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProduct.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboProduct.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboProduct.FocusedState.Parent = this.cboProduct;
            this.cboProduct.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboProduct.HoverState.Parent = this.cboProduct;
            this.cboProduct.ItemHeight = 30;
            this.cboProduct.ItemsAppearance.Parent = this.cboProduct;
            this.cboProduct.Location = new System.Drawing.Point(0, 60);
            this.cboProduct.Name = "cboProduct";
            this.cboProduct.ShadowDecoration.Parent = this.cboProduct;
            this.cboProduct.Size = new System.Drawing.Size(320, 36);
            this.cboProduct.TabIndex = 1;
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.BorderRadius = 4;
            this.btnAddProduct.CheckedState.Parent = this.btnAddProduct;
            this.btnAddProduct.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddProduct.CustomImages.Parent = this.btnAddProduct;
            this.btnAddProduct.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(183)))), ((int)(((byte)(255)))));
            this.btnAddProduct.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddProduct.ForeColor = System.Drawing.Color.White;
            this.btnAddProduct.HoverState.Parent = this.btnAddProduct;
            this.btnAddProduct.Location = new System.Drawing.Point(825, 60);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.ShadowDecoration.Parent = this.btnAddProduct;
            this.btnAddProduct.Size = new System.Drawing.Size(135, 36);
            this.btnAddProduct.TabIndex = 8;
            this.btnAddProduct.Text = "+ Thêm vào phiếu";
            // 
            // txtPrice
            // 
            this.txtPrice.BorderRadius = 4;
            this.txtPrice.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPrice.DefaultText = "";
            this.txtPrice.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtPrice.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtPrice.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPrice.DisabledState.Parent = this.txtPrice;
            this.txtPrice.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPrice.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPrice.FocusedState.Parent = this.txtPrice;
            this.txtPrice.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPrice.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPrice.HoverState.Parent = this.txtPrice;
            this.txtPrice.Location = new System.Drawing.Point(500, 60);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.PasswordChar = '\0';
            this.txtPrice.PlaceholderText = "";
            this.txtPrice.SelectedText = "";
            this.txtPrice.ShadowDecoration.Parent = this.txtPrice;
            this.txtPrice.Size = new System.Drawing.Size(150, 36);
            this.txtPrice.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(497, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "Đơn giá nhập";
            // 
            // txtQuantity
            // 
            this.txtQuantity.BorderRadius = 4;
            this.txtQuantity.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtQuantity.DefaultText = "1";
            this.txtQuantity.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtQuantity.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtQuantity.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtQuantity.DisabledState.Parent = this.txtQuantity;
            this.txtQuantity.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtQuantity.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtQuantity.FocusedState.Parent = this.txtQuantity;
            this.txtQuantity.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtQuantity.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtQuantity.HoverState.Parent = this.txtQuantity;
            this.txtQuantity.Location = new System.Drawing.Point(340, 60);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.PasswordChar = '\0';
            this.txtQuantity.PlaceholderText = "";
            this.txtQuantity.SelectedText = "";
            this.txtQuantity.SelectionStart = 1;
            this.txtQuantity.ShadowDecoration.Parent = this.txtQuantity;
            this.txtQuantity.Size = new System.Drawing.Size(150, 36);
            this.txtQuantity.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(337, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Số lượng";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(-3, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Tìm sản phẩm";
            // 
            // dgvStockInDetail
            // 
            this.dgvStockInDetail.AllowUserToAddRows = false;
            this.dgvStockInDetail.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvStockInDetail.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvStockInDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStockInDetail.BackgroundColor = System.Drawing.Color.White;
            this.dgvStockInDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvStockInDetail.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvStockInDetail.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStockInDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvStockInDetail.ColumnHeadersHeight = 40;
            this.dgvStockInDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colQty,
            this.colPrice,
            this.colExpiry,
            this.colTotal,
            this.colDelete});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStockInDetail.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvStockInDetail.EnableHeadersVisualStyles = false;
            this.dgvStockInDetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.dgvStockInDetail.Location = new System.Drawing.Point(20, 231);
            this.dgvStockInDetail.Name = "dgvStockInDetail";
            this.dgvStockInDetail.ReadOnly = true;
            this.dgvStockInDetail.RowHeadersVisible = false;
            this.dgvStockInDetail.RowTemplate.Height = 40;
            this.dgvStockInDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStockInDetail.Size = new System.Drawing.Size(960, 250);
            this.dgvStockInDetail.TabIndex = 4;
            this.dgvStockInDetail.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Light;
            this.dgvStockInDetail.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvStockInDetail.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvStockInDetail.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvStockInDetail.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvStockInDetail.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvStockInDetail.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvStockInDetail.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.dgvStockInDetail.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.White;
            this.dgvStockInDetail.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvStockInDetail.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvStockInDetail.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvStockInDetail.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvStockInDetail.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvStockInDetail.ThemeStyle.ReadOnly = true;
            this.dgvStockInDetail.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvStockInDetail.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvStockInDetail.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dgvStockInDetail.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvStockInDetail.ThemeStyle.RowsStyle.Height = 40;
            this.dgvStockInDetail.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(241)))), ((int)(((byte)(243)))));
            this.dgvStockInDetail.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            // 
            // colName
            // 
            this.colName.HeaderText = "Sản phẩm";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colQty
            // 
            this.colQty.HeaderText = "Số lượng";
            this.colQty.Name = "colQty";
            this.colQty.ReadOnly = true;
            // 
            // colPrice
            // 
            this.colPrice.HeaderText = "Đơn giá nhập";
            this.colPrice.Name = "colPrice";
            this.colPrice.ReadOnly = true;
            // 
            // colExpiry
            // 
            this.colExpiry.HeaderText = "Hạn sử dụng";
            this.colExpiry.Name = "colExpiry";
            this.colExpiry.ReadOnly = true;
            // 
            // colTotal
            // 
            this.colTotal.HeaderText = "Thành tiền";
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;
            // 
            // colDelete
            // 
            this.colDelete.HeaderText = "Xoá";
            this.colDelete.Name = "colDelete";
            this.colDelete.ReadOnly = true;
            this.colDelete.Text = "Xoá";
            this.colDelete.UseColumnTextForButtonValue = true;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Controls.Add(this.btnImportExcel);
            this.pnlFooter.Controls.Add(this.btnChooseFile);
            this.pnlFooter.Controls.Add(this.lblFileName);
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Controls.Add(this.lblTotalAmount);
            this.pnlFooter.Controls.Add(this.label7);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 520);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.ShadowDecoration.Color = System.Drawing.Color.Silver;
            this.pnlFooter.ShadowDecoration.Depth = 5;
            this.pnlFooter.ShadowDecoration.Enabled = true;
            this.pnlFooter.ShadowDecoration.Parent = this.pnlFooter;
            this.pnlFooter.Size = new System.Drawing.Size(1000, 80);
            this.pnlFooter.TabIndex = 4;
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.BorderRadius = 4;
            this.btnImportExcel.CheckedState.Parent = this.btnImportExcel;
            this.btnImportExcel.CustomImages.Parent = this.btnImportExcel;
            this.btnImportExcel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnImportExcel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnImportExcel.ForeColor = System.Drawing.Color.White;
            this.btnImportExcel.HoverState.Parent = this.btnImportExcel;
            this.btnImportExcel.Location = new System.Drawing.Point(157, 35);
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.ShadowDecoration.Parent = this.btnImportExcel;
            this.btnImportExcel.Size = new System.Drawing.Size(120, 35);
            this.btnImportExcel.TabIndex = 6;
            this.btnImportExcel.Text = "Import từ Excel";
            // 
            // btnChooseFile
            // 
            this.btnChooseFile.BorderColor = System.Drawing.Color.Gray;
            this.btnChooseFile.BorderRadius = 4;
            this.btnChooseFile.BorderThickness = 1;
            this.btnChooseFile.CheckedState.Parent = this.btnChooseFile;
            this.btnChooseFile.CustomImages.Parent = this.btnChooseFile;
            this.btnChooseFile.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnChooseFile.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnChooseFile.ForeColor = System.Drawing.Color.Black;
            this.btnChooseFile.HoverState.Parent = this.btnChooseFile;
            this.btnChooseFile.Location = new System.Drawing.Point(20, 35);
            this.btnChooseFile.Name = "btnChooseFile";
            this.btnChooseFile.ShadowDecoration.Parent = this.btnChooseFile;
            this.btnChooseFile.Size = new System.Drawing.Size(80, 35);
            this.btnChooseFile.TabIndex = 5;
            this.btnChooseFile.Text = "Choose File";
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFileName.ForeColor = System.Drawing.Color.Gray;
            this.lblFileName.Location = new System.Drawing.Point(105, 45);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(81, 15);
            this.lblFileName.TabIndex = 4;
            this.lblFileName.Text = "No file chosen";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BorderRadius = 4;
            this.btnCancel.CheckedState.Parent = this.btnCancel;
            this.btnCancel.CustomImages.Parent = this.btnCancel;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.HoverState.Parent = this.btnCancel;
            this.btnCancel.Location = new System.Drawing.Point(770, 35);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.ShadowDecoration.Parent = this.btnCancel;
            this.btnCancel.Size = new System.Drawing.Size(80, 35);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Huỷ";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BorderRadius = 4;
            this.btnSave.CheckedState.Parent = this.btnSave;
            this.btnSave.CustomImages.Parent = this.btnSave;
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(183)))), ((int)(((byte)(255)))));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.HoverState.Parent = this.btnSave;
            this.btnSave.Location = new System.Drawing.Point(860, 35);
            this.btnSave.Name = "btnSave";
            this.btnSave.ShadowDecoration.Parent = this.btnSave;
            this.btnSave.Size = new System.Drawing.Size(120, 35);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Lưu Phiếu Nhập";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblTotalAmount.Location = new System.Drawing.Point(930, 5);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(43, 30);
            this.lblTotalAmount.TabIndex = 1;
            this.lblTotalAmount.Text = "0 đ";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(820, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 25);
            this.label7.TabIndex = 0;
            this.label7.Text = "Tổng Tiền:";
            // 
            // frmStockIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmStockIn";
            this.Text = "frmStockIn";
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            this.pnlInput.ResumeLayout(false);
            this.pnlInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockInDetail)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private System.Windows.Forms.Label lblSection1;
        private Guna.UI2.WinForms.Guna2Panel pnlInfo;
        private Guna.UI2.WinForms.Guna2ComboBox cboStaff;
        private System.Windows.Forms.Label label8;
        private Guna.UI2.WinForms.Guna2ComboBox cboSupplier;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpImportDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSection2;
        private Guna.UI2.WinForms.Guna2Panel pnlInput;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpExpiryDate;
        private System.Windows.Forms.Label label9;
        private Guna.UI2.WinForms.Guna2ComboBox cboProduct;
        private Guna.UI2.WinForms.Guna2Button btnAddProduct;
        private Guna.UI2.WinForms.Guna2TextBox txtPrice;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2TextBox txtQuantity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2DataGridView dgvStockInDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colExpiry;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.DataGridViewButtonColumn colDelete;
        private Guna.UI2.WinForms.Guna2Panel pnlFooter;
        private Guna.UI2.WinForms.Guna2Button btnImportExcel;
        private Guna.UI2.WinForms.Guna2Button btnChooseFile;
        private System.Windows.Forms.Label lblFileName;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label label7;
    }
}