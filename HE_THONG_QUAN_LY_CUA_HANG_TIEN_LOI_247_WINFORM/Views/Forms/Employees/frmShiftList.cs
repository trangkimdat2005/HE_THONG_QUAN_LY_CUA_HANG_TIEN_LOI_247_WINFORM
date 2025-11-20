using ClosedXML.Excel;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.Forms.Employees
{
    public partial class frmShiftList : Form
    {
        private readonly IQuanLyServices _services;

        public frmShiftList()
        {
            InitializeComponent();
            _services = new QuanLyServices();
            CustomizeInterface();
        }

        private void frmShiftList_Load(object sender, EventArgs e)
        {
            LoadComboboxNhanVien();
            dtpFrom.Value = GetMondayOfWeek(DateTime.Now);
            dtpTo.Value = dtpFrom.Value.AddDays(6);
            LoadScheduleData();
            dtpFrom.Enabled = false;
            dtpTo.Enabled = false;
        }

        private void CustomizeInterface()
        {
            dtpFrom.Format = DateTimePickerFormat.Custom;
            dtpFrom.CustomFormat = "dd/MM/yyyy";
            dtpTo.Format = DateTimePickerFormat.Custom;
            dtpTo.CustomFormat = "dd/MM/yyyy";

            dgvSchedule.BorderStyle = BorderStyle.None;
            dgvSchedule.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvSchedule.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dgvSchedule.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSchedule.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvSchedule.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSchedule.EnableHeadersVisualStyles = false;
            dgvSchedule.RowHeadersVisible = false;

            dgvSchedule.RowTemplate.Height = 150;

            dgvSchedule.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvSchedule.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSchedule.AllowUserToAddRows = false;

            dgvSchedule.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvSchedule.DefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            dgvSchedule.RowTemplate.Height = 100;
            SetupGridColumns();
        }

        private void SetupGridColumns()
        {
            dgvSchedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvSchedule.Columns.Clear();

            var colCa = new DataGridViewTextBoxColumn();
            colCa.Name = "colShift";
            colCa.HeaderText = "Ca làm việc";
            colCa.Width = 200;
            colCa.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colCa.Frozen = true;
            colCa.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvSchedule.Columns.Add(colCa);

            string[] days = { "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7", "Chủ Nhật" };
            for (int i = 0; i < 7; i++)
            {
                var col = new DataGridViewTextBoxColumn();
                col.Name = $"colDay_{i}";
                col.HeaderText = days[i];
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvSchedule.Columns.Add(col);
            }
        }

        private void LoadComboboxNhanVien()
        {
            var listNV = _services.GetList<NhanVien>();

            var items = new List<dynamic> { new { id = "ALL", Display = "--- Tất cả nhân viên ---" } };
            items.AddRange(listNV.Select(x => new { id = x.id, Display = $"{x.id} - {x.hoTen}" }));

            cmbEmployee.DataSource = items;
            cmbEmployee.DisplayMember = "Display";
            cmbEmployee.ValueMember = "id";
            cmbEmployee.SelectedIndex = 0;
        }

        private DateTime GetMondayOfWeek(DateTime date)
        {
            var day = date.DayOfWeek;
            int diff = (7 + (day - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        private void LoadScheduleData()
        {
            dgvSchedule.Rows.Clear();

            DateTime startWeek = GetMondayOfWeek(dtpFrom.Value);
            DateTime endWeek = startWeek.AddDays(6);

            dtpFrom.Value = startWeek;
            dtpTo.Value = endWeek;

            for (int i = 0; i < 7; i++)
            {
                DateTime d = startWeek.AddDays(i);
                string dayName = i == 6 ? "Chủ Nhật" : $"Thứ {i + 2}";
                dgvSchedule.Columns[i + 1].HeaderText = $"{dayName}\n({d:dd/MM})";
            }

            string selectedEmpId = cmbEmployee.SelectedValue.ToString();

            var listCa = _services.GetList<CaLamViec>();
            var listAssignments = _services.GetList<PhanCongCaLamViec>()
                                           .Where(x => x.ngay >= startWeek && x.ngay <= endWeek && x.isDelete == false)
                                           .ToList();
            var listNhanVien = _services.GetList<NhanVien>();

            foreach (var ca in listCa)
            {
                int rowIndex = dgvSchedule.Rows.Add();
                var row = dgvSchedule.Rows[rowIndex];

                row.Cells[0].Value = $"{ca.tenCa}\n({ca.thoiGianBatDau:hh\\:mm} - {ca.thoiGianKetThuc:hh\\:mm})";

                for (int i = 0; i < 7; i++)
                {
                    DateTime currentDate = startWeek.AddDays(i);

                    var assignmentsInShift = listAssignments.Where(x =>
                        x.caLamViecId == ca.id &&
                        x.ngay.Date == currentDate.Date
                    );

                    if (selectedEmpId != "ALL")
                    {
                        assignmentsInShift = assignmentsInShift.Where(x => x.nhanVienId == selectedEmpId);
                    }

                    var empIds = assignmentsInShift.Select(x => x.nhanVienId).ToList();

                    if (empIds.Count > 0)
                    {
                        var names = listNhanVien.Where(nv => empIds.Contains(nv.id))
                                                .Select(nv => nv.hoTen)
                                                .ToArray();

                        row.Cells[i + 1].Value = string.Join(",\n", names);
                    }
                    else
                    {
                        row.Cells[i + 1].Value = "Trống";
                        row.Cells[i + 1].Style.ForeColor = Color.Gray;
                    }
                }
            }
        }

        private void btnNext_Click_1(object sender, EventArgs e)
        {
            dtpFrom.Value = dtpFrom.Value.AddDays(7);
            LoadScheduleData();
        }

        private void btnPrevious_Click_1(object sender, EventArgs e)
        {
            dtpFrom.Value = dtpFrom.Value.AddDays(-7);
            LoadScheduleData();
        }

        private void btnViewSchedule_Click(object sender, EventArgs e)
        {
            LoadScheduleData();

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvSchedule.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Workbook|*.xlsx";
                    sfd.FileName = $"LichLamViec_{dtpFrom.Value:ddMMyyyy}_{dtpTo.Value:ddMMyyyy}.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Lịch Làm Việc");

                            worksheet.Cell(1, 1).Value = $"LỊCH LÀM VIỆC TỪ {dtpFrom.Value:dd/MM/yyyy} ĐẾN {dtpTo.Value:dd/MM/yyyy}";
                            var titleRange = worksheet.Range(1, 1, 1, dgvSchedule.Columns.Count);
                            titleRange.Merge().Style.Font.Bold = true;
                            titleRange.Style.Font.FontSize = 14;
                            titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                            for (int i = 0; i < dgvSchedule.Columns.Count; i++)
                            {
                                var cell = worksheet.Cell(3, i + 1);
                                cell.Value = dgvSchedule.Columns[i].HeaderText.Replace("\n", " ");
                                cell.Style.Font.Bold = true;
                                cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#3498db");
                                cell.Style.Font.FontColor = XLColor.White;
                                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            }

                            for (int i = 0; i < dgvSchedule.Rows.Count; i++)
                            {
                                for (int j = 0; j < dgvSchedule.Columns.Count; j++)
                                {
                                    var cell = worksheet.Cell(i + 4, j + 1);
                                    var val = dgvSchedule.Rows[i].Cells[j].Value?.ToString() ?? "";

                                    cell.Value = val;
                                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                    cell.Style.Alignment.WrapText = true;

                                    if (val == "Trống")
                                    {
                                        cell.Style.Font.FontColor = XLColor.Gray;
                                        cell.Style.Font.Italic = true;
                                    }
                                }
                            }

                            worksheet.Columns().AdjustToContents();
                            workbook.SaveAs(sfd.FileName);
                        }
                        MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var frm = new frmShiftAssignment())
            {
                frm.ShowDialog();
                LoadScheduleData();
            }
        }
    }
}