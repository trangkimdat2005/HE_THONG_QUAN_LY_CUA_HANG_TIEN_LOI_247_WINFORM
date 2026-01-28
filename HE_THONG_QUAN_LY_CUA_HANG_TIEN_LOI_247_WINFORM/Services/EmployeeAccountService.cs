using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Services
{
    public class EmployeeAccountService
    {
        private readonly AppDbContext _context;
        private readonly QuanLyServices _services;

        public EmployeeAccountService()
        {
            _context = new AppDbContext();
            _services = new QuanLyServices();
        }

        public dynamic GetAllEmployeeAccounts()
        {
            try
            {
                return (from tknv in _context.TaiKhoanNhanViens
                        where !tknv.isDelete
                        join nv in _context.NhanViens on tknv.nhanVienId equals nv.id
                        join tk in _context.TaiKhoans on tknv.taiKhoanId equals tk.id
                        where !nv.isDelete && !tk.isDelete
                        orderby nv.hoTen
                        let roleInfo = (from ur in _context.UserRoles
                                        join r in _context.Roles on ur.roleId equals r.id
                                        where ur.taiKhoanId == tk.id
                                              && !ur.isDelete
                                              && !r.isDelete
                                              && (ur.hieuLucDen == null || ur.hieuLucDen >= DateTime.Now)
                                        orderby ur.hieuLucTu descending
                                        select new { r.id, r.ten }).FirstOrDefault()
                        select new
                        {
                            NhanVienId = nv.id,
                            HoTen = nv.hoTen,
                            ChucVu = nv.chucVu,
                            SoDienThoai = nv.soDienThoai,
                            Email = nv.email,
                            TaiKhoanId = tk.id,
                            TenDangNhap = tk.tenDangNhap,
                            EmailTK = tk.email,
                            TrangThaiTK = tk.trangThai,
                            TrangThaiNV = nv.trangThai,
                            RoleId = roleInfo == null ? null : roleInfo.id,
                            RoleName = roleInfo == null ? "Ch?a gán" : roleInfo.ten
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"L?i l?y danh s?ch t?i kho?n: {ex.Message}");
            }
        }

        public dynamic SearchEmployeeAccounts(string keyword)
        {
            try
            {
                keyword = keyword?.ToLower() ?? "";
                return (from tknv in _context.TaiKhoanNhanViens
                        where !tknv.isDelete
                        join nv in _context.NhanViens on tknv.nhanVienId equals nv.id
                        join tk in _context.TaiKhoans on tknv.taiKhoanId equals tk.id
                        where !nv.isDelete && !tk.isDelete &&
                        (nv.hoTen.ToLower().Contains(keyword) ||
                         nv.soDienThoai.Contains(keyword) ||
                         tk.tenDangNhap.ToLower().Contains(keyword) ||
                         nv.id.ToLower().Contains(keyword))
                        orderby nv.hoTen
                        let roleInfo = (from ur in _context.UserRoles
                                        join r in _context.Roles on ur.roleId equals r.id
                                        where ur.taiKhoanId == tk.id
                                              && !ur.isDelete
                                              && !r.isDelete
                                              && (ur.hieuLucDen == null || ur.hieuLucDen >= DateTime.Now)
                                        orderby ur.hieuLucTu descending
                                        select new { r.id, r.ten }).FirstOrDefault()
                        select new
                        {
                            NhanVienId = nv.id,
                            HoTen = nv.hoTen,
                            ChucVu = nv.chucVu,
                            SoDienThoai = nv.soDienThoai,
                            Email = nv.email,
                            TaiKhoanId = tk.id,
                            TenDangNhap = tk.tenDangNhap,
                            EmailTK = tk.email,
                            TrangThaiTK = tk.trangThai,
                            TrangThaiNV = nv.trangThai,
                            RoleId = roleInfo == null ? null : roleInfo.id,
                            RoleName = roleInfo == null ? "Ch?a gán" : roleInfo.ten
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"L?i t?m ki?m: {ex.Message}");
            }
        }

        public List<NhanVien> GetEmployeesWithoutAccount()
        {
            try
            {
                var employeesWithAccount = _context.TaiKhoanNhanViens
                    .Where(tknv => !tknv.isDelete)
                    .Select(tknv => tknv.nhanVienId)
                    .ToList();

                return _context.NhanViens
                    .Where(nv => !nv.isDelete && 
                           nv.trangThai == "Active" &&
                           !employeesWithAccount.Contains(nv.id))
                    .OrderBy(nv => nv.hoTen)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"L?i l?y danh sách nhân viên: {ex.Message}");
            }
        }

        public (bool success, string message, TaiKhoan account) CreateEmployeeAccount(
            string employeeId, 
            string username, 
            string password, 
            string email)
        {
            using (var db = new AppDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (db.TaiKhoans.Any(t => t.tenDangNhap == username && !t.isDelete))
                            return (false, "Tên ??ng nh?p ?ã t?n t?i!", null);

                        if (!string.IsNullOrEmpty(email) && 
                            db.TaiKhoans.Any(t => t.email == email && !t.isDelete))
                            return (false, "Email ?ã ???c s? d?ng!", null);

                        var employee = db.NhanViens.FirstOrDefault(nv => nv.id == employeeId);
                        if (employee == null)
                            return (false, "Không tìm th?y nhân viên!", null);

                        if (db.TaiKhoanNhanViens.Any(tknv => tknv.nhanVienId == employeeId && !tknv.isDelete))
                            return (false, "Nhân viên ?ã có tài kho?n!", null);

                        string accountId = _services.GenerateNewId<TaiKhoan>("TK", 6);
                        string hashedPassword = AuthenticationService.HashPassword(password);

                        var account = new TaiKhoan
                        {
                            id = accountId,
                            tenDangNhap = username,
                            matKhauHash = hashedPassword,
                            email = email,
                            trangThai = "Active",
                            isDelete = false
                        };

                        db.TaiKhoans.Add(account);

                        var empAccount = new TaiKhoanNhanVien
                        {
                            nhanVienId = employeeId,
                            taiKhoanId = accountId,
                            isDelete = false
                        };

                        db.TaiKhoanNhanViens.Add(empAccount);

                        var defaultRole = db.Roles.FirstOrDefault(r => r.code == "NV_BANHANG");
                        if (defaultRole != null)
                        {
                            var userRole = new UserRole
                            {
                                taiKhoanId = accountId,
                                roleId = defaultRole.id,
                                hieuLucTu = DateTime.Now,
                                hieuLucDen = null,
                                isDelete = false
                            };
                            db.UserRoles.Add(userRole);
                        }

                        db.SaveChanges();
                        transaction.Commit();

                        return (true, $"T?o tài kho?n thành công! Tên ??ng nh?p: {username}", account);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return (false, $"L?i: {ex.Message}", null);
                    }
                }
            }
        }

        public (bool success, string message) ResetPassword(string accountId, string newPassword)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    var account = db.TaiKhoans.FirstOrDefault(t => t.id == accountId);
                    if (account == null)
                        return (false, "Không tìm th?y tài kho?n!");

                    account.matKhauHash = AuthenticationService.HashPassword(newPassword);
                    db.SaveChanges();

                    return (true, "??t l?i m?t kh?u thành công!");
                }
                catch (Exception ex)
                {
                    return (false, $"L?i: {ex.Message}");
                }
            }
        }

        public (bool success, string message) ToggleAccountStatus(string accountId)
        {
            using (var db = new AppDbContext())
            {
                try
                {
                    var account = db.TaiKhoans.FirstOrDefault(t => t.id == accountId);
                    if (account == null)
                        return (false, "Không tìm th?y tài kho?n!");

                    account.trangThai = account.trangThai == "Active" ? "Inactive" : "Active";
                    db.SaveChanges();

                    string status = account.trangThai == "Active" ? "kích ho?t" : "vô hi?u hóa";
                    return (true, $"?ã {status} tài kho?n thành công!");
                }
                catch (Exception ex)
                {
                    return (false, $"L?i: {ex.Message}");
                }
            }
        }

        public (bool success, string message) DeleteEmployeeAccount(string employeeId)
        {
            using (var db = new AppDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var empAccount = db.TaiKhoanNhanViens
                            .FirstOrDefault(tknv => tknv.nhanVienId == employeeId && !tknv.isDelete);

                        if (empAccount == null)
                            return (false, "Kh?ng t?m th?y t?i kho?n!");

                        empAccount.isDelete = true;

                        var account = db.TaiKhoans.FirstOrDefault(t => t.id == empAccount.taiKhoanId);
                        if (account != null)
                        {
                            account.isDelete = true;
                        }

                        db.SaveChanges();
                        transaction.Commit();

                        return (true, "X?a t?i kho?n th?nh c?ng!");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return (false, $"L?i: {ex.Message}");
                    }
                }
            }
        }

        public List<Role> GetRoles()
        {
            try
            {
                return _context.Roles
                    .Where(r => !r.isDelete)
                    .OrderBy(r => r.ten)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"L?i l?y danh s?ch vai tr?: {ex.Message}");
            }
        }

        public (bool success, string message) UpdateAccountRole(string accountId, string roleId)
        {
            using (var db = new AppDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var account = db.TaiKhoans.FirstOrDefault(t => t.id == accountId && !t.isDelete);
                        if (account == null)
                            return (false, "Kh?ng t?m th?y t?i kho?n!");

                        var role = db.Roles.FirstOrDefault(r => r.id == roleId && !r.isDelete);
                        if (role == null)
                            return (false, "Vai tr? kh?ng h?p l?!");

                        var activeRoles = db.UserRoles
                            .Where(ur => ur.taiKhoanId == accountId
                                         && !ur.isDelete
                                         && (!ur.hieuLucDen.HasValue || ur.hieuLucDen.Value >= DateTime.Now))
                            .ToList();

                        if (activeRoles.Any(ur => ur.roleId == roleId))
                            return (false, "T?i kho?n ?ang c? vai tr? n?y!");

                        foreach (var userRole in activeRoles)
                        {
                            userRole.hieuLucDen = DateTime.Now;
                            userRole.isDelete = true;
                        }

                        var existing = db.UserRoles
                            .FirstOrDefault(ur => ur.taiKhoanId == accountId && ur.roleId == roleId);

                        if (existing != null)
                        {
                            existing.hieuLucTu = DateTime.Now;
                            existing.hieuLucDen = null;
                            existing.isDelete = false;
                        }
                        else
                        {
                            db.UserRoles.Add(new UserRole
                            {
                                taiKhoanId = accountId,
                                roleId = roleId,
                                hieuLucTu = DateTime.Now,
                                hieuLucDen = null,
                                isDelete = false
                            });
                        }

                        db.SaveChanges();
                        transaction.Commit();

                        return (true, "C?p nh?t vai tr? th?nh c?ng!");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return (false, $"L?i: {ex.Message}");
                    }
                }
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
