using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Utils;
using System;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Examples
{
    /// <summary>
    /// Ví dụ về cách sử dụng phân quyền trong form
    /// </summary>
    public partial class ExampleFormWithPermissions : Form
    {
        public ExampleFormWithPermissions()
        {
            InitializeComponent();
        }

        private void ExampleForm_Load(object sender, EventArgs e)
        {
            // Cách 1: Disable button nếu không có quyền
            btnAdd.SetPermission(PermissionConstants.ADD_PRODUCTS);
            btnEdit.SetPermission(PermissionConstants.EDIT_PRODUCTS);
            btnDelete.SetPermission(PermissionConstants.DELETE_PRODUCTS);

            // Cách 2: Hide button nếu không có quyền
            btnExport.HideIfNoPermission(PermissionConstants.EXPORT_REPORTS);

            // Cách 3: Kiểm tra role
            if (PermissionExtensions.CheckRole(RoleConstants.ADMIN, showMessage: false))
            {
                // Hiển thị menu quản trị
                pnlAdminMenu.Visible = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Kiểm tra permission trước khi thực hiện
            if (!PermissionExtensions.CheckPermission(PermissionConstants.ADD_PRODUCTS))
                return;

            // Thực hiện thêm sản phẩm
            AddProduct();

            // Log activity
            PermissionExtensions.LogActivity("Thêm sản phẩm");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Kiểm tra permission
            if (!PermissionExtensions.CheckPermission(PermissionConstants.EDIT_PRODUCTS))
                return;

            EditProduct();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Chỉ Admin và Manager mới có quyền xóa
            if (!PermissionExtensions.HasAnyPermission(
                PermissionConstants.DELETE_PRODUCTS))
            {
                MessageBox.Show("Chỉ Admin và Manager mới có quyền xóa sản phẩm!");
                return;
            }

            // Xác nhận trước khi xóa
            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DeleteProduct();
                PermissionExtensions.LogActivity("Xóa sản phẩm");
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            // Chỉ Manager trở lên mới có quyền approve
            if (!PermissionExtensions.CheckRole(RoleConstants.MANAGER))
            {
                MessageBox.Show("Chỉ Manager mới có quyền phê duyệt!");
                return;
            }

            ApproveInvoice();
        }

        // Dummy methods
        private void AddProduct() { }
        private void EditProduct() { }
        private void DeleteProduct() { }
        private void ApproveInvoice() { }

        // Dummy controls
        private Button btnAdd = new Button();
        private Button btnEdit = new Button();
        private Button btnDelete = new Button();
        private Button btnExport = new Button();
        private Panel pnlAdminMenu = new Panel();
    }
}
