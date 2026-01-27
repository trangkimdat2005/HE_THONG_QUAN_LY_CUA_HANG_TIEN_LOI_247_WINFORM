using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Utils;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Inventory
{
    public partial class frmInventoryMain : Form
    {
        private readonly StockInController _stockInController;
        private AppDbContext _context;

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
        public frmInventoryMain()
        {
            InitializeComponent();
            _stockInController = new StockInController();
            _context = new AppDbContext();

            SetPlaceholder(txtSearch, "Nhập mã phiếu hoặc tên nhà cung cấp...");
            if (btnExport != null) btnExport.Click += btnExport_Click;

            this.Load += frmInventoryMain_Load;
            //if (btnSearch != null) btnSearch.Click += btnSearch_Click;
            if (btnAdd != null) btnAdd.Click += btnAdd_Click;
            //if (btnDetail != null) btnDetail.Click += btnDetail_Click;
            if (btnDelete != null) btnDelete.Click += btnDelete_Click;

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

        private void frmInventoryMain_Load(object sender, EventArgs e)
        {
            try
            {
                if (dgvImportList != null)
                {
                    dgvImportList.AutoGenerateColumns = false;
                }
                LoadImportReceipts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load danh sách phiếu nhập
        private void LoadImportReceipts()
        {
            try
            {
                var receipts = _stockInController.GetAllImportReceipts();
                dgvImportList.DataSource = receipts;

                MapDataGridViewColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Map cột DataGridView
        private void MapDataGridViewColumns()
        {
            if (dgvImportList.Columns.Count == 0) return;

            if (dgvImportList.Columns["colId"] != null)
                dgvImportList.Columns["colId"].DataPropertyName = "Id";

            if (dgvImportList.Columns["colSupplier"] != null)
                dgvImportList.Columns["colSupplier"].DataPropertyName = "NhaCungCap";

            if (dgvImportList.Columns["colDate"] != null)
            {
                dgvImportList.Columns["colDate"].DataPropertyName = "NgayNhap";
                dgvImportList.Columns["colDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (dgvImportList.Columns["colTotal"] != null)
            {
                dgvImportList.Columns["colTotal"].DataPropertyName = "TongTien";
                dgvImportList.Columns["colTotal"].DefaultCellStyle.Format = "#,##0 đ";
                dgvImportList.Columns["colTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvImportList.Columns["colStatus"] != null)
                dgvImportList.Columns["colStatus"].DataPropertyName = "NhanVien";
        }

        // Tìm kiếm phiếu nhập
        private void PerformSearch()
        {
            try
            {
                string keyword = txtSearch.Text.Trim();

                if (string.IsNullOrEmpty(keyword))
                {
                    LoadImportReceipts();
                    return;
                }

                var searchResults = _stockInController.SearchImportReceipts(keyword);
                dgvImportList.DataSource = searchResults;
                MapDataGridViewColumns();
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

        // Mở form tạo phiếu nhập mới
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var frmStockIn = new Views.Forms.Inventory.frmStockIn();

                DialogResult result = frmStockIn.ShowDialog();

                if (result == DialogResult.OK)
                {
                    LoadImportReceipts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form nhập hàng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xem chi tiết phiếu nhập
        private void btnDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvImportList.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn phiếu nhập để xem chi tiết!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string phieuNhapId = GetCurrentReceiptId();
                if (string.IsNullOrEmpty(phieuNhapId))
                {
                    MessageBox.Show("Không thể xác định mã phiếu nhập!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var details = _stockInController.GetImportReceiptDetails(phieuNhapId);

                string message = $"Chi tiết phiếu nhập: {phieuNhapId}\n\n";
                if (details is System.Collections.IList list)
                {
                    foreach (var item in list)
                    {
                        var itemType = item.GetType();
                        var sanPham = itemType.GetProperty("SanPham")?.GetValue(item);
                        var soLuong = itemType.GetProperty("SoLuong")?.GetValue(item);
                        var donGia = itemType.GetProperty("DonGia")?.GetValue(item);
                        var thanhTien = itemType.GetProperty("ThanhTien")?.GetValue(item);

                        message += $"{sanPham} - SL: {soLuong} - Giá: {donGia:N0}đ - Tổng: {thanhTien:N0}đ\n";
                    }
                }

                MessageBox.Show(message, "Chi tiết phiếu nhập",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xem chi tiết: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvImportList.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn phiếu nhập cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string phieuNhapId = GetCurrentReceiptId();
                if (string.IsNullOrEmpty(phieuNhapId)) return;

                // 3. Xác nhận xóa
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu nhập này?",
                    "Cảnh báo quan trọng",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    // 4. Gọi Controller xóa
                    var result = _stockInController.DeleteImportReceipt(phieuNhapId);

                    if (result.success)
                    {
                        MessageBox.Show(result.message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadImportReceipts(); // Tải lại danh sách
                    }
                    else
                    {
                        MessageBox.Show(result.message, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Lấy ID phiếu nhập hiện tại
        private string GetCurrentReceiptId()
        {
            if (dgvImportList.CurrentRow == null) return null;

            var dataItem = dgvImportList.CurrentRow.DataBoundItem;
            if (dataItem != null)
            {
                var prop = dataItem.GetType().GetProperty("Id");
                if (prop != null) return prop.GetValue(dataItem)?.ToString();
            }

            if (dgvImportList.Columns["colId"] != null && dgvImportList.CurrentRow.Cells["colId"].Value != null)
                return dgvImportList.CurrentRow.Cells["colId"].Value.ToString();

            return null;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel(dgvImportList);
        }

        private void ExportToExcel(DataGridView dgv)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Workbook|*.xlsx";
                    sfd.FileName = "DanhSachPhieuNhap_" + DateTime.Now.ToString("ddMMyyy_HHmmss") + ".xlsx";


                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (var workbook = new ClosedXML.Excel.XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Phiếu Nhập Hàng");


                            // Header (dòng 4)
                            int headerRow = 1;
                            for (int i = 0; i < dgv.Columns.Count; i++)
                            {
                                worksheet.Cell(headerRow, i + 1).Value = dgv.Columns[i].HeaderText;
                                worksheet.Cell(headerRow, i + 1).Style.Font.Bold = true;
                                worksheet.Cell(headerRow, i + 1).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightGreen;
                                worksheet.Cell(headerRow, i + 1).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                            }

                            // Data
                            decimal totalAmount = 0;
                            for (int i = 0; i < dgv.Rows.Count; i++)
                            {
                                for (int j = 0; j < dgv.Columns.Count; j++)
                                {
                                    var cell = worksheet.Cell(i + headerRow + 1, j + 1);
                                    var cellValue = dgv.Rows[i].Cells[j].Value;
                                    
                                    if (cellValue != null)
                                    {
                                        cell.Value = cellValue.ToString();

                                        // Format cột tiền
                                        if (dgv.Columns[j].Name == "colTotal")
                                        {
                                            string cleanValue = cellValue.ToString().Replace(" đ", "").Replace(",", "");
                                            if (decimal.TryParse(cleanValue, out decimal amount))
                                            {
                                                cell.Value = (double)amount;
                                                cell.Style.NumberFormat.Format = "#,##0";
                                                cell.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right;
                                                totalAmount += amount;
                                            }
                                        }
                                        // Format cột ngày
                                        else if (dgv.Columns[j].Name == "colDate")
                                        {
                                            if (DateTime.TryParse(cellValue.ToString(), out DateTime date))
                                            {
                                                cell.Value = date;
                                                cell.Style.DateFormat.Format = "dd/MM/yyyy";
                                                cell.Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        cell.Value = "";
                                    }
                                }
                            }

                            // Tổng cộng
                            int totalRow = dgv.Rows.Count + headerRow + 1;
                            worksheet.Cell(totalRow, 4).Value = "TỔNG CỘNG:";
                            worksheet.Cell(totalRow, 4).Style.Font.Bold = true;
                            worksheet.Cell(totalRow, 4).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right;

                            worksheet.Cell(totalRow, 5).Value = (double)totalAmount;
                            worksheet.Cell(totalRow, 5).Style.Font.Bold = true;
                            worksheet.Cell(totalRow, 5).Style.NumberFormat.Format = "#,##0";
                            worksheet.Cell(totalRow, 5).Style.Alignment.Horizontal = ClosedXML.Excel.XLAlignmentHorizontalValues.Right;
                            worksheet.Cell(totalRow, 5).Style.Fill.BackgroundColor = ClosedXML.Excel.XLColor.LightYellow;

                            // Auto-fit columns
                            worksheet.Columns().AdjustToContents();

                            workbook.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show("Xuất file Excel thành công!\nĐường dẫn: " + sfd.FileName, 
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _stockInController?.Dispose();
            base.OnFormClosing(e);
        }

        private void panelControls_Paint(object sender, PaintEventArgs e)
        {
        }

        private void btnExport_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedNccId = "";

            // Tạo form tạm thời
            using (Form dialogForm = new Form())
            {
                dialogForm.Text = "Chọn Nhà Cung Cấp";
                dialogForm.Size = new Size(350, 180);
                dialogForm.StartPosition = FormStartPosition.CenterParent;
                dialogForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialogForm.MinimizeBox = false;
                dialogForm.MaximizeBox = false;

                // Tạo Label
                Label lbl = new Label();
                lbl.Text = "Vui lòng chọn nhà cung cấp:";
                lbl.Location = new Point(20, 20);
                lbl.AutoSize = true;

                // Tạo ComboBox và đổ dữ liệu
                ComboBox cboNCC = new ComboBox();
                cboNCC.Location = new Point(20, 45);
                cboNCC.Size = new Size(290, 25);
                cboNCC.DropDownStyle = ComboBoxStyle.DropDownList; // Chỉ cho chọn, không cho gõ linh tinh

                var listNcc = _context.NhaCungCaps.Where(n => !n.isDelete).ToList();
                cboNCC.DataSource = listNcc;
                cboNCC.DisplayMember = "ten"; 
                cboNCC.ValueMember = "id";             

                // Tạo nút Xác nhận
                Button btnConfirm = new Button();
                btnConfirm.Text = "Xác nhận";
                btnConfirm.Location = new Point(110, 90);
                btnConfirm.Size = new Size(100, 30);
                btnConfirm.DialogResult = DialogResult.OK; // Bấm nút này = OK
                btnConfirm.Cursor = Cursors.Hand;

                // Thêm các controls vào form
                dialogForm.Controls.AddRange(new Control[] { lbl, cboNCC, btnConfirm });
                dialogForm.AcceptButton = btnConfirm; // Cho phép ấn Enter để xác nhận

                // Hiển thị Dialog và chờ kết quả
                if (dialogForm.ShowDialog() == DialogResult.OK)
                {
                    if (cboNCC.SelectedValue != null)
                    {
                        selectedNccId = cboNCC.SelectedValue.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Bạn chưa chọn nhà cung cấp!", "Cảnh báo");
                        return;
                    }
                }
                else
                {
                    // Người dùng ấn dấu X đóng form hoặc ấn Hủy
                    return;
                }
            }

            string nvId = UserSession.Instance.EmployeeId;


            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Excel Files|*.xlsx;*.xls";
            openFile.Title = "Chọn file danh sách hàng nhập cho NCC: " + selectedNccId;

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;

                // Gọi Service Import
                var service = new HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Services.ExcelImportService();
                var result = service.ImportPhieuNhap(openFile.FileName, selectedNccId, nvId);

                this.Cursor = Cursors.Default;

                if (result.IsSuccess)
                {
                    MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Gọi hàm load lại danh sách phiếu nhập (nếu có)
                     LoadImportReceipts(); 
                }
                else
                {
                    string msg = result.Message;
                    if (result.ErrorLogs != null && result.ErrorLogs.Count > 0)
                        msg += "\n\nChi tiết lỗi:\n" + string.Join("\n", result.ErrorLogs.Take(10));

                    MessageBox.Show(msg, "Lỗi nhập hàng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}