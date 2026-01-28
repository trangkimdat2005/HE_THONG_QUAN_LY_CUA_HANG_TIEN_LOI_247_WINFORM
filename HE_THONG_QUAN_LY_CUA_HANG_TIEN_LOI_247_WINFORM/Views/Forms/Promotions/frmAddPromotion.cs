using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Promotions
{
    public partial class frmAddPromotion : Form
    {
        private readonly PromotionController _controller;
        private readonly CategoryController _categoryController;
        private readonly ProductController _productController;
        
        private string _promotionId;
        private bool _isEditMode;
        
        private const string GIAM_THEO_PHAN_TRAM = "Phần trăm";
        private const string GIAM_THEO_GIA_TIEN = "Giá tiền";
        private const decimal GIA_TRI_TOI_DA_MAC_DINH = 999999999m;

        private Button btnSelectCategories;
        private Button btnSelectProducts;
        private Label lblSelectedCategories;
        private Label lblSelectedProducts;
        
        private List<SelectableItem> _selectedCategories = new List<SelectableItem>();
        private List<SelectableItem> _selectedProducts = new List<SelectableItem>();
        private List<SelectableItem> _allCategories = new List<SelectableItem>();
        private List<SelectableItem> _allProducts = new List<SelectableItem>();

        public frmAddPromotion()
        {
            InitializeComponent();
            _controller = new PromotionController();
            _categoryController = new CategoryController();
            _productController = new ProductController();
            InitializeForm();
        }

        public frmAddPromotion(string promotionId) : this()
        {
            _isEditMode = true;
            _promotionId = promotionId;
            lblTitle.Text = "Chỉnh sửa khuyến mãi";
            LoadPromotionData(promotionId);
        }

        private void InitializeForm()
        {
            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now.AddMonths(1);
            cboDiscountType.SelectedIndex = 0;
            cboStatus.SelectedIndex = 0;
            txtUsageLimit.Text = "100";
            
            LoadCategories();
            LoadProducts();
            CreateCategorySelector();
            CreateProductSelector();
            
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;
            btnGenerateCode.Click += btnGenerateCode_Click;
            cboDiscountType.SelectedIndexChanged += cboDiscountType_SelectedIndexChanged;
            rdoAll.CheckedChanged += RadioScope_CheckedChanged;
            rdoCategory.CheckedChanged += RadioScope_CheckedChanged;
            rdoProduct.CheckedChanged += RadioScope_CheckedChanged;
            
            txtDiscountValue.KeyPress += NumericTextBox_KeyPress;
            txtMaxDiscount.KeyPress += NumericTextBox_KeyPress;
            txtMinOrder.KeyPress += NumericTextBox_KeyPress;
            txtUsageLimit.KeyPress += IntegerTextBox_KeyPress;
            
            if (!_isEditMode)
                GenerateRandomCode();
            
            UpdateScopeVisibility();
        }

        private void CreateCategorySelector()
        {
            btnSelectCategories = new Button
            {
                Text = "Chọn danh mục...",
                Font = new Font("Segoe UI", 9F),
                Location = new Point(20, 125),
                Size = new Size(150, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 183, 255),
                ForeColor = Color.White,
                Visible = false
            };
            btnSelectCategories.Click += BtnSelectCategories_Click;
            
            lblSelectedCategories = new Label
            {
                Text = "Chưa chọn danh mục nào",
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                Location = new Point(180, 130),
                AutoSize = true,
                ForeColor = Color.Gray,
                Visible = false
            };
            
            grpScope.Controls.Add(btnSelectCategories);
            grpScope.Controls.Add(lblSelectedCategories);
        }

        private void CreateProductSelector()
        {
            btnSelectProducts = new Button
            {
                Text = "Chọn sản phẩm...",
                Font = new Font("Segoe UI", 9F),
                Location = new Point(20, 125),
                Size = new Size(150, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 183, 255),
                ForeColor = Color.White,
                Visible = false
            };
            btnSelectProducts.Click += BtnSelectProducts_Click;
            
            lblSelectedProducts = new Label
            {
                Text = "Chưa chọn sản phẩm nào",
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                Location = new Point(180, 130),
                AutoSize = true,
                ForeColor = Color.Gray,
                Visible = false
            };
            
            grpScope.Controls.Add(btnSelectProducts);
            grpScope.Controls.Add(lblSelectedProducts);
        }

        private void LoadCategories()
        {
            try
            {
                var categories = _categoryController.GetAllCategories();
                _allCategories.Clear();
                
                foreach (var cat in categories)
                {
                    string id = null, name = null;
                    try { id = cat.Id; } catch { try { id = cat.id; } catch { } }
                    try { name = cat.Ten; } catch { try { name = cat.ten; } catch { } }
                    
                    if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(name))
                        _allCategories.Add(new SelectableItem { Id = id, Name = name });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi tải danh mục: {ex.Message}");
            }
        }

        private void LoadProducts()
        {
            try
            {
                var products = _productController.GetAllProducts();
                _allProducts.Clear();
                
                foreach (var prod in products)
                {
                    string id = null, name = null;
                    try { id = prod.id; } catch { }
                    try { name = prod.ten; } catch { }
                    
                    if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(name))
                        _allProducts.Add(new SelectableItem { Id = id, Name = name });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi tải sản phẩm: {ex.Message}");
            }
        }

        private void BtnSelectCategories_Click(object sender, EventArgs e)
        {
            var preSelectedIds = _selectedCategories.Select(x => x.Id).ToList();
            using (var frm = new frmSelectItems("Chọn danh mục áp dụng", _allCategories, preSelectedIds))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    _selectedCategories = frm.SelectedItems;
                    UpdateCategoryLabel();
                }
            }
        }

        private void BtnSelectProducts_Click(object sender, EventArgs e)
        {
            var preSelectedIds = _selectedProducts.Select(x => x.Id).ToList();
            using (var frm = new frmSelectItems("Chọn sản phẩm áp dụng", _allProducts, preSelectedIds))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    _selectedProducts = frm.SelectedItems;
                    UpdateProductLabel();
                }
            }
        }

        private void UpdateCategoryLabel()
        {
            if (_selectedCategories.Count == 0)
            {
                lblSelectedCategories.Text = "Chưa chọn danh mục nào";
                lblSelectedCategories.ForeColor = Color.Gray;
            }
            else
            {
                lblSelectedCategories.Text = $"Đã chọn {_selectedCategories.Count} danh mục";
                lblSelectedCategories.ForeColor = Color.FromArgb(46, 204, 113);
            }
        }

        private void UpdateProductLabel()
        {
            if (_selectedProducts.Count == 0)
            {
                lblSelectedProducts.Text = "Chưa chọn sản phẩm nào";
                lblSelectedProducts.ForeColor = Color.Gray;
            }
            else
            {
                lblSelectedProducts.Text = $"Đã chọn {_selectedProducts.Count} sản phẩm";
                lblSelectedProducts.ForeColor = Color.FromArgb(46, 204, 113);
            }
        }

        private void RadioScope_CheckedChanged(object sender, EventArgs e) => UpdateScopeVisibility();

        private void UpdateScopeVisibility()
        {
            btnSelectCategories.Visible = rdoCategory.Checked;
            lblSelectedCategories.Visible = rdoCategory.Checked;
            btnSelectProducts.Visible = rdoProduct.Checked;
            lblSelectedProducts.Visible = rdoProduct.Checked;
        }

        private void LoadPromotionData(string promotionId)
        {
            try
            {
                var promotion = _controller.GetPromotionById(promotionId);
                if (promotion == null)
                {
                    MessageBox.Show("Không tìm thấy chương trình khuyến mãi!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                txtName.Text = promotion.ten;
                txtDescription.Text = promotion.moTa;
                dtpStartDate.Value = promotion.ngayBatDau;
                dtpEndDate.Value = promotion.ngayKetThuc;
                
                switch (promotion.loai)
                {
                    case "Danh mục": rdoCategory.Checked = true; break;
                    case "Sản phẩm": rdoProduct.Checked = true; break;
                    default: rdoAll.Checked = true; break;
                }
                
                var promoCodes = _controller.GetPromoCodesByProgramId(promotionId);
                if (promoCodes?.Count > 0)
                {
                    var firstCode = promoCodes[0];
                    txtCode.Text = firstCode.Code;
                    txtUsageLimit.Text = firstCode.SoLanSuDung.ToString();
                    cboStatus.SelectedIndex = firstCode.TrangThai == "Hoạt động" ? 0 : 1;
                }
                
                var conditions = _controller.GetConditionsByProgramId(promotionId);
                if (conditions?.Count > 0)
                {
                    var cond = conditions[0];
                    cboDiscountType.SelectedIndex = cond.giamTheo == GIAM_THEO_PHAN_TRAM ? 0 : 1;
                    txtDiscountValue.Text = cond.giaTriToiThieu.ToString();
                    
                    if (cond.giamTheo == GIAM_THEO_PHAN_TRAM && cond.giaTriToiDa > 0 && cond.giaTriToiDa < GIA_TRI_TOI_DA_MAC_DINH)
                        txtMaxDiscount.Text = cond.giaTriToiDa.ToString();
                }
                
                var selectedCategoryIds = _controller.GetSelectedCategoryIds(promotionId);
                if (selectedCategoryIds?.Count > 0)
                {
                    _selectedCategories = _allCategories.Where(c => selectedCategoryIds.Contains(c.Id)).ToList();
                    UpdateCategoryLabel();
                }
                
                var selectedProductIds = _controller.GetSelectedProductIds(promotionId);
                if (selectedProductIds?.Count > 0)
                {
                    _selectedProducts = _allProducts.Where(p => selectedProductIds.Contains(p.Id)).ToList();
                    UpdateProductLabel();
                }
                
                UpdateScopeVisibility();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerateCode_Click(object sender, EventArgs e) => GenerateRandomCode();

        private void GenerateRandomCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            txtCode.Text = new string(Enumerable.Range(0, 8).Select(_ => chars[random.Next(chars.Length)]).ToArray());
        }

        private void cboDiscountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isPercent = cboDiscountType.SelectedIndex == 0;
            lblDiscountValue.Text = isPercent ? "Phần trăm giảm (%)" : "Số tiền giảm (VNĐ)";
            lblMaxDiscount.Visible = txtMaxDiscount.Visible = isPercent;
            if (!isPercent) txtMaxDiscount.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                var promotion = new ChuongTrinhKhuyenMai
                {
                    id = _isEditMode ? _promotionId : null,
                    ten = txtName.Text.Trim(),
                    loai = GetPromotionType(),
                    ngayBatDau = dtpStartDate.Value.Date,
                    ngayKetThuc = dtpEndDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59),
                    moTa = txtDescription.Text.Trim(),
                    isDelete = false
                };

                decimal.TryParse(txtDiscountValue.Text, out decimal discountValue);
                decimal.TryParse(txtMinOrder.Text, out decimal minOrder);
                
                decimal maxDiscount = 0;
                if (cboDiscountType.SelectedIndex == 0 && !string.IsNullOrWhiteSpace(txtMaxDiscount.Text))
                    decimal.TryParse(txtMaxDiscount.Text, out maxDiscount);
                else if (cboDiscountType.SelectedIndex == 1)
                    maxDiscount = GIA_TRI_TOI_DA_MAC_DINH;

                string giamTheo = cboDiscountType.SelectedIndex == 0 ? GIAM_THEO_PHAN_TRAM : GIAM_THEO_GIA_TIEN;
                
                string conditionDesc = rdoCategory.Checked 
                    ? $"Áp dụng cho {_selectedCategories.Count} danh mục"
                    : rdoProduct.Checked 
                        ? $"Áp dụng cho {_selectedProducts.Count} sản phẩm"
                        : "Áp dụng cho toàn bộ cửa hàng";
                
                if (minOrder > 0)
                    conditionDesc += $" - Đơn hàng tối thiểu: {minOrder:N0} VNĐ";

                var condition = new DieuKienApDung
                {
                    dieuKien = conditionDesc,
                    giaTriToiThieu = discountValue,
                    giamTheo = giamTheo,
                    giaTriToiDa = maxDiscount,
                    isDelete = false
                };

                int.TryParse(txtUsageLimit.Text, out int soLanSuDung);
                if (soLanSuDung <= 0) soLanSuDung = 100;
                
                string promoTrangThai = cboStatus.SelectedIndex == 0 ? "Hoạt động" : "Không hoạt động";

                var categoryIds = rdoCategory.Checked ? _selectedCategories.Select(c => c.Id).ToList() : null;
                var productIds = rdoProduct.Checked ? _selectedProducts.Select(p => p.Id).ToList() : null;

                if (_isEditMode)
                {
                    var result = _controller.UpdatePromotionComplete(promotion, condition, txtCode.Text.Trim(),
                        discountValue, soLanSuDung, promoTrangThai, categoryIds, productIds);
                    ShowResultAndClose(result.success, result.message);
                }
                else
                {
                    var result = _controller.CreatePromotionComplete(promotion, condition, txtCode.Text.Trim(),
                        discountValue, soLanSuDung, promoTrangThai, categoryIds, productIds);
                    ShowResultAndClose(result.success, result.message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu khuyến mãi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowResultAndClose(bool success, string message)
        {
            MessageBox.Show(message, success ? "Thành công" : "Lỗi", MessageBoxButtons.OK,
                success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            if (success)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
                return ShowWarning("Vui lòng nhập tên chương trình!", txtName);

            if (dtpStartDate.Value >= dtpEndDate.Value)
                return ShowWarning("Ngày kết thúc phải sau ngày bắt đầu!", dtpEndDate);

            if (string.IsNullOrWhiteSpace(txtDiscountValue.Text))
                return ShowWarning("Vui lòng nhập giá trị giảm!", txtDiscountValue);

            if (!decimal.TryParse(txtDiscountValue.Text, out decimal discountValue) || discountValue <= 0)
                return ShowWarning("Giá trị giảm phải là số dương!", txtDiscountValue);

            if (cboDiscountType.SelectedIndex == 0 && discountValue > 100)
                return ShowWarning("Phần trăm giảm không được vượt quá 100%!", txtDiscountValue);

            if (cboDiscountType.SelectedIndex == 0 && !string.IsNullOrWhiteSpace(txtMaxDiscount.Text))
            {
                if (!decimal.TryParse(txtMaxDiscount.Text, out decimal maxDiscount) || maxDiscount <= 0)
                    return ShowWarning("Giảm tối đa phải là số dương!", txtMaxDiscount);
            }

            if (!string.IsNullOrWhiteSpace(txtMinOrder.Text))
            {
                if (!decimal.TryParse(txtMinOrder.Text, out decimal minOrder) || minOrder < 0)
                    return ShowWarning("Đơn hàng tối thiểu phải là số không âm!", txtMinOrder);
            }

            if (!string.IsNullOrWhiteSpace(txtUsageLimit.Text))
            {
                if (!int.TryParse(txtUsageLimit.Text, out int usageLimit) || usageLimit <= 0)
                    return ShowWarning("Tổng số lần sử dụng phải là số nguyên dương!", txtUsageLimit);
            }

            if (rdoCategory.Checked && _selectedCategories.Count == 0)
                return ShowWarning("Vui lòng chọn ít nhất một danh mục!", null);

            if (rdoProduct.Checked && _selectedProducts.Count == 0)
                return ShowWarning("Vui lòng chọn ít nhất một sản phẩm!", null);

            return true;
        }

        private bool ShowWarning(string message, Control control)
        {
            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            control?.Focus();
            return false;
        }

        private string GetPromotionType()
        {
            if (rdoCategory.Checked) return "Danh mục";
            if (rdoProduct.Checked) return "Sản phẩm";
            return "Toàn bộ cửa hàng";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn hủy? Dữ liệu chưa lưu sẽ bị mất!", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            var textBox = sender as TextBox;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            if (e.KeyChar == '.' && textBox.Text.Contains("."))
                e.Handled = true;
        }

        private void IntegerTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _controller?.Dispose();
            _categoryController?.Dispose();
            _productController?.Dispose();
            base.OnFormClosing(e);
        }

        private void lblMaxDiscount_Click(object sender, EventArgs e) { }
        private void txtMaxDiscount_TextChanged(object sender, EventArgs e) { }
    }
}
