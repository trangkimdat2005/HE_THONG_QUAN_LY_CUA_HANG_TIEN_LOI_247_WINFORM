using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Promotions
{
    public partial class frmPromotionDetail : Form
    {
        private AppDbContext _context;
        private string _promotionId;
        private bool _isAddMode;
        private bool _isViewMode;

        public frmPromotionDetail(string promotionId, AppDbContext context, bool isViewMode = false)
        {
            InitializeComponent();
            _promotionId = promotionId;
            _context = context;
            _isAddMode = string.IsNullOrEmpty(promotionId);
            _isViewMode = isViewMode;
        }

        private void frmPromotionDetail_Load(object sender, EventArgs e)
        {
            try
            {
                LoadPromotionTypes();
                LoadDiscountTypes();

                if (_isViewMode)
                {
                    SetViewMode();
                    lblTitle.Text = "XEM CHI TIẾT CHƯƠNG TRÌNH KHUYẾN MÃI";
                }
                else if (_isAddMode)
                {
                    lblTitle.Text = "THÊM CHƯƠNG TRÌNH KHUYẾN MÃI MỚI";
                    txtPromotionId.Text = "Tự động tạo";
                    dtpStartDate.Value = DateTime.Now;
                    dtpEndDate.Value = DateTime.Now.AddMonths(1);
                }
                else
                {
                    lblTitle.Text = "CHỈNH SỬA CHƯƠNG TRÌNH KHUYẾN MÃI";
                    LoadPromotionDetail();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPromotionTypes()
        {
            var types = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Discount", "Giảm giá"),
                new KeyValuePair<string, string>("Gift", "Tặng quà"),
                new KeyValuePair<string, string>("BOGO", "Mua 1 tặng 1"),
                new KeyValuePair<string, string>("Loyalty", "Ưu đãi khách hàng")
            };

            cmbType.DataSource = types;
            cmbType.DisplayMember = "Value";
            cmbType.ValueMember = "Key";
        }

        private void LoadDiscountTypes()
        {
            var discountTypes = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("PhanTram", "Phần trăm (%)"),
                new KeyValuePair<string, string>("GiaTri", "Giá trị (VNĐ)"),
                new KeyValuePair<string, string>("KhongGiam", "Không giảm")
            };

            cmbDiscountType.DataSource = discountTypes;
            cmbDiscountType.DisplayMember = "Value";
            cmbDiscountType.ValueMember = "Key";
        }

        private void LoadPromotionDetail()
        {
            try
            {
                var promotion = _context.ChuongTrinhKhuyenMais.Find(_promotionId);
                if (promotion != null)
                {
                    txtPromotionId.Text = promotion.id;
                    txtPromotionName.Text = promotion.ten;
                    cmbType.SelectedValue = promotion.loai;
                    dtpStartDate.Value = promotion.ngayBatDau;
                    dtpEndDate.Value = promotion.ngayKetThuc;
                    txtDescription.Text = promotion.moTa ?? "";

                    // Load conditions if exists
                    var condition = promotion.DieuKienApDungs.FirstOrDefault(d => !d.isDelete);
                    if (condition != null)
                    {
                        txtCondition.Text = condition.dieuKien;
                        numMinValue.Value = condition.giaTriToiThieu;
                        cmbDiscountType.SelectedValue = condition.giamTheo;
                        numMaxValue.Value = condition.giaTriToiDa;
                    }

                    // Load promotion codes
                    LoadPromotionCodes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết chương trình: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPromotionCodes()
        {
            try
            {
                var codes = _context.MaKhuyenMais
                    .Where(m => m.chuongTrinhId == _promotionId && !m.isDelete)
                    .Select(m => new
                    {
                        m.id,
                        m.code,
                        m.giaTri,
                        m.soLanSuDung,
                        m.trangThai
                    })
                    .ToList();

                dgvPromotionCodes.DataSource = codes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải mã khuyến mãi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetViewMode()
        {
            txtPromotionName.ReadOnly = true;
            cmbType.Enabled = false;
            dtpStartDate.Enabled = false;
            dtpEndDate.Enabled = false;
            txtDescription.ReadOnly = true;
            txtCondition.ReadOnly = true;
            numMinValue.Enabled = false;
            cmbDiscountType.Enabled = false;
            numMaxValue.Enabled = false;
            btnSave.Visible = false;
            btnCancel.Text = "Đóng";
            groupBoxCodes.Visible = true;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtPromotionName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên chương trình!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPromotionName.Focus();
                return false;
            }

            if (cmbType.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại chương trình!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbType.Focus();
                return false;
            }

            if (dtpStartDate.Value >= dtpEndDate.Value)
            {
                MessageBox.Show("Ngày kết thúc phải sau ngày bắt đầu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpEndDate.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCondition.Text))
            {
                MessageBox.Show("Vui lòng nhập điều kiện áp dụng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCondition.Focus();
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_isViewMode)
                return;

            if (!ValidateInput())
                return;

            try
            {
                if (_isAddMode)
                {
                    // Add new promotion
                    var newPromotion = new ChuongTrinhKhuyenMai
                    {
                        id = Guid.NewGuid().ToString(),
                        ten = txtPromotionName.Text.Trim(),
                        loai = cmbType.SelectedValue.ToString(),
                        ngayBatDau = dtpStartDate.Value,
                        ngayKetThuc = dtpEndDate.Value,
                        moTa = txtDescription.Text.Trim(),
                        isDelete = false
                    };

                    _context.ChuongTrinhKhuyenMais.Add(newPromotion);

                    // Add condition
                    var condition = new DieuKienApDung
                    {
                        id = Guid.NewGuid().ToString(),
                        chuongTrinhId = newPromotion.id,
                        dieuKien = txtCondition.Text.Trim(),
                        giaTriToiThieu = numMinValue.Value,
                        giamTheo = cmbDiscountType.SelectedValue.ToString(),
                        giaTriToiDa = numMaxValue.Value,
                        isDelete = false
                    };

                    _context.DieuKienApDungs.Add(condition);
                    _context.SaveChanges();

                    MessageBox.Show("Thêm chương trình khuyến mãi thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Update existing promotion
                    var promotion = _context.ChuongTrinhKhuyenMais.Find(_promotionId);
                    if (promotion != null)
                    {
                        promotion.ten = txtPromotionName.Text.Trim();
                        promotion.loai = cmbType.SelectedValue.ToString();
                        promotion.ngayBatDau = dtpStartDate.Value;
                        promotion.ngayKetThuc = dtpEndDate.Value;
                        promotion.moTa = txtDescription.Text.Trim();

                        // Update or create condition
                        var condition = promotion.DieuKienApDungs.FirstOrDefault(d => !d.isDelete);
                        if (condition != null)
                        {
                            condition.dieuKien = txtCondition.Text.Trim();
                            condition.giaTriToiThieu = numMinValue.Value;
                            condition.giamTheo = cmbDiscountType.SelectedValue.ToString();
                            condition.giaTriToiDa = numMaxValue.Value;
                        }
                        else
                        {
                            var newCondition = new DieuKienApDung
                            {
                                id = Guid.NewGuid().ToString(),
                                chuongTrinhId = _promotionId,
                                dieuKien = txtCondition.Text.Trim(),
                                giaTriToiThieu = numMinValue.Value,
                                giamTheo = cmbDiscountType.SelectedValue.ToString(),
                                giaTriToiDa = numMaxValue.Value,
                                isDelete = false
                            };
                            _context.DieuKienApDungs.Add(newCondition);
                        }

                        _context.SaveChanges();

                        MessageBox.Show("Cập nhật chương trình khuyến mãi thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu chương trình khuyến mãi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAddCode_Click(object sender, EventArgs e)
        {
            if (_isAddMode)
            {
                MessageBox.Show("Vui lòng lưu chương trình trước khi thêm mã khuyến mãi!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Open dialog to add promotion code
            MessageBox.Show("Chức năng thêm mã khuyến mãi đang được phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
