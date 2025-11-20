using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.Forms.Employees
{
    public partial class frmShiftAssignment : Form
    {
        // _quanLyServices này chỉ dùng để Load dữ liệu hiển thị (Read-only operations)
        private readonly IQuanLyServices _quanLyServices;
        private DateTime _currentWeekStart;

        public frmShiftAssignment()
        {
            InitializeComponent();
            _quanLyServices = new QuanLyServices();
            _currentWeekStart = GetMondayOfWeek(DateTime.Now);
            CustomizeInterface();
        }

        private void frmShiftAssignment_Load(object sender, EventArgs e)
        {
            SetupComboBox();
            SetupDataGridViewStructure();
            UpdateWeekInfo();
        }

        private DateTime GetMondayOfWeek(DateTime date)
        {
            date = date.Date;
            var dayOfWeek = date.DayOfWeek;
            if (dayOfWeek == DayOfWeek.Sunday)
                return date.AddDays(-6);
            return date.AddDays(-(int)dayOfWeek + 1);
        }

        private void CustomizeInterface()
        {
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.CustomFormat = "dd/MM/yyyy";
            dtpStart.Enabled = false;

            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.CustomFormat = "dd/MM/yyyy";
            dtpEnd.Enabled = false;

            dgvShiftMatrix.BorderStyle = BorderStyle.None;
            dgvShiftMatrix.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvShiftMatrix.GridColor = Color.FromArgb(230, 230, 230);
            dgvShiftMatrix.RowHeadersVisible = false;
            dgvShiftMatrix.EnableHeadersVisualStyles = false;
            dgvShiftMatrix.ColumnHeadersHeight = 60;
            dgvShiftMatrix.RowTemplate.Height = 60;

            dgvShiftMatrix.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            dgvShiftMatrix.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvShiftMatrix.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvShiftMatrix.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvShiftMatrix.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            dgvShiftMatrix.DefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;
            dgvShiftMatrix.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvShiftMatrix.DefaultCellStyle.Font = new Font("Segoe UI", 10F);

            dgvShiftMatrix.CellContentClick += DgvShiftMatrix_CellContentClick;
        }

        private void SetupComboBox()
        {
            var nhanViens = _quanLyServices.GetList<NhanVien>();
            var items = nhanViens.Select(nv => new { id = nv.id, Display = $"{nv.id} - {nv.hoTen}" }).ToList();

            cmbEmployee.DataSource = items;
            cmbEmployee.DisplayMember = "Display";
            cmbEmployee.ValueMember = "id";

            cmbEmployee.SelectedIndexChanged += CmbEmployee_SelectedIndexChanged;

            if (items.Count > 0)
            {
                cmbEmployee.SelectedIndex = 0;
            }
        }

        private void SetupDataGridViewStructure()
        {
            dgvShiftMatrix.Columns.Clear();

            var colCa = new DataGridViewTextBoxColumn();
            colCa.Name = "colShiftName";
            colCa.HeaderText = "Ca làm việc";
            colCa.Width = 200;
            colCa.ReadOnly = true;
            colCa.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvShiftMatrix.Columns.Add(colCa);

            string[] days = { "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7", "Chủ Nhật" };

            for (int i = 0; i < 7; i++)
            {
                var colCheck = new DataGridViewCheckBoxColumn();
                colCheck.Name = $"colDay_{i}";
                colCheck.Width = 100;
                colCheck.HeaderText = days[i];
                dgvShiftMatrix.Columns.Add(colCheck);
            }
        }

        private void UpdateWeekInfo()
        {
            dtpStart.Value = _currentWeekStart;
            dtpEnd.Value = _currentWeekStart.AddDays(6);

            for (int i = 0; i < 7; i++)
            {
                DateTime dayDate = _currentWeekStart.AddDays(i);
                string dayName = i == 6 ? "Chủ Nhật" : $"Thứ {i + 2}";

                dgvShiftMatrix.Columns[i + 1].HeaderText = $"{dayName}\n{dayDate:dd/MM/yyyy}";
                dgvShiftMatrix.Columns[i + 1].Tag = dayDate.Date;
            }

            LoadShiftData();
        }

        private void LoadShiftData()
        {
            dgvShiftMatrix.Rows.Clear();
            var listCa = _quanLyServices.GetList<CaLamViec>();

            if (cmbEmployee.SelectedIndex < 0)
            {
                foreach (var ca in listCa) AddRowToGrid(ca, null);
                return;
            }

            string empId = cmbEmployee.SelectedValue.ToString();
            DateTime endDate = _currentWeekStart.AddDays(6);

            var phanCongTrongTuan = _quanLyServices.GetList<PhanCongCaLamViec>()
                .Where(pc =>
                    pc.nhanVienId == empId &&
                    pc.ngay.Date >= _currentWeekStart.Date &&
                    pc.ngay.Date <= endDate.Date &&
                    pc.isDelete == false
                ).ToList();

            foreach (var ca in listCa)
            {
                AddRowToGrid(ca, phanCongTrongTuan);
            }
        }

        private void AddRowToGrid(CaLamViec ca, List<PhanCongCaLamViec> phanCongs)
        {
            int rowIndex = dgvShiftMatrix.Rows.Add();
            var row = dgvShiftMatrix.Rows[rowIndex];

            row.Cells[0].Value = $"{ca.tenCa}\n({ca.thoiGianBatDau:hh\\:mm} - {ca.thoiGianKetThuc:hh\\:mm})";
            row.Tag = ca;

            DateTime today = DateTime.Today;

            for (int i = 0; i < 7; i++)
            {
                DateTime dateOfColumn = ((DateTime)dgvShiftMatrix.Columns[i + 1].Tag).Date;

                if (phanCongs != null)
                {
                    bool isAssigned = phanCongs.Any(pc =>
                        pc.caLamViecId == ca.id &&
                        pc.ngay.Date == dateOfColumn.Date
                    );
                    row.Cells[i + 1].Value = isAssigned;
                }
                else
                {
                    row.Cells[i + 1].Value = false;
                }

                if (dateOfColumn < today)
                {
                    row.Cells[i + 1].ReadOnly = true;
                    row.Cells[i + 1].Style.BackColor = Color.LightGray;
                    row.Cells[i + 1].Style.ForeColor = Color.DarkGray;
                }
                else
                {
                    row.Cells[i + 1].ReadOnly = false;
                    row.Cells[i + 1].Style.BackColor = Color.White;
                }
            }
        }

        private void DgvShiftMatrix_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 1) return;

            var cell = dgvShiftMatrix.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (cell.ReadOnly)
            {
                MessageBox.Show("Không được phép chỉnh sửa lịch làm việc trong quá khứ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgvShiftMatrix.CommitEdit(DataGridViewDataErrorContexts.Commit);

            if (Convert.ToBoolean(cell.Value) == true)
            {
                foreach (DataGridViewRow row in dgvShiftMatrix.Rows)
                {
                    if (row.Index != e.RowIndex)
                    {
                        row.Cells[e.ColumnIndex].Value = false;
                    }
                }
            }
        }

        private void CmbEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadShiftData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbEmployee.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // VALIDATION: Kiểm tra mỗi ngày chỉ được chọn tối đa 1 ca
            for (int i = 1; i <= 7; i++)
            {
                int countChecked = 0;
                foreach (DataGridViewRow row in dgvShiftMatrix.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[i].Value))
                    {
                        countChecked++;
                    }
                }

                if (countChecked > 1)
                {
                    DateTime dateError = ((DateTime)dgvShiftMatrix.Columns[i].Tag).Date;
                    MessageBox.Show($"Nhân viên không thể làm quá 1 ca trong ngày {dateError:dd/MM/yyyy}!\nVui lòng kiểm tra lại.", "Lỗi phân công", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string empId = cmbEmployee.SelectedValue.ToString();
            DateTime today = DateTime.Today;

            var service = new QuanLyServices();

            DateTime weekEnd = _currentWeekStart.AddDays(6);

            var currentAssignments = service.GetList<PhanCongCaLamViec>()
                .Where(x => x.nhanVienId == empId && x.ngay.Date >= _currentWeekStart && x.ngay.Date <= weekEnd)
                .ToList();

            try
            {
                foreach (DataGridViewRow row in dgvShiftMatrix.Rows)
                {
                    if (row.Tag == null) continue;
                    var ca = (CaLamViec)row.Tag;

                    for (int i = 1; i <= 7; i++)
                    {
                        DateTime date = ((DateTime)dgvShiftMatrix.Columns[i].Tag).Date;

                        if (date < today) continue;

                        bool isChecked = Convert.ToBoolean(row.Cells[i].Value);

                        var existingPC = currentAssignments.FirstOrDefault(x =>
                            x.caLamViecId == ca.id &&
                            x.ngay.Date == date
                        );

                        if (isChecked)
                        {
                            if (existingPC == null)
                            {
                                string newId = service.GenerateNewId<PhanCongCaLamViec>("PCCLV", 9);

                                var newPC = new PhanCongCaLamViec
                                {
                                    id = newId,
                                    nhanVienId = empId,
                                    caLamViecId = ca.id,
                                    ngay = date,
                                    isDelete = false
                                };
                                service.Add(newPC);
                            }
                            else
                            {
                                var propIsDelete = existingPC.GetType().GetProperty("isDelete");
                                if ((bool)propIsDelete.GetValue(existingPC))
                                {
                                    propIsDelete.SetValue(existingPC, false);
                                    service.Update(existingPC);
                                }
                            }
                        }
                        else
                        {
                            if (existingPC != null)
                            {
                                if (!service.HardDelete(existingPC))
                                {
                                    var propIsDelete = existingPC.GetType().GetProperty("isDelete");
                                    if (!(bool)propIsDelete.GetValue(existingPC))
                                    {
                                        service.SoftDelete(existingPC);
                                    }
                                }
                            }
                        }
                    }
                }

                MessageBox.Show("Lưu phân công thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadShiftData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            _currentWeekStart = _currentWeekStart.AddDays(-7);
            UpdateWeekInfo();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _currentWeekStart = _currentWeekStart.AddDays(7);
            UpdateWeekInfo();
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            if (cmbEmployee.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên để sao chép lịch!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string empId = cmbEmployee.SelectedValue.ToString();
            DateTime today = DateTime.Today;
            DateTime sourceWeekStart = _currentWeekStart.AddDays(-7);

            var service = new QuanLyServices();

            var listCa = service.GetList<CaLamViec>();
            var allPhanCong = service.GetList<PhanCongCaLamViec>();

            int countChanged = 0;

            try
            {
                for (int i = 0; i < 7; i++)
                {
                    DateTime targetDate = _currentWeekStart.AddDays(i).Date;
                    DateTime sourceDate = sourceWeekStart.AddDays(i).Date;

                    if (targetDate < today) continue;

                    foreach (var ca in listCa)
                    {
                        bool isSourceActive = allPhanCong.Any(pc =>
                            pc.nhanVienId == empId &&
                            pc.caLamViecId == ca.id &&
                            pc.ngay.Date == sourceDate &&
                            pc.isDelete == false
                        );

                        var targetPC = allPhanCong.FirstOrDefault(pc =>
                            pc.nhanVienId == empId &&
                            pc.caLamViecId == ca.id &&
                            pc.ngay.Date == targetDate
                        );

                        if (isSourceActive)
                        {
                            if (targetPC == null)
                            {
                                string newId = service.GenerateNewId<PhanCongCaLamViec>("PCCLV", 9);

                                var newPC = new PhanCongCaLamViec
                                {
                                    id = newId,
                                    nhanVienId = empId,
                                    caLamViecId = ca.id,
                                    ngay = targetDate,
                                    isDelete = false
                                };
                                if (service.Add(newPC)) countChanged++;
                            }
                            else
                            {
                                var propIsDelete = targetPC.GetType().GetProperty("isDelete");
                                if ((bool)propIsDelete.GetValue(targetPC))
                                {
                                    propIsDelete.SetValue(targetPC, false);
                                    if (service.Update(targetPC)) countChanged++;
                                }
                            }
                        }
                        else
                        {
                            if (targetPC != null)
                            {
                                if (!service.HardDelete(targetPC))
                                {
                                    var propIsDelete = targetPC.GetType().GetProperty("isDelete");
                                    if (!(bool)propIsDelete.GetValue(targetPC))
                                    {
                                        if (service.SoftDelete(targetPC)) countChanged++;
                                    }
                                }
                                else
                                {
                                    countChanged++;
                                }
                            }
                        }
                    }
                }

                if (countChanged > 0)
                {
                    MessageBox.Show($"Sao chép thành công! Đã cập nhật {countChanged} mục.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadShiftData();
                }
                else
                {
                    MessageBox.Show("Lịch làm việc đã giống tuần trước.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sao chép lịch: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}