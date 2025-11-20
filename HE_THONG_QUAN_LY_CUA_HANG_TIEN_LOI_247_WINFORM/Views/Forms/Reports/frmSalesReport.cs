using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Services;
using ClosedXML.Excel;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Reports
{
    public partial class frmSalesReport : Form
    {
        private readonly IQuanLyServices _services;

        public frmSalesReport()
        {
            InitializeComponent();
            _services = new QuanLyServices();
            CustomizeInterface();
        }

        private void CustomizeInterface()
        {
            // Định dạng DateTimePicker
            dtpFromDate.Format = DateTimePickerFormat.Custom;
            dtpFromDate.CustomFormat = "dd/MM/yyyy";
            dtpToDate.Format = DateTimePickerFormat.Custom;
            dtpToDate.CustomFormat = "dd/MM/yyyy";

            // --- Style GridView (Đồng bộ các form trước) ---
            dgvReports.BorderStyle = BorderStyle.None;
            dgvReports.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // Chỉ kẻ ngang
            dgvReports.GridColor = Color.FromArgb(230, 230, 230);
            dgvReports.RowHeadersVisible = false;
            dgvReports.EnableHeadersVisualStyles = false;
            dgvReports.ColumnHeadersHeight = 40;
            dgvReports.RowTemplate.Height = 40;

            // Header màu xanh
            dgvReports.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dgvReports.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvReports.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvReports.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            // Row style
            dgvReports.DefaultCellStyle.SelectionBackColor = Color.FromArgb(211, 233, 252);
            dgvReports.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvReports.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvReports.DefaultCellStyle.Padding = new Padding(5, 0, 0, 0);

            // Căn chỉnh cột
            colSTT.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTotalRevenue.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colAction.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Validation ngày: Đến ngày không được nhỏ hơn Từ ngày
            if (dtpToDate.Value < dtpFromDate.Value)
            {
                dtpToDate.Value = dtpFromDate.Value;
            }
            dtpToDate.MinDate = dtpFromDate.Value;

            dtpFromDate.ValueChanged += (s, e) =>
            {
                dtpToDate.MinDate = DateTimePicker.MinimumDateTime;

                if (dtpFromDate.Value > dtpToDate.Value)
                {
                    dtpToDate.Value = dtpFromDate.Value;
                }

                dtpToDate.MinDate = dtpFromDate.Value;
            };
        }

        private void frmSalesReport_Load(object sender, EventArgs e)
        {
            try
            {
                // Thiết lập ngày mặc định: 1 năm trước đến hôm nay
                dtpFromDate.Value = DateTime.Now.AddMonths(-12);
                dtpToDate.Value = DateTime.Now;

                LoadReports();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadReports()
        {
            try
            {
                dgvReports.Rows.Clear();

                DateTime fromDate = dtpFromDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date.AddDays(1).AddSeconds(-1); // Bao gồm cả ngày cuối

                // Lấy danh sách báo cáo doanh thu từ database
                var reports = _services.GetList<BaoCao>()
                    .Where(bc => bc.loaiBaoCao == "DoanhThu"
                              && !bc.isDelete
                              && bc.tuNgay >= fromDate
                              && bc.denNgay <= toDate)
                    .OrderByDescending(bc => bc.ngayLap)
                    .ToList();

                int stt = 1;
                foreach (var report in reports)
                {
                    // Tính tổng doanh thu từ BaoCaoDoanhThu
                    var totalRevenue = _services.GetList<BaoCaoDoanhThu>()
                        .Where(bcdt => bcdt.baoCaoId == report.id)
                        .Sum(bcdt => bcdt.tongDoanhThu);

                    string period = $"{report.tuNgay:dd/MM/yyyy} - {report.denNgay:dd/MM/yyyy}";
                    string formattedRevenue = $"{totalRevenue:N0} đ";

                    dgvReports.Rows.Add(stt++, report.id, period, formattedRevenue);
                }

                if (dgvReports.Rows.Count == 0)
                {
                    MessageBox.Show("Không có báo cáo nào trong khoảng thời gian này.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải báo cáo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadReports();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Reset về giá trị mặc định
            dtpFromDate.Value = DateTime.Now.AddMonths(-12);
            dtpToDate.Value = DateTime.Now;
            LoadReports();
        }

        /// <summary>
        /// Xuất toàn bộ danh sách báo cáo ra Excel
        /// </summary>
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (dgvReports.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Workbook|*.xlsx";
                    sfd.FileName = $"DanhSachBaoCaoDoanhThu_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Danh Sách Báo Cáo");

                            // Header
                            worksheet.Cell(1, 1).Value = "STT";
                            worksheet.Cell(1, 2).Value = "Mã Báo Cáo";
                            worksheet.Cell(1, 3).Value = "Kỳ Báo Cáo";
                            worksheet.Cell(1, 4).Value = "Tổng Doanh Thu (VNĐ)";

                            // Style header
                            var headerRange = worksheet.Range(1, 1, 1, 4);
                            headerRange.Style.Font.Bold = true;
                            headerRange.Style.Fill.BackgroundColor = XLColor.FromArgb(41, 128, 185);
                            headerRange.Style.Font.FontColor = XLColor.White;
                            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                            // Data
                            for (int i = 0; i < dgvReports.Rows.Count; i++)
                            {
                                var row = dgvReports.Rows[i];
                                int excelRow = i + 2;

                                worksheet.Cell(excelRow, 1).Value = row.Cells["colSTT"].Value?.ToString();
                                worksheet.Cell(excelRow, 2).Value = row.Cells["colReportId"].Value?.ToString();
                                worksheet.Cell(excelRow, 3).Value = row.Cells["colPeriod"].Value?.ToString();

                                // Parse doanh thu (loại bỏ "đ" và format)
                                string revenueStr = row.Cells["colTotalRevenue"].Value?.ToString().Replace(" đ", "").Replace(",", "");
                                if (decimal.TryParse(revenueStr, out decimal revenue))
                                {
                                    worksheet.Cell(excelRow, 4).Value = revenue;
                                    worksheet.Cell(excelRow, 4).Style.NumberFormat.Format = "#,##0";
                                }

                                // Center align STT
                                worksheet.Cell(excelRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                // Right align doanh thu
                                worksheet.Cell(excelRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                            }

                            // Auto-fit columns
                            worksheet.Columns().AdjustToContents();

                            workbook.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xử lý khi nhấn nút Download (Xuất chi tiết báo cáo cụ thể)
        /// </summary>
        private void dgvReports_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dgvReports.Columns["colAction"].Index)
            {
                string reportId = dgvReports.Rows[e.RowIndex].Cells["colReportId"].Value?.ToString();
                if (string.IsNullOrEmpty(reportId))
                {
                    MessageBox.Show("Không tìm thấy mã báo cáo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ExportReportDetail(reportId);
            }
        }

        /// <summary>
        /// Xuất chi tiết một báo cáo cụ thể ra Excel
        /// </summary>
        private void ExportReportDetail(string reportId)
        {
            try
            {
                // Lấy thông tin báo cáo
                var report = _services.GetList<BaoCao>()
                    .FirstOrDefault(bc => bc.id == reportId);

                if (report == null)
                {
                    MessageBox.Show("Không tìm thấy báo cáo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Lấy tất cả hóa đơn trong kỳ báo cáo
                var invoices = _services.GetList<HoaDon>()
                    .Where(hd => hd.ngayLap >= report.tuNgay 
                              && hd.ngayLap <= report.denNgay
                              && !hd.isDelete
                              && hd.trangThai != "Đã hủy")
                    .ToList();

                if (invoices.Count == 0)
                {
                    MessageBox.Show("Không có hóa đơn nào trong kỳ báo cáo này!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy danh sách ID hóa đơn
                var invoiceIds = invoices.Select(inv => inv.id).ToList();

                // Lấy chi tiết hóa đơn
                var chiTietHoaDons = _services.GetList<ChiTietHoaDon>()
                    .Where(ct => invoiceIds.Contains(ct.hoaDonId) && !ct.isDelete)
                    .ToList();

                if (chiTietHoaDons.Count == 0)
                {
                    MessageBox.Show("Báo cáo không có dữ liệu chi tiết!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Workbook|*.xlsx";
                    sfd.FileName = $"BaoCaoDoanhThu_{reportId}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Chi Tiết Doanh Thu");

                            // Tiêu đề báo cáo
                            worksheet.Cell(1, 1).Value = "BÁO CÁO DOANH THU CHI TIẾT";
                            worksheet.Range(1, 1, 1, 6).Merge();
                            worksheet.Cell(1, 1).Style.Font.Bold = true;
                            worksheet.Cell(1, 1).Style.Font.FontSize = 16;
                            worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                            // Thông tin báo cáo
                            worksheet.Cell(2, 1).Value = $"Mã báo cáo: {report.id}";
                            worksheet.Cell(3, 1).Value = $"Kỳ báo cáo: {report.tuNgay:dd/MM/yyyy} - {report.denNgay:dd/MM/yyyy}";
                            worksheet.Cell(4, 1).Value = $"Ngày lập: {report.ngayLap:dd/MM/yyyy HH:mm}";

                            // Header bảng dữ liệu
                            int headerRow = 6;
                            worksheet.Cell(headerRow, 1).Value = "STT";
                            worksheet.Cell(headerRow, 2).Value = "Mã Sản Phẩm";
                            worksheet.Cell(headerRow, 3).Value = "Tên Sản Phẩm";
                            worksheet.Cell(headerRow, 4).Value = "Số Lượng Bán";
                            worksheet.Cell(headerRow, 5).Value = "Đơn Giá";
                            worksheet.Cell(headerRow, 6).Value = "Tổng Doanh Thu";

                            var headerRange = worksheet.Range(headerRow, 1, headerRow, 6);
                            headerRange.Style.Font.Bold = true;
                            headerRange.Style.Fill.BackgroundColor = XLColor.FromArgb(41, 128, 185);
                            headerRange.Style.Font.FontColor = XLColor.White;
                            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                            // Lấy thông tin sản phẩm và đơn vị
                            var sanPhamDonVis = _services.GetList<SanPhamDonVi>();
                            var sanPhams = _services.GetList<SanPham>();
                            var donVis = _services.GetList<DonViDoLuong>();

                            // Nhóm theo sản phẩm và tính tổng
                            var productSummary = chiTietHoaDons
                                .GroupBy(ct => ct.sanPhamDonViId)
                                .Select(g => new
                                {
                                    SanPhamDonViId = g.Key,
                                    SoLuong = g.Sum(x => x.soLuong),
                                    DonGia = g.Average(x => x.donGia),
                                    TongTien = g.Sum(x => x.tongTien)
                                })
                                .ToList();

                            // Data
                            int rowIndex = headerRow + 1;
                            int stt = 1;
                            decimal totalRevenue = 0;

                            foreach (var item in productSummary.OrderByDescending(x => x.TongTien))
                            {
                                var sanPhamDonVi = sanPhamDonVis.FirstOrDefault(spdv => spdv.id == item.SanPhamDonViId);
                                var sanPham = sanPhamDonVi != null 
                                    ? sanPhams.FirstOrDefault(sp => sp.id == sanPhamDonVi.sanPhamId) 
                                    : null;
                                var donVi = sanPhamDonVi != null 
                                    ? donVis.FirstOrDefault(dv => dv.id == sanPhamDonVi.donViId) 
                                    : null;

                                string tenSanPham = sanPham?.ten ?? "N/A";
                                if (donVi != null)
                                {
                                    tenSanPham += $" ({donVi.ten})";
                                }

                                worksheet.Cell(rowIndex, 1).Value = stt++;
                                worksheet.Cell(rowIndex, 2).Value = sanPham?.id ?? "N/A";
                                worksheet.Cell(rowIndex, 3).Value = tenSanPham;
                                worksheet.Cell(rowIndex, 4).Value = item.SoLuong;
                                worksheet.Cell(rowIndex, 5).Value = (double)item.DonGia;
                                worksheet.Cell(rowIndex, 6).Value = (double)item.TongTien;

                                // Format
                                worksheet.Cell(rowIndex, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                worksheet.Cell(rowIndex, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                worksheet.Cell(rowIndex, 5).Style.NumberFormat.Format = "#,##0";
                                worksheet.Cell(rowIndex, 6).Style.NumberFormat.Format = "#,##0";

                                totalRevenue += item.TongTien;
                                rowIndex++;
                            }

                            // Tổng cộng
                            worksheet.Cell(rowIndex, 5).Value = "TỔNG CỘNG:";
                            worksheet.Cell(rowIndex, 5).Style.Font.Bold = true;
                            worksheet.Cell(rowIndex, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                            worksheet.Cell(rowIndex, 6).Value = (double)totalRevenue;
                            worksheet.Cell(rowIndex, 6).Style.Font.Bold = true;
                            worksheet.Cell(rowIndex, 6).Style.NumberFormat.Format = "#,##0";
                            worksheet.Cell(rowIndex, 6).Style.Fill.BackgroundColor = XLColor.LightYellow;

                            // Auto-fit columns
                            worksheet.Columns().AdjustToContents();

                            workbook.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show($"Xuất báo cáo {reportId} thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất báo cáo: {ex.Message}\n\nStack trace: {ex.StackTrace}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}