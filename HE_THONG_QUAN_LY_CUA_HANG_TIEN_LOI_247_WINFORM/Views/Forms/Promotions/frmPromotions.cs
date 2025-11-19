using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Promotions
{
    public partial class frmPromotions : Form
    {
        private AppDbContext _context;
        private string _selectedPromotionId;
        private bool _isDataLoaded = false;

        public frmPromotions()
        {
            InitializeComponent();
            _context = new AppDbContext();
        }

        private async void frmPromotions_Load(object sender, EventArgs e)
        {
            try
            {
                // Hiển thị loading message
                dgvPromotions.DataSource = null;
                lblTitle.Text = "QUẢN LÝ CHƯƠNG TRÌNH KHUYẾN MÃI - Đang tải dữ liệu...";

                // Disable controls trong khi loading
                DisableControlsWhileLoading();

                // Load data async để không block UI
                await LoadDataAsync();

                _isDataLoaded = true;

                lblTitle.Text = "QUẢN LÝ CHƯƠNG TRÌNH KHUYẾN MÃI";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblTitle.Text = "QUẢN LÝ CHƯƠNG TRÌNH KHUYẾN MÃI - Lỗi tải dữ liệu";
            }
            finally
            {
                EnableControlsAfterLoading();
            }
        }

        private void DisableControlsWhileLoading()
        {
            panelSearch.Enabled = false;
            panelButtons.Enabled = false;
            dgvPromotions.Enabled = false;
        }

        private void EnableControlsAfterLoading()
        {
            panelSearch.Enabled = true;
            panelButtons.Enabled = true;
            dgvPromotions.Enabled = true;
        }

        private async Task LoadDataAsync()
        {
            await Task.Run(() =>
            {
                // Load promotions synchronously in background thread
                this.Invoke((MethodInvoker)delegate
                {
                    LoadPromotionTypes();
                    LoadPromotions();
                });
            });
        }

        private void LoadPromotionTypes()
        {
            try
            {
                var types = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("", "-- Tất cả --"),
                    new KeyValuePair<string, string>("Discount", "Giảm giá"),
                    new KeyValuePair<string, string>("Gift", "Tặng quà"),
                    new KeyValuePair<string, string>("BOGO", "Mua 1 tặng 1"),
                    new KeyValuePair<string, string>("Loyalty", "Ưu đãi khách hàng")
                };

                cmbType.DataSource = types;
                cmbType.DisplayMember = "Value";
                cmbType.ValueMember = "Key";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách loại khuyến mãi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPromotions()
        {
            try
            {
                var query = _context.ChuongTrinhKhuyenMais
                    .Where(p => !p.isDelete)
                    .AsQueryable();

                // Apply search filter
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    string searchText = txtSearch.Text.ToLower();
                    query = query.Where(p => p.ten.ToLower().Contains(searchText) || 
                                           p.moTa.ToLower().Contains(searchText));
                }

                // Apply type filter
                if (cmbType.SelectedValue != null && !string.IsNullOrEmpty(cmbType.SelectedValue.ToString()))
                {
                    string type = cmbType.SelectedValue.ToString();
                    if (!string.IsNullOrEmpty(type))
                    {
                        query = query.Where(p => p.loai == type);
                    }
                }

                // Apply date filter
                if (chkDateFilter.Checked)
                {
                    DateTime startDate = dtpStartDate.Value.Date;
                    DateTime endDate = dtpEndDate.Value.Date;
                    query = query.Where(p => p.ngayBatDau >= startDate && p.ngayKetThuc <= endDate);
                }

                // Take only 1000 records max to prevent performance issues
                var promotions = query
                    .Take(1000)
                    .Select(p => new
                    {
                        p.id,
                        p.ten,
                        Loai = p.loai == "Discount" ? "Giảm giá" :
                               p.loai == "Gift" ? "Tặng quà" :
                               p.loai == "BOGO" ? "Mua 1 tặng 1" :
                               p.loai == "Loyalty" ? "Ưu đãi khách hàng" : p.loai,
                        p.ngayBatDau,
                        p.ngayKetThuc,
                        TrangThai = DateTime.Now < p.ngayBatDau ? "Chưa bắt đầu" :
                                   DateTime.Now > p.ngayKetThuc ? "Đã kết thúc" : "Đang áp dụng",
                        p.moTa
                    })
                    .OrderByDescending(p => p.ngayBatDau)
                    .ToList();

                dgvPromotions.DataSource = promotions;

                // Update status label
                lblStatus.Text = $"Tổng số: {promotions.Count} chương trình";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách chương trình khuyến mãi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPromotions_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPromotions.CurrentRow != null && _isDataLoaded)
            {
                _selectedPromotionId = dgvPromotions.CurrentRow.Cells["colId"].Value?.ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var detailForm = new frmPromotionDetail(null, _context);
            detailForm.FormClosed += (s, args) =>
            {
                // Reload data after detail form is closed
                LoadPromotions();
            };
            detailForm.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPromotions.CurrentRow == null || string.IsNullOrEmpty(_selectedPromotionId))
            {
                MessageBox.Show("Vui lòng chọn chương trình khuyến mãi cần chỉnh sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var detailForm = new frmPromotionDetail(_selectedPromotionId, _context);
            detailForm.FormClosed += (s, args) =>
            {
                // Reload data after detail form is closed
                LoadPromotions();
            };
            detailForm.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPromotions.CurrentRow == null || string.IsNullOrEmpty(_selectedPromotionId))
            {
                MessageBox.Show("Vui lòng chọn chương trình khuyến mãi cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa chương trình khuyến mãi này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var promotion = _context.ChuongTrinhKhuyenMais.Find(_selectedPromotionId);
                    if (promotion != null)
                    {
                        promotion.isDelete = true;
                        _context.SaveChanges();
                        MessageBox.Show("Xóa chương trình khuyến mãi thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPromotions();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa chương trình khuyến mãi: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadPromotions();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbType.SelectedIndex = 0;
            chkDateFilter.Checked = false;
            dtpStartDate.Value = DateTime.Now.AddMonths(-1);
            dtpEndDate.Value = DateTime.Now.AddMonths(1);
            LoadPromotions();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xuất Excel đang được phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnViewDetail_Click(object sender, EventArgs e)
        {
            if (dgvPromotions.CurrentRow == null || string.IsNullOrEmpty(_selectedPromotionId))
            {
                MessageBox.Show("Vui lòng chọn chương trình khuyến mãi cần xem!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var detailForm = new frmPromotionDetail(_selectedPromotionId, _context, true);
            detailForm.ShowDialog();
        }

        private void chkDateFilter_CheckedChanged(object sender, EventArgs e)
        {
            dtpStartDate.Enabled = chkDateFilter.Checked;
            dtpEndDate.Enabled = chkDateFilter.Checked;
        }
    }
}
