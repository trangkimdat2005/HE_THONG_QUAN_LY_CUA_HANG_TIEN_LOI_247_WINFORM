using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Bills;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Customers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Employees;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Inventory;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Products;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Suppliers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Promotions;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.Forms.Employees;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Reports;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.Forms.Inventory;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.Forms.Products;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Main;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Inventory;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Utils;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Main
{
    public partial class frmMain : Form
    {
        private Form currentChildForm;
        
        // Cache forms để tránh khởi tạo lại nhiều lần
        private Dictionary<Type, Form> formCache = new Dictionary<Type, Form>();

        // Menu dropdown cho account
        private ContextMenuStrip accountMenu;
        private System.Windows.Forms.ToolTip accountTooltip;

        public frmMain()
        {
            InitializeComponent();

            // Phóng to cửa sổ khi khởi động (vẫn có title bar và nút minimize/maximize/close)
            this.WindowState = FormWindowState.Maximized;
            
            // Khởi tạo account menu và tooltip
            InitializeAccountMenu();
            InitializeAccountTooltip();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Hiển thị thông tin user đã đăng nhập
            UpdateUserInfo();
            
            // Load Dashboard mặc định khi mở chương trình
            LoadFormIntoPanel(new frmDashboard());
            SetActiveButton(btn_dashboard, null); // Highlight nút Dashboard
        }

        /// <summary>
        /// Khởi tạo tooltip cho nút account
        /// </summary>
        private void InitializeAccountTooltip()
        {
            accountTooltip = new System.Windows.Forms.ToolTip();
            accountTooltip.AutoPopDelay = 5000;
            accountTooltip.InitialDelay = 500;
            accountTooltip.ReshowDelay = 500;
            accountTooltip.ShowAlways = true;
        }

        /// <summary>
        /// Khởi tạo menu dropdown cho nút account
        /// </summary>
        private void InitializeAccountMenu()
        {
            accountMenu = new ContextMenuStrip();
            accountMenu.ShowImageMargin = true;
            accountMenu.AutoSize = true;
            
            // Menu item: Thông tin tài khoản
            ToolStripMenuItem profileItem = new ToolStripMenuItem("👤 Thông tin tài khoản");
            profileItem.Click += ProfileItem_Click;
            profileItem.Font = new Font("Segoe UI", 10F);
            
            //// Menu item: Đổi mật khẩu
            //ToolStripMenuItem changePasswordItem = new ToolStripMenuItem("🔑 Đổi mật khẩu");
            //changePasswordItem.Click += ChangePasswordItem_Click;
            //changePasswordItem.Font = new Font("Segoe UI", 10F);
            
            // Menu item: Cài đặt
            ToolStripMenuItem settingsItem = new ToolStripMenuItem("⚙️ Cài đặt");
            settingsItem.Click += SettingsItem_Click;
            settingsItem.Font = new Font("Segoe UI", 10F);
            
            // Separator
            ToolStripSeparator separator = new ToolStripSeparator();
            
            // Menu item: Đăng xuất
            ToolStripMenuItem logoutItem = new ToolStripMenuItem("🚪 Đăng xuất");
            logoutItem.Click += LogoutItem_Click;
            logoutItem.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            logoutItem.ForeColor = Color.Red;
            
            // Thêm vào menu
            accountMenu.Items.Add(profileItem);
            //accountMenu.Items.Add(changePasswordItem);
            accountMenu.Items.Add(settingsItem);
            accountMenu.Items.Add(separator);
            accountMenu.Items.Add(logoutItem);
            
            // Style cho menu
            accountMenu.RenderMode = ToolStripRenderMode.Professional;
            accountMenu.Renderer = new ToolStripProfessionalRenderer(new MenuColorTable());
        }

        /// <summary>
        /// Cập nhật thông tin user hiển thị trên header
        /// </summary>
        private void UpdateUserInfo()
        {
            var session = UserSession.Instance;
            if (session.IsLoggedIn)
            {
                // Hiển thị tên viết tắt trên button
                btn_account.Text = session.GetDisplayName();
                
                // Set tooltip với thông tin đầy đủ
                string tooltipText = $"👤 {session.EmployeeName}\n" +
                                   $"💼 {session.Position}\n" +
                                   $"📧 {session.Email ?? "Chưa có email"}\n" +
                                   $"📱 {session.PhoneNumber}\n" +
                                   $"🎭 {session.Role}";
                
                accountTooltip.SetToolTip(btn_account, tooltipText);
            }
            else
            {
                btn_account.Text = "👤";
                accountTooltip.SetToolTip(btn_account, "Chưa đăng nhập");
            }
        }

        private void ProfileItem_Click(object sender, EventArgs e)
        {
            var session = UserSession.Instance;
            if (!session.IsLoggedIn)
            {
                MessageBox.Show("Bạn chưa đăng nhập!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mở form Profile
            frmProfile profileForm = new frmProfile();
            profileForm.ShowDialog();
        }

        //private void ChangePasswordItem_Click(object sender, EventArgs e)
        //{
        //    // TODO: Mở form đổi mật khẩu
        //    MessageBox.Show("Chức năng đổi mật khẩu đang được phát triển!", 
        //        "Thông báo", 
        //        MessageBoxButtons.OK, 
        //        MessageBoxIcon.Information);
        //}

        private void SettingsItem_Click(object sender, EventArgs e)
        {
            // TODO: Mở form cài đặt
            MessageBox.Show("Chức năng cài đặt đang được phát triển!", 
                "Thông báo", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Information);
        }

        private void LogoutItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất?\n\n" +
                "Tất cả dữ liệu chưa lưu sẽ bị mất!",
                "⚠️ Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Logout();
            }
        }

        /// <summary>
        /// Xử lý đăng xuất
        /// </summary>
        private void Logout()
        {
            try
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
                
                // Xóa session
                UserSession.Instance.ClearSession();
                
                // Đóng form main
                this.Hide();
                
                // Hiển thị lại form login
                frmLogin loginForm = new frmLogin();
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Nếu đăng nhập thành công, hiển thị lại main form
                    UpdateUserInfo();
                    LoadFormIntoPanel(new frmDashboard());
                    SetActiveButton(btn_dashboard, null);
                    this.Show();
                }
                else
                {
                    // Nếu không đăng nhập, đóng ứng dụng
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đăng xuất: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            LogoutItem_Click(sender, e);
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
            pnlStorageSubmenu.Visible = false;
            SetActiveButton(btn_products, null);
        }
        private void btn_reports_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new frmSalesReport());
            HideSubMenus();
            SetActiveButton(btn_reports, null);
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

        private void btnSub_Barcode_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new frmBarcode());
            SetActiveButton(btn_products, btnSub_Barcode);
        }

        // --- Click cho các nút menu con của Nhân viên ---

        private void btnSub_DanhSachNV_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new frmEmployeeList());
            SetActiveButton(btn_employees, btnSub_DanhSachNV);
        }

        private void btnSub_PhanCong_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new frmShiftList());
            SetActiveButton(btn_employees, btnSub_PhanCong);
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
            pnlStorageSubmenu.Visible = false;
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
            pnlStorageSubmenu.Visible = false;

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
            pnlStorageSubmenu.Visible = false;

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

        private void btn_show_Click(object sender, EventArgs e)
        {
            //load side bar
            if(pnlSidebar.Visible == true)
                pnlSidebar.Visible = false;
            else
                pnlSidebar.Visible = true;
            HideSubMenus();

        }

        private void flpMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_account_Click(object sender, EventArgs e)
        {
            // Hiển thị menu dropdown tại vị trí nút account
            Point menuLocation = btn_account.PointToScreen(new Point(0, btn_account.Height));
            accountMenu.Show(menuLocation);
        }

        private void pnlSidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlProductSubmenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlEmployeeSubmenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlCustomerSubmenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlSupplierSubmenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlStorageSubmenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlUserInfo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }
    }

    // Custom color table cho menu
    public class MenuColorTable : ProfessionalColorTable
    {
        public override Color MenuItemSelected
        {
            get { return Color.FromArgb(230, 240, 255); }
        }

        public override Color MenuItemSelectedGradientBegin
        {
            get { return Color.FromArgb(230, 240, 255); }
        }

        public override Color MenuItemSelectedGradientEnd
        {
            get { return Color.FromArgb(210, 230, 255); }
        }

        public override Color MenuItemBorder
        {
            get { return Color.FromArgb(94, 148, 255); }
        }
    }
}