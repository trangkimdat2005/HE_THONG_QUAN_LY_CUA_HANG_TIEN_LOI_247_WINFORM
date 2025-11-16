using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

// Đảm bảo namespace này khớp với dự án của bạn
namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Main
{
    // Đây là tệp code-behind cho frmDashboard.Designer.cs
    public partial class frmDashboard : Form
    {
        private Timer timer;

        public frmDashboard()
        {
            InitializeComponent();
        }

        // 1. SỰ KIỆN LOAD FORM: Tải tất cả dữ liệu mẫu khi form được mở
        private void frmDashboard_Load(object sender, EventArgs e)
        {
            // Đặt màu nền xám nhạt cho Form
            this.BackColor = Color.FromArgb(248, 249, 250);

            LoadSampleData();
            SetupTimer();
        }

        // 2. SỰ KIỆN NÚT REFRESH: Tải lại dữ liệu mẫu
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Tạm thời chỉ hiển thị thông báo
            // Trong thực tế, bạn sẽ gọi lại các hàm tải dữ liệu từ CSDL
            MessageBox.Show("Đã làm mới dữ liệu!");
            // LoadSampleData(); // Bạn có thể bỏ bình luận dòng này để tải lại
        }

        // Hàm thiết lập đồng hồ
        private void SetupTimer()
        {
            timer = new Timer();
            timer.Interval = 1000; // 1 giây
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        // Cập nhật đồng hồ mỗi giây
        private void Timer_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        // === HÀM TẢI DỮ LIỆU MẪU ===

        // Hàm tổng hợp để gọi tất cả các hàm tải dữ liệu
        private void LoadSampleData()
        {
            LoadKpiData();
            LoadRevenueChartData();
            LoadTopProductsChartData();
            LoadAlertsGridData();
            LoadActivitiesGridData();
            LoadShiftInfo();
        }

        // Tải dữ liệu 4 ô KPI
        private void LoadKpiData()
        {
            lblRevenueValue.Text = "250.000.000đ";
            lblOrdersValue.Text = "124 đơn";
            lblCustomersValue.Text = "1,234";
            lblAlertsValue.Text = "3 sản phẩm";
        }

        // Tải dữ liệu biểu đồ doanh thu
        private void LoadRevenueChartData()
        {
            chartRevenue.Series.Clear();
            chartRevenue.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chartRevenue.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;

            var series = new Series("Doanh thu")
            {
                ChartType = SeriesChartType.SplineArea, // Biểu đồ đường cong có tô màu
                Color = Color.FromArgb(100, 94, 148, 255), // Màu xanh
                BorderColor = Color.FromArgb(94, 148, 255),
                BorderWidth = 2
            };

            // Thêm dữ liệu mẫu 7 ngày
            Random rand = new Random();
            for (int i = 7; i > 0; i--)
            {
                series.Points.AddXY($"Ngày {i}", rand.Next(10, 50) * 1000000);
            }

            chartRevenue.Series.Add(series);
            chartRevenue.Legends[0].Enabled = false; // Ẩn chú thích
        }

        // Tải dữ liệu biểu đồ top sản phẩm (ĐÃ CẬP NHẬT SANG BIỂU ĐỒ DONUT)
        private void LoadTopProductsChartData()
        {
            chartTopProducts.Series.Clear();
            chartTopProducts.Legends.Clear(); // Xóa legend cũ
            chartTopProducts.ChartAreas[0].BackColor = Color.Transparent;

            // Thêm Legend (chú thích) mới và tùy chỉnh
            Legend legend = new Legend("TopProductsLegend");
            legend.Docking = Docking.Right;
            legend.Alignment = StringAlignment.Center;
            legend.Font = new Font("Segoe UI", 9F);
            legend.IsEquallySpacedItems = true;
            legend.BackColor = Color.Transparent;
            chartTopProducts.Legends.Add(legend);

            var series = new Series("Số lượng")
            {
                ChartType = SeriesChartType.Doughnut, // Thay đổi thành biểu đồ Donut
                IsValueShownAsLabel = false // Tắt hiển thị giá trị trên chart
            };

            // Thêm dữ liệu mẫu
            // Thêm tên sản phẩm làm Label để hiển thị trong Legend
            series.Points.AddXY("Coca Cola", 120);
            series.Points[0].Label = "Coca Cola";
            series.Points[0].LegendText = "Coca Cola (120)";

            series.Points.AddXY("Nước suối", 98);
            series.Points[1].Label = "Nước suối";
            series.Points[1].LegendText = "Nước suối (98)";

            series.Points.AddXY("Mì Hảo Hảo", 87);
            series.Points[2].Label = "Mì Hảo Hảo";
            series.Points[2].LegendText = "Mì Hảo Hảo (87)";

            series.Points.AddXY("Bánh Oreo", 55);
            series.Points[3].Label = "Bánh Oreo";
            series.Points[3].LegendText = "Bánh Oreo (55)";

            series.Points.AddXY("Pepsi", 42);
            series.Points[4].Label = "Pepsi";
            series.Points[4].LegendText = "Pepsi (42)";


            // Tô màu
            series.Points[0].Color = Color.FromArgb(0, 123, 255);
            series.Points[1].Color = Color.FromArgb(40, 167, 69);
            series.Points[2].Color = Color.FromArgb(255, 193, 7);
            series.Points[3].Color = Color.FromArgb(220, 53, 69);
            series.Points[4].Color = Color.FromArgb(108, 117, 125);

            series["PieLabelStyle"] = "Disabled"; // Ẩn nhãn bên trong Donut
            series["DoughnutRadius"] = "70"; // Tăng độ dày của Donut

            chartTopProducts.Series.Add(series);
        }

        // Tải dữ liệu lưới Cảnh báo tồn kho
        private void LoadAlertsGridData()
        {
            // Thiết lập Guna DataGridView
            SetupGridStyle(dgvAlerts, Color.FromArgb(220, 53, 69));

            // Tạo cột
            dgvAlerts.Columns.Clear();
            dgvAlerts.Columns.Add("MaSP", "Mã SP");
            dgvAlerts.Columns.Add("TenSP", "Tên Sản Phẩm");
            dgvAlerts.Columns.Add("TonKho", "Tồn Kho");

            // Thêm dữ liệu mẫu
            dgvAlerts.Rows.Add("SP003", "Pepsi Lon", "10 chai");
            dgvAlerts.Rows.Add("SP015", "Mì ly", "8 hộp");
            dgvAlerts.Rows.Add("SP022", "Bánh Oreo", "5 gói");
        }

        // Tải dữ liệu lưới Hoạt động gần đây
        private void LoadActivitiesGridData()
        {
            // Thiết lập Guna DataGridView
            SetupGridStyle(dgvRecentActivities, Color.FromArgb(255, 193, 7));

            // Tạo cột
            dgvRecentActivities.Columns.Clear();
            dgvRecentActivities.Columns.Add("MaHD", "Mã HĐ");
            dgvRecentActivities.Columns.Add("NhanVien", "Nhân Viên");
            dgvRecentActivities.Columns.Add("GiaTri", "Giá Trị");
            dgvRecentActivities.Columns.Add("ThoiGian", "Thời Gian");

            // Thêm dữ liệu mẫu
            dgvRecentActivities.Rows.Add("HD081", "Nguyễn Văn A", "50.000đ", "10:30 AM");
            dgvRecentActivities.Rows.Add("HD082", "Trần Thị B", "128.000đ", "10:25 AM");
            dgvRecentActivities.Rows.Add("PN003", "Nhập kho NCC X", "10.000.000đ", "10:00 AM");
        }

        // Tải thông tin ca làm việc
        private void LoadShiftInfo()
        {
            lblShiftName.Text = "Ca 1 (Sáng)";
            lblShiftTime.Text = "🕐 Thời gian: 06:00 - 14:00";
            lblShiftEmployees.Text = "👤 Nhân viên: Nguyễn Văn A, Trần Thị B";
        }

        // Hàm helper để tùy chỉnh Guna DataGridView (THEO PHONG CÁCH MỚI)
        private void SetupGridStyle(Guna.UI2.WinForms.Guna2DataGridView grid, Color headerColor)
        {
            // Thiết lập header
            grid.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(242, 242, 242); // Màu header xám nhạt
            grid.ThemeStyle.HeaderStyle.ForeColor = Color.Black; // Chữ đen
            grid.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grid.ThemeStyle.HeaderStyle.Height = 40;
            grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;

            // Thiết lập Rows
            grid.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9.5F);
            grid.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            grid.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            grid.RowTemplate.Height = 35;

            // Thiết lập viền
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = Color.FromArgb(231, 229, 255); // Màu kẻ ngang
        }
    }
}