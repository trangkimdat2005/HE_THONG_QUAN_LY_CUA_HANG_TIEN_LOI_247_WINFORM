using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Invoice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Bills
{
    public partial class frmInvoiceDetails : Form
    {
        // Helper for combobox items to ensure SelectedValue is a string
        private class ComboItem
        {
            public string id { get; set; }
            public string ten { get; set; }
        }

        private InvoiceController _invoiceController;
        private CustomerController _customerController;
        private ProductController _productController;
        private CategoryController _categoryController;

        private string _invoiceId;
        private List<ChiTietHoaDon> _invoiceDetails;
        private bool _isEditMode = false;
        private readonly IQuanLyServices _quanLyServices = new QuanLyServices();

        // store applied promo ids and computed promo value per row
        private Dictionary<int, List<string>> _appliedPromosPerRow = new Dictionary<int, List<string>>();
        private Dictionary<int, decimal> _promoValuePerRow = new Dictionary<int, decimal>();

        public frmInvoiceDetails()
        {
            InitializeComponent();
            _invoiceController = new InvoiceController();
            _customerController = new CustomerController();
            _product_controller = new ProductController();
            _category_controller = new CategoryController();
            _invoiceDetails = new List<ChiTietHoaDon>();
            CustomizeInterface();
        }

        // Note: small naming fixes to match original fields
        private ProductController _product_controller { get { return _productController; } set { _productController = value; } }
        private CategoryController _category_controller { get { return _categoryController; } set { _categoryController = value; } }

        private void CustomizeInterface()
        {
            // Style cho bảng Chi tiết hóa đơn (Bên trái)
            StyleGrid(dgvInvoiceDetails);
            dgvInvoiceDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185); // Xanh Dương

            // Style cho bảng Sản phẩm (Bên phải)
            StyleGrid(dgvProducts);
            dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(39, 174, 96); // Xanh Lá


        }
        private void StyleGrid(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(230, 230, 230);
            dgv.RowHeadersVisible = false;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeight = 40;
            dgv.RowTemplate.Height = 40;

            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(211, 233, 252);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgv.DefaultCellStyle.Padding = new Padding(5, 0, 0, 0);
        }
        public frmInvoiceDetails(string invoiceId) : this()
        {
            _invoiceId = invoiceId;
            _isEditMode = true;
        }

        private void frmInvoiceDetails_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(frmChiTietHoaDon_KeyDown);
            InitializeUI();
            LoadCustomers();
            LoadCategories();
            LoadProducts();

            if (_isEditMode && !string.IsNullOrEmpty(_invoiceId))
            {
                LoadInvoiceData();
            }
        }
        private void frmChiTietHoaDon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.H)
            {
                btnCancel.PerformClick();
                e.SuppressKeyPress = true;
            }

            if (e.Control && e.KeyCode == Keys.S)
            {
                btnScan.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
        private void InitializeUI()
        {
            // Setup DataGridViews
            dgvInvoiceDetails.AutoGenerateColumns = false;
            dgvProducts.AutoGenerateColumns = false;

            // Disable payment button initially
            btnPayment.Enabled = false;
        }

        private void LoadCustomers()
        {
            try
            {
                var customers = _customerController.GetAllCustomers();

                cmbCustomer.DataSource = customers;
                cmbCustomer.DisplayMember = "hoTen";
                cmbCustomer.ValueMember = "id";

                if (customers.Count > 0)
                {
                    cmbCustomer.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách khách hàng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategories()
        {
            try
            {
                var categories = _categoryController.GetAllCategories();

                var categoryList = new List<ComboItem>();
                categoryList.Add(new ComboItem { id = "", ten = "Tất cả" });

                foreach (var cat in categories)
                {
                    string cid = null;
                    string cten = null;
                    try { cid = cat.id; } catch { try { cid = cat.Id; } catch { } }
                    try { cten = cat.ten; } catch { try { cten = cat.Ten; } catch { } }

                    if (string.IsNullOrEmpty(cid) && cat is DanhMuc dm) { cid = dm.id; cten = dm.ten; }

                    categoryList.Add(new ComboItem { id = cid ?? "", ten = cten ?? "" });
                }

                cmbCategory.DataSource = categoryList;
                cmbCategory.DisplayMember = "ten";
                cmbCategory.ValueMember = "id";
                cmbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh mục: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts(string keyword = null, string categoryId = null)
        {
            try
            {
                var products = _productController.FilterProducts(keyword, null, categoryId);

                dgvProducts.Rows.Clear();
                foreach (var product in products)
                {
                    // Designer has hidden colProductId as first column — put product.Id there
                    dgvProducts.Rows.Add(
                        product.Id,
                        product.Ten,
                        product.DanhMuc,
                        product.DonVi,
                        product.GiaBan.ToString("N0") + " đ"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải sản phẩm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInvoiceData()
        {
            try
            {
                var invoice = _invoiceController.GetInvoiceById(_invoiceId);
                if (invoice == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Load invoice info
                lblInvoiceId.Text = invoice.id;
                cmbCustomer.SelectedValue = invoice.khachHangId;

                // Load invoice details
                _invoiceDetails = _invoiceController.GetInvoiceDetails(_invoiceId);
                
                // ✅ FIX: Load applied promotions BEFORE displaying
                LoadAppliedPromotions();
                
                DisplayInvoiceDetails();
                CalculateTotal();

                // Disable editing if already paid
                if (invoice.trangThai == "Đã thanh toán")
                {
                    SetReadOnlyMode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải hóa đơn: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// ✅ Load khuyến mãi đã áp dụng từ database
        /// </summary>
        private void LoadAppliedPromotions()
        {
            try
            {
                _appliedPromosPerRow.Clear();
                _promoValuePerRow.Clear();

                using (var ctx = new AppDbContext())
                {
                    // Lấy tất cả khuyến mãi đã áp dụng cho hóa đơn này
                    var appliedPromos = ctx.ChiTietHoaDonKhuyenMais
                        .Include(c => c.MaKhuyenMai)
                        .Where(c => c.hoaDonId == _invoiceId && !c.isDelete)
                        .ToList();

                    if (appliedPromos == null || appliedPromos.Count == 0)
                    {
                        return; // Không có khuyến mãi
                    }

                    // Nhóm theo sanPhamDonViId để map với từng dòng trong chi tiết hóa đơn
                    var promoGroups = appliedPromos.GroupBy(p => p.sanPhamDonViId).ToList();

                    foreach (var group in promoGroups)
                    {
                        var sanPhamDonViId = group.Key;
                        
                        // Tìm row index cho sản phẩm này
                        int rowIndex = FindRowIndexBySanPhamDonViId(sanPhamDonViId);
                        
                        if (rowIndex >= 0)
                        {
                            // Lưu các mã khuyến mãi cho row này
                            var promoCodes = new List<string>();
                            decimal totalPromoValue = 0m;

                            foreach (var promo in group)
                            {
                                if (promo.MaKhuyenMai != null)
                                {
                                    promoCodes.Add(promo.MaKhuyenMai.code);
                                    totalPromoValue += promo.giaTriApDung;
                                }
                            }

                            _appliedPromosPerRow[rowIndex] = promoCodes;
                            _promoValuePerRow[rowIndex] = totalPromoValue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error nhưng không fail cả quá trình load
                System.Diagnostics.Debug.WriteLine($"Error loading applied promotions: {ex.Message}");
            }
        }

        /// <summary>
        /// Helper: Tìm row index theo SanPhamDonViId
        /// </summary>
        private int FindRowIndexBySanPhamDonViId(string sanPhamDonViId)
        {
            for (int i = 0; i < _invoiceDetails.Count; i++)
            {
                if (_invoiceDetails[i].sanPhamDonViId == sanPhamDonViId)
                {
                    return i;
                }
            }
            return -1;
        }

        private void DisplayInvoiceDetails()
        {
            dgvInvoiceDetails.Rows.Clear();

            using (var ctx = new AppDbContext())
            {
                for (int i = 0; i < _invoiceDetails.Count; i++)
                {
                    var detail = _invoiceDetails[i];
                    string productName = detail.sanPhamDonViId;
                    string unitName = "";
                    string sanPhamDonViId = detail.sanPhamDonViId;

                    try
                    {
                        var spdv = ctx.SanPhamDonVis
                            .Include(s => s.SanPham)
                            .Include(s => s.DonViDoLuong)
                            .FirstOrDefault(s => s.id == detail.sanPhamDonViId && !s.isDelete);

                        if (spdv != null)
                        {
                            productName = spdv.SanPham?.ten ?? detail.sanPhamDonViId;
                            unitName = spdv.DonViDoLuong?.ten ?? "";
                            sanPhamDonViId = spdv.id;
                        }
                    }
                    catch { }

                    var unitPriceStr = detail.donGia.ToString("N0") + " đ";
                    
                    // ✅ FIX: Tính tổng tiền có áp dụng khuyến mãi (nếu có)
                    decimal lineTotal = detail.soLuong * detail.donGia;
                    if (_promoValuePerRow.ContainsKey(i))
                    {
                        lineTotal -= _promoValuePerRow[i];
                        if (lineTotal < 0) lineTotal = 0;
                    }
                    
                    var totalStr = lineTotal.ToString("N0") + " đ";

                    // Add row: hidden sanPhamDonViId in first column
                    dgvInvoiceDetails.Rows.Add(
                        sanPhamDonViId,
                        productName,
                        unitName,
                        detail.soLuong,
                        unitPriceStr,
                        totalStr,
                        null // promo cell sẽ được populate sau
                    );

                    int rowIndex = dgvInvoiceDetails.Rows.Count - 1;
                    
                    // Populate promo options cho row này
                    if (!string.IsNullOrEmpty(sanPhamDonViId))
                    {
                        PopulatePromoComboForRow(rowIndex, sanPhamDonViId);
                        
                        // ✅ FIX: Set giá trị khuyến mãi đã chọn (nếu có)
                        if (_appliedPromosPerRow.ContainsKey(i) && _appliedPromosPerRow[i].Count > 0)
                        {
                            try
                            {
                                var promoCode = _appliedPromosPerRow[i][0]; // Lấy mã khuyến mãi đầu tiên
                                
                                // Tìm ID của MaKhuyenMai theo code
                                var maKM = ctx.MaKhuyenMais.FirstOrDefault(m => m.code == promoCode && !m.isDelete);
                                if (maKM != null)
                                {
                                    var comboCell = dgvInvoiceDetails.Rows[rowIndex].Cells["colPromo"] as DataGridViewComboBoxCell;
                                    if (comboCell != null && comboCell.Items.Count > 0)
                                    {
                                        // Set giá trị đã chọn
                                        comboCell.Value = maKM.id;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Error setting promo value for row {rowIndex}: {ex.Message}");
                            }
                        }
                    }
                }
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow != null)
            {
                var productIdHidden = dgvProducts.CurrentRow.Cells["colProductId"].Value?.ToString();
                var productName = dgvProducts.CurrentRow.Cells["colProductName"].Value?.ToString();
                var unitName = dgvProducts.CurrentRow.Cells["colUnitProduct"].Value?.ToString();
                var priceStr = dgvProducts.CurrentRow.Cells["colPrice"].Value?.ToString();

                if (string.IsNullOrEmpty(productName)) return;

                var price = decimal.Parse(priceStr.Replace(" đ", "").Replace(",", ""));

                // resolve sanPhamDonViId
                string sanPhamDonViId = null;
                using (var ctx = new AppDbContext())
                {
                    var spdv = ctx.SanPhamDonVis
                        .Include(s => s.SanPham)
                        .Include(s => s.DonViDoLuong)
                        .FirstOrDefault(s => (s.SanPham.ten == productName || s.sanPhamId == productIdHidden) && s.DonViDoLuong.ten == unitName && !s.isDelete);
                    if (spdv != null) sanPhamDonViId = spdv.id;
                }

                // check existing
                var existingRow = -1;
                for (int i = 0; i < dgvInvoiceDetails.Rows.Count; i++)
                {
                    if (dgvInvoiceDetails.Rows[i].Cells["colProduct"].Value?.ToString() == productName
                        && dgvInvoiceDetails.Rows[i].Cells["colUnitInvoice"].Value?.ToString() == unitName)
                    {
                        existingRow = i; break;
                    }
                }

                if (existingRow >= 0)
                {
                    var currentQty = int.Parse(dgvInvoiceDetails.Rows[existingRow].Cells["colQuantity"].Value.ToString());
                    dgvInvoiceDetails.Rows[existingRow].Cells["colQuantity"].Value = currentQty + 1;
                    var newTotal = (currentQty + 1) * price;
                    dgvInvoiceDetails.Rows[existingRow].Cells["colTotal"].Value = newTotal.ToString("N0") + " đ";
                }
                else
                {
                    dgvInvoiceDetails.Rows.Add(
                        sanPhamDonViId ?? "",
                        productName,
                        unitName,
                        1,
                        priceStr,
                        price.ToString("N0") + " đ",
                        null
                    );

                    int newRow = dgvInvoiceDetails.Rows.Count - 1;
                    if (!string.IsNullOrEmpty(sanPhamDonViId))
                        PopulatePromoComboForRow(newRow, sanPhamDonViId);
                }

                CalculateTotal();
            }
        }

        private void btnRemoveProduct_Click(object sender, EventArgs e)
        {
            if (dgvInvoiceDetails.CurrentRow != null)
            {
                dgvInvoiceDetails.Rows.Remove(dgvInvoiceDetails.CurrentRow);
                CalculateTotal();
            }
        }

        private void dgvInvoiceDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvInvoiceDetails.Columns["colQuantity"].Index)
            {
                UpdateRowTotal(e.RowIndex);
                CalculateTotal();
            }

            // handle promo selection change if combobox cell changed
            if (e.RowIndex >= 0 && dgvInvoiceDetails.Columns[e.ColumnIndex].Name == "colPromo")
            {
                try
                {
                    var row = dgvInvoiceDetails.Rows[e.RowIndex];
                    var cell = row.Cells["colPromo"] as DataGridViewComboBoxCell;
                    if (cell != null)
                    {
                        var selectedId = cell.Value?.ToString();
                        if (string.IsNullOrEmpty(selectedId))
                        {
                            if (_appliedPromosPerRow.ContainsKey(e.RowIndex)) _appliedPromosPerRow.Remove(e.RowIndex);
                            _promoValuePerRow[e.RowIndex] = 0;
                            UpdateRowTotal(e.RowIndex);
                            CalculateTotal();
                            return;
                        }

                        using (var ctx = new AppDbContext())
                        {
                            var mk = ctx.MaKhuyenMais.FirstOrDefault(m => m.id == selectedId && !m.isDelete && m.trangThai == "Active");
                            if (mk != null)
                            {
                                var unitPrice = decimal.Parse(row.Cells["colUnitPrice"].Value.ToString().Replace(" đ", "").Replace(",", ""));
                                var qty = int.Parse(row.Cells["colQuantity"].Value?.ToString() ?? "0");
                                decimal applied = 0;
                                if (mk.giaTri > 0 && mk.giaTri < 1) applied = unitPrice * qty * mk.giaTri; else applied = mk.giaTri;
                                _promoValuePerRow[e.RowIndex] = applied;
                                _appliedPromosPerRow[e.RowIndex] = new List<string> { mk.code };

                                var newTotal = (unitPrice * qty) - applied;
                                if (newTotal < 0) newTotal = 0;
                                row.Cells["colTotal"].Value = newTotal.ToString("N0") + " đ";
                                CalculateTotal();
                            }
                        }
                    }
                }
                catch { }
            }
        }

        private void UpdateRowTotal(int rowIndex)
        {
            try
            {
                var row = dgvInvoiceDetails.Rows[rowIndex];
                var quantity = int.Parse(row.Cells["colQuantity"].Value?.ToString() ?? "0");
                var priceStr = row.Cells["colUnitPrice"].Value?.ToString() ?? "0";
                var price = decimal.Parse(priceStr.Replace(" đ", "").Replace(",", ""));

                decimal promo = 0;
                if (_promoValuePerRow.TryGetValue(rowIndex, out decimal pv)) promo = pv;

                var total = quantity * price - promo;
                if (total < 0) total = 0;
                row.Cells["colTotal"].Value = total.ToString("N0") + " đ";
            }
            catch { }
        }

        private void CalculateTotal()
        {
            decimal subtotal = 0;

            foreach (DataGridViewRow row in dgvInvoiceDetails.Rows)
            {
                if (row.Cells["colTotal"].Value != null)
                {
                    var totalStr = row.Cells["colTotal"].Value.ToString();
                    subtotal += decimal.Parse(totalStr.Replace(" đ", "").Replace(",", ""));
                }
            }

            decimal discount = ReadDiscountFromControl();
            if (discount > subtotal)
            {
                discount = subtotal;
                SetDiscountTextControl(discount);
            }

            var total = subtotal - discount;

            lblSubtotal.Text = subtotal.ToString("N0") + " đ";
            lblDiscountValue.Text = discount.ToString("N0") + " đ";
            lblTotal.Text = total.ToString("N0") + " đ";

            btnPayment.Enabled = dgvInvoiceDetails.Rows.Count > 0;
        }

        private void btnApplyDiscount_Click(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        // Search by barcode (MaDinhDanhSanPham) when typing
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var keyword = txtSearch.Text?.Trim();

            // If empty show all products
            if (string.IsNullOrEmpty(keyword))
            {
                LoadProducts(null);
                return;
            }

            try
            {
                using (var ctx = new AppDbContext())
                {
                    // Find MaDinhDanh entries matching the entered code (contains)
                    var matches = ctx.MaDinhDanhSanPhams
                        .Where(m => !m.isDelete && m.maCode.Contains(keyword))
                        .Select(m => m.sanPhamDonViId)
                        .Distinct()
                        .ToList();

                    dgvProducts.Rows.Clear();

                    if (matches.Count ==0)
                    {
                        // no barcode matches -> show empty list
                        return;
                    }

                    // Load SanPhamDonVi for matched ids
                    var spdvs = ctx.SanPhamDonVis
                        .Include(s => s.SanPham)
                        .Include(s => s.DonViDoLuong)
                        .Where(s => matches.Contains(s.id) && !s.isDelete)
                        .ToList();

                    foreach (var spdv in spdvs)
                    {
                        string prodId = spdv.sanPhamId ?? spdv.id; // prefer sanPhamId for product Id column
                        string prodName = spdv.SanPham?.ten ?? "";
                        string category = ""; // category not resolved here
                        string unit = spdv.DonViDoLuong?.ten ?? "";

                        // try to get price from known property names safely
                        string priceStr = "0 đ";
                        try
                        {
                            var priceProp = spdv.GetType().GetProperty("giaBan") ?? spdv.GetType().GetProperty("GiaBan") ?? spdv.GetType().GetProperty("gia_ban");
                            if (priceProp != null)
                            {
                                var priceVal = priceProp.GetValue(spdv);
                                if (priceVal != null && decimal.TryParse(priceVal.ToString(), out decimal p))
                                {
                                    priceStr = p.ToString("N0") + " đ";
                                }
                            }
                        }
                        catch { }

                        dgvProducts.Rows.Add(
                            prodId,
                            prodName,
                            category,
                            unit,
                            priceStr
                        );
                    }
                }
            }
            catch
            {
                // on error, do nothing (leave list as-is)
            }
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategory.SelectedValue == null) return;
            var raw = cmbCategory.SelectedValue;
            string categoryId = raw as string ?? raw.ToString();
            if (string.IsNullOrWhiteSpace(categoryId)) categoryId = null;
            LoadProducts(txtSearch.Text, categoryId);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn hủy?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            ProcessPayment();
        }

        private void ProcessPayment()
        {
            try
            {
                if (dgvInvoiceDetails.Rows.Count == 0)
                {
                    MessageBox.Show("Hóa đơn chưa có sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var totalText = lblTotal.Text.Replace(" đ", "").Replace(",", "").Trim();
                if (!decimal.TryParse(totalText, out decimal totalAmount))
                {
                    MessageBox.Show("Không thể xác định tổng tiền", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var paymentForm = new HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Invoice.frmPayment(totalAmount);
                paymentForm.Owner = this;
                paymentForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xử lý thanh toán: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SaveInvoiceFromPayment(decimal customerPay, string paymentMethodName, string note)
        {
            try
            {
                using (var ctx = new AppDbContext())
                {
                    using (var transaction = ctx.Database.BeginTransaction())
                    {
                        try
                        {
                            HoaDon invoice;
                            bool isNewInvoice = string.IsNullOrEmpty(_invoiceId);

                            string paymentMethodId = null;
                            var allMethods = ctx.KenhThanhToans
                                .Where(k => !k.isDelete)
                                .ToList();

                            var method = allMethods.FirstOrDefault(k =>
                                (k.tenKenh ?? "").IndexOf(paymentMethodName ?? "", StringComparison.CurrentCultureIgnoreCase) >= 0
                            );

                            if (method != null)
                            {
                                paymentMethodId = method.id;
                            }
                            else
                            {
                                var anyMethod = allMethods.FirstOrDefault();
                                if (anyMethod != null)
                                {
                                    paymentMethodId = anyMethod.id;
                                    paymentMethodName = anyMethod.tenKenh;
                                }
                            }

                            if (string.IsNullOrEmpty(paymentMethodId))
                            {
                                MessageBox.Show("Không tìm thấy phương thức thanh toán trong hệ thống.\nVui lòng thêm phương thức thanh toán trước khi tạo hóa đơn.",
                                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            if (isNewInvoice)
                            {
                                var newInvoiceId = _quanLyServices.GenerateNewId<HoaDon>("HD", 6);
                                int retryCount = 0;
                                while (ctx.HoaDons.Any(h => h.id == newInvoiceId) && retryCount < 10)
                                {
                                    newInvoiceId = _quanLyServices.GenerateNewId<HoaDon>("HD", 6);
                                    retryCount++;
                                }

                                if (string.IsNullOrEmpty(newInvoiceId) || ctx.HoaDons.Any(h => h.id == newInvoiceId))
                                {
                                    MessageBox.Show("Không thể tạo mã hóa đơn duy nhất", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                var details = BuildInvoiceDetails(newInvoiceId);
                                if (details == null)
                                {
                                    transaction.Rollback();
                                    return;
                                }

                                decimal calculatedTotal = details.Sum(d => d.tongTien);

                                invoice = new HoaDon()
                                {
                                    id = newInvoiceId,
                                    ngayLap = DateTime.Now,
                                    khachHangId = cmbCustomer.SelectedValue?.ToString() ?? "",
                                    nhanVienId = _quanLyServices.GetList<NhanVien>().FirstOrDefault()?.id ?? "NV001",
                                    isDelete = false,
                                    trangThai = "Chưa thanh toán",
                                    tongTien = calculatedTotal
                                };

                                ctx.HoaDons.Add(invoice);
                                ctx.ChiTietHoaDons.AddRange(details);
                                ctx.SaveChanges();
                                _invoiceId = newInvoiceId;
                            }
                            else
                            {
                                invoice = ctx.HoaDons.FirstOrDefault(h => h.id == _invoiceId && !h.isDelete);
                                if (invoice == null)
                                {
                                    MessageBox.Show($"Không tìm thấy hóa đơn {_invoiceId}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    transaction.Rollback();
                                    return;
                                }

                                if (invoice.trangThai == "Đã thanh toán")
                                {
                                    MessageBox.Show("Hóa đơn này đã được thanh toán rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    transaction.Rollback();
                                    return;
                                }

                                invoice.khachHangId = cmbCustomer.SelectedValue?.ToString() ?? invoice.khachHangId;
                                invoice.ngayLap = DateTime.Now;

                                var oldDetails = ctx.ChiTietHoaDons.Where(d => d.hoaDonId == _invoiceId).ToList();
                                ctx.ChiTietHoaDons.RemoveRange(oldDetails);

                                var newDetails = BuildInvoiceDetails(_invoiceId);
                                if (newDetails == null)
                                {
                                    transaction.Rollback();
                                    return;
                                }

                                ctx.ChiTietHoaDons.AddRange(newDetails);
                                ctx.SaveChanges();
                            }

                            string transactionId;
                            int maxRetry = 10;
                            int retryCounter = 0;

                            do
                            {
                                transactionId = "GD" + DateTime.Now.ToString("yyMMddHHmmss") + new Random().Next(1000, 9999);
                                retryCounter++;
                            }
                            while (ctx.GiaoDichThanhToans.Any(g => g.id == transactionId) && retryCounter < maxRetry);

                            var payment = new GiaoDichThanhToan
                            {
                                id = transactionId,
                                hoaDonId = invoice.id,
                                soTien = customerPay,
                                ngayGD = DateTime.Now,
                                kenhThanhToanId = paymentMethodId,
                                moTa = string.IsNullOrEmpty(note) ? $"Thanh toán hóa đơn {invoice.id} qua {paymentMethodName}" : note,
                                isDelete = false
                            };

                            ctx.GiaoDichThanhToans.Add(payment);
                            invoice.trangThai = "Đã thanh toán";
                            ctx.Entry(invoice).State = EntityState.Modified;

                            ctx.SaveChanges();

                            var promoDetails = BuildPromoDetails(invoice.id);
                            if (promoDetails != null && promoDetails.Count > 0)
                            {
                                if (!isNewInvoice)
                                {
                                    var oldPromos = ctx.ChiTietHoaDonKhuyenMais.Where(p => p.hoaDonId == invoice.id).ToList();
                                    ctx.ChiTietHoaDonKhuyenMais.RemoveRange(oldPromos);
                                }

                                foreach (var pd in promoDetails)
                                {
                                    pd.id = _quanLyServices.GenerateNewId<ChiTietHoaDonKhuyenMai>("CTHD", 8) ?? Guid.NewGuid().ToString();
                                    ctx.ChiTietHoaDonKhuyenMais.Add(pd);
                                }

                                ctx.SaveChanges();
                            }

                            transaction.Commit();
                            ctx.Entry(invoice).Reload();

                            MessageBox.Show($"Thanh toán thành công!\n\nMã hóa đơn: {invoice.id}\nTổng tiền: {invoice.tongTien?.ToString("N0") ?? "0"} đ\nPhương thức: {paymentMethodName}",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.DialogResult = DialogResult.OK;
                            this.BeginInvoke(new Action(() => { this.Close(); }));
                        }
                        catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                        {
                            transaction.Rollback();
                            var errorMessages = new System.Text.StringBuilder();
                            foreach (var validationErrors in ex.EntityValidationErrors)
                            {
                                foreach (var validationError in validationErrors.ValidationErrors)
                                {
                                    errorMessages.AppendLine($"- {validationError.PropertyName}: {validationError.ErrorMessage}");
                                }
                            }
                            MessageBox.Show($"Lỗi validation:\n{errorMessages.ToString()}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                        {
                            transaction.Rollback();
                            var innerException = ex.InnerException;
                            var errorDetails = new System.Text.StringBuilder();
                            errorDetails.AppendLine("Lỗi database:");

                            while (innerException != null)
                            {
                                errorDetails.AppendLine($"\n{innerException.Message}");
                                innerException = innerException.InnerException;
                            }

                            MessageBox.Show(errorDetails.ToString(), "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var innerMsg = ex.InnerException != null ? $"\nChi tiết: {ex.InnerException.Message}" : "";
                MessageBox.Show($"Lỗi khi lưu hóa đơn:\n{ex.Message}{innerMsg}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<ChiTietHoaDon> BuildInvoiceDetails(string invoiceId)
        {
            var details = new List<ChiTietHoaDon>();

            for (int i = 0; i < dgvInvoiceDetails.Rows.Count; i++)
            {
                var row = dgvInvoiceDetails.Rows[i];
                var sanPhamDonViId = row.Cells["colSanPhamId"].Value?.ToString();
                var productName = row.Cells["colProduct"].Value?.ToString();
                var unitName = row.Cells["colUnitInvoice"].Value?.ToString();

                if (string.IsNullOrEmpty(sanPhamDonViId))
                {
                    try
                    {
                        using (var ctx = new AppDbContext())
                        {
                            var spdv = ctx.SanPhamDonVis
                                .Include(s => s.SanPham)
                                .Include(s => s.DonViDoLuong)
                                .FirstOrDefault(s => s.SanPham.ten == productName && s.DonViDoLuong.ten == unitName && !s.isDelete);

                            if (spdv != null) sanPhamDonViId = spdv.id;
                            else
                            {
                                MessageBox.Show($"❌ Dòng {i + 1}: Không tìm thấy SanPhamDonVi\n- Sản phẩm: {productName}\n- Đơn vị: {unitName}\n\nCó thể sản phẩm đã bị xóa hoặc không tồn tại trong DB.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"❌ Lỗi truy vấn SanPhamDonVi:\n{ex.Message}", "Lỗi");
                    }
                }

                if (string.IsNullOrEmpty(sanPhamDonViId))
                {
                    MessageBox.Show($"❌ Dòng {i + 1}: Không xác định được đơn vị sản phẩm\n- Sản phẩm: {productName}\n- Đơn vị: {unitName}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                int qty = 0;
                decimal unitPrice = 0m;
                decimal promoValue = 0m;

                int.TryParse(row.Cells["colQuantity"].Value?.ToString() ?? "0", out qty);
                decimal.TryParse((row.Cells["colUnitPrice"].Value?.ToString() ?? "0").Replace(" đ", "").Replace(",", ""), out unitPrice);
                if (!_promoValuePerRow.TryGetValue(i, out promoValue)) promoValue = 0m;

                var lineTotal = (unitPrice * qty) - promoValue;
                if (lineTotal < 0) lineTotal = 0m;

                if (details.Any(d => d.sanPhamDonViId == sanPhamDonViId))
                    {
                    MessageBox.Show($"❌ Dòng {i + 1}: Sản phẩm '{productName}' bị trùng lặp\n\nChi tiết hóa đơn không được chứa cùng một sản phẩm-đơn vị nhiều lần.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                details.Add(new ChiTietHoaDon
                {
                    hoaDonId = invoiceId,
                    sanPhamDonViId = sanPhamDonViId,
                    soLuong = qty,
                    donGia = unitPrice,
                    giamGia = null,
                    tongTien = lineTotal,
                    isDelete = false
                });
            }

            if (details.Count == 0)
            {
                MessageBox.Show("❌ Hóa đơn phải có ít nhất một sản phẩm", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            return details;
        }

        private void SetReadOnlyMode()
        {
            cmbCustomer.Enabled = false;
            dgvProducts.Enabled = false;
            SetDiscountControlEnabled(false);
            btnPayment.Enabled = false;

            if (dgvInvoiceDetails.Columns.Contains("colRemove"))
            {
                dgvInvoiceDetails.Columns["colRemove"].Visible = false;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _invoiceController?.Dispose();
            _productController?.Dispose();
            _categoryController?.Dispose();
        }

        private void dgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == dgvProducts.Columns["colAdd"].Index)
            {
                btnAddProduct_Click(sender, EventArgs.Empty);
            }
        }

        private void dgvDanhSachSanPham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgvProducts.CurrentRow != null)
                {
                    btnAddProduct_Click(sender, EventArgs.Empty);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void dgvInvoiceDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var colName = dgvInvoiceDetails.Columns[e.ColumnIndex].Name;

            if (colName == "colPromo")
            {
                var cell = dgvInvoiceDetails.Rows[e.RowIndex].Cells["colPromo"];
                if (cell is DataGridViewComboBoxCell)
                {
                    dgvInvoiceDetails.CurrentCell = cell;
                    dgvInvoiceDetails.BeginEdit(true);
                }
            }
            else if (colName == "colRemove")
            {
                try
                {
                    dgvInvoiceDetails.Rows.RemoveAt(e.RowIndex);
                    if (_appliedPromosPerRow.ContainsKey(e.RowIndex)) _appliedPromosPerRow.Remove(e.RowIndex);
                    if (_promoValuePerRow.ContainsKey(e.RowIndex)) _promoValuePerRow.Remove(e.RowIndex);
                    _appliedPromosPerRow = _appliedPromosPerRow.ToDictionary(k => k.Key > e.RowIndex ? k.Key - 1 : k.Key, v => v.Value);
                    _promoValuePerRow = _promoValuePerRow.ToDictionary(k => k.Key > e.RowIndex ? k.Key - 1 : k.Key, v => v.Value);
                    CalculateTotal();
                }
                catch { }
            }
        }

        private void PopulatePromoComboForRow(int rowIndex, string sanPhamDonViId)
        {
            try
            {
                if (rowIndex < 0 || rowIndex >= dgvInvoiceDetails.Rows.Count) return;

                var comboCell = new DataGridViewComboBoxCell();
                var svc = new QuanLyServices();

                var sanPhamDonVis = svc.GetList<SanPhamDonVi>();
                var spdv = sanPhamDonVis.FirstOrDefault(s => s.id == sanPhamDonViId && !s.isDelete);
                if (spdv == null) { dgvInvoiceDetails.Rows[rowIndex].Cells["colPromo"] = comboCell; return; }

                var sanPhamId = spdv.sanPhamId;
                var sanPhamDanhMucs = svc.GetList<SanPhamDanhMuc>();
                var productCategoryIds = sanPhamDanhMucs.Where(x => x.sanPhamId == sanPhamId && !x.isDelete).Select(x => x.danhMucId).ToList();

                var allMa = svc.GetList<MaKhuyenMai>().Where(m => !m.isDelete && m.trangThai == "Active").ToList();
                var allDieuKien = svc.GetList<DieuKienApDung>().Where(d => !d.isDelete).ToList();
                var allDKSanPham = svc.GetList<DieuKienApDungSanPham>().Where(d => !d.isDelete).ToList();
                var allDKDanhMuc = svc.GetList<DieuKienApDungDanhMuc>().Where(d => !d.isDelete).ToList();
                var allDKToanBo = svc.GetList<DieuKienApDungToanBo>().Where(d => !d.isDelete).ToList();

                var promos = new List<dynamic>();
                foreach (var m in allMa)
                {
                    var dks = allDieuKien.Where(d => d.chuongTrinhId == m.chuongTrinhId).ToList();
                    bool applicable = false;
                    foreach (var dk in dks)
                    {
                        if (allDKSanPham.Any(sp => sp.dieuKienId == dk.id && sp.sanPhamId == sanPhamId)) { applicable = true; break; }
                        if (allDKDanhMuc.Any(dm => dm.dieuKienId == dk.id && productCategoryIds.Contains(dm.danhMucId))) { applicable = true; break; }
                        if (allDKToanBo.Any(tb => tb.dieuKienId == dk.id)) { applicable = true; break; }
                    }
                    if (applicable) promos.Add(new { Id = m.id, Code = m.code, GiaTri = m.giaTri });
                }

                var list = promos.Select(p => new { Id = p.Id, Display = p.Code + " (" + (p.GiaTri < 1 ? (p.GiaTri * 100).ToString("0") + "%" : p.GiaTri.ToString("N0") + " đ") + ")" }).ToList();

                if (list == null || list.Count == 0)
                {
                    comboCell.Items.Clear();
                    comboCell.Items.Add("(Không có mã KM)");
                    comboCell.Value = null;
                }
                else
                {
                    comboCell.DataSource = list;
                    comboCell.DisplayMember = "Display";
                    comboCell.ValueMember = "Id";
                    comboCell.Value = null;
                }

                dgvInvoiceDetails.Rows[rowIndex].Cells["colPromo"] = comboCell;
            }
            catch { }
        }

        private void btnApplyDiscount_Click_1(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private List<ChiTietHoaDonKhuyenMai> BuildPromoDetails(string invoiceId)
        {
            var promoDetails = new List<ChiTietHoaDonKhuyenMai>();
            for (int i = 0; i < dgvInvoiceDetails.Rows.Count; i++)
            {
                if (!_appliedPromosPerRow.ContainsKey(i) || _appliedPromosPerRow[i].Count == 0) continue;
                var row = dgvInvoiceDetails.Rows[i];
                var sanPhamDonViId = row.Cells["colSanPhamId"].Value?.ToString();
                foreach (var code in _appliedPromosPerRow[i])
                {
                    using (var ctx = new AppDbContext())
                    {
                        var mk = ctx.MaKhuyenMais.FirstOrDefault(m => m.code == code && !m.isDelete && m.trangThai == "Active");
                        if (mk == null) continue;
                        var unitPrice = decimal.Parse(row.Cells["colUnitPrice"].Value.ToString().Replace(" đ", "").Replace(",", ""));
                        var qty = int.Parse(row.Cells["colQuantity"].Value?.ToString() ?? "0");
                        var appliedValue = mk.giaTri > 0 && mk.giaTri < 1 ? unitPrice * qty * mk.giaTri : mk.giaTri;
                        promoDetails.Add(new ChiTietHoaDonKhuyenMai
                        {
                            hoaDonId = invoiceId,
                            sanPhamDonViId = sanPhamDonViId,
                            maKhuyenMaiId = mk.id,
                            giaTriApDung = appliedValue,
                            id = Guid.NewGuid().ToString(),
                            isDelete = false
                        });
                    }
                }
            }
            return promoDetails;
        }

        private decimal ReadDiscountFromControl()
        {
            try
            {
                var ctrl = this.Controls.Find("txtDiscount", true).FirstOrDefault() as TextBox;
                if (ctrl == null) return 0m;
                if (decimal.TryParse(ctrl.Text.Replace(",", ""), out decimal d)) return d;
                return 0m;
            }
            catch { return 0m; }
        }

        private void SetDiscountTextControl(decimal value)
        {
            try
            {
                var ctrl = this.Controls.Find("txtDiscount", true).FirstOrDefault() as TextBox;
                if (ctrl != null) ctrl.Text = value.ToString("N0");
            }
            catch { }
        }

        private void SetDiscountControlEnabled(bool enabled)
        {
            try
            {
                var ctrl = this.Controls.Find("txtDiscount", true).FirstOrDefault() as TextBox;
                if (ctrl != null) ctrl.Enabled = enabled;
                var btn = this.Controls.Find("btnApplyDiscount", true).FirstOrDefault() as Button;
                if (btn != null) btn.Enabled = enabled;
            }
            catch { }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            using (frmScanBarcode scanForm = new frmScanBarcode())
            {
                if (scanForm.ShowDialog() != DialogResult.OK) return;

                string code = scanForm.ScannedCode?.Trim();
                if (string.IsNullOrEmpty(code)) return;

                // place barcode into search box
                txtSearch.Text = code;

                try
                {
                    using (var ctx = new AppDbContext())
                    {
                        // Try to find MaDinhDanhSanPham by exact or contains
                        var md = ctx.MaDinhDanhSanPhams.FirstOrDefault(m => m.maCode == code && !m.isDelete)
                            ?? ctx.MaDinhDanhSanPhams.FirstOrDefault(m => m.maCode.Contains(code) && !m.isDelete);

                        if (md != null)
                        {
                            var spdv = ctx.SanPhamDonVis.Include(s => s.SanPham).Include(s => s.DonViDoLuong)
                                .FirstOrDefault(s => s.id == md.sanPhamDonViId && !s.isDelete);

                            if (spdv != null)
                            {
                                int idx = FindProductRowBySanPhamDonVi(spdv);
                                if (idx >= 0)
                                {
                                    dgvProducts.ClearSelection();
                                    dgvProducts.Rows[idx].Selected = true;
                                    dgvProducts.CurrentCell = dgvProducts.Rows[idx].Cells.Cast<DataGridViewCell>().FirstOrDefault(c => c.Visible) ?? dgvProducts.Rows[idx].Cells[0];
                                    btnAddProduct_Click(sender, EventArgs.Empty);
                                    return;
                                }

                                // reload products by name and try again
                                txtSearch.Text = spdv.SanPham?.ten ?? string.Empty;
                                LoadProducts(txtSearch.Text);
                                idx = FindProductRowBySanPhamDonVi(spdv);
                                if (idx >= 0)
                                {
                                    dgvProducts.ClearSelection();
                                    dgvProducts.Rows[idx].Selected = true;
                                    dgvProducts.CurrentCell = dgvProducts.Rows[idx].Cells.Cast<DataGridViewCell>().FirstOrDefault(c => c.Visible) ?? dgvProducts.Rows[idx].Cells[0];
                                    btnAddProduct_Click(sender, EventArgs.Empty);
                                    return;
                                }

                                // add directly
                                AddProductDirectlyFromSanPhamDonVi(spdv);
                                return;
                            }
                        }

                        // fallback: try find product by id or name
                        LoadProducts(code);
                        int match = FindProductRowByCodeOrName(code);
                        if (match >= 0)
                        {
                            dgvProducts.ClearSelection();
                            dgvProducts.Rows[match].Selected = true;
                            dgvProducts.CurrentCell = dgvProducts.Rows[match].Cells.Cast<DataGridViewCell>().FirstOrDefault(c => c.Visible) ?? dgvProducts.Rows[match].Cells[0];
                            btnAddProduct_Click(sender, EventArgs.Empty);
                            return;
                        }

                        // try full list
                        LoadProducts(null);
                        match = FindProductRowByCodeOrName(code);
                        if (match >= 0)
                        {
                            dgvProducts.ClearSelection();
                            dgvProducts.Rows[match].Selected = true;
                            dgvProducts.CurrentCell = dgvProducts.Rows[match].Cells.Cast<DataGridViewCell>().FirstOrDefault(c => c.Visible) ?? dgvProducts.Rows[match].Cells[0];
                            btnAddProduct_Click(sender, EventArgs.Empty);
                            return;
                        }

                        MessageBox.Show($"Không tìm thấy sản phẩm tương ứng với mã: {code}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi tìm mã: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Helper: find a row in dgvProducts by hidden Id or by name containing code
        private int FindProductRowByCodeOrName(String code)
        {
            if (string.IsNullOrEmpty(code)) return -1;
            for (int r = 0; r < dgvProducts.Rows.Count; r++)
            {
                var idHidden = dgvProducts.Rows[r].Cells["colProductId"].Value?.ToString();
                var name = dgvProducts.Rows[r].Cells["colProductName"].Value?.ToString();
                if (!string.IsNullOrEmpty(idHidden) && string.Equals(idHidden, code, StringComparison.OrdinalIgnoreCase)) return r;
                if (!string.IsNullOrEmpty(name) && (string.Equals(name, code, StringComparison.CurrentCultureIgnoreCase) || name.IndexOf(code, StringComparison.CurrentCultureIgnoreCase) >= 0)) return r;
            }
            return -1;
        }

        // Helper: try to find product row by SanPhamDonVi info
        private int FindProductRowBySanPhamDonVi(SanPhamDonVi spdv)
        {
            if (spdv == null) return -1;
            var productName = spdv.SanPham?.ten ?? string.Empty;
            var unitName = spdv.DonViDoLuong?.ten ?? string.Empty;

            for (int r = 0; r < dgvProducts.Rows.Count; r++)
            {
                var idHidden = dgvProducts.Rows[r].Cells["colProductId"].Value?.ToString();
                var name = dgvProducts.Rows[r].Cells["colProductName"].Value?.ToString();
                var unit = dgvProducts.Rows[r].Cells["colUnitProduct"].Value?.ToString();

                if ((!string.IsNullOrEmpty(idHidden) && string.Equals(idHidden, spdv.sanPhamId, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(name) && string.Equals(name, productName, StringComparison.CurrentCultureIgnoreCase) &&
                    !string.IsNullOrEmpty(unit) && string.Equals(unit, unitName, StringComparison.CurrentCultureIgnoreCase)))
                {
                    return r;
                }
            }
            return -1;
        }

        // Helper: add product directly to invoice details when not present in dgvProducts
        private void AddProductDirectlyFromSanPhamDonVi(SanPhamDonVi spdv)
        {
            if (spdv == null) return;
            var productName = spdv.SanPham?.ten ?? string.Empty;
            var unitName = spdv.DonViDoLuong?.ten ?? string.Empty;
            decimal price = 0m;
            try { price = spdv.giaBan; } catch { }

            var priceStr = price > 0 ? price.ToString("N0") + " đ" : "0 đ";

            dgvInvoiceDetails.Rows.Add(
                spdv.id,
                productName,
                unitName,
                1,
                priceStr,
                price > 0 ? price.ToString("N0") + " đ" : "0 đ",
                null
            );

            int newRow = dgvInvoiceDetails.Rows.Count - 1;
            if (!string.IsNullOrEmpty(spdv.id)) PopulatePromoComboForRow(newRow, spdv.id);

            CalculateTotal();
        }
    }

    // Payment dialog form
    public class frmPayment : Form
    {
        private InvoiceController _invoiceController;
        private string _invoiceId;
        private ComboBox cmbPaymentMethod;
        private TextBox txtAmount;
        private Label lblTotal;
        private Button btnConfirm;
        private Button btnCancel;

        public frmPayment(string invoiceId)
        {
            _invoiceId = invoiceId;
            _invoiceController = new InvoiceController();
            InitializeComponent();
            LoadPaymentMethods();
            LoadInvoiceTotal();
        }

        private void InitializeComponent()
        {
            this.Text = "Thanh toán";
            this.Size = new Size(450, 320);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            var pnlTop = new Panel { Dock = DockStyle.Top, Height = 60, BackColor = Color.FromArgb(41, 128, 185) };
            var lblTitle = new Label { Text = "💳 THANH TOÁN HÓA ĐƠN", Location = new Point(20, 15), AutoSize = true, Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.White };
            pnlTop.Controls.Add(lblTitle);

            var lblPaymentMethod = new Label { Text = "Phương thức thanh toán:", Location = new Point(30, 90), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            cmbPaymentMethod = new ComboBox { Location = new Point(220, 87), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };

            var lblAmountLabel = new Label { Text = "Tổng tiền:", Location = new Point(30, 130), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            lblTotal = new Label { Location = new Point(220, 128), AutoSize = true, Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.FromArgb(231, 76, 60) };

            var lblAmountPaid = new Label { Text = "Số tiền thanh toán:", Location = new Point(30, 170), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtAmount = new TextBox { Location = new Point(220, 167), Width = 200, Font = new Font("Segoe UI", 10) };

            btnConfirm = new Button { Text = "✅ Xác nhận", Location = new Point(140, 230), Size = new Size(130, 40), BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            btnCancel = new Button { Text = "❌ Hủy", Location = new Point(280, 230), Size = new Size(100, 40), BackColor = Color.FromArgb(231, 76, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

            btnConfirm.Click += btnConfirm_Click;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            this.Controls.AddRange(new Control[] { pnlTop, lblPaymentMethod, cmbPaymentMethod, lblAmountLabel, lblTotal, lblAmountPaid, txtAmount, btnConfirm, btnCancel });
        }

        private void LoadPaymentMethods()
        {
            try
            {
                var methods = _invoiceController.GetPaymentMethods();
                cmbPaymentMethod.DataSource = methods;
                cmbPaymentMethod.DisplayMember = "tenKenh";
                cmbPaymentMethod.ValueMember = "id";
            }
            catch { }
        }

        private void LoadInvoiceTotal()
        {
            try
            {
                var invoice = _invoiceController.GetInvoiceById(_invoiceId);
                if (invoice != null)
                {
                    lblTotal.Text = (invoice.tongTien ?? 0).ToString("N0") + " đ";
                    txtAmount.Text = (invoice.tongTien ?? 0).ToString("N0");
                }
            }
            catch { }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbPaymentMethod.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn phương thức thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtAmount.Text.Replace(",", ""), out decimal amount) || amount <= 0)
                {
                    MessageBox.Show("Vui lòng nhập số tiền hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var paymentMethodId = cmbPaymentMethod.SelectedValue.ToString();
                var paymentResult = _invoiceController.ProcessPayment(_invoiceId, paymentMethodId, amount, "Thanh toán hóa đơn");
                bool success = paymentResult.Item1;
                string message = paymentResult.Item2;

                if (success)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _invoiceController?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}