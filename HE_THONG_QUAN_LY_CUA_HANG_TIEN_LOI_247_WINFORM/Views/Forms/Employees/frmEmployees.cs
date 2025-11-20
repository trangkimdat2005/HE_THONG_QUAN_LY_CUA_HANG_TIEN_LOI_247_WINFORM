using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.PresentationLayer.Forms.Employees
{
    public partial class frmEmployees : Form
    {
        private readonly QuanLyServices context;
        private readonly FormMode mode;
        private readonly string employeeId;

        public frmEmployees(FormMode mode, string employeeId = null)
        {
            InitializeComponent();
            context = new QuanLyServices();
            this.mode = mode;
            this.employeeId = employeeId;

            if (mode == FormMode.Add)
            {
                txtId.Text = context.GenerateNewId<NhanVien>("NV", 6);
            }
        }

        public frmEmployees() : this(FormMode.Add)
        {
        }

        private void frmEmployees_Load(object sender, EventArgs e)
        {
            SetupDiaLog();
            BatRangBuoc();
            SetUpCombobox();
            SetUpDateTime();

            if (mode == FormMode.Add)
            {
                this.Text = "Thêm nhân viên";
            }
            else if (mode == FormMode.Edit)
            {
                this.Text = "Chỉnh sửa nhân viên";
                LoadEmployeeInfo();
            }
        }

        private void SetupDiaLog()
        {
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
            btnSave.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            btnSave.DialogResult = DialogResult.None;
        }

        private void SetUpDateTime()
        {
            EventService.SetupDateTimePicker_AgeRange(dtpBirthDate, 18, 65);
            EventService.SetupDatePicker_ByCondition(dtpStartDate, DateTime.Now, ">=");
        }

        private void SetUpCombobox()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Đang làm");
            cmbStatus.Items.Add("Nghỉ");
            cmbStatus.SelectedIndex = 0;

            cmbPosition.Items.Clear();
            cmbPosition.Items.Add("Admin");
            cmbPosition.Items.Add("Quản lý");
            cmbPosition.Items.Add("Nhân viên");
            cmbPosition.SelectedIndex = 0;
        }

        private void BatRangBuoc()
        {
            txtPhone.KeyPress += EventService.TextBox_KhongNhapChu_KeyPress;
            txtPhone.Validating += EventService.TextBox_SoDienThoai_Validating;
            txtName.KeyPress += EventService.TextBox_KhongNhapSo_KeyPress;
            txtEmail.Validating += EventService.TextBox_Email_Validating;
            txtSalary.KeyPress += EventService.TextBox_SoTien_KeyPress;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Ảnh (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png|Tất cả (*.*)|*.*";
                    ofd.Title = "Chọn ảnh nhân viên";

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        if (ptrbAnh.Image != null)
                        {
                            ptrbAnh.Image.Dispose();
                            ptrbAnh.Image = null;
                        }
                        ptrbAnh.Image = Image.FromFile(ofd.FileName);
                        ptrbAnh.SizeMode = PictureBoxSizeMode.Zoom;
                        ptrbAnh.Tag = ofd.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải ảnh!\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!EventService.KiemTraRong(
                (txtName, "Vui lòng nhập tên"),
                (txtPhone, "Vui lòng nhập số điện thoại"),
                (txtSalary, "Vui lòng nhập lương"),
                (txtAddress, "Vui lòng nhập địa chỉ")
            )) return;

            if (ptrbAnh.Tag == null && mode == FormMode.Add)
            {
                MessageBox.Show("Vui lòng chọn ảnh!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (mode == FormMode.Add)
                Save_Add();
            else
                Save_Edit();
        }

        private void Save_Add()
        {
            string nvId = context.GenerateNewId<NhanVien>("NV", 6);
            string imgId = context.GenerateNewId<HinhAnh>("ANH", 7);
            string filePath = ptrbAnh.Tag.ToString();
            byte[] imgBytes = context.ConvertImageToByteArray(filePath);

            var hinh = new HinhAnh
            {
                Id = imgId,
                TenAnh = Path.GetFileName(filePath),
                Anh = imgBytes
            };

            var nv = new NhanVien
            {
                id = nvId,
                hoTen = txtName.Text.Trim(),
                chucVu = cmbPosition.SelectedItem.ToString(),
                luongCoBan = decimal.Parse(txtSalary.Text.Trim()),
                soDienThoai = txtPhone.Text.Trim(),
                email = txtEmail.Text.Trim(),
                diaChi = txtAddress.Text.Trim(),
                ngayVaoLam = dtpStartDate.Value,
                trangThai = cmbStatus.SelectedItem.ToString(),
                isDelete = false,
                gioiTinh = rbMale.Checked,
                anhId = imgId,
                HinhAnh = hinh
            };

            if (context.Add(nv))
            {
                MessageBox.Show("Thêm thành công!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void Save_Edit()
        {
            var nv = context.GetNhanVienById(employeeId);
            if (nv == null)
            {
                MessageBox.Show("Không tìm thấy NV!");
                return;
            }

            nv.hoTen = txtName.Text.Trim();
            nv.chucVu = cmbPosition.SelectedItem.ToString();
            nv.luongCoBan = decimal.Parse(txtSalary.Text);
            nv.soDienThoai = txtPhone.Text.Trim();
            nv.email = txtEmail.Text.Trim();
            nv.diaChi = txtAddress.Text.Trim();
            nv.ngayVaoLam = dtpStartDate.Value;
            nv.gioiTinh = rbMale.Checked;
            nv.trangThai = cmbStatus.SelectedItem.ToString();

            if (ptrbAnh.Tag != null && ptrbAnh.Tag.ToString() != "DB_IMAGE")
            {
                string filePath = ptrbAnh.Tag.ToString();
                byte[] imgBytes = context.ConvertImageToByteArray(filePath);

                if (nv.HinhAnh == null)
                {
                    string imgId = context.GenerateNewId<HinhAnh>("ANH", 7);
                    nv.HinhAnh = new HinhAnh
                    {
                        Id = imgId,
                        TenAnh = Path.GetFileName(filePath),
                        Anh = imgBytes
                    };
                    nv.anhId = imgId;
                }
                else
                {
                    nv.HinhAnh.Anh = imgBytes;
                    nv.HinhAnh.TenAnh = Path.GetFileName(filePath);
                }
            }

            if (context.Update(nv))
            {
                MessageBox.Show("Cập nhật thành công!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void LoadEmployeeInfo()
        {
            if (employeeId == null) return;
            var nv = context.GetNhanVienById(employeeId);

            if (nv == null)
            {
                MessageBox.Show("Không tìm thấy nhân viên!", "Lỗi");
                this.Close();
                return;
            }

            txtId.Text = nv.id;
            txtName.Text = nv.hoTen;
            txtPhone.Text = nv.soDienThoai;
            txtEmail.Text = nv.email;
            txtAddress.Text = nv.diaChi;
            txtSalary.Text = nv.luongCoBan.ToString("0.##");
            cmbPosition.SelectedItem = nv.chucVu;
            cmbStatus.SelectedItem = nv.trangThai;

            dtpStartDate.MinDate = DateTimePicker.MinimumDateTime;
            dtpStartDate.MaxDate = DateTimePicker.MaximumDateTime;
            dtpStartDate.Value = nv.ngayVaoLam;

            rbMale.Checked = nv.gioiTinh;
            rbFemale.Checked = !nv.gioiTinh;

            if (nv.HinhAnh != null && nv.HinhAnh.Anh != null)
            {
                using (var ms = new MemoryStream(nv.HinhAnh.Anh))
                {
                    ptrbAnh.Image = Image.FromStream(ms);
                }
                ptrbAnh.Tag = "DB_IMAGE";
            }
        }
    }
}