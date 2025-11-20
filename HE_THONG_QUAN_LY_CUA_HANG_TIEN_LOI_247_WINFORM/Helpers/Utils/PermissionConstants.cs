namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Utils
{
    /// <summary>
    /// Constants cho các permissions trong hệ thống
    /// </summary>
    public static class PermissionConstants
    {
        // Dashboard
        public const string VIEW_DASHBOARD = "VIEW_DASHBOARD";
        
        // Sản phẩm
        public const string VIEW_PRODUCTS = "VIEW_PRODUCTS";
        public const string ADD_PRODUCTS = "ADD_PRODUCTS";
        public const string EDIT_PRODUCTS = "EDIT_PRODUCTS";
        public const string DELETE_PRODUCTS = "DELETE_PRODUCTS";
        
        // Danh mục
        public const string VIEW_CATEGORIES = "VIEW_CATEGORIES";
        public const string MANAGE_CATEGORIES = "MANAGE_CATEGORIES";
        
        // Nhãn hiệu
        public const string VIEW_BRANDS = "VIEW_BRANDS";
        public const string MANAGE_BRANDS = "MANAGE_BRANDS";
        
        // Đơn vị tính
        public const string VIEW_UNITS = "VIEW_UNITS";
        public const string MANAGE_UNITS = "MANAGE_UNITS";
        
        // Hóa đơn
        public const string VIEW_INVOICES = "VIEW_INVOICES";
        public const string CREATE_INVOICES = "CREATE_INVOICES";
        public const string EDIT_INVOICES = "EDIT_INVOICES";
        public const string DELETE_INVOICES = "DELETE_INVOICES";
        public const string APPROVE_INVOICES = "APPROVE_INVOICES";
        
        // Nhân viên
        public const string VIEW_EMPLOYEES = "VIEW_EMPLOYEES";
        public const string ADD_EMPLOYEES = "ADD_EMPLOYEES";
        public const string EDIT_EMPLOYEES = "EDIT_EMPLOYEES";
        public const string DELETE_EMPLOYEES = "DELETE_EMPLOYEES";
        public const string MANAGE_WORK_SCHEDULE = "MANAGE_WORK_SCHEDULE";
        
        // Khách hàng
        public const string VIEW_CUSTOMERS = "VIEW_CUSTOMERS";
        public const string ADD_CUSTOMERS = "ADD_CUSTOMERS";
        public const string EDIT_CUSTOMERS = "EDIT_CUSTOMERS";
        public const string DELETE_CUSTOMERS = "DELETE_CUSTOMERS";
        public const string VIEW_MEMBER_CARDS = "VIEW_MEMBER_CARDS";
        public const string MANAGE_MEMBER_CARDS = "MANAGE_MEMBER_CARDS";
        
        // Khuyến mãi
        public const string VIEW_PROMOTIONS = "VIEW_PROMOTIONS";
        public const string ADD_PROMOTIONS = "ADD_PROMOTIONS";
        public const string EDIT_PROMOTIONS = "EDIT_PROMOTIONS";
        public const string DELETE_PROMOTIONS = "DELETE_PROMOTIONS";
        
        // Nhà cung cấp
        public const string VIEW_SUPPLIERS = "VIEW_SUPPLIERS";
        public const string ADD_SUPPLIERS = "ADD_SUPPLIERS";
        public const string EDIT_SUPPLIERS = "EDIT_SUPPLIERS";
        public const string DELETE_SUPPLIERS = "DELETE_SUPPLIERS";
        public const string VIEW_SUPPLIER_TRANSACTIONS = "VIEW_SUPPLIER_TRANSACTIONS";
        
        // Kho hàng
        public const string VIEW_INVENTORY = "VIEW_INVENTORY";
        public const string MANAGE_STOCK_IN = "MANAGE_STOCK_IN";
        public const string MANAGE_STOCK_OUT = "MANAGE_STOCK_OUT";
        public const string VIEW_STOCK_REPORT = "VIEW_STOCK_REPORT";
        
        // Báo cáo
        public const string VIEW_SALES_REPORT = "VIEW_SALES_REPORT";
        public const string VIEW_REVENUE_REPORT = "VIEW_REVENUE_REPORT";
        public const string VIEW_INVENTORY_REPORT = "VIEW_INVENTORY_REPORT";
        public const string EXPORT_REPORTS = "EXPORT_REPORTS";
        
        // Hệ thống
        public const string MANAGE_USERS = "MANAGE_USERS";
        public const string MANAGE_ROLES = "MANAGE_ROLES";
        public const string MANAGE_PERMISSIONS = "MANAGE_PERMISSIONS";
        public const string VIEW_ACTIVITY_LOG = "VIEW_ACTIVITY_LOG";
        public const string MANAGE_SYSTEM_SETTINGS = "MANAGE_SYSTEM_SETTINGS";
    }

    /// <summary>
    /// Constants cho các roles trong hệ thống
    /// </summary>
    public static class RoleConstants
    {
        public const string ADMIN = "Admin";
        public const string MANAGER = "MANAGER";
        public const string CASHIER = "CASHIER";
        public const string WAREHOUSE_STAFF = "WAREHOUSE_STAFF";
        public const string SALES_STAFF = "SALES_STAFF";
    }
}
