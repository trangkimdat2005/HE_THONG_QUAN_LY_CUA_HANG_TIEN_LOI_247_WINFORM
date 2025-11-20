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

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Suppliers
{
    public partial class frmTransactionHistory : Form
    {
        private AppDbContext _context;
        private string _selectedTransactionId;

        public frmTransactionHistory()
        {
            InitializeComponent();
            _context = new AppDbContext();
            CustomizeInterface(); // Gọi hàm làm đẹp
        }

        private void CustomizeInterface()
        {
            // --- 1. Style Grid Master (Giao dịch) ---
            StyleGrid(dgvTransactions);
            dgvTransactions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185); // Xanh dương
            dgvTransactions.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvTransactions.DefaultCellStyle.SelectionBackColor = Color.FromArgb(211, 233, 252); // Xanh nhạt khi chọn
            dgvTransactions.DefaultCellStyle.SelectionForeColor = Color.Black;

            // --- 2. Style Grid Detail (Chi tiết) ---
            StyleGrid(dgvDetails);
            dgvDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94); // Xám đen cho phân biệt
            dgvDetails.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDetails.DefaultCellStyle.SelectionBackColor = Color.FromArgb(236, 240, 241); // Xám rất nhạt
            dgvDetails.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void StyleGrid(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // Chỉ kẻ ngang
            dgv.GridColor = Color.FromArgb(230, 230, 230);
            dgv.RowHeadersVisible = false;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersHeight = 40;
            dgv.RowTemplate.Height = 40;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.75F);
            dgv.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
        }

        private void frmTransactionHistory_Load(object sender, EventArgs e)
        {
            try
            {
                SetupDataGridView();
                LoadSupplierComboBox();

                dtpFromDate.Value = DateTime.Now.AddMonths(-1);
                dtpToDate.Value = DateTime.Now;

                LoadTransactions();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            // Code định nghĩa cột giữ nguyên như cũ, chỉ thêm căn chỉnh nếu cần
            dgvTransactions.AutoGenerateColumns = false;
            dgvDetails.AutoGenerateColumns = false;

            // ... (Giữ nguyên phần Add Columns của bạn ở đây) ...

            // Ví dụ mẫu 1 cột để đảm bảo bạn không quên:
            if (dgvTransactions.Columns.Count == 0)
            {
                // Thêm cột cho Transactions
                dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "Mã GD", DataPropertyName = "id", Width = 120 });
                dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSupplierName", HeaderText = "Nhà cung cấp", DataPropertyName = "TenNCC", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
                dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDate", HeaderText = "Ngày giao dịch", DataPropertyName = "ngayGD", Width = 150 });
                dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTotalAmount", HeaderText = "Tổng tiền", DataPropertyName = "tongTien", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
                // ...
            }

            if (dgvDetails.Columns.Count == 0)
            {
                // Thêm cột cho Details
                dgvDetails.Columns.Add(new DataGridViewTextBoxColumn { Name = "colProductName", HeaderText = "Tên sản phẩm", DataPropertyName = "TenSanPham", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
                // ...
            }
        }
        private void LoadSupplierComboBox()
        {
            try
            {
                var suppliers = _context.NhaCungCaps
                    .Where(s => !s.isDelete)
                    .OrderBy(s => s.ten)
                    .Select(s => new
                    {
                        s.id,
                        s.ten
                    })
                    .ToList();

                // Thêm item "Tất cả"
                var allSuppliers = new List<object>();
                allSuppliers.Add(new { id = "", ten = "-- Tất cả nhà cung cấp --" });
                allSuppliers.AddRange(suppliers);

                cboSupplier.DataSource = allSuppliers;
                cboSupplier.DisplayMember = "ten";
                cboSupplier.ValueMember = "id";
                cboSupplier.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhà cung cấp: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTransactions()
        {
            try
            {
                string supplierId = cboSupplier.SelectedValue?.ToString();
                DateTime fromDate = dtpFromDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date.AddDays(1).AddSeconds(-1);

                var query = _context.LichSuGiaoDiches
                    .Where(t => !t.isDelete && t.ngayGD >= fromDate && t.ngayGD <= toDate);

                // Lọc theo nhà cung cấp nếu được chọn
                if (!string.IsNullOrEmpty(supplierId))
                {
                    query = query.Where(t => t.nhaCungCapId == supplierId);
                }

                var transactions = query
                    .Select(t => new
                    {
                        t.id,
                        TenNCC = t.NhaCungCap.ten,
                        t.ngayGD,
                        t.tongTien,
                        SoMatHang = t.ChiTietGiaoDichNCCs.Count(ct => !ct.isDelete)
                    })
                    .OrderByDescending(t => t.ngayGD)
                    .ToList();

                dgvTransactions.DataSource = transactions;

                // Tính tổng
                decimal totalAmount = transactions.Sum(t => t.tongTien);
                lblTotalAmount.Text = $"Tổng giá trị: {totalAmount:N0} VNĐ";
                //lblStatus.Text = $"Tổng số: {transactions.Count} giao dịch";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách giao dịch: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTransactions_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTransactions.CurrentRow != null)
            {
                _selectedTransactionId = dgvTransactions.CurrentRow.Cells["colId"].Value?.ToString();
                LoadTransactionDetails(_selectedTransactionId);
            }
        }

        private void LoadTransactionDetails(string transactionId)
        {
            if (string.IsNullOrEmpty(transactionId))
            {
                dgvDetails.DataSource = null;
                return;
            }

            try
            {
                var details = _context.ChiTietGiaoDichNCCs
                    .Where(ct => ct.giaoDichId == transactionId && !ct.isDelete)
                    .ToList()
                    .Select(ct => 
                    {
                        // Tách sanPhamDonViId thành sanPhamId và donViId
                        var parts = ct.sanPhamDonViId.Split('-');
                        string sanPhamId = parts.Length > 0 ? parts[0] : "";
                        string donViId = parts.Length > 1 ? parts[1] : "";

                        // Lấy thông tin sản phẩm
                        var sanPham = _context.SanPhams.Find(sanPhamId);
                        var donVi = _context.DonViDoLuongs.Find(donViId);

                        return new
                        {
                            TenSanPham = sanPham?.ten ?? "N/A",
                            DonVi = donVi?.ten ?? "N/A",
                            ct.soLuong,
                            ct.donGia,
                            ct.thanhTien
                        };
                    })
                    .ToList();

                dgvDetails.DataSource = details;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết giao dịch: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadTransactions();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cboSupplier.SelectedIndex = 0;
            dtpFromDate.Value = DateTime.Now.AddMonths(-1);
            dtpToDate.Value = DateTime.Now;
            LoadTransactions();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xuất Excel đang được phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cboSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSupplier.SelectedIndex > 0) // Chỉ load khi không phải "Tất cả"
            {
                LoadTransactions();
            }
        }

        // Cleanup
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _context?.Dispose();
        }
    }
}
