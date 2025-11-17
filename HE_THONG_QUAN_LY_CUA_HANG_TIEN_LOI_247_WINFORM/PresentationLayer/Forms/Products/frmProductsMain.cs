using System;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Products
{
    public partial class frmProductsMain : Form
    {
        private frmProducts _frmProducts;
        private frmCategorys _frmCategorys;
        private frmBrands _frmBrands;
        private frmSuppliers _frmSuppliers;
        private frmMeasurements _frmMeasurements;

        public frmProductsMain()
        {
            InitializeComponent();
        }

        private void frmProductsMain_Load(object sender, EventArgs e)
        {
            // Load form c?a tab hi?n t?i thay vì load t?t c?
            LoadCurrentTabForm();
            
            // Subscribe to tab change event
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Load form khi user chuy?n tab
            LoadCurrentTabForm();
        }

        private void LoadCurrentTabForm()
        {
            if (tabControl.SelectedTab == tabProducts)
            {
                LoadFormIntoTab(tabProducts, ref _frmProducts);
            }
            else if (tabControl.SelectedTab == tabCategories)
            {
                LoadFormIntoTab(tabCategories, ref _frmCategorys);
            }
            else if (tabControl.SelectedTab == tabBrands)
            {
                LoadFormIntoTab(tabBrands, ref _frmBrands);
            }
            else if (tabControl.SelectedTab == tabSuppliers)
            {
                LoadFormIntoTab(tabSuppliers, ref _frmSuppliers);
            }
            else if (tabControl.SelectedTab == tabMeasurements)
            {
                LoadFormIntoTab(tabMeasurements, ref _frmMeasurements);
            }
        }

        private void LoadFormIntoTab<T>(TabPage tabPage, ref T form) where T : Form, new()
        {
            if (form == null || form.IsDisposed)
            {
                form = new T
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill
                };
                
                tabPage.Controls.Clear();
                tabPage.Controls.Add(form);
                form.Show();
            }
        }

        // Public method ?? chuy?n tab t? bên ngoài
        public void SwitchToTab(string tabName)
        {
            switch (tabName.ToLower())
            {
                case "products":
                case "sanpham":
                    tabControl.SelectedTab = tabProducts;
                    break;
                case "categories":
                case "danhmuc":
                    tabControl.SelectedTab = tabCategories;
                    break;
                case "brands":
                case "nhanhieu":
                    tabControl.SelectedTab = tabBrands;
                    break;
                case "suppliers":
                case "nhacungcap":
                    tabControl.SelectedTab = tabSuppliers;
                    break;
                case "measurements":
                case "donvitinh":
                    tabControl.SelectedTab = tabMeasurements;
                    break;
            }
            // LoadCurrentTabForm s? ???c g?i t? ??ng qua SelectedIndexChanged event
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            
            // Cleanup forms
            _frmProducts?.Dispose();
            _frmCategorys?.Dispose();
            _frmBrands?.Dispose();
            _frmSuppliers?.Dispose();
            _frmMeasurements?.Dispose();
        }
    }
}
