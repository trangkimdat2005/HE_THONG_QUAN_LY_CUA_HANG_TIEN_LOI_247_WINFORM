using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Linq;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Main
{
    public partial class frmDashboard : Form
    {
        private Timer timer;
        private AppDbContext _context;

        public frmDashboard()
        {
            InitializeComponent();
            _context = new AppDbContext();
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            try
            {
                // Đặt màu nền xám nhạt cho Form
                this.BackColor = Color.FromArgb(248, 249, 250);

                // Test connection first
                if (!TestDatabaseConnection())
                {
                    MessageBox.Show(
                        "Không thể kết nối database!\n\n" +
                        "Vui lòng kiểm tra:\n" +
                        "1. SQL Server đang chạy\n" +
                        "2. Connection string trong App.config\n" +
                        "3. Database đã được tạo\n\n" +
                        "Hiển thị dữ liệu mẫu...",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    LoadSampleData();
                }
                else
                {
                    LoadRealData();
                }

                SetupTimer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Lỗi khi tải Dashboard:\n{ex.Message}\n\nHiển thị dữ liệu mẫu...",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                LoadSampleData();
            }
        }

        private bool TestDatabaseConnection()
        {
            try
            {
                return _context.Database.Exists();
            }
            catch
            {
                return false;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadRealData();
                MessageBox.Show("Đã làm mới dữ liệu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi làm mới dữ liệu:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupTimer()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        // ==================== LOAD DỮ LIỆU THẬT TỪ DATABASE ====================

        private void LoadRealData()
        {
            try
            {
                LoadKpiDataFromDb();
                LoadRevenueChartDataFromDb();
                LoadTopProductsChartDataFromDb();
                LoadAlertsGridDataFromDb();
                LoadActivitiesGridDataFromDb();
                LoadShiftInfoFromDb();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi load dữ liệu: {ex.Message}", ex);
            }
        }

        private void LoadKpiDataFromDb()
        {
            try
            {
                DateTime today = DateTime.Today;

                // 1. Doanh thu hôm nay
                var todayRevenue = _context.HoaDons
                    .Where(h => !h.isDelete && 
                           h.ngayLap.Year == today.Year &&
                           h.ngayLap.Month == today.Month &&
                           h.ngayLap.Day == today.Day)
                    .Sum(h => (decimal?)h.tongTien) ?? 0;

                lblRevenueValue.Text = todayRevenue.ToString("N0") + "đ";

                // 2. Số đơn hàng hôm nay
                var todayOrders = _context.HoaDons
                    .Count(h => !h.isDelete && 
                           h.ngayLap.Year == today.Year &&
                           h.ngayLap.Month == today.Month &&
                           h.ngayLap.Day == today.Day);

                lblOrdersValue.Text = $"{todayOrders} đơn";

                // 3. Tổng số khách hàng
                var totalCustomers = _context.KhachHangs
                    .Count(k => !k.isDelete);

                lblCustomersValue.Text = totalCustomers.ToString("N0");

                // 4. Số sản phẩm tồn kho thấp (< 20)
                var lowStockCount = _context.TonKhoes
                    .Count(t => t.soLuongTon < 20);

                lblAlertsValue.Text = $"{lowStockCount} sản phẩm";
            }
            catch (Exception ex)
            {
                lblRevenueValue.Text = "0đ";
                lblOrdersValue.Text = "0 đơn";
                lblCustomersValue.Text = "0";
                lblAlertsValue.Text = "0 sản phẩm";
                throw new Exception($"Lỗi load KPI: {ex.Message}");
            }
        }

        private void LoadRevenueChartDataFromDb()
        {
            try
            {
                chartRevenue.Series.Clear();
                chartRevenue.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chartRevenue.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;

                var series = new Series("Doanh thu")
                {
                    ChartType = SeriesChartType.SplineArea,
                    Color = Color.FromArgb(100, 94, 148, 255),
                    BorderColor = Color.FromArgb(94, 148, 255),
                    BorderWidth = 2
                };

                // Lấy doanh thu 7 ngày gần nhất
                for (int i = 6; i >= 0; i--)
                {
                    DateTime date = DateTime.Today.AddDays(-i);
                    
                    var dailyRevenue = _context.HoaDons
                        .Where(h => !h.isDelete &&
                               h.ngayLap.Year == date.Year &&
                               h.ngayLap.Month == date.Month &&
                               h.ngayLap.Day == date.Day)
                        .Sum(h => (decimal?)h.tongTien) ?? 0;

                    series.Points.AddXY(date.ToString("dd/MM"), (double)dailyRevenue);
                }

                chartRevenue.Series.Add(series);
                chartRevenue.Legends[0].Enabled = false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi load biểu đồ doanh thu: {ex.Message}");
            }
        }

        private void LoadTopProductsChartDataFromDb()
        {
            try
            {
                chartTopProducts.Series.Clear();
                chartTopProducts.Legends.Clear();
                chartTopProducts.ChartAreas[0].BackColor = Color.Transparent;

                Legend legend = new Legend("TopProductsLegend");
                legend.Docking = Docking.Right;
                legend.Alignment = StringAlignment.Center;
                legend.Font = new Font("Segoe UI", 9F);
                legend.IsEquallySpacedItems = true;
                legend.BackColor = Color.Transparent;
                chartTopProducts.Legends.Add(legend);

                var series = new Series("Số lượng")
                {
                    ChartType = SeriesChartType.Doughnut,
                    IsValueShownAsLabel = false
                };

                // Lấy top 5 sản phẩm bán chạy
                var topProducts = _context.ChiTietHoaDons
                    .Where(ct => !ct.isDelete)
                    .GroupBy(ct => ct.sanPhamDonViId)
                    .Select(g => new
                    {
                        SanPhamDonViId = g.Key,
                        TongSoLuong = g.Sum(ct => ct.soLuong)
                    })
                    .OrderByDescending(x => x.TongSoLuong)
                    .Take(5)
                    .ToList();

                Color[] colors = new Color[]
                {
                    Color.FromArgb(0, 123, 255),
                    Color.FromArgb(40, 167, 69),
                    Color.FromArgb(255, 193, 7),
                    Color.FromArgb(220, 53, 69),
                    Color.FromArgb(108, 117, 125)
                };

                if (topProducts.Any())
                {
                    int index = 0;
                    foreach (var product in topProducts)
                    {
                        // Lấy tên sản phẩm - Fix: Use LINQ query instead of Find for composite key
                        var sanPhamDonVi = _context.SanPhamDonVis
                            .FirstOrDefault(spv => spv.id == product.SanPhamDonViId);
                        
                        string tenSanPham = "N/A";
                        if (sanPhamDonVi != null)
                        {
                            var sanPham = _context.SanPhams.Find(sanPhamDonVi.sanPhamId);
                            tenSanPham = sanPham?.ten ?? "N/A";
                        }

                        int pointIndex = series.Points.AddXY(tenSanPham, product.TongSoLuong);
                        series.Points[pointIndex].Label = tenSanPham;
                        series.Points[pointIndex].LegendText = $"{tenSanPham} ({product.TongSoLuong})";
                        series.Points[pointIndex].Color = colors[index % colors.Length];
                        index++;
                    }
                }
                else
                {
                    // Nếu không có dữ liệu, hiển thị thông báo
                    series.Points.AddXY("Chưa có dữ liệu", 1);
                    series.Points[0].Color = Color.LightGray;
                }

                series["PieLabelStyle"] = "Disabled";
                series["DoughnutRadius"] = "70";

                chartTopProducts.Series.Add(series);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi load top sản phẩm: {ex.Message}");
            }
        }

        private void LoadAlertsGridDataFromDb()
        {
            try
            {
                SetupGridStyle(dgvAlerts, Color.FromArgb(220, 53, 69));

                dgvAlerts.Columns.Clear();
                dgvAlerts.Columns.Add("MaSP", "Mã SP");
                dgvAlerts.Columns.Add("TenSP", "Tên Sản Phẩm");
                dgvAlerts.Columns.Add("TonKho", "Tồn Kho");

                // Lấy sản phẩm có tồn kho < 20
                var lowStockProducts = _context.TonKhoes
                    .Where(t => t.soLuongTon < 20)
                    .OrderBy(t => t.soLuongTon)
                    .Take(10)
                    .ToList();

                foreach (var tonKho in lowStockProducts)
                {
                    // Fix: Use LINQ query instead of Find for composite key
                    var sanPhamDonVi = _context.SanPhamDonVis
                        .FirstOrDefault(spv => spv.id == tonKho.sanPhamDonViId);
                    
                    if (sanPhamDonVi != null)
                    {
                        var sanPham = _context.SanPhams.Find(sanPhamDonVi.sanPhamId);
                        var donVi = _context.DonViDoLuongs.Find(sanPhamDonVi.donViId);

                        dgvAlerts.Rows.Add(
                            sanPham?.id ?? "N/A",
                            sanPham?.ten ?? "N/A",
                            $"{tonKho.soLuongTon} {donVi?.ten ?? ""}"
                        );
                    }
                }

                if (lowStockProducts.Count == 0)
                {
                    dgvAlerts.Rows.Add("N/A", "Không có sản phẩm cảnh báo", "0");
                }
            }
            catch (Exception ex)
            {
                dgvAlerts.Rows.Clear();
                dgvAlerts.Rows.Add("ERROR", $"Lỗi: {ex.Message}", "0");
            }
        }

        private void LoadActivitiesGridDataFromDb()
        {
            try
            {
                SetupGridStyle(dgvRecentActivities, Color.FromArgb(255, 193, 7));

                dgvRecentActivities.Columns.Clear();
                dgvRecentActivities.Columns.Add("MaHD", "Mã HĐ");
                dgvRecentActivities.Columns.Add("NhanVien", "Nhân Viên");
                dgvRecentActivities.Columns.Add("GiaTri", "Giá Trị");
                dgvRecentActivities.Columns.Add("ThoiGian", "Thời Gian");

                // Lấy 10 hóa đơn gần nhất
                var recentInvoices = _context.HoaDons
                    .Where(h => !h.isDelete)
                    .OrderByDescending(h => h.ngayLap)
                    .Take(10)
                    .ToList();

                foreach (var hoaDon in recentInvoices)
                {
                    var nhanVien = _context.NhanViens.Find(hoaDon.nhanVienId);
                    
                    dgvRecentActivities.Rows.Add(
                        hoaDon.id,
                        nhanVien?.hoTen ?? "N/A",
                        ((decimal)hoaDon.tongTien).ToString("N0") + "đ",
                        hoaDon.ngayLap.ToString("HH:mm dd/MM")
                    );
                }

                if (recentInvoices.Count == 0)
                {
                    dgvRecentActivities.Rows.Add("N/A", "Chưa có hoạt động", "0đ", "-");
                }
            }
            catch (Exception ex)
            {
                dgvRecentActivities.Rows.Clear();
                dgvRecentActivities.Rows.Add("ERROR", $"Lỗi: {ex.Message}", "0đ", "-");
            }
        }

        private void LoadShiftInfoFromDb()
        {
            try
            {
                DateTime now = DateTime.Now;
                
                // Tìm ca làm việc hiện tại
                var currentShift = _context.CaLamViecs
                    .Where(c => !c.isDelete)
                    .ToList()
                    .FirstOrDefault(c => 
                    {
                        var currentTime = now.TimeOfDay;
                        return currentTime >= c.thoiGianBatDau && currentTime <= c.thoiGianKetThuc;
                    });

                if (currentShift != null)
                {
                    lblShiftName.Text = currentShift.tenCa;
                    lblShiftTime.Text = $"🕐 Thời gian: {currentShift.thoiGianBatDau:hh\\:mm} - {currentShift.thoiGianKetThuc:hh\\:mm}";

                    // Lấy nhân viên trong ca
                    var employees = _context.PhanCongCaLamViecs
                        .Where(pc => pc.caLamViecId == currentShift.id && !pc.isDelete)
                        .Take(3)
                        .ToList();

                    if (employees.Any())
                    {
                        var employeeNames = string.Join(", ", 
                            employees.Select(e => _context.NhanViens.Find(e.nhanVienId)?.hoTen ?? "N/A"));
                        lblShiftEmployees.Text = $"👤 Nhân viên: {employeeNames}";
                    }
                    else
                    {
                        lblShiftEmployees.Text = "👤 Nhân viên: Chưa phân công";
                    }
                }
                else
                {
                    lblShiftName.Text = "Không có ca làm việc";
                    lblShiftTime.Text = "🕐 Thời gian: -";
                    lblShiftEmployees.Text = "👤 Nhân viên: -";
                }
            }
            catch (Exception ex)
            {
                lblShiftName.Text = "Lỗi load ca làm việc";
                lblShiftTime.Text = $"🕐 {ex.Message}";
                lblShiftEmployees.Text = "👤 -";
            }
        }

        // ==================== DỮ LIỆU MẪU (FALLBACK) ====================

        private void LoadSampleData()
        {
            LoadKpiData();
            LoadRevenueChartData();
            LoadTopProductsChartData();
            LoadAlertsGridData();
            LoadActivitiesGridData();
            LoadShiftInfo();
        }

        private void LoadKpiData()
        {
            lblRevenueValue.Text = "250.000.000đ";
            lblOrdersValue.Text = "124 đơn";
            lblCustomersValue.Text = "1,234";
            lblAlertsValue.Text = "3 sản phẩm";
        }

        private void LoadRevenueChartData()
        {
            chartRevenue.Series.Clear();
            chartRevenue.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartRevenue.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;

            var series = new Series("Doanh thu")
            {
                ChartType = SeriesChartType.SplineArea,
                Color = Color.FromArgb(100, 94, 148, 255),
                BorderColor = Color.FromArgb(94, 148, 255),
                BorderWidth = 2
            };

            Random rand = new Random();
            for (int i = 7; i > 0; i--)
            {
                series.Points.AddXY($"Ngày {i}", rand.Next(10, 50) * 1000000);
            }

            chartRevenue.Series.Add(series);
            chartRevenue.Legends[0].Enabled = false;
        }

        private void LoadTopProductsChartData()
        {
            chartTopProducts.Series.Clear();
            chartTopProducts.Legends.Clear();
            chartTopProducts.ChartAreas[0].BackColor = Color.Transparent;

            Legend legend = new Legend("TopProductsLegend");
            legend.Docking = Docking.Right;
            legend.Alignment = StringAlignment.Center;
            legend.Font = new Font("Segoe UI", 9F);
            legend.IsEquallySpacedItems = true;
            legend.BackColor = Color.Transparent;
            chartTopProducts.Legends.Add(legend);

            var series = new Series("Số lượng")
            {
                ChartType = SeriesChartType.Doughnut,
                IsValueShownAsLabel = false
            };

            series.Points.AddXY("Coca Cola", 120);
            series.Points[0].LegendText = "Coca Cola (120)";
            series.Points[0].Color = Color.FromArgb(0, 123, 255);

            series.Points.AddXY("Nước suối", 98);
            series.Points[1].LegendText = "Nước suối (98)";
            series.Points[1].Color = Color.FromArgb(40, 167, 69);

            series.Points.AddXY("Mì Hảo Hảo", 87);
            series.Points[2].LegendText = "Mì Hảo Hảo (87)";
            series.Points[2].Color = Color.FromArgb(255, 193, 7);

            series.Points.AddXY("Bánh Oreo", 55);
            series.Points[3].LegendText = "Bánh Oreo (55)";
            series.Points[3].Color = Color.FromArgb(220, 53, 69);

            series.Points.AddXY("Pepsi", 42);
            series.Points[4].LegendText = "Pepsi (42)";
            series.Points[4].Color = Color.FromArgb(108, 117, 125);

            series["PieLabelStyle"] = "Disabled";
            series["DoughnutRadius"] = "70";

            chartTopProducts.Series.Add(series);
        }

        private void LoadAlertsGridData()
        {
            SetupGridStyle(dgvAlerts, Color.FromArgb(220, 53, 69));

            dgvAlerts.Columns.Clear();
            dgvAlerts.Columns.Add("MaSP", "Mã SP");
            dgvAlerts.Columns.Add("TenSP", "Tên Sản Phẩm");
            dgvAlerts.Columns.Add("TonKho", "Tồn Kho");

            dgvAlerts.Rows.Add("SP003", "Pepsi Lon", "10 chai");
            dgvAlerts.Rows.Add("SP015", "Mì ly", "8 hộp");
            dgvAlerts.Rows.Add("SP022", "Bánh Oreo", "5 gói");
        }

        private void LoadActivitiesGridData()
        {
            SetupGridStyle(dgvRecentActivities, Color.FromArgb(255, 193, 7));

            dgvRecentActivities.Columns.Clear();
            dgvRecentActivities.Columns.Add("MaHD", "Mã HĐ");
            dgvRecentActivities.Columns.Add("NhanVien", "Nhân Viên");
            dgvRecentActivities.Columns.Add("GiaTri", "Giá Trị");
            dgvRecentActivities.Columns.Add("ThoiGian", "Thời Gian");

            dgvRecentActivities.Rows.Add("HD081", "Nguyễn Văn A", "50.000đ", "10:30 AM");
            dgvRecentActivities.Rows.Add("HD082", "Trần Thị B", "128.000đ", "10:25 AM");
            dgvRecentActivities.Rows.Add("PN003", "Nhập kho NCC X", "10.000.000đ", "10:00 AM");
        }

        private void LoadShiftInfo()
        {
            lblShiftName.Text = "Ca 1 (Sáng)";
            lblShiftTime.Text = "🕐 Thời gian: 06:00 - 14:00";
            lblShiftEmployees.Text = "👤 Nhân viên: Nguyễn Văn A, Trần Thị B";
        }

        private void SetupGridStyle(Guna.UI2.WinForms.Guna2DataGridView grid, Color headerColor)
        {
            grid.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(242, 242, 242);
            grid.ThemeStyle.HeaderStyle.ForeColor = Color.Black;
            grid.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grid.ThemeStyle.HeaderStyle.Height = 40;
            grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;

            grid.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9.5F);
            grid.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            grid.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            grid.RowTemplate.Height = 35;

            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = Color.FromArgb(231, 229, 255);
        }
    }
}