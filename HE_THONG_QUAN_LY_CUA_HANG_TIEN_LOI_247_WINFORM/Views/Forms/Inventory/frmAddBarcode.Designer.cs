namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Inventory
{
    partial class frmAddBarcode
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtImagePath = new System.Windows.Forms.TextBox();
            this.lblImagePath = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.txtBarcodeCode = new System.Windows.Forms.TextBox();
            this.lblBarcodeCode = new System.Windows.Forms.Label();
            this.cboCodeType = new System.Windows.Forms.ComboBox();
            this.lblCodeType = new System.Windows.Forms.Label();
            this.cboProductUnit = new System.Windows.Forms.ComboBox();
            this.lblProductUnit = new System.Windows.Forms.Label();
            this.txtBarcodeId = new System.Windows.Forms.TextBox();
            this.lblBarcodeId = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.picBarcode = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcode)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(800, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "THÊM BARCODE MỚI";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 60);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(800, 490);
            this.panel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtImagePath);
            this.groupBox1.Controls.Add(this.lblImagePath);
            this.groupBox1.Controls.Add(this.btnGenerate);
            this.groupBox1.Controls.Add(this.txtBarcodeCode);
            this.groupBox1.Controls.Add(this.lblBarcodeCode);
            this.groupBox1.Controls.Add(this.cboCodeType);
            this.groupBox1.Controls.Add(this.lblCodeType);
            this.groupBox1.Controls.Add(this.cboProductUnit);
            this.groupBox1.Controls.Add(this.lblProductUnit);
            this.groupBox1.Controls.Add(this.txtBarcodeId);
            this.groupBox1.Controls.Add(this.lblBarcodeId);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox1.Size = new System.Drawing.Size(400, 470);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin Barcode";
            // 
            // txtImagePath
            // 
            this.txtImagePath.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtImagePath.Location = new System.Drawing.Point(20, 335);
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.Size = new System.Drawing.Size(360, 25);
            this.txtImagePath.TabIndex = 10;
            // 
            // lblImagePath
            // 
            this.lblImagePath.AutoSize = true;
            this.lblImagePath.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblImagePath.Location = new System.Drawing.Point(20, 310);
            this.lblImagePath.Name = "lblImagePath";
            this.lblImagePath.Size = new System.Drawing.Size(123, 19);
            this.lblImagePath.TabIndex = 9;
            this.lblImagePath.Text = "Đường dẫn lưu ảnh:";
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.Location = new System.Drawing.Point(20, 380);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(360, 40);
            this.btnGenerate.TabIndex = 11;
            this.btnGenerate.Text = "Tạo Barcode";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // txtBarcodeCode
            // 
            this.txtBarcodeCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBarcodeCode.Location = new System.Drawing.Point(20, 270);
            this.txtBarcodeCode.Name = "txtBarcodeCode";
            this.txtBarcodeCode.Size = new System.Drawing.Size(360, 25);
            this.txtBarcodeCode.TabIndex = 8;
            // 
            // lblBarcodeCode
            // 
            this.lblBarcodeCode.AutoSize = true;
            this.lblBarcodeCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBarcodeCode.Location = new System.Drawing.Point(20, 245);
            this.lblBarcodeCode.Name = "lblBarcodeCode";
            this.lblBarcodeCode.Size = new System.Drawing.Size(95, 19);
            this.lblBarcodeCode.TabIndex = 7;
            this.lblBarcodeCode.Text = "Mã Barcode: *";
            // 
            // cboCodeType
            // 
            this.cboCodeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCodeType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboCodeType.FormattingEnabled = true;
            this.cboCodeType.Items.AddRange(new object[] {
            "Barcode",
            "QR Code",
            "EAN-13"});
            this.cboCodeType.Location = new System.Drawing.Point(20, 205);
            this.cboCodeType.Name = "cboCodeType";
            this.cboCodeType.Size = new System.Drawing.Size(360, 25);
            this.cboCodeType.TabIndex = 6;
            // 
            // lblCodeType
            // 
            this.lblCodeType.AutoSize = true;
            this.lblCodeType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCodeType.Location = new System.Drawing.Point(20, 180);
            this.lblCodeType.Name = "lblCodeType";
            this.lblCodeType.Size = new System.Drawing.Size(74, 19);
            this.lblCodeType.TabIndex = 5;
            this.lblCodeType.Text = "Loại mã: *";
            // 
            // cboProductUnit
            // 
            this.cboProductUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProductUnit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboProductUnit.FormattingEnabled = true;
            this.cboProductUnit.Location = new System.Drawing.Point(20, 140);
            this.cboProductUnit.Name = "cboProductUnit";
            this.cboProductUnit.Size = new System.Drawing.Size(360, 25);
            this.cboProductUnit.TabIndex = 4;
            // 
            // lblProductUnit
            // 
            this.lblProductUnit.AutoSize = true;
            this.lblProductUnit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblProductUnit.Location = new System.Drawing.Point(20, 115);
            this.lblProductUnit.Name = "lblProductUnit";
            this.lblProductUnit.Size = new System.Drawing.Size(146, 19);
            this.lblProductUnit.TabIndex = 3;
            this.lblProductUnit.Text = "Sản phẩm - Đơn vị: *";
            // 
            // txtBarcodeId
            // 
            this.txtBarcodeId.Enabled = false;
            this.txtBarcodeId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBarcodeId.Location = new System.Drawing.Point(20, 75);
            this.txtBarcodeId.Name = "txtBarcodeId";
            this.txtBarcodeId.Size = new System.Drawing.Size(360, 25);
            this.txtBarcodeId.TabIndex = 2;
            // 
            // lblBarcodeId
            // 
            this.lblBarcodeId.AutoSize = true;
            this.lblBarcodeId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBarcodeId.Location = new System.Drawing.Point(20, 50);
            this.lblBarcodeId.Name = "lblBarcodeId";
            this.lblBarcodeId.Size = new System.Drawing.Size(94, 19);
            this.lblBarcodeId.TabIndex = 1;
            this.lblBarcodeId.Text = "Mã định danh:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.picBarcode);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.groupBox2.Location = new System.Drawing.Point(410, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox2.Size = new System.Drawing.Size(380, 470);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Xem trước Barcode";
            // 
            // picBarcode
            // 
            this.picBarcode.BackColor = System.Drawing.Color.White;
            this.picBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBarcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBarcode.Location = new System.Drawing.Point(10, 28);
            this.picBarcode.Name = "picBarcode";
            this.picBarcode.Size = new System.Drawing.Size(360, 432);
            this.picBarcode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picBarcode.TabIndex = 0;
            this.picBarcode.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 550);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 60);
            this.panel2.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(420, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 40);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(230, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 40);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Thêm mới";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmAddBarcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 610);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddBarcode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm Barcode";
            this.Load += new System.EventHandler(this.frmAddBarcode_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBarcode)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBarcodeId;
        private System.Windows.Forms.Label lblBarcodeId;
        private System.Windows.Forms.ComboBox cboProductUnit;
        private System.Windows.Forms.Label lblProductUnit;
        private System.Windows.Forms.ComboBox cboCodeType;
        private System.Windows.Forms.Label lblCodeType;
        private System.Windows.Forms.TextBox txtBarcodeCode;
        private System.Windows.Forms.Label lblBarcodeCode;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.TextBox txtImagePath;
        private System.Windows.Forms.Label lblImagePath;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox picBarcode;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}
