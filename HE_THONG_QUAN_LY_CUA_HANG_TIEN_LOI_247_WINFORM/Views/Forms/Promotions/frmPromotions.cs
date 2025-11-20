using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Promotions
{
    public partial class frmPromotions : Form
    {
        private readonly PromotionController _promotionController;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        private void SetPlaceholder(TextBox txt, string text)
        {
            if (txt != null)
            {
                SendMessage(txt.Handle, EM_SETCUEBANNER, 0, text);
            }
        }

        public frmPromotions()
        {
            InitializeComponent();
            _promotionController = new PromotionController();

            SetPlaceholder(txtSearch, "Nhập tên hoặc mã chương trình...");

            this.Load += frmPromotions_Load;
            
            if (txtSearch != null)
            {
                txtSearch.TextChanged += (s, e) => { PerformSearch(); };
                txtSearch.KeyPress += (s, e) =>
                {
                    if (e.KeyChar == (char)Keys.Enter)
                    {
                        PerformSearch();
                        e.Handled = true;
                    }
                };
            }
        }

        private void frmPromotions_Load(object sender, EventArgs e)
        {
            try
            {
                if (dgvPromotions != null)
                {
                    dgvPromotions.AutoGenerateColumns = false;
                }
                LoadPromotions();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPromotions()
        {
            AutoScroll = true;
            try
            {
                var promotions = _promotionController.GetAllPromotions();
                dgvPromotions.DataSource = promotions;
                MapDataGridViewColumns();
                
                if (lblStatus != null)
                    lblStatus.Text = $"Tổng số: {promotions.Count} chương trình khuyến mãi";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MapDataGridViewColumns()
        {
            if (dgvPromotions.Columns.Count == 0) return;

            if (dgvPromotions.Columns["colId"] != null)
                dgvPromotions.Columns["colId"].DataPropertyName = "Id";

            if (dgvPromotions.Columns["colName"] != null)
                dgvPromotions.Columns["colName"].DataPropertyName = "Ten";

            if (dgvPromotions.Columns["colType"] != null)
                dgvPromotions.Columns["colType"].DataPropertyName = "Loai";

            if (dgvPromotions.Columns["colStartDate"] != null)
            {
                dgvPromotions.Columns["colStartDate"].DataPropertyName = "NgayBatDau";
                dgvPromotions.Columns["colStartDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (dgvPromotions.Columns["colEndDate"] != null)
            {
                dgvPromotions.Columns["colEndDate"].DataPropertyName = "NgayKetThuc";
                dgvPromotions.Columns["colEndDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (dgvPromotions.Columns.Contains("colCodeCount"))
            {
                dgvPromotions.Columns["colCodeCount"].DataPropertyName = "SoLuongMa";
                dgvPromotions.Columns["colCodeCount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvPromotions.Columns["colStatus"] != null)
                dgvPromotions.Columns["colStatus"].DataPropertyName = "TrangThai";

            if (dgvPromotions.Columns["colDescription"] != null)
                dgvPromotions.Columns["colDescription"].DataPropertyName = "MoTa";
        }

        private void PerformSearch()
        {
            try
            {
                string keyword = txtSearch.Text.Trim();

                if (string.IsNullOrEmpty(keyword))
                {
                    LoadPromotions();
                    return;
                }

                var searchResults = _promotionController.SearchPromotions(keyword);
                dgvPromotions.DataSource = searchResults;
                MapDataGridViewColumns();
                
                if (lblStatus != null)
                    lblStatus.Text = $"Tìm thấy: {searchResults.Count} chương trình";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var frmDetail = new frmPromotionDetail();
                DialogResult result = frmDetail.ShowDialog();

                if (result == DialogResult.OK)
                {
                    LoadPromotions();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form thêm mới: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPromotions.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn chương trình cần sửa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string promotionId = GetCurrentPromotionId();
                if (string.IsNullOrEmpty(promotionId))
                {
                    MessageBox.Show("Không thể xác định mã chương trình!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var frmDetail = new frmPromotionDetail(promotionId);
                DialogResult result = frmDetail.ShowDialog();

                if (result == DialogResult.OK)
                {
                    LoadPromotions();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form sửa: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPromotions.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn chương trình cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string promotionId = GetCurrentPromotionId();
                if (string.IsNullOrEmpty(promotionId))
                {
                    MessageBox.Show("Không thể xác định mã chương trình!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa chương trình này?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var (success, message) = _promotionController.DeletePromotion(promotionId);
                    
                    if (success)
                    {
                        MessageBox.Show(message, "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPromotions();
                    }
                    else
                    {
                        MessageBox.Show(message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa chương trình: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadPromotions();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng đang phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void chkDateFilter_CheckedChanged(object sender, EventArgs e)
        {
            dtpStartDate.Enabled = chkDateFilter.Checked;
            dtpEndDate.Enabled = chkDateFilter.Checked;
        }

        private void dgvPromotions_SelectionChanged(object sender, EventArgs e)
        {
            // Optional: Show detail info when row selected
        }

        private string GetCurrentPromotionId()
        {
            if (dgvPromotions.CurrentRow == null) return null;

            var dataItem = dgvPromotions.CurrentRow.DataBoundItem;
            if (dataItem != null)
            {
                var prop = dataItem.GetType().GetProperty("Id");
                if (prop != null) return prop.GetValue(dataItem)?.ToString();
            }

            if (dgvPromotions.Columns["colId"] != null && dgvPromotions.CurrentRow.Cells["colId"].Value != null)
                return dgvPromotions.CurrentRow.Cells["colId"].Value.ToString();

            return null;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _promotionController?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
