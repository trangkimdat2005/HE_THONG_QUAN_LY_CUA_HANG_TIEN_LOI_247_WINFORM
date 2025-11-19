using ClosedXML.Excel;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            lblTotal.Text= $"Tổng số nhân viên: {xs.Count}";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string id = dgvEmployees.CurrentRow.Cells["colId"].Value.ToString();

            var f = new frmEmployees(FormMode.Edit, id);
            if (f.ShowDialog() == DialogResult.OK)
                DisplayCustomers(quanLyServices.GetList<NhanVien>());

        }

        private void pnlTop_Paint(object sender, PaintEventArgs e)
        {

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

        private void btnSearch_Click_1(object sender, EventArgs e)
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

        private void btnReload_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            cmbSearchStatus.SelectedIndex = 0;
            cmbSearchPosition.SelectedIndex = 0;
            DisplayCustomers(quanLyServices.GetList<NhanVien>());
        }
        private void ExportToExcel(DataGridView dgv)
        {
            // 1. Kiểm tra xem có dữ liệu không
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Mở hộp thoại chọn nơi lưu
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Workbook|*.xlsx";
                    sfd.FileName = "DanhSachNhanVien_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        // 3. Tạo Workbook và Worksheet
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Danh Sách");

                            // 4. Xuất tiêu đề cột (Header)
                            for (int i = 0; i < dgv.Columns.Count; i++)
                            {
                                // Dòng 1 trong Excel là tiêu đề
                                worksheet.Cell(1, i + 1).Value = dgv.Columns[i].HeaderText;

                                // Làm đậm tiêu đề
                                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                                worksheet.Cell(1, i + 1).Style.Fill.BackgroundColor = XLColor.LightGray;
                            }

                            // 5. Xuất dữ liệu dòng (Data)
                            for (int i = 0; i < dgv.Rows.Count; i++)
                            {
                                for (int j = 0; j < dgv.Columns.Count; j++)
                                {
                                    // Kiểm tra null trước khi lấy giá trị
                                    var cellValue = dgv.Rows[i].Cells[j].Value;
                                    worksheet.Cell(i + 2, j + 1).Value = cellValue != null ? cellValue.ToString() : "";
                                }
                            }

                            // 6. Tự động chỉnh độ rộng cột cho đẹp
                            worksheet.Columns().AdjustToContents();

                            // 7. Lưu file
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
    }
}
