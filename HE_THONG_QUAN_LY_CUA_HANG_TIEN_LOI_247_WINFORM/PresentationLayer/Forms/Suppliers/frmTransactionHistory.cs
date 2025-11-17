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
        }

        private void frmTransactionHistory_Load(object sender, EventArgs e)
        {
            try
            {
                SetupDataGridView();
                LoadSupplierComboBox();
                
                // Khởi tạo date range mặc định
                dtpFromDate.Value = DateTime.Now.AddMonths(-1);
                dtpToDate.Value = DateTime.Now;
                
                LoadTransactions();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            // Cấu hình DataGridView cho giao dịch
            if (dgvTransactions.Columns.Count == 0)
            {
                dgvTransactions.AutoGenerateColumns = false;
                
                dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colId",
                    HeaderText = "Mã giao dịch",
                    DataPropertyName = "id",
                    Width = 150
                });

                dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colSupplierName",
                    HeaderText = "Nhà cung cấp",
                    DataPropertyName = "TenNCC",
                    Width = 200
                });

                dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colDate",
                    HeaderText = "Ngày giao dịch",
                    DataPropertyName = "ngayGD",
                    Width = 150,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
                });

                dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colTotalAmount",
                    HeaderText = "Tổng tiền",
                    DataPropertyName = "tongTien",
                    Width = 150,
                    DefaultCellStyle = new DataGridViewCellStyle 
                    { 
                        Format = "N0",
                        Alignment = DataGridViewContentAlignment.MiddleRight
                    }
                });

                dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colProductCount",
                    HeaderText = "Số mặt hàng",
                    DataPropertyName = "SoMatHang",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle 
                    { 
                        Alignment = DataGridViewContentAlignment.MiddleCenter
                    }
                });
            }

            // Cấu hình DataGridView cho chi tiết
            if (dgvDetails.Columns.Count == 0)
            {
                dgvDetails.AutoGenerateColumns = false;
                
                dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colProductName",
                    HeaderText = "Tên sản phẩm",
                    DataPropertyName = "TenSanPham",
                    Width = 250
                });

                dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colUnit",
                    HeaderText = "Đơn vị",
                    DataPropertyName = "DonVi",
                    Width = 100
                });

                dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colQuantity",
                    HeaderText = "Số lượng",
                    DataPropertyName = "soLuong",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle 
                    { 
                        Alignment = DataGridViewContentAlignment.MiddleCenter
                    }
                });

                dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colPrice",
                    HeaderText = "Đơn giá",
                    DataPropertyName = "donGia",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle 
                    { 
                        Format = "N0",
                        Alignment = DataGridViewContentAlignment.MiddleRight
                    }
                });

                dgvDetails.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "colAmount",
                    HeaderText = "Thành tiền",
                    DataPropertyName = "thanhTien",
                    Width = 150,
                    DefaultCellStyle = new DataGridViewCellStyle 
                    { 
                        Format = "N0",
                        Alignment = DataGridViewContentAlignment.MiddleRight
                    }
                });
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
                lblStatus.Text = $"Tổng số: {transactions.Count} giao dịch";
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
