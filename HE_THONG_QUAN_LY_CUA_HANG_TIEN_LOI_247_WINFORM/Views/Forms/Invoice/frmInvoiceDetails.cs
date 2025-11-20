using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using System.Data.Entity;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;

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
            _quanLyServices = new QuanLyServices();
        }

        // Note: small naming fixes to match original fields
        private ProductController _product_controller { get { return _productController; } set { _productController = value; } }
        private CategoryController _category_controller { get { return _categoryController; } set { _categoryController = value; } }

        private void CustomizeInterface()
        {
            // Style cho b?ng Chi ti?t hóa đơn (Bên trái)
            StyleGrid(dgvInvoiceDetails);
            dgvInvoiceDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185); // Xanh Dương

            // Style cho b?ng S?n ph?m (Bên ph?i)
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
            InitializeUI();
            LoadCustomers();
            LoadCategories();
            LoadProducts();

            if (_isEditMode && !string.IsNullOrEmpty(_invoiceId))
            {
                LoadInvoiceData();
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
                MessageBox.Show($"L?i khi t?i danh sách khách hàng: {ex.Message}", "L?i",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategories()
        {
            try
            {
                var categories = _categoryController.GetAllCategories();

                var categoryList = new List<ComboItem>();
                categoryList.Add(new ComboItem { id = "", ten = "T?t c?" });

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
                MessageBox.Show($"L?i khi t?i danh m?c: {ex.Message}", "L?i",
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
                MessageBox.Show($"L?i khi t?i s?n ph?m: {ex.Message}", "L?i",
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
                    MessageBox.Show("Không t?m th?y hóa đơn!", "L?i",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Load invoice info
                lblInvoiceId.Text = invoice.id;
                cmbCustomer.SelectedValue = invoice.khachHangId;

                // Load invoice details
                _invoiceDetails = _invoiceController.GetInvoiceDetails(_invoiceId);
                DisplayInvoiceDetails();
                CalculateTotal();

                // Disable editing if already paid
                if (invoice.trangThai == "Đ? thanh toán")
                {
                    SetReadOnlyMode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i khi t?i hóa đơn: {ex.Message}", "L?i",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayInvoiceDetails()
        {
            dgvInvoiceDetails.Rows.Clear();

            using (var ctx = new AppDbContext())
            {
                foreach (var detail in _invoiceDetails)
                {
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
                    var totalStr = (detail.soLuong * detail.donGia).ToString("N0") + " đ";

                    // Add row: hidden sanPhamDonViId in first column
                    dgvInvoiceDetails.Rows.Add(
                        sanPhamDonViId,
                        productName,
                        unitName,
                        detail.soLuong,
                        unitPriceStr,
                        totalStr,
                        null // promo cell will be populated to combobox later when user clicks
                    );

                    int rowIndex = dgvInvoiceDetails.Rows.Count - 1;
                    // populate promo options for this row
                    if (!string.IsNullOrEmpty(sanPhamDonViId))
                        PopulatePromoComboForRow(rowIndex, sanPhamDonViId);
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProducts(txtSearch.Text);
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
            var result = MessageBox.Show("B?n có ch?c ch?n mu?n h?y?", "Xác nh?n",
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
                    MessageBox.Show("Hóa đơn chưa có s?n ph?m", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Calculate total from UI (already considers discount)
                var totalText = lblTotal.Text.Replace(" đ", "").Replace(",", "").Trim();
                if (!decimal.TryParse(totalText, out decimal totalAmount))
                {
                    MessageBox.Show("Không th? xác đ?nh t?ng ti?n", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Open external payment form to collect payment info
                var paymentForm = new HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Invoice.frmPayment(totalAmount);
                // set owner so child can call back to this form to perform saving
                paymentForm.Owner = this;
                paymentForm.ShowDialog();
                // saving will be performed by paymentForm via SaveInvoiceFromPayment on Owner
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i khi x? l? thanh toán: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Called by child payment form to perform DB save when user confirms payment
        public void SaveInvoiceFromPayment(decimal customerPay, string paymentMethodName, string note)
        {
            try
            {
                // Generate new invoice id using QuanLyServices (HD +4 digits => total length6)
                var newInvoiceId = _quanLyServices.GenerateNewId<HoaDon>("HD", 6);
                if (string.IsNullOrEmpty(newInvoiceId))
                {
                    MessageBox.Show("Không th? t?o m? hóa đơn", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Build invoice: do NOT set tongTien here; DB trigger will compute it
                var invoice = new HoaDon()
                {
                    id = newInvoiceId,
                    ngayLap = DateTime.Now,
                    khachHangId = cmbCustomer.SelectedValue?.ToString() ?? "",
                    nhanVienId = _quanLyServices.GetList<NhanVien>().FirstOrDefault()?.id ?? "NV001",
                    isDelete = false,
                    trangThai = "Chưa thanh toán"
                };

                // Build details with tongTien computed from UI (donGia * soLuong - promo)
                var details = new List<ChiTietHoaDon>();

                for (int i = 0; i < dgvInvoiceDetails.Rows.Count; i++)
                {
                    var row = dgvInvoiceDetails.Rows[i];
                    var sanPhamDonViId = row.Cells["colSanPhamId"].Value?.ToString();

                    if (string.IsNullOrEmpty(sanPhamDonViId))
                    {
                        try
                        {
                            var productName = row.Cells["colProduct"].Value?.ToString();
                            var unitName = row.Cells["colUnitInvoice"].Value?.ToString();
                            using (var ctx = new AppDbContext())
                            {
                                var spdv = ctx.SanPhamDonVis
                                    .Include(s => s.SanPham)
                                    .Include(s => s.DonViDoLuong)
                                    .FirstOrDefault(s => (s.SanPham.ten == productName || s.sanPhamId == productName) && s.DonViDoLuong.ten == unitName && !s.isDelete);
                                if (spdv != null) sanPhamDonViId = spdv.id;
                            }
                        }
                        catch { }
                    }

                    if (string.IsNullOrEmpty(sanPhamDonViId))
                    {
                        MessageBox.Show($"D?ng {i + 1}: không xác đ?nh đư?c đơn v? s?n ph?m (SanPhamDonVi)", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int qty = 0;
                    decimal unitPrice = 0m;
                    decimal promoValue = 0m;

                    int.TryParse(row.Cells["colQuantity"].Value?.ToString() ?? "0", out qty);
                    decimal.TryParse((row.Cells["colUnitPrice"].Value?.ToString() ?? "0").Replace(" đ", "").Replace(",", ""), out unitPrice);
                    if (!_promoValuePerRow.TryGetValue(i, out promoValue)) promoValue = 0m;

                    var lineTotal = (unitPrice * qty) - promoValue;
                    if (lineTotal < 0) lineTotal = 0m;

                    // giamGia will be calculated by DB trigger if needed; but we still supply tongTien for each detail
                    details.Add(new ChiTietHoaDon
                    {
                        hoaDonId = newInvoiceId,
                        sanPhamDonViId = sanPhamDonViId,
                        soLuong = qty,
                        donGia = unitPrice,
                        // Leave giamGia null so DB trigger/procedure can compute it if that's required
                        giamGia = null,
                        // Provide tongTien as required by user
                        tongTien = lineTotal,
                        isDelete = false
                    });
                }

                // Persist invoice and details so DB trigger can recompute HoaDon.tongTien
                using (var ctx = new AppDbContext())
                {
                    ctx.HoaDons.Add(invoice);
                    ctx.ChiTietHoaDons.AddRange(details);
                    ctx.SaveChanges();

                    // Determine payment method id from DB using paymentMethodName (fallback to first active)
                    string paymentMethodId = null;
                    try
                    {
                        var method = ctx.KenhThanhToans.FirstOrDefault(k => !k.isDelete && (k.tenKenh ?? "").IndexOf(paymentMethodName ?? "", StringComparison.CurrentCultureIgnoreCase) >= 0);
                        if (method != null) paymentMethodId = method.id;
                        else
                        {
                            var first = ctx.KenhThanhToans.FirstOrDefault(k => !k.isDelete);
                            if (first != null) paymentMethodId = first.id;
                        }
                    }
                    catch { }

                    // Create payment transaction record
                    if (!string.IsNullOrEmpty(paymentMethodId))
                    {
                        var payment = new GiaoDichThanhToan
                        {
                            id = _quanLyServices.GenerateNewId<GiaoDichThanhToan>("GD", 6) ?? Guid.NewGuid().ToString(),
                            hoaDonId = invoice.id,
                            soTien = customerPay,
                            ngayGD = DateTime.Now,
                            kenhThanhToanId = paymentMethodId,
                            moTa = string.IsNullOrEmpty(note) ? "Thanh toán hóa đơn" : note,
                            isDelete = false
                        };

                        ctx.GiaoDichThanhToans.Add(payment);

                        // Update invoice status to paid
                        invoice.trangThai = "Đ? thanh toán";
                        ctx.Entry(invoice).State = EntityState.Modified;

                        ctx.SaveChanges();
                    }

                    // Save promo details if any
                    var promoDetails = BuildPromoDetails(invoice.id);
                    if (promoDetails != null && promoDetails.Count > 0)
                    {
                        foreach (var pd in promoDetails)
                        {
                            pd.id = _quanLyServices.GenerateNewId<ChiTietHoaDonKhuyenMai>("CTHD", 8) ?? pd.id;
                            ctx.ChiTietHoaDonKhuyenMais.Add(pd);
                        }
                        ctx.SaveChanges();
                    }

                    // Optionally reload invoice to get trigger-updated tongTien
                    try
                    {
                        ctx.Entry(invoice).Reload();
                    }
                    catch { }

                    MessageBox.Show($"Lưu hoá đơn thành công. M?: {invoice.id}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Close this form after successful save
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i khi lưu hóa đơn: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                // delegate to Add button handler
                btnAddProduct_Click(sender, EventArgs.Empty);
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
                // remove clicked row
                try
                {
                    dgvInvoiceDetails.Rows.RemoveAt(e.RowIndex);
                    // cleanup promo maps
                    if (_appliedPromosPerRow.ContainsKey(e.RowIndex)) _appliedPromosPerRow.Remove(e.RowIndex);
                    if (_promoValuePerRow.ContainsKey(e.RowIndex)) _promoValuePerRow.Remove(e.RowIndex);
                    // shift keys greater than removed index
                    _appliedPromosPerRow = _appliedPromosPerRow.ToDictionary(k => k.Key > e.RowIndex ? k.Key - 1 : k.Key, v => v.Value);
                    _promoValuePerRow = _promoValuePerRow.ToDictionary(k => k.Key > e.RowIndex ? k.Key - 1 : k.Key, v => v.Value);
                    CalculateTotal();
                }
                catch { }
            }
        }

        // Populate promo options for a row based on SanPhamDonVi -> SanPham and its categories
        private void PopulatePromoComboForRow(int rowIndex, string sanPhamDonViId)
        {
            try
            {
                if (rowIndex < 0 || rowIndex >= dgvInvoiceDetails.Rows.Count) return;

                var comboCell = new DataGridViewComboBoxCell();

                // Use QuanLyServices to retrieve lists (no new DB queries)
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
                    // find dieu kien for this chuongTrinh
                    var dks = allDieuKien.Where(d => d.chuongTrinhId == m.chuongTrinhId).ToList();
                    bool applicable = false;
                    foreach (var dk in dks)
                    {
                        // by product
                        if (allDKSanPham.Any(sp => sp.dieuKienId == dk.id && sp.sanPhamId == sanPhamId)) { applicable = true; break; }
                        // by category
                        if (allDKDanhMuc.Any(dm => dm.dieuKienId == dk.id && productCategoryIds.Contains(dm.danhMucId))) { applicable = true; break; }
                        // by all
                        if (allDKToanBo.Any(tb => tb.dieuKienId == dk.id)) { applicable = true; break; }
                    }
                    if (applicable)
                    {
                        promos.Add(new { Id = m.id, Code = m.code, GiaTri = m.giaTri });
                    }
                }

                var list = promos.Select(p => new { Id = p.Id, Display = p.Code + " (" + (p.GiaTri < 1 ? (p.GiaTri * 100).ToString("0") + "%" : p.GiaTri.ToString("N0") + " đ") + ")" }).ToList();

                if (list == null || list.Count == 0)
                {
                    // no promos - show single disabled item
                    comboCell.Items.Clear();
                    comboCell.Items.Add("(Không có m? KM)");
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
            var lblTitle = new Label { Text = "?? THANH TOÁN HÓA ĐƠN", Location = new Point(20, 15), AutoSize = true, Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.White };
            pnlTop.Controls.Add(lblTitle);

            var lblPaymentMethod = new Label { Text = "Phương th?c thanh toán:", Location = new Point(30, 90), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            cmbPaymentMethod = new ComboBox { Location = new Point(220, 87), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 10) };

            var lblAmountLabel = new Label { Text = "T?ng ti?n:", Location = new Point(30, 130), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            lblTotal = new Label { Location = new Point(220, 128), AutoSize = true, Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.FromArgb(231, 76, 60) };

            var lblAmountPaid = new Label { Text = "S? ti?n thanh toán:", Location = new Point(30, 170), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtAmount = new TextBox { Location = new Point(220, 167), Width = 200, Font = new Font("Segoe UI", 10) };

            btnConfirm = new Button { Text = "? Xác nh?n", Location = new Point(140, 230), Size = new Size(130, 40), BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            btnCancel = new Button { Text = "? H?y", Location = new Point(280, 230), Size = new Size(100, 40), BackColor = Color.FromArgb(231, 76, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

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
                    MessageBox.Show("Vui l?ng ch?n phương th?c thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtAmount.Text.Replace(",", ""), out decimal amount) || amount <= 0)
                {
                    MessageBox.Show("Vui l?ng nh?p s? ti?n h?p l?!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show(message, "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
