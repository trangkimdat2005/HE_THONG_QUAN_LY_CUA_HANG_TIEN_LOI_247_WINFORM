using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Utils;
using System;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Views.forms.Main
{
    public partial class frmProfile : Form
    {
        private UserSession _session;
        private QuanLyServices _quanLyService;
        
        public frmProfile()
        {
            InitializeComponent();
            _session = UserSession.Instance;
            _quanLyService = new QuanLyServices();
        }
        
        private void frmProfile_Load(object sender, EventArgs e)
        {
            LoadUserData();
        }
        
        private void LoadUserData()
        {
            if (!_session.IsLoggedIn)
            {
                MessageBox.Show("Chưa đăng nhập!", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
            
            // Left panel - Thông tin cơ bản
            lblEmployeeName.Text = _session.EmployeeName;
            lblPosition.Text = _session.Position;
            lblAccountValue.Text = _session.Username;
            lblJoinDateValue.Text = _session.EmploymentDate?.ToString("dd/MM/yyyy") ?? "N/A";
            
            // Right panel - Thông tin chi tiết
            lblFullName.Text = _session.EmployeeName;
            lblEmail.Text = _session.Email ?? "Chưa có";
            lblPhone.Text = _session.PhoneNumber;
            lblGender.Text = _session.GetGenderText() + (_session.Gender ? " ♂️" : " ♀️");
            lblAddress.Text = _session.Address;
            
            // Load avatar
            LoadAvatarFromDatabase();
            
            // Load lương từ DB (không có trong cache)
            LoadSalaryFromDatabase();
        }
        
        /// <summary>
        /// Load avatar từ database
        /// </summary>
        private void LoadAvatarFromDatabase()
        {
            try
            {
                // Ưu tiên load từ cache (nhanh)
                if (_session.Avatar != null && _session.Avatar.Length > 0)
                {
                    try
                    {
                        using (MemoryStream ms = new MemoryStream(_session.Avatar))
                        {
                            pbAvatar.Image = Image.FromStream(ms);
                        }
                        return;
                    }
                    catch (ArgumentException)
                    {
                        // Byte array không hợp lệ, xóa cache và tiếp tục
                        _session.Avatar = null;
                    }
                }
                
                // Load từ database nếu cache rỗng
                using (var context = new AppDbContext())
                {
                    var employee = context.NhanViens
                        .Include(e => e.HinhAnh)
                        .FirstOrDefault(e => e.id == _session.EmployeeId && !e.isDelete);
                    
                    if (employee != null)
                    {
                        // Kiểm tra xem nhân viên có ảnh hợp lệ không
                        if (!string.IsNullOrEmpty(employee.anhId) && 
                            employee.HinhAnh != null && 
                            employee.HinhAnh.Anh != null && 
                            employee.HinhAnh.Anh.Length > 0)
                        {
                            byte[] imageBytes = employee.HinhAnh.Anh;
                            
                            try
                            {
                                using (MemoryStream ms = new MemoryStream(imageBytes))
                                {
                                    pbAvatar.Image = Image.FromStream(ms);
                                }
                                
                                // Cập nhật cache
                                _session.Avatar = imageBytes;
                                return;
                            }
                            catch (ArgumentException)
                            {
                                // Dữ liệu ảnh không hợp lệ, sử dụng default
                            }
                        }
                        
                        // Nếu chưa có ảnh hoặc ảnh không hợp lệ, tạo avatar mặc định
                        SetDefaultAvatarAndSaveToDatabase(employee);
                    }
                    else
                    {
                        SetDefaultAvatar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load ảnh: {ex.Message}", "Lỗi");
                SetDefaultAvatar();
            }
        }
        
        /// <summary>
        /// Tạo avatar mặc định từ logo và lưu vào database
        /// </summary>
        private void SetDefaultAvatarAndSaveToDatabase(NhanVien employee)
        {
            try
            {
                // Lấy logo từ Resources
                byte[] defaultAvatarBytes = GetDefaultAvatarBytes();
                
                if (defaultAvatarBytes != null && defaultAvatarBytes.Length > 0)
                {
                    // Hiển thị logo làm avatar
                    using (MemoryStream ms = new MemoryStream(defaultAvatarBytes))
                    {
                        pbAvatar.Image = Image.FromStream(ms);
                    }
                    
                    // Lưu vào database
                    using (var context = new AppDbContext())
                    {
                        var empToUpdate = context.NhanViens.Find(employee.id);
                        
                        if (empToUpdate != null)
                        {
                            // Tạo record HinhAnh mới với tên chuẩn
                            var newImage = new HinhAnh
                            {
                                Id = "AnhDefault",
                                TenAnh = "Avatar_Default",
                                Anh = defaultAvatarBytes
                            };
                            
                            // Kiểm tra xem ảnh mặc định đã tồn tại chưa
                            var existingDefaultImage = context.HinhAnhs.Find("AnhDefault");
                            if (existingDefaultImage == null)
                            {
                                context.HinhAnhs.Add(newImage);
                            }
                            
                            // Cập nhật anhId cho nhân viên
                            empToUpdate.anhId = "AnhDefault";
                            
                            context.SaveChanges();
                            
                            // Cập nhật cache
                            _session.Avatar = defaultAvatarBytes;
                        }
                    }
                }
                else
                {
                    SetDefaultAvatar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tạo avatar mặc định: {ex.Message}", "Lỗi");
                SetDefaultAvatar();
            }
        }
        
        /// <summary>
        /// Lấy byte array của logo từ Resources
        /// </summary>
        private byte[] GetDefaultAvatarBytes()
        {
            try
            {
                // Đường dẫn đến file logo
                string logoPath = Path.Combine(Application.StartupPath, "Resources", "img", "logo.png");
                
                if (File.Exists(logoPath))
                {
                    return File.ReadAllBytes(logoPath);
                }
                
                // Nếu không tìm thấy file, thử load từ Resources (nếu đã embed)
                if (Properties.Resources.logo != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Properties.Resources.logo.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        return ms.ToArray();
                    }
                }
                
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đọc logo: {ex.Message}", "Lỗi");
                return null;
            }
        }
        
        /// <summary>
        /// Load lương từ database
        /// </summary>
        private void LoadSalaryFromDatabase()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var employee = context.NhanViens
                        .FirstOrDefault(e => e.id == _session.EmployeeId && !e.isDelete);
                    
                    if (employee != null)
                    {
                        lblSalary.Text = employee.luongCoBan.ToString("N0") + " ₫";
                        
                        // ✅ FIX: Cập nhật màu sắc cho status badge
                        lblStatusBadge.Text = employee.trangThai;
                        
                        // Kiểm tra trạng thái và set màu tương ứng
                        if (employee.trangThai == "Đang làm việc" || employee.trangThai == "Hoạt động")
                        {
                            // Màu xanh lá cho trạng thái hoạt động
                            lblStatusBadge.BackColor = Color.FromArgb(40, 167, 69);   // Green
                            lblStatusBadge.ForeColor = Color.White;
                        }
                        else if (employee.trangThai == "Nghỉ việc" || employee.trangThai == "Tạm nghỉ")
                        {
                            // Màu đỏ cho trạng thái nghỉ việc
                            lblStatusBadge.BackColor = Color.FromArgb(220, 53, 69);  // Red
                            lblStatusBadge.ForeColor = Color.White;
                        }
                        else
                        {
                            // Màu vàng cam cho các trạng thái khác (nếu có)
                            lblStatusBadge.BackColor = Color.FromArgb(255, 193, 7);  // Warning Yellow
                            lblStatusBadge.ForeColor = Color.Black;
                        }
                        
                        lblStatusBadge.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
                        lblStatusBadge.AutoSize = true;
                    }
                }
            }
            catch (Exception ex)
            {
                lblSalary.Text = "N/A";
            }
        }
        
        /// <summary>
        /// Lưu avatar vào database
        /// </summary>
        private void SaveAvatarToDatabase(string filePath)
        {
            try
            {
                // Validate file
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("File ảnh không tồn tại!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Validate file size (max 5MB)
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Length > 5 * 1024 * 1024)
                {
                    MessageBox.Show("Kích thước ảnh quá lớn! (Max 5MB)", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // 1. Convert image → byte[]
                byte[] imageBytes = _quanLyService.ConvertImageToByteArray(filePath);
                
                if (imageBytes == null || imageBytes.Length == 0)
                {
                    MessageBox.Show("Không thể đọc file ảnh!", "Lỗi");
                    return;
                }
                
                // Validate byte array
                try
                {
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        using (var testImage = Image.FromStream(ms))
                        {
                            // OK
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Dữ liệu ảnh không hợp lệ!", "Lỗi");
                    return;
                }
                
                // 2. Lưu vào database
                using (var context = new AppDbContext())
                {
                    var employee = context.NhanViens
                        .Include(e => e.HinhAnh)
                        .FirstOrDefault(e => e.id == _session.EmployeeId);
                    
                    if (employee == null)
                    {
                        MessageBox.Show("Không tìm thấy nhân viên!", "Lỗi");
                        return;
                    }
                    
                    // Kiểm tra đã có ảnh chưa
                    if (!string.IsNullOrEmpty(employee.anhId) && employee.anhId != "AnhDefault")
                    {
                        // Cập nhật ảnh cũ (nếu không phải ảnh mặc định)
                        var existingImage = context.HinhAnhs.Find(employee.anhId);
                        if (existingImage != null)
                        {
                            existingImage.Anh = imageBytes;
                            existingImage.TenAnh = Path.GetFileName(filePath);
                        }
                    }
                    else
                    {
                        // Tạo ảnh mới
                        var newImage = new HinhAnh
                        {
                            Id = Guid.NewGuid().ToString(),
                            TenAnh = Path.GetFileName(filePath),
                            Anh = imageBytes
                        };
                        
                        context.HinhAnhs.Add(newImage);
                        employee.anhId = newImage.Id;
                    }
                    
                    context.SaveChanges();
                    
                    // 3. Cập nhật cache
                    _session.Avatar = imageBytes;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu ảnh: {ex.Message}", "Lỗi");
            }
        }
        
        private void SetDefaultAvatar()
        {
            // Thử load logo làm avatar mặc định
            byte[] defaultAvatarBytes = GetDefaultAvatarBytes();
            
            if (defaultAvatarBytes != null)
            {
                try
                {
                    using (MemoryStream ms = new MemoryStream(defaultAvatarBytes))
                    {
                        pbAvatar.Image = Image.FromStream(ms);
                    }
                    return;
                }
                catch { }
            }
            
            // Fallback: hiển thị background màu xám
            pbAvatar.BackColor = Color.FromArgb(230, 230, 230);
            pbAvatar.Image = null;
        }
        
        private void btnChangeAvatar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Chọn ảnh đại diện";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Hiển thị ảnh preview
                        pbAvatar.Image = Image.FromFile(openFileDialog.FileName);
                        
                        // Lưu vào database
                        SaveAvatarToDatabase(openFileDialog.FileName);
                        
                        MessageBox.Show(
                            "Đã cập nhật ảnh đại diện thành công!",
                            "Thành công",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            $"Lỗi khi tải ảnh: {ex.Message}",
                            "Lỗi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
        }
        
        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            var frmChange = new frmChangePassword();
            if (frmChange.ShowDialog() == DialogResult.OK)
            {
                // User chose to logout after password change
                // Perform logout logic here
            }
        }
        
        // Thêm biến tracking chế độ
        private bool _isEditMode = false;
        private TextBox txtFullNameEdit;
        private TextBox txtEmailEdit;
        private TextBox txtPhoneEdit;
        private TextBox txtAddressEdit;
        private ComboBox cboGenderEdit;

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!_isEditMode)
            {
                // Chuyển sang chế độ Edit
                EnableEditMode();
            }
            else
            {
                // Lưu thay đổi
                SaveChanges();
            }
        }
        
        /// <summary>
        /// Bật chế độ edit
        /// </summary>
        private void EnableEditMode()
        {
            _isEditMode = true;
            
            // Ẩn labels, hiển thị textboxes
            CreateEditControls();
            
            // Đổi text nút
            btnUpdate.Text = "💾 Lưu thay đổi";
            btnUpdate.BackColor = Color.FromArgb(40, 167, 69); // Xanh lá
        }
        
        /// <summary>
        /// Tạo các control để edit
        /// </summary>
        private void CreateEditControls()
        {
            // Tạo TextBox cho Họ tên
            txtFullNameEdit = new TextBox
            {
                Location = lblFullName.Location,
                Size = lblFullName.Size,
                Text = lblFullName.Text,
                Font = lblFullName.Font
            };
            pnlRow1.Controls.Add(txtFullNameEdit);
            txtFullNameEdit.BringToFront();
            lblFullName.Visible = false;
            
            // Tương tự cho Email, Phone, Address...
            txtEmailEdit = new TextBox
            {
                Location = lblEmail.Location,
                Size = lblEmail.Size,
                Text = lblEmail.Text == "Chưa có" ? "" : lblEmail.Text,
                Font = lblEmail.Font
            };
            pnlRow2.Controls.Add(txtEmailEdit);
            txtEmailEdit.BringToFront();
            lblEmail.Visible = false;
            
            // Phone
            txtPhoneEdit = new TextBox
            {
                Location = lblPhone.Location,
                Size = lblPhone.Size,
                Text = lblPhone.Text,
                Font = lblPhone.Font
            };
            pnlRow3.Controls.Add(txtPhoneEdit);
            txtPhoneEdit.BringToFront();
            lblPhone.Visible = false;
            
            // Gender - ComboBox
            cboGenderEdit = new ComboBox
            {
                Location = lblGender.Location,
                Size = new Size(lblGender.Width, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = lblGender.Font
            };
            cboGenderEdit.Items.AddRange(new string[] { "Nam ♂️", "Nữ ♀️" });
            cboGenderEdit.SelectedIndex = _session.Gender ? 0 : 1;
            pnlRow4.Controls.Add(cboGenderEdit);
            cboGenderEdit.BringToFront();
            lblGender.Visible = false;
            
            // Address
            txtAddressEdit = new TextBox
            {
                Location = lblAddress.Location,
                Size = lblAddress.Size,
                Text = lblAddress.Text,
                Font = lblAddress.Font,
                Multiline = true
            };
            pnlRow5.Controls.Add(txtAddressEdit);
            txtAddressEdit.BringToFront();
            lblAddress.Visible = false;
        }
        
        /// <summary>
        /// Lưu thay đổi
        /// </summary>
        private void SaveChanges()
        {
            // Validate
            if (!ValidateEditInput())
            {
                return;
            }
            
            var result = MessageBox.Show(
                "Bạn có chắc chắn muốn lưu thay đổi?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
            
            if (result != DialogResult.Yes)
            {
                return;
            }
            
            try
            {
                using (var context = new AppDbContext())
                {
                    var employee = context.NhanViens
                        .FirstOrDefault(e => e.id == _session.EmployeeId && !e.isDelete);
                    
                    if (employee == null)
                    {
                        MessageBox.Show("Không tìm thấy nhân viên!", "Lỗi");
                        return;
                    }
                    
                    // Cập nhật
                    employee.hoTen = txtFullNameEdit.Text.Trim();
                    employee.email = string.IsNullOrWhiteSpace(txtEmailEdit.Text) ? 
                        null : txtEmailEdit.Text.Trim();
                    employee.soDienThoai = txtPhoneEdit.Text.Trim();
                    employee.diaChi = txtAddressEdit.Text.Trim();
                    employee.gioiTinh = cboGenderEdit.SelectedIndex == 0;
                    
                    context.SaveChanges();
                    
                    // Cập nhật session
                    _session.EmployeeName = employee.hoTen;
                    _session.Email = employee.email;
                    _session.PhoneNumber = employee.soDienThoai;
                    _session.Address = employee.diaChi;
                    _session.Gender = employee.gioiTinh;
                    
                    MessageBox.Show("Cập nhật thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Tắt chế độ edit
                    DisableEditMode();
                    
                    // Reload dữ liệu
                    LoadUserData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi");
            }
        }
        
        /// <summary>
        /// Tắt chế độ edit
        /// </summary>
        private void DisableEditMode()
        {
            _isEditMode = false;
            
            // Xóa controls edit
            if (txtFullNameEdit != null)
            {
                pnlRow1.Controls.Remove(txtFullNameEdit);
                txtFullNameEdit.Dispose();
            }
            if (txtEmailEdit != null)
            {
                pnlRow2.Controls.Remove(txtEmailEdit);
                txtEmailEdit.Dispose();
            }
            if (txtPhoneEdit != null)
            {
                pnlRow3.Controls.Remove(txtPhoneEdit);
                txtPhoneEdit.Dispose();
            }
            if (cboGenderEdit != null)
            {
                pnlRow4.Controls.Remove(cboGenderEdit);
                cboGenderEdit.Dispose();
            }
            if (txtAddressEdit != null)
            {
                pnlRow5.Controls.Remove(txtAddressEdit);
                txtAddressEdit.Dispose();
            }
            
            // Hiển thị lại labels
            lblFullName.Visible = true;
            lblEmail.Visible = true;
            lblPhone.Visible = true;
            lblGender.Visible = true;
            lblAddress.Visible = true;
            
            // Đổi lại text nút
            btnUpdate.Text = "✏️ Cập nhật thông tin";
            btnUpdate.BackColor = Color.FromArgb(0, 170, 255); // Xanh dương
        }
        
        /// <summary>
        /// Validate input khi edit
        /// </summary>
        private bool ValidateEditInput()
        {
            if (string.IsNullOrWhiteSpace(txtFullNameEdit.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Cảnh báo");
                txtFullNameEdit.Focus();
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(txtPhoneEdit.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Cảnh báo");
                txtPhoneEdit.Focus();
                return false;
            }
            
            // Validate phone format
            string phonePattern = @"^(0|\+84)[0-9]{9}$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtPhoneEdit.Text.Trim(), phonePattern))
            {
                MessageBox.Show("Số điện thoại không hợp lệ!", "Cảnh báo");
                txtPhoneEdit.Focus();
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(txtAddressEdit.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ!", "Cảnh báo");
                txtAddressEdit.Focus();
                return false;
            }
            
            return true;
        }
    }
}
