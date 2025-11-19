namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Employees
{
    partial class frmWorkSchedule
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
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.btnViewSchedule = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.cmbEmployee = new System.Windows.Forms.ComboBox();
            this.lblEmployee = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgvSchedule = new System.Windows.Forms.DataGridView();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmployeeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShift = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).BeginInit();
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
            this.lblTitle.Size = new System.Drawing.Size(306, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "LỊCH LÀM VIỆC NHÂN VIÊN";
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilter.Controls.Add(this.btnViewSchedule);
            this.pnlFilter.Controls.Add(this.dtpEndDate);
            this.pnlFilter.Controls.Add(this.lblEndDate);
            this.pnlFilter.Controls.Add(this.dtpStartDate);
            this.pnlFilter.Controls.Add(this.lblStartDate);
            this.pnlFilter.Controls.Add(this.cmbEmployee);
            this.pnlFilter.Controls.Add(this.lblEmployee);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 60);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(10);
            this.pnlFilter.Size = new System.Drawing.Size(1400, 80);
            this.pnlFilter.TabIndex = 1;
            // 
            // btnViewSchedule
            // 
            this.btnViewSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewSchedule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnViewSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewSchedule.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnViewSchedule.ForeColor = System.Drawing.Color.White;
            this.btnViewSchedule.Location = new System.Drawing.Point(1230, 25);
            this.btnViewSchedule.Name = "btnViewSchedule";
            this.btnViewSchedule.Size = new System.Drawing.Size(150, 35);
            this.btnViewSchedule.TabIndex = 6;
            this.btnViewSchedule.Text = "Xem lịch";
            this.btnViewSchedule.UseVisualStyleBackColor = false;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(950, 30);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(150, 25);
            this.dtpEndDate.TabIndex = 5;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblEndDate.Location = new System.Drawing.Point(850, 33);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(70, 17);
            this.lblEndDate.TabIndex = 4;
            this.lblEndDate.Text = "Đến ngày:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(650, 30);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(150, 25);
            this.dtpStartDate.TabIndex = 3;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblStartDate.Location = new System.Drawing.Point(560, 33);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(60, 17);
            this.lblStartDate.TabIndex = 2;
            this.lblStartDate.Text = "Từ ngày:";
            // 
            // cmbEmployee
            // 
            this.cmbEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmployee.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbEmployee.FormattingEnabled = true;
            this.cmbEmployee.Location = new System.Drawing.Point(120, 30);
            this.cmbEmployee.Name = "cmbEmployee";
            this.cmbEmployee.Size = new System.Drawing.Size(400, 25);
            this.cmbEmployee.TabIndex = 1;
            // 
            // lblEmployee
            // 
            this.lblEmployee.AutoSize = true;
            this.lblEmployee.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblEmployee.Location = new System.Drawing.Point(20, 33);
            this.lblEmployee.Name = "lblEmployee";
            this.lblEmployee.Size = new System.Drawing.Size(75, 17);
            this.lblEmployee.TabIndex = 0;
            this.lblEmployee.Text = "Nhân viên:";
            // 
            // pnlButtons
            // 
            this.pnlButtons.BackColor = System.Drawing.Color.White;
            this.pnlButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlButtons.Controls.Add(this.btnExport);
            this.pnlButtons.Controls.Add(this.btnDelete);
            this.pnlButtons.Controls.Add(this.btnEdit);
            this.pnlButtons.Controls.Add(this.btnAdd);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtons.Location = new System.Drawing.Point(0, 140);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1400, 60);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(520, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(160, 40);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Xuất Excel";
            this.btnExport.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(360, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(147, 40);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(187, 12);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(160, 40);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Chỉnh sửa";
            this.btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(13, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(160, 40);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Thêm lịch";
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.dgvSchedule);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 200);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(10);
            this.pnlGrid.Size = new System.Drawing.Size(1400, 500);
            this.pnlGrid.TabIndex = 3;
            // 
            // dgvSchedule
            // 
            this.dgvSchedule.AllowUserToAddRows = false;
            this.dgvSchedule.AllowUserToDeleteRows = false;
            this.dgvSchedule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSchedule.BackgroundColor = System.Drawing.Color.White;
            this.dgvSchedule.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSchedule.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSchedule.ColumnHeadersHeight = 40;
            this.dgvSchedule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colEmployeeName,
            this.colDate,
            this.colShift,
            this.colStartTime,
            this.colEndTime,
            this.colNote});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSchedule.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSchedule.EnableHeadersVisualStyles = false;
            this.dgvSchedule.Location = new System.Drawing.Point(10, 10);
            this.dgvSchedule.MultiSelect = false;
            this.dgvSchedule.Name = "dgvSchedule";
            this.dgvSchedule.ReadOnly = true;
            this.dgvSchedule.RowHeadersVisible = false;
            this.dgvSchedule.RowTemplate.Height = 40;
            this.dgvSchedule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSchedule.Size = new System.Drawing.Size(1380, 480);
            this.dgvSchedule.TabIndex = 0;
            // 
            // colId
            // 
            this.colId.FillWeight = 60F;
            this.colId.HeaderText = "Mã";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            // 
            // colEmployeeName
            // 
            this.colEmployeeName.HeaderText = "Nhân viên";
            this.colEmployeeName.Name = "colEmployeeName";
            this.colEmployeeName.ReadOnly = true;
            // 
            // colDate
            // 
            this.colDate.FillWeight = 80F;
            this.colDate.HeaderText = "Ngày làm";
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            // 
            // colShift
            // 
            this.colShift.FillWeight = 70F;
            this.colShift.HeaderText = "Ca làm";
            this.colShift.Name = "colShift";
            this.colShift.ReadOnly = true;
            // 
            // colStartTime
            // 
            this.colStartTime.FillWeight = 70F;
            this.colStartTime.HeaderText = "Giờ bắt đầu";
            this.colStartTime.Name = "colStartTime";
            this.colStartTime.ReadOnly = true;
            // 
            // colEndTime
            // 
            this.colEndTime.FillWeight = 70F;
            this.colEndTime.HeaderText = "Giờ kết thúc";
            this.colEndTime.Name = "colEndTime";
            this.colEndTime.ReadOnly = true;
            // 
            // colNote
            // 
            this.colNote.HeaderText = "Ghi chú";
            this.colNote.Name = "colNote";
            this.colNote.ReadOnly = true;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBottom.Controls.Add(this.lblTotal);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 700);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(10);
            this.pnlBottom.Size = new System.Drawing.Size(1400, 40);
            this.pnlBottom.TabIndex = 4;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(15, 10);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(117, 19);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Tổng số: 0 ca làm";
            // 
            // frmWorkSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 740);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmWorkSchedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lịch làm việc";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Button btnViewSchedule;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.ComboBox cmbEmployee;
        private System.Windows.Forms.Label lblEmployee;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.DataGridView dgvSchedule;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmployeeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShift;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEndTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNote;
    }
}