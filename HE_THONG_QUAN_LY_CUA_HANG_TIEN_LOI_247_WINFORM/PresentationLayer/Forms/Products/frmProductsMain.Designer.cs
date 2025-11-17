namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Products
{
    partial class frmProductsMain
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabProducts = new System.Windows.Forms.TabPage();
            this.tabCategories = new System.Windows.Forms.TabPage();
            this.tabBrands = new System.Windows.Forms.TabPage();
            this.tabSuppliers = new System.Windows.Forms.TabPage();
            this.tabMeasurements = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabProducts);
            this.tabControl.Controls.Add(this.tabCategories);
            this.tabControl.Controls.Add(this.tabBrands);
            this.tabControl.Controls.Add(this.tabSuppliers);
            this.tabControl.Controls.Add(this.tabMeasurements);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tabControl.ItemSize = new System.Drawing.Size(120, 40);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1200, 700);
            this.tabControl.TabIndex = 0;
            // 
            // tabProducts
            // 
            this.tabProducts.Location = new System.Drawing.Point(4, 44);
            this.tabProducts.Name = "tabProducts";
            this.tabProducts.Padding = new System.Windows.Forms.Padding(3);
            this.tabProducts.Size = new System.Drawing.Size(1192, 652);
            this.tabProducts.TabIndex = 0;
            this.tabProducts.Text = "S?n ph?m";
            this.tabProducts.UseVisualStyleBackColor = true;
            // 
            // tabCategories
            // 
            this.tabCategories.Location = new System.Drawing.Point(4, 44);
            this.tabCategories.Name = "tabCategories";
            this.tabCategories.Padding = new System.Windows.Forms.Padding(3);
            this.tabCategories.Size = new System.Drawing.Size(1192, 652);
            this.tabCategories.TabIndex = 1;
            this.tabCategories.Text = "Danh m?c";
            this.tabCategories.UseVisualStyleBackColor = true;
            // 
            // tabBrands
            // 
            this.tabBrands.Location = new System.Drawing.Point(4, 44);
            this.tabBrands.Name = "tabBrands";
            this.tabBrands.Padding = new System.Windows.Forms.Padding(3);
            this.tabBrands.Size = new System.Drawing.Size(1192, 652);
            this.tabBrands.TabIndex = 2;
            this.tabBrands.Text = "Nhãn hi?u";
            this.tabBrands.UseVisualStyleBackColor = true;
            // 
            // tabSuppliers
            // 
            this.tabSuppliers.Location = new System.Drawing.Point(4, 44);
            this.tabSuppliers.Name = "tabSuppliers";
            this.tabSuppliers.Padding = new System.Windows.Forms.Padding(3);
            this.tabSuppliers.Size = new System.Drawing.Size(1192, 652);
            this.tabSuppliers.TabIndex = 3;
            this.tabSuppliers.Text = "Nhà cung c?p";
            this.tabSuppliers.UseVisualStyleBackColor = true;
            // 
            // tabMeasurements
            // 
            this.tabMeasurements.Location = new System.Drawing.Point(4, 44);
            this.tabMeasurements.Name = "tabMeasurements";
            this.tabMeasurements.Padding = new System.Windows.Forms.Padding(3);
            this.tabMeasurements.Size = new System.Drawing.Size(1192, 652);
            this.tabMeasurements.TabIndex = 4;
            this.tabMeasurements.Text = "??n v? tính";
            this.tabMeasurements.UseVisualStyleBackColor = true;
            // 
            // frmProductsMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmProductsMain";
            this.Text = "Qu?n lý s?n ph?m";
            this.Load += new System.EventHandler(this.frmProductsMain_Load);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabProducts;
        private System.Windows.Forms.TabPage tabCategories;
        private System.Windows.Forms.TabPage tabBrands;
        private System.Windows.Forms.TabPage tabSuppliers;
        private System.Windows.Forms.TabPage tabMeasurements;
    }
}
