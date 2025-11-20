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
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.groupBoxCondition = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbGiamTheo = new System.Windows.Forms.ComboBox();
            this.numMaxValue = new System.Windows.Forms.NumericUpDown();
            this.lblMaxValue = new System.Windows.Forms.Label();
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
            this.panelTop.Size = new System.Drawing.Size(1003, 60);
            this.panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(424, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "CHI TIẾT CHƯƠNG TRÌNH KHUYẾN MÃI";
            // 
            // panelMain
            // 
            this.panelMain.AutoScroll = true;
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.groupBoxCondition);
            this.panelMain.Controls.Add(this.groupBoxInfo);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 60);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20);
            this.panelMain.Size = new System.Drawing.Size(1003, 546);
            this.panelMain.TabIndex = 1;
            // 
            // groupBoxCondition
            // 
            this.groupBoxCondition.Controls.Add(this.label1);
            this.groupBoxCondition.Controls.Add(this.cmbGiamTheo);
            this.groupBoxCondition.Controls.Add(this.numMaxValue);
            this.groupBoxCondition.Controls.Add(this.lblMaxValue);
            this.groupBoxCondition.Controls.Add(this.numMinValue);
            this.groupBoxCondition.Controls.Add(this.lblMinValue);
            this.groupBoxCondition.Controls.Add(this.txtCondition);
            this.groupBoxCondition.Controls.Add(this.lblCondition);
            this.groupBoxCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxCondition.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupBoxCondition.Location = new System.Drawing.Point(20, 320);
            this.groupBoxCondition.Name = "groupBoxCondition";
            this.groupBoxCondition.Size = new System.Drawing.Size(963, 220);
            this.groupBoxCondition.TabIndex = 1;
            this.groupBoxCondition.TabStop = false;
            this.groupBoxCondition.Text = "Điều kiện áp dụng";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(20, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Giảm theo:";
            // 
            // cmbGiamTheo
            // 
            this.cmbGiamTheo.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbGiamTheo.FormattingEnabled = true;
            this.cmbGiamTheo.Location = new System.Drawing.Point(150, 168);
            this.cmbGiamTheo.Name = "cmbGiamTheo";
            this.cmbGiamTheo.Size = new System.Drawing.Size(146, 25);
            this.cmbGiamTheo.TabIndex = 8;
            // 
            // numMaxValue
            // 
            this.numMaxValue.DecimalPlaces = 2;
            this.numMaxValue.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.numMaxValue.Location = new System.Drawing.Point(620, 113);
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
            this.lblMaxValue.Location = new System.Drawing.Point(490, 116);
            this.lblMaxValue.Name = "lblMaxValue";
            this.lblMaxValue.Size = new System.Drawing.Size(90, 17);
            this.lblMaxValue.TabIndex = 6;
            this.lblMaxValue.Text = "Giá trị tối đa:";
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
            this.lblMinValue.Size = new System.Drawing.Size(107, 17);
            this.lblMinValue.TabIndex = 2;
            this.lblMinValue.Text = "Giá trị tối thiểu:";
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
            this.lblCondition.Size = new System.Drawing.Size(71, 17);
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
            this.groupBoxInfo.Size = new System.Drawing.Size(963, 300);
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
            this.lblEndDate.Size = new System.Drawing.Size(98, 17);
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
            this.lblStartDate.Size = new System.Drawing.Size(95, 17);
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
            this.lblType.Size = new System.Drawing.Size(38, 17);
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
            this.lblPromotionName.Size = new System.Drawing.Size(119, 17);
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
            this.lblPromotionId.Size = new System.Drawing.Size(116, 17);
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
            this.panelButtons.Location = new System.Drawing.Point(0, 606);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(1003, 76);
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
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1003, 682);
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
        private System.Windows.Forms.NumericUpDown numMaxValue;
        private System.Windows.Forms.Label lblMaxValue;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbGiamTheo;
        private System.Windows.Forms.Label label1;
    }
}