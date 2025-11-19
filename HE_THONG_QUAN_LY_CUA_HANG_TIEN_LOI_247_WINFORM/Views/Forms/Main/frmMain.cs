using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Bills;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Customers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Employees;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Inventory;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Products;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Suppliers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Promotions;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.Forms.Inventory;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.Forms.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Main
{
    public partial class frmMain : Form
    {
        private Form currentChildForm;
        
        // Cache forms để tránh khởi tạo lại nhiều lần
        private Dictionary<Type, Form> formCache = new Dictionary<Type, Form>();

        public frmMain()
        {
            InitializeComponent();

            // Phóng to cửa sổ khi khởi động (vẫn có title bar và nút minimize/maximize/close)
            this.WindowState = FormWindowState.Maximized;
            // Giữ nguyên FormBorderStyle để có title bar
            // this.FormBorderStyle = FormBorderStyle.Sizable; // Mặc định đã có sẵn
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Load Dashboard mặc định khi mở chương trình
            LoadFormIntoPanel(new frmDashboard());
            SetActiveButton(btn_dashboard, null); // Highlight nút Dashboard
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            // Cleanup cached forms
            foreach (var form in formCache.Values)
            {
                if (form != null && !form.IsDisposed)
                {
                    form.Dispose();
                }
            }
            formCache.Clear();
            
            // Thêm code đăng xuất ở đây
            this.Close();
        }

        // Ẩn tất cả các menu con
        private void HideSubMenus()
        {
            pnlProductSubmenu.Visible = false;
            pnlEmployeeSubmenu.Visible = false;
            pnlCustomerSubmenu.Visible = false;
            pnlSupplierSubmenu.Visible = false;
            pnlStorageSubmenu.Visible = false;
        }

        // Highlight nút được chọn
        private void SetActiveButton(Guna.UI2.WinForms.Guna2Button activeButton, Guna.UI2.WinForms.Guna2Button activeSubButton)
        {
            // Reset tất cả các nút cha
            foreach (Control ctrl in flpMenu.Controls)
            {
                if (ctrl is Guna.UI2.WinForms.Guna2Button)
                {
                    (ctrl as Guna.UI2.WinForms.Guna2Button).FillColor = Color.Transparent;
                    (ctrl as Guna.UI2.WinForms.Guna2Button).ForeColor = Color.Black;
                }
            }

            // Reset tất cả các nút con Products
            foreach (Control ctrl in pnlProductSubmenu.Controls)
            {
                if (ctrl is Guna.UI2.WinForms.Guna2Button)
                {
                    (ctrl as Guna.UI2.WinForms.Guna2Button).FillColor = Color.Transparent;
                    (ctrl as Guna.UI2.WinForms.Guna2Button).ForeColor = Color.Black;
                }
            }

            // Reset tất cả các nút con Employees
            foreach (Control ctrl in pnlEmployeeSubmenu.Controls)
            {
                if (ctrl is Guna.UI2.WinForms.Guna2Button)
                {
                    (ctrl as Guna.UI2.WinForms.Guna2Button).FillColor = Color.Transparent;
                    (ctrl as Guna.UI2.WinForms.Guna2Button).ForeColor = Color.Black;
                }
            }

            // Reset tất cả các nút con Customers
            foreach (Control ctrl in pnlCustomerSubmenu.Controls)
            {
                if (ctrl is Guna.UI2.WinForms.Guna2Button)
                {
                    (ctrl as Guna.UI2.WinForms.Guna2Button).FillColor = Color.Transparent;
                    (ctrl as Guna.UI2.WinForms.Guna2Button).ForeColor = Color.Black;
                }
            }

            // Reset tất cả các nút con Suppliers
            foreach (Control ctrl in pnlSupplierSubmenu.Controls)
            {
                if (ctrl is Guna.UI2.WinForms.Guna2Button)
                {
                    (ctrl as Guna.UI2.WinForms.Guna2Button).FillColor = Color.Transparent;
                    (ctrl as Guna.UI2.WinForms.Guna2Button).ForeColor = Color.Black;
                }
            }

            // Reset tất cả các nút con Storage/Inventory
            foreach (Control ctrl in pnlStorageSubmenu.Controls)
            {
                if (ctrl is Guna.UI2.WinForms.Guna2Button)
                {
                    (ctrl as Guna.UI2.WinForms.Guna2Button).FillColor = Color.Transparent;
                    (ctrl as Guna.UI2.WinForms.Guna2Button).ForeColor = Color.Black;
                }
            }

            // Highlight nút cha
            if (activeButton != null)
            {
                activeButton.FillColor = Color.FromArgb(94, 148, 255);
                activeButton.ForeColor = Color.White;
            }

            // Highlight nút con
            if (activeSubButton != null)
            {
                activeSubButton.FillColor = Color.FromArgb(142, 192, 255); // Màu nhạt hơn
                activeSubButton.ForeColor = Color.White;
            }
        }


        private void btn_dashboard_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new frmDashboard());
            HideSubMenus();
            SetActiveButton(btn_dashboard, null);
        }

        private void LoadFormIntoPanel(Form childForm)
        {
            Type formType = childForm.GetType();
            
            // Kiểm tra nếu form đã được cache
            if (formCache.ContainsKey(formType))
            {
                // Reuse cached form
                Form cachedForm = formCache[formType];
                
                // Kiểm tra nếu form đã bị dispose thì tạo lại
                if (cachedForm.IsDisposed)
                {
                    formCache[formType] = childForm;
                    cachedForm = childForm;
                }
                else
                {
                    // Dispose form mới vì đã có cache
                    childForm.Dispose();
                }
                
                // Kiểm tra nếu form này đang hiển thị
                if (currentChildForm == cachedForm && sharePanel.Controls.Contains(cachedForm))
                {
                    return; // Không làm gì cả
                }
                
                childForm = cachedForm;
            }
            else
            {
                // Cache form mới
                formCache[formType] = childForm;
            }

            // Ẩn form cũ nếu có (không dispose để có thể reuse)
            if (currentChildForm != null)
            {
                currentChildForm.Hide();
            }

            // Xóa các control cũ trong sharePanel
            sharePanel.Controls.Clear();

            // Setup child form
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Thêm form vào sharePanel
            sharePanel.Controls.Add(childForm);
            sharePanel.Tag = childForm;

            // Hiển thị form
            childForm.Show();
            childForm.BringToFront();
        }

        private void btn_products_Click(object sender, EventArgs e)
        {
            // Chỉ toggle menu con, không load form
            pnlProductSubmenu.Visible = !pnlProductSubmenu.Visible;
            // Ẩn các menu khác
            pnlEmployeeSubmenu.Visible = false;
            pnlCustomerSubmenu.Visible = false;
            pnlSupplierSubmenu.Visible = false;
            SetActiveButton(btn_products, null);
        }

        // --- Click cho các nút menu con của Sản phẩm ---

        private void btnSub_SanPham_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new frmProducts());
            SetActiveButton(btn_products, btnSub_SanPham);
        }
        private void btnSub_HangHoa_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new frmGoods());
            SetActiveButton(btn_products, btnSub_HangHoa);
        }
        private void btnSub_DanhMuc_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new frmCategorys());
            SetActiveButton(btn_products, btnSub_DanhMuc);
        }


        private void btnSub_NhanHieu_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new frmBrands());
            SetActiveButton(btn_products, btnSub_NhanHieu);
        }

        private void btnSub_DonViTinh_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new frmMeasurements());
            SetActiveButton(btn_products, btnSub_DonViTinh);
        }

        // --- Click cho các nút menu con của Nhân viên ---

        private void btnSub_DanhSachNV_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new frmEmployeeList());
            SetActiveButton(btn_employees, btnSub_DanhSachNV);
        }

        private void btnSub_PhanCong_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new frmWorkSchedule());
            SetActiveButton(btn_employees, btnSub_PhanCong);
        }

        private void btnSub_ChamCong_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new frmEmployees());
            SetActiveButton(btn_employees, btnSub_ChamCong);
        }

        // --- Click cho các nút menu con của Khách hàng ---

        private void btnSub_DanhSachKH_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new frmCustomers());
            SetActiveButton(btn_customers, btnSub_DanhSachKH);
        }

        private void btnSub_TheThanhVien_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new frmMemberCards());
            SetActiveButton(btn_customers, btnSub_TheThanhVien);
        }


        // --- Click cho các nút menu con của Nhà cung cấp ---

        private void btnSub_DanhSachNCC_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new frmSupplierList());

            SetActiveButton(btn_suppliers, btnSub_DanhSachNCC);
        }

        private void btnSub_LichSuGiaoDich_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new frmTransactionHistory());
            SetActiveButton(btn_suppliers, btnSub_LichSuGiaoDich);
        }

        // --- Click cho các nút cha khác ---

        private void btn_bills_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new frmInvoices());
            HideSubMenus();
            SetActiveButton(btn_bills, null);
        }

        private void btn_employees_Click(object sender, EventArgs e)
        {
            // Toggle menu con Nhân viên
            pnlEmployeeSubmenu.Visible = !pnlEmployeeSubmenu.Visible;
            // Ẩn các menu khác
            pnlProductSubmenu.Visible = false;
            pnlCustomerSubmenu.Visible = false;
            pnlSupplierSubmenu.Visible = false;
            SetActiveButton(btn_employees, null);
        }

        private void btn_customers_Click(object sender, EventArgs e)
        {
            // Toggle menu con Khách hàng
            pnlCustomerSubmenu.Visible = !pnlCustomerSubmenu.Visible;
            // Ẩn các menu khác
            pnlProductSubmenu.Visible = false;
            pnlEmployeeSubmenu.Visible = false;
            pnlSupplierSubmenu.Visible = false;
            SetActiveButton(btn_customers, null);
        }

        private void btn_promotions_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new frmPromotions());
            HideSubMenus();
            SetActiveButton(btn_promotions, null);
        }

        private void btn_suppliers_Click(object sender, EventArgs e)
        {
            // Toggle menu con Nhà cung cấp
            pnlSupplierSubmenu.Visible = !pnlSupplierSubmenu.Visible;
            // Ẩn các menu khác
            pnlProductSubmenu.Visible = false;
            pnlEmployeeSubmenu.Visible = false;
            pnlCustomerSubmenu.Visible = false;
            SetActiveButton(btn_suppliers, null);
        }

        private void btn_inventory_Click(object sender, EventArgs e)
        {
            // Toggle menu con Kho hàng
            pnlStorageSubmenu.Visible = !pnlStorageSubmenu.Visible;
            // Ẩn các menu khác
            pnlProductSubmenu.Visible = false;
            pnlEmployeeSubmenu.Visible = false;
            pnlCustomerSubmenu.Visible = false;
            pnlSupplierSubmenu.Visible = false;
            SetActiveButton(btn_inventory, null);
        }

        // --- Click cho các nút menu con của Kho hàng ---

        private void btnSub_NhapKho_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new frmInventoryMain());
            SetActiveButton(btn_inventory, btnSub_NhapKho);
        }

        private void btnSub_TonKho_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new frmInventoryList());
            SetActiveButton(btn_inventory, btnSub_TonKho);
        }

      
    }
}