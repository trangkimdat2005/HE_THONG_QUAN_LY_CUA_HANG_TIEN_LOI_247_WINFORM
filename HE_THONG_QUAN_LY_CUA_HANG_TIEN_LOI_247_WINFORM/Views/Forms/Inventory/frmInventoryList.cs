using System;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices; // <--- 1. Thêm thư viện này
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Inventory
{
    public partial class frmInventoryList : Form
    {
        private readonly InventoryController _inventoryController;

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

        public frmInventoryList()
        {
            InitializeComponent();
            _inventoryController = new InventoryController();

            SetPlaceholder(txtSearch, "Nhập mã hoặc tên sản phẩm để tìm kiếm...");

            this.Load += frmInventoryList_Load;

            if (btnSearch != null) btnSearch.Click += btnSearch_Click;
            if (btnRefresh != null) btnRefresh.Click += btnRefresh_Click;

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

        private void frmInventoryList_Load(object sender, EventArgs e)
        {
            try
            {
                if (dgvInventory != null)
                {
                    dgvInventory.AutoGenerateColumns = false;
                }
                LoadInventoryData();
                LoadStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInventoryData()
        {
            try
            {
                var inventory = _inventoryController.GetAllInventory();

                if (inventory == null)
                {
                    dgvInventory.DataSource = null;
                    return;
                }

                dgvInventory.DataSource = inventory;
                MapDataGridViewColumns();
                FormatInventoryRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị dữ liệu: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MapDataGridViewColumns()
        {
            if (dgvInventory.Columns.Count == 0) return;

            // Map theo tên cột trong Designer
            if (dgvInventory.Columns["colMaSP"] != null)
                dgvInventory.Columns["colMaSP"].DataPropertyName = "MaSP";

            if (dgvInventory.Columns["colTenSP"] != null)
                dgvInventory.Columns["colTenSP"].DataPropertyName = "TenSP";

            if (dgvInventory.Columns["colDVT"] != null)
                dgvInventory.Columns["colDVT"].DataPropertyName = "DonVi";

            if (dgvInventory.Columns["colSoLuong"] != null)
                dgvInventory.Columns["colSoLuong"].DataPropertyName = "SoLuongTon";
        }

        private void FormatInventoryRows()
        {
            foreach (DataGridViewRow row in dgvInventory.Rows)
            {
                if (row.IsNewRow) continue;

                var soLuongCell = row.Cells["colSoLuong"]?.Value;
                if (soLuongCell != null && int.TryParse(soLuongCell.ToString(), out int soLuong))
                {
                    if (soLuong == 0)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200);
                        row.DefaultCellStyle.ForeColor = Color.DarkRed;
                    }
                    else if (soLuong < 10)
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200);
                        row.DefaultCellStyle.ForeColor = Color.DarkOrange;
                    }
                }
            }
        }

        // Load thống kê tồn kho
        private void LoadStatistics()
        {
            try
            {
                var stats = _inventoryController.GetInventoryStatistics();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi load thống kê: {ex.Message}");
            }
        }

        private void PerformSearch()
        {
            try
            {
                string keyword = txtSearch.Text.Trim();

                if (string.IsNullOrEmpty(keyword))
                {
                    LoadInventoryData();
                    return;
                }

                // Tìm kiếm theo từ khóa
                var searchResults = _inventoryController.SearchInventory(keyword);
                dgvInventory.DataSource = searchResults;
                MapDataGridViewColumns();
                FormatInventoryRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Clear();
                LoadInventoryData();
                LoadStatistics();

                // Kiểm tra số dòng sau khi load
                int rowCount = dgvInventory.Rows.Count;
                if (rowCount > 0)
                {
                    MessageBox.Show($"Đã làm mới dữ liệu! Tìm thấy {rowCount} sản phẩm.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu tồn kho!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi làm mới: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _inventoryController?.Dispose();
            base.OnFormClosing(e);
        }
    }
}