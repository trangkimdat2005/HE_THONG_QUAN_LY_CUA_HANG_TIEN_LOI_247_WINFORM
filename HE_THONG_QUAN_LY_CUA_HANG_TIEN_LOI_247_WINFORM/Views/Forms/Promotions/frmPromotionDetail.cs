using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO; 

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Promotions
{
    public partial class frmPromotionDetail : Form
    {
        #region Fields & Properties
        private readonly PromotionController _promotionController;
        private string _promotionId;
        private bool _isEditMode = false;
        #endregion

        #region Constructors
        public frmPromotionDetail()
        {
            InitializeComponent();
            _promotionController = new PromotionController();
            _isEditMode = false;
            this.Load += frmPromotionDetail_Load;
        }

        public frmPromotionDetail(string promotionId) : this()
        {
            _promotionId = promotionId;
            _isEditMode = true;
        }
        #endregion

        #region Form Load & Init
        private void frmPromotionDetail_Load(object sender, EventArgs e)
        {
            this.AutoScroll = true;
            try
            {
                LoadPromotionTypes();
                LoadGiamTheoComboBox();

                if (_isEditMode && !string.IsNullOrEmpty(_promotionId))
                {
                    LoadPromotionData();
                    this.Text = $"Chỉnh sửa - {_promotionId}";
                    lblTitle.Text = $"CHỈNH SỬA CHƯƠNG TRÌNH: {_promotionId}";

                    if (txtPromotionId != null)
                    {
                        txtPromotionId.Text = _promotionId;
                        txtPromotionId.ReadOnly = true;
                        txtPromotionId.BackColor = SystemColors.Control;
                    }
                }
                else
                {
                    this.Text = "Thêm mới chương trình khuyến mãi";
                    lblTitle.Text = "THÊM MỚI CHƯƠNG TRÌNH KHUYẾN MÃI";
                    
                    GenerateAndShowNewId();
                    
                    dtpStartDate.Value = DateTime.Now;
                    dtpEndDate.Value = DateTime.Now.AddMonths(1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải form: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateAndShowNewId()
        {
            if (txtPromotionId == null) return;

            try
            {
                string newId = _promotionController.GenerateNewPromotionId();
                txtPromotionId.Text = newId;
                txtPromotionId.ReadOnly = true;
                txtPromotionId.BackColor = SystemColors.Control;
                txtPromotionId.ForeColor = Color.Black;
            }
            catch (Exception)
            {
                txtPromotionId.Text = "(Tự động)";
                txtPromotionId.ForeColor = Color.Gray;
            }
        }

        private void LoadPromotionTypes()
        {
            var types = new List<string> { 
                "Giảm giá hóa đơn", 
                "Giảm giá sản phẩm", 
                "Tặng quà", 
                "Đồng giá", 
                "Khác" 
            };
            
            if (cmbType != null)
            {
                cmbType.Items.Clear();
                foreach (var type in types)
                {
                    cmbType.Items.Add(type);
                }
                cmbType.SelectedIndex = 0;
                cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        private void LoadGiamTheoComboBox()
        {
            // Load vào ComboBox có sẵn trong Designer
            if (cmbGiamTheo != null)
            {
                cmbGiamTheo.Items.Clear();
                cmbGiamTheo.Items.Add("Phần trăm");
                cmbGiamTheo.Items.Add("Giá tiền");
                cmbGiamTheo.SelectedIndex = 0;
                cmbGiamTheo.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        private void LoadPromotionData()
        {
            try
            {
                var promotion = _promotionController.GetPromotionById(_promotionId);
                if (promotion == null)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu khuyến mãi!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Load thông tin chương trình
                txtPromotionName.Text = promotion.ten;
                cmbType.SelectedItem = promotion.loai;
                dtpStartDate.Value = promotion.ngayBatDau;
                dtpEndDate.Value = promotion.ngayKetThuc;
                if (txtDescription != null) 
                    txtDescription.Text = promotion.moTa ?? "";

                // Load điều kiện áp dụng (nếu có)
                var conditions = _promotionController.GetConditionsByProgramId(_promotionId);
                if (conditions != null && conditions.Count > 0)
                {
                    var firstCondition = conditions.First();
                    if (txtCondition != null)
                        txtCondition.Text = firstCondition.dieuKien ?? "";
                    if (numMinValue != null)
                        numMinValue.Value = firstCondition.giaTriToiThieu;
                    if (numMaxValue != null)
                        numMaxValue.Value = firstCondition.giaTriToiDa;
                    
                    // Set giamTheo
                    if (cmbGiamTheo != null)
                    {
                        if (firstCondition.giamTheo == "Phần trăm")
                            cmbGiamTheo.SelectedIndex = 0;
                        else if (firstCondition.giamTheo == "Giá tiền")
                            cmbGiamTheo.SelectedIndex = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Button Save & Cancel
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtPromotionName?.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên chương trình!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPromotionName?.Focus();
                    return;
                }

                if (cmbType?.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn loại chương trình!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbType?.Focus();
                    return;
                }

                if (dtpStartDate.Value.Date >= dtpEndDate.Value.Date)
                {
                    MessageBox.Show("Ngày kết thúc phải sau ngày bắt đầu!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpEndDate?.Focus();
                    return;
                }
                List<DieuKienApDung> conditions = null;
                
                if (!string.IsNullOrWhiteSpace(txtCondition?.Text))
                {
                    if (cmbGiamTheo?.SelectedItem == null)
                    {
                        MessageBox.Show("Vui lòng chọn loại giảm (Phần trăm/Giá tiền)!", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbGiamTheo?.Focus();
                        return;
                    }

                    string giamTheo = cmbGiamTheo.SelectedItem.ToString();

                    if (numMinValue != null && numMinValue.Value < 0)
                    {
                        MessageBox.Show("Giá trị tối thiểu phải >= 0!", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        numMinValue.Focus();
                        return;
                    }

                    if (numMaxValue != null && numMaxValue.Value > 0)
                    {
                        if (numMaxValue.Value < numMinValue.Value)
                        {
                            MessageBox.Show("Giá trị tối đa phải >= giá trị tối thiểu!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            numMaxValue.Focus();
                            return;
                        }
                    }

                    if (giamTheo == "Phần trăm")
                    {
                        if (numMinValue.Value > 100)
                        {
                            MessageBox.Show("Phần trăm giảm không được vượt quá 100%!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            numMinValue.Focus();
                            return;
                        }

                        if (numMaxValue.Value > 100)
                        {
                            MessageBox.Show("Phần trăm tối đa không được vượt quá 100%!", "Thông báo", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            numMaxValue.Focus();
                            return;
                        }
                    }

                    // Tạo điều kiện
                    conditions = new List<DieuKienApDung>
                    {
                        new DieuKienApDung
                        {
                            dieuKien = txtCondition.Text.Trim(),
                            giaTriToiThieu = numMinValue?.Value ?? 0,
                            giamTheo = giamTheo, // "Phần trăm" hoặc "Giá tiền"
                            giaTriToiDa = numMaxValue?.Value ?? 0,
                            isDelete = false
                        }
                    };
                }

                // Xác nhận lưu
                string confirmMessage = _isEditMode 
                    ? $"Bạn có chắc muốn cập nhật chương trình '{txtPromotionName.Text}'?" 
                    : $"Bạn có chắc muốn tạo chương trình '{txtPromotionName.Text}'?";

                if (MessageBox.Show(confirmMessage, "Xác nhận", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                // Tạo đối tượng ChuongTrinhKhuyenMai
                string currentId = _isEditMode ? _promotionId : txtPromotionId?.Text;
                var promotion = new ChuongTrinhKhuyenMai
                {
                    id = currentId,
                    ten = txtPromotionName.Text.Trim(),
                    loai = cmbType.SelectedItem?.ToString() ?? "Giảm giá hóa đơn",
                    ngayBatDau = dtpStartDate.Value.Date,
                    ngayKetThuc = dtpEndDate.Value.Date,
                    moTa = txtDescription?.Text?.Trim() ?? "",
                    isDelete = false
                };

                // Lưu dữ liệu
                bool success;
                string message;

                if (_isEditMode)
                {
                    (success, message) = _promotionController.UpdatePromotion(promotion, conditions);
                }
                else
                {
                    string newId;
                    (success, message, newId) = _promotionController.CreatePromotion(promotion, conditions);
                }

                if (success)
                {
                    MessageBox.Show($" {message}", "Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($" {message}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($" Lỗi: {ex.Message}\n\n{ex.InnerException?.Message}", 
                    "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bool hasData = !string.IsNullOrWhiteSpace(txtPromotionName?.Text);

            if (hasData)
            {
                if (MessageBox.Show("Hủy bỏ? Dữ liệu chưa lưu sẽ mất!", 
                    "Xác nhận", MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        #region Cleanup
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _promotionController?.Dispose();
            base.OnFormClosing(e);
        }
        #endregion
    }
}