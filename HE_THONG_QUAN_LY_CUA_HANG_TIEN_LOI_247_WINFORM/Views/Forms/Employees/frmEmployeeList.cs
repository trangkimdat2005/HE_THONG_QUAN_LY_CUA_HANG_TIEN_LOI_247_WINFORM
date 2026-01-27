using ClosedXML.Excel;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Employees
{
    public partial class frmEmployeeList : Form
    {
        private readonly IQuanLyServices quanLyServices;
        public frmEmployeeList()
        {
            InitializeComponent();
            quanLyServices = new QuanLyServices();
            DisplayCustomers(quanLyServices.GetList<NhanVien>());
            SetupSearchComboboxes();
        }
        private void DisplayCustomers(List<NhanVien> xs)
        {
            dgvEmployees.Rows.Clear();

            foreach (var x in xs)
            {
                dgvEmployees.Rows.Add(
                    x.id,
                    x.hoTen,
                    x.gioiTinh ? "Nam" : "Nữ",
                    x.soDienThoai,
                    x.email,
                    x.chucVu,
                    x.trangThai
                );
            }
            lblTotal.Text = $"Tổng số nhân viên: {xs.Count}";
        }
        private void CustomizeInterface()
        {
            // Style GridView
            dgvEmployees.BorderStyle = BorderStyle.None;
            dgvEmployees.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvEmployees.GridColor = Color.FromArgb(230, 230, 230);
            dgvEmployees.RowHeadersVisible = false;
            dgvEmployees.EnableHeadersVisualStyles = false;
            dgvEmployees.ColumnHeadersHeight = 40;
            dgvEmployees.RowTemplate.Height = 40;

            dgvEmployees.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185); // Xanh Dương
            dgvEmployees.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvEmployees.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvEmployees.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            dgvEmployees.DefaultCellStyle.SelectionBackColor = Color.FromArgb(211, 233, 252); // Xanh Nhạt
            dgvEmployees.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvEmployees.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvEmployees.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);

            // Căn giữa các cột ngắn
            colId.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colGender.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void LoadDummyData()
        {
            dgvEmployees.Rows.Clear();
            dgvEmployees.Rows.Add("NV001", "Nguyễn Văn An", "Nam", "0901234567", "an.nguyen@email.com", "Quản lý", "Hoạt động");
            dgvEmployees.Rows.Add("NV002", "Trần Thị Bích", "Nữ", "0902345678", "bich.tran@email.com", "Thu ngân", "Hoạt động");
            dgvEmployees.Rows.Add("NV003", "Lê Hoàng Nam", "Nam", "0903456789", "nam.le@email.com", "Kho", "Nghỉ phép");
            dgvEmployees.Rows.Add("NV004", "Phạm Thu Hà", "Nữ", "0904567890", "ha.pham@email.com", "Thu ngân", "Hoạt động");

            lblTotal.Text = $"Tổng số: {dgvEmployees.Rows.Count} nhân viên";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var f = new frmEmployees(Services.FormMode.Add))
            {
                var result = f.ShowDialog();

                if (result == DialogResult.OK)
                {
                    DisplayCustomers(quanLyServices.GetList<NhanVien>());
                }
            }

        }

        private void pnlTop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string id = dgvEmployees.CurrentRow.Cells["colId"].Value.ToString();

            var f = new frmEmployees(FormMode.Edit, id);
            if (f.ShowDialog() == DialogResult.OK)
                DisplayCustomers(quanLyServices.GetList<NhanVien>());
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.CurrentRow == null || dgvEmployees.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string id = dgvEmployees.CurrentRow.Cells["colId"].Value.ToString();
            string name = dgvEmployees.CurrentRow.Cells["colName"].Value.ToString();

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa nhân viên: {name}?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                var nv = quanLyServices.GetById<NhanVien>(id);

                if (nv == null)
                {
                    MessageBox.Show("Không tìm thấy nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (quanLyServices.HardDelete(nv))
                {
                    MessageBox.Show("Đã xóa vĩnh viễn nhân viên khỏi hệ thống!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (quanLyServices.SoftDelete(nv))
                    {
                        MessageBox.Show(
                            "Nhân viên này đã có dữ liệu liên quan (Hóa đơn/Phiếu nhập) nên không thể xóa vĩnh viễn.\n\nHệ thống đã chuyển sang XÓA MỀM (Ẩn nhân viên).",
                            "Thông báo chuyển đổi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi: Không thể xóa nhân viên này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                DisplayCustomers(quanLyServices.GetList<NhanVien>());
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            cmbSearchStatus.SelectedIndex = 0;
            cmbSearchPosition.SelectedIndex = 0;
            DisplayCustomers(quanLyServices.GetList<NhanVien>());
        }
        private void SetupSearchComboboxes()
        {
            // Setup ComboBox Trạng Thái
            cmbSearchStatus.Items.Clear();
            cmbSearchStatus.Items.Add("Tất cả");
            cmbSearchStatus.Items.Add("Đang làm");
            cmbSearchStatus.Items.Add("Nghỉ");
            cmbSearchStatus.SelectedIndex = 0;

            // Setup ComboBox Chức Vụ
            cmbSearchPosition.Items.Clear();
            cmbSearchPosition.Items.Add("Tất cả");
            cmbSearchPosition.Items.Add("Admin");
            cmbSearchPosition.Items.Add("Quản lý");
            cmbSearchPosition.Items.Add("Nhân viên");
            cmbSearchPosition.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            string status = cmbSearchStatus.SelectedItem?.ToString();
            string position = cmbSearchPosition.SelectedItem?.ToString();

            var listData = quanLyServices.GetList<NhanVien>();

            var result = listData.Where(x =>
                (string.IsNullOrEmpty(keyword) ||
                 x.id.ToLower().Contains(keyword) ||
                 x.hoTen.ToLower().Contains(keyword) ||
                 x.soDienThoai.Contains(keyword))
                &&
                (status == "Tất cả" || x.trangThai == status)
                &&
                (position == "Tất cả" || x.chucVu == position)
            ).ToList();

            DisplayCustomers(result);

            if (result.Count == 0)
            {
                MessageBox.Show("Không tìm thấy kết quả nào phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
                    sfd.FileName = "DanhSachNhanVien_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Danh Sách");

                            for (int i = 0; i < dgv.Columns.Count; i++)
                            {
                                // Dòng 1 trong Excel là tiêu đề
                                worksheet.Cell(1, i + 1).Value = dgv.Columns[i].HeaderText;

                                // Làm đậm tiêu đề
                                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                                worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightGray;
                            }

                            for (int i = 0; i < dgv.Rows.Count; i++)
                            {
                                for (int j = 0; j < dgv.Columns.Count; j++)
                                {
                                    // Kiểm tra null trước khi lấy giá trị
                                    var cellValue = dgv.Rows[i].Cells[j].Value;
                                    worksheet.Cell(i + 2, j + 1).Value = cellValue != null ? cellValue.ToString() : "";
                                }
                            }

                            worksheet.Columns().AdjustToContents();

                            workbook.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show("Xuất file Excel thành công!\nĐường dẫn: " + sfd.FileName, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel(dgvEmployees);
        }

        private void dgvEmployees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Excel Files|*.xlsx;*.xls";
            dialog.Title = "Chọn file Excel danh sách nhân viên";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Hiển thị con trỏ xoay xoay để người dùng biết đang xử lý
                this.Cursor = Cursors.WaitCursor;

                try
                {
                    // Gọi Service Excel riêng
                    var excelService = new HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Services.ExcelImportService();
                    var result = excelService.ImportNhanVien(dialog.FileName);

                    this.Cursor = Cursors.Default; // Trả lại con trỏ chuột

                    if (result.IsSuccess)
                    {
                        MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnRefresh_Click(null, null); // Load lại danh sách
                    }
                    else
                    {
                        // Nếu có danh sách lỗi cụ thể -> Hiện thông báo chi tiết
                        if (result.ErrorLogs != null && result.ErrorLogs.Count > 0)
                        {
                            string errorMsg = result.Message + "\n\nChi tiết lỗi:\n" + string.Join("\n", result.ErrorLogs.Take(10)); // Chỉ hiện 10 lỗi đầu tiên cho đỡ dài
                            if (result.ErrorLogs.Count > 10) errorMsg += "\n... và còn nhiều lỗi khác.";

                            MessageBox.Show(errorMsg, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Lỗi Application: " + ex.Message);
                }
            }
        }
    }
}