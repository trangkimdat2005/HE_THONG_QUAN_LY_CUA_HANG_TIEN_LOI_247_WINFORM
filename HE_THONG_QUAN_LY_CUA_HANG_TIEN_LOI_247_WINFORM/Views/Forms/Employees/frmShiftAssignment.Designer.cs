<<<<<<< HEAD
﻿namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.Forms.Employees
{
    partial class frmShiftAssignment
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbEmployee = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgvShiftMatrix = new System.Windows.Forms.DataGridView();
            this.colShiftName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMon = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colTue = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colWed = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colThu = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colFri = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSat = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSun = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pnlTop.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShiftMatrix)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1000, 60);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(315, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "PHÂN CÔNG CA LÀM VIỆC";
            // 
            // pnlControls
            // 
            this.pnlControls.BackColor = System.Drawing.Color.White;
            this.pnlControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlControls.Controls.Add(this.label1);
            this.pnlControls.Controls.Add(this.cmbEmployee);
            this.pnlControls.Controls.Add(this.btnSave);
            this.pnlControls.Controls.Add(this.btnCancel);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControls.Location = new System.Drawing.Point(0, 60);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(1000, 70);
            this.pnlControls.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(20, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chọn nhân viên:";
            // 
            // cmbEmployee
            // 
            this.cmbEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmployee.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbEmployee.FormattingEnabled = true;
            this.cmbEmployee.Location = new System.Drawing.Point(150, 22);
            this.cmbEmployee.Name = "cmbEmployee";
            this.cmbEmployee.Size = new System.Drawing.Size(300, 25);
            this.cmbEmployee.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(750, 17);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 35);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Lưu phân công";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(880, 17);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.Controls.Add(this.dgvShiftMatrix);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 130);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(20);
            this.pnlGrid.Size = new System.Drawing.Size(1000, 470);
            this.pnlGrid.TabIndex = 2;
            // 
            // dgvShiftMatrix
            // 
            this.dgvShiftMatrix.AllowUserToAddRows = false;
            this.dgvShiftMatrix.AllowUserToDeleteRows = false;
            this.dgvShiftMatrix.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvShiftMatrix.BackgroundColor = System.Drawing.Color.White;
            this.dgvShiftMatrix.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvShiftMatrix.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvShiftMatrix.ColumnHeadersHeight = 45;
            this.dgvShiftMatrix.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colShiftName,
            this.colMon,
            this.colTue,
            this.colWed,
            this.colThu,
            this.colFri,
            this.colSat,
            this.colSun});
            this.dgvShiftMatrix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShiftMatrix.EnableHeadersVisualStyles = false;
            this.dgvShiftMatrix.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.dgvShiftMatrix.Location = new System.Drawing.Point(20, 20);
            this.dgvShiftMatrix.Name = "dgvShiftMatrix";
            this.dgvShiftMatrix.RowHeadersVisible = false;
            this.dgvShiftMatrix.RowTemplate.Height = 40;
            this.dgvShiftMatrix.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvShiftMatrix.Size = new System.Drawing.Size(960, 430);
            this.dgvShiftMatrix.TabIndex = 0;
            // 
            // colShiftName
            // 
            this.colShiftName.FillWeight = 150F;
            this.colShiftName.HeaderText = "Ca làm việc";
            this.colShiftName.Name = "colShiftName";
            this.colShiftName.ReadOnly = true;
            // 
            // colMon
            // 
            this.colMon.HeaderText = "Thứ 2";
            this.colMon.Name = "colMon";
            // 
            // colTue
            // 
            this.colTue.HeaderText = "Thứ 3";
            this.colTue.Name = "colTue";
            // 
            // colWed
            // 
            this.colWed.HeaderText = "Thứ 4";
            this.colWed.Name = "colWed";
            // 
            // colThu
            // 
            this.colThu.HeaderText = "Thứ 5";
            this.colThu.Name = "colThu";
            // 
            // colFri
            // 
            this.colFri.HeaderText = "Thứ 6";
            this.colFri.Name = "colFri";
            // 
            // colSat
            // 
            this.colSat.HeaderText = "Thứ 7";
            this.colSat.Name = "colSat";
            // 
            // colSun
            // 
            this.colSun.HeaderText = "Chủ Nhật";
            this.colSun.Name = "colSun";
            // 
            // frmShiftAssignment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmShiftAssignment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phân công ca làm việc";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShiftMatrix)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbEmployee;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.DataGridView dgvShiftMatrix;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShiftName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colMon;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colTue;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWed;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colThu;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colFri;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSat;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSun;
    }
=======
﻿namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.Forms.Employees
{
    partial class frmShiftAssignment
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbEmployee = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgvShiftMatrix = new System.Windows.Forms.DataGridView();
            this.colShiftName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMon = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colTue = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colWed = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colThu = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colFri = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSat = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSun = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lblInstruction = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShiftMatrix)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1000, 60);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(325, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "PHÂN CÔNG CA LÀM VIỆC";
            // 
            // pnlControls
            // 
            this.pnlControls.BackColor = System.Drawing.Color.White;
            this.pnlControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlControls.Controls.Add(this.label1);
            this.pnlControls.Controls.Add(this.cmbEmployee);
            this.pnlControls.Controls.Add(this.btnSave);
            this.pnlControls.Controls.Add(this.btnCancel);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControls.Location = new System.Drawing.Point(0, 60);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(1000, 70);
            this.pnlControls.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(20, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chọn nhân viên:";
            // 
            // cmbEmployee
            // 
            this.cmbEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmployee.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbEmployee.FormattingEnabled = true;
            this.cmbEmployee.Location = new System.Drawing.Point(150, 22);
            this.cmbEmployee.Name = "cmbEmployee";
            this.cmbEmployee.Size = new System.Drawing.Size(300, 25);
            this.cmbEmployee.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(750, 17);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 35);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Lưu phân công";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(880, 17);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.Controls.Add(this.lblInstruction);
            this.pnlGrid.Controls.Add(this.dgvShiftMatrix);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 130);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(20);
            this.pnlGrid.Size = new System.Drawing.Size(1000, 470);
            this.pnlGrid.TabIndex = 2;
            // 
            // dgvShiftMatrix
            // 
            this.dgvShiftMatrix.AllowUserToAddRows = false;
            this.dgvShiftMatrix.AllowUserToDeleteRows = false;
            this.dgvShiftMatrix.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvShiftMatrix.BackgroundColor = System.Drawing.Color.White;
            this.dgvShiftMatrix.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvShiftMatrix.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvShiftMatrix.ColumnHeadersHeight = 45;
            this.dgvShiftMatrix.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colShiftName,
            this.colMon,
            this.colTue,
            this.colWed,
            this.colThu,
            this.colFri,
            this.colSat,
            this.colSun});
            this.dgvShiftMatrix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShiftMatrix.EnableHeadersVisualStyles = false;
            this.dgvShiftMatrix.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.dgvShiftMatrix.Location = new System.Drawing.Point(20, 50);
            this.dgvShiftMatrix.Name = "dgvShiftMatrix";
            this.dgvShiftMatrix.RowHeadersVisible = false;
            this.dgvShiftMatrix.RowTemplate.Height = 40;
            this.dgvShiftMatrix.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvShiftMatrix.Size = new System.Drawing.Size(960, 400);
            this.dgvShiftMatrix.TabIndex = 0;
            // 
            // colShiftName
            // 
            this.colShiftName.FillWeight = 150F;
            this.colShiftName.HeaderText = "Ca làm việc";
            this.colShiftName.Name = "colShiftName";
            this.colShiftName.ReadOnly = true;
            // 
            // colMon
            // 
            this.colMon.HeaderText = "Thứ 2";
            this.colMon.Name = "colMon";
            // 
            // colTue
            // 
            this.colTue.HeaderText = "Thứ 3";
            this.colTue.Name = "colTue";
            // 
            // colWed
            // 
            this.colWed.HeaderText = "Thứ 4";
            this.colWed.Name = "colWed";
            // 
            // colThu
            // 
            this.colThu.HeaderText = "Thứ 5";
            this.colThu.Name = "colThu";
            // 
            // colFri
            // 
            this.colFri.HeaderText = "Thứ 6";
            this.colFri.Name = "colFri";
            // 
            // colSat
            // 
            this.colSat.HeaderText = "Thứ 7";
            this.colSat.Name = "colSat";
            // 
            // colSun
            // 
            this.colSun.HeaderText = "Chủ Nhật";
            this.colSun.Name = "colSun";
            // 
            // lblInstruction
            // 
            this.lblInstruction.AutoSize = true;
            this.lblInstruction.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInstruction.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.lblInstruction.ForeColor = System.Drawing.Color.DimGray;
            this.lblInstruction.Location = new System.Drawing.Point(20, 20);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.lblInstruction.Size = new System.Drawing.Size(389, 29);
            this.lblInstruction.TabIndex = 1;
            this.lblInstruction.Text = "Tích chọn vào ô tương ứng để phân công ca làm việc cho nhân viên.";
            // 
            // frmShiftAssignment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmShiftAssignment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phân công ca làm việc";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            this.pnlGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShiftMatrix)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbEmployee;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.DataGridView dgvShiftMatrix;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShiftName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colMon;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colTue;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colWed;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colThu;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colFri;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSat;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSun;
    }
>>>>>>> 56b069679b1de63bae93e54d08068b01b13755f6
}