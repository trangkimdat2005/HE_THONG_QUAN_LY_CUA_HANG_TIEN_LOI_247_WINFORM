using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using MiniExcelLibs;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Services
{
    public class ExcelImportService
    {
        private readonly AppDbContext _context;
        public ExcelImportService()
        {
            _context = new AppDbContext();
        }
        public ImportResult ImportNhanVien(string filePath)
        {
            var result = new ImportResult();

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // 1. Đọc dữ liệu từ Excel
                    var rows = MiniExcel.Query<NhanVienImportDto>(filePath).ToList();
                    if (rows == null || rows.Count == 0)
                    {
                        result.IsSuccess = false;
                        result.Message = "File Excel không có dữ liệu!";
                        return result;
                    }

                    // 2. Chuẩn bị dữ liệu ban đầu
                    // Lấy số ID lớn nhất hiện tại trong DB để làm mốc tăng
                    int currentMaxId = GetCurrentMaxIdNumeric<NhanVien>("NV");

                    // Tạo ảnh mặc định nếu chưa có
                    var defaultImgId = "DEFAULT_AVATAR";
                    SetupDefaultImage(defaultImgId);

                    // Danh sách để kiểm tra trùng lặp trong chính file Excel (tránh 2 dòng Excel giống hệt nhau)
                    var excelPhoneCheck = new HashSet<string>();
                    var excelEmailCheck = new HashSet<string>();

                    int excelRowIndex = 1; // Bắt đầu từ dòng 2 (dòng 1 là header)

                    foreach (var row in rows)
                    {
                        excelRowIndex++; // Tăng dòng lên để báo lỗi cho chính xác

                        // --- VALIDATION (Kiểm tra lỗi) ---

                        // Kiểm tra dữ liệu trống
                        if (string.IsNullOrWhiteSpace(row.HoTen))
                        {
                            result.ErrorLogs.Add($"Dòng {excelRowIndex}: Họ tên không được để trống.");
                            continue; // Bỏ qua dòng này, đi tiếp dòng sau
                        }
                        if (string.IsNullOrWhiteSpace(row.SoDienThoai))
                        {
                            result.ErrorLogs.Add($"Dòng {excelRowIndex}: Số điện thoại không được để trống.");
                            continue;
                        }

                        // Kiểm tra trùng trong file Excel
                        if (excelPhoneCheck.Contains(row.SoDienThoai))
                        {
                            result.ErrorLogs.Add($"Dòng {excelRowIndex}: SĐT {row.SoDienThoai} bị trùng lặp trong file Excel.");
                            continue;
                        }
                        excelPhoneCheck.Add(row.SoDienThoai);

                        // Kiểm tra trùng trong Database (SĐT và Email)
                        if (_context.NhanViens.Any(x => x.soDienThoai == row.SoDienThoai && !x.isDelete))
                        {
                            result.ErrorLogs.Add($"Dòng {excelRowIndex}: SĐT {row.SoDienThoai} đã tồn tại trong hệ thống.");
                            continue;
                        }
                        if (!string.IsNullOrEmpty(row.Email) && _context.NhanViens.Any(x => x.email == row.Email && !x.isDelete))
                        {
                            result.ErrorLogs.Add($"Dòng {excelRowIndex}: Email {row.Email} đã tồn tại trong hệ thống.");
                            continue;
                        }


                        // --- XỬ LÝ DỮ LIỆU ---

                        // Tự tăng ID thủ công cho lô này
                        currentMaxId++;
                        // Lưu ý: Prefix "NV", totalLength 6 sẽ ra NV0001 (Nếu bạn muốn NV0001)
                        // Nếu bạn muốn NV001 (totalLength 5), hãy sửa số 6 bên dưới.
                        string newNvId = "NV" + currentMaxId.ToString().PadLeft(6 - 2, '0');

                        var nhanVien = new NhanVien
                        {
                            id = newNvId,
                            hoTen = row.HoTen,
                            gioiTinh = row.GioiTinhText?.Trim().ToLower() == "nam",
                            soDienThoai = row.SoDienThoai,
                            email = row.Email,
                            diaChi = row.DiaChi ?? "",
                            chucVu = row.ChucVu ?? "Nhân viên",
                            luongCoBan = row.LuongCoBan,
                            ngayVaoLam = DateTime.Now,
                            trangThai = "Đang làm",
                            isDelete = false,
                            anhId = defaultImgId
                        };
                        _context.NhanViens.Add(nhanVien);

                        // Tạo tài khoản tự động
                        var newTkId = Guid.NewGuid().ToString();
                        var taiKhoan = new TaiKhoan
                        {
                            id = newTkId,
                            tenDangNhap = row.SoDienThoai, // User là SĐT
                            matKhauHash = AuthenticationService.HashPassword("123456"), // Pass mặc định
                            email = row.Email,
                            trangThai = "Active",
                            isDelete = false
                        };
                        _context.TaiKhoans.Add(taiKhoan);

                        var tkNv = new TaiKhoanNhanVien
                        {
                            nhanVienId = newNvId,
                            taiKhoanId = newTkId,
                            isDelete = false
                        };
                        _context.TaiKhoanNhanViens.Add(tkNv);
                    }

                    // 3. Quyết định Lưu hay Hủy
                    if (result.ErrorLogs.Count > 0)
                    {
                        // Nếu có bất kỳ lỗi nào -> Rollback toàn bộ
                        transaction.Rollback();
                        result.IsSuccess = false;
                        result.Message = $"Có {result.ErrorLogs.Count} lỗi dữ liệu. Không dòng nào được thêm vào.";
                    }
                    else
                    {
                        _context.SaveChanges();
                        transaction.Commit();
                        result.IsSuccess = true;
                        result.Message = $"Đã thêm thành công {rows.Count} nhân viên!";
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    result.IsSuccess = false;
                    result.Message = "Lỗi hệ thống: " + ex.Message;
                }
            }

            return result;
        }
        private int GetCurrentMaxIdNumeric<T>(string prefix) where T : class
        {
            try
            {
                // Sử dụng DbSet<T> để trỏ đúng bảng (NhanVien hoặc SanPham)
                var table = _context.Set<T>();

                // Lấy tất cả ID (kể cả đã xóa mềm - isDelete=true) để tránh trùng lặp
                var lastEntity = table
                    .AsNoTracking()
                    .ToList() // Lấy về RAM xử lý vì LINQ to SQL đôi khi không parse được hàm tùy biến
                    .Select(e => {
                        var idProp = e.GetType().GetProperty("id") ?? e.GetType().GetProperty("Id");
                        return idProp?.GetValue(e)?.ToString();
                    })
                    .Where(id => !string.IsNullOrEmpty(id) && id.StartsWith(prefix))
                    .OrderByDescending(id => id) // Sắp xếp giảm dần để lấy số to nhất
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(lastEntity)) return 0;

                // Cắt lấy phần số (Ví dụ SP0005 -> lấy số 5)
                string numberPart = lastEntity.Substring(prefix.Length);
                if (int.TryParse(numberPart, out int number))
                {
                    return number;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }
        private void SetupDefaultImage(string imgId)
        {
            if (!_context.HinhAnhs.Any(x => x.Id == imgId))
            {
                var defaultImg = new HinhAnh { Id = imgId, TenAnh = "default.png", Anh = new byte[0] };
                _context.HinhAnhs.Add(defaultImg);
                _context.SaveChanges();
            }
        }
        public ImportResult ImportSanPham(string filePath)
        {
            var result = new ImportResult();

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // 1. Đọc Excel
                    var rows = MiniExcel.Query<SanPhamImportDto>(filePath).ToList();
                    if (rows == null || rows.Count == 0)
                    {
                        result.IsSuccess = false;
                        result.Message = "File Excel rỗng!";
                        return result;
                    }

                    // 2. Chuẩn bị dữ liệu
                    // Lấy danh sách ID Nhãn hiệu hiện có để kiểm tra nhanh
                    var listNhanHieuIds = _context.NhanHieux
                                            .Where(x => !x.isDelete)
                                            .Select(x => x.id)
                                            .ToList();
                    // Chuyển về HashSet để tìm kiếm cực nhanh (O(1))
                    var validBrandIds = new HashSet<string>(listNhanHieuIds, StringComparer.OrdinalIgnoreCase);

                    int currentSpId = GetCurrentMaxIdNumeric<SanPham>("SP");
                    var excelNameCheck = new HashSet<string>();
                    int excelRowIndex = 1;

                    foreach (var row in rows)
                    {
                        excelRowIndex++;

                        // --- VALIDATION ---
                        if (string.IsNullOrWhiteSpace(row.TenSanPham))
                        {
                            result.ErrorLogs.Add($"Dòng {excelRowIndex}: Tên sản phẩm trống.");
                            continue;
                        }

                        // Kiểm tra Mã Nhãn Hiệu
                        if (string.IsNullOrWhiteSpace(row.NhanHieuId))
                        {
                            result.ErrorLogs.Add($"Dòng {excelRowIndex}: Mã nhãn hiệu trống.");
                            continue;
                        }

                        // Validate định dạng NHxxxx (Optional - nếu bạn muốn chặt chẽ)
                        
                        if (!System.Text.RegularExpressions.Regex.IsMatch(row.NhanHieuId, @"^NH\d{4}$"))
                        {
                            result.ErrorLogs.Add($"Dòng {excelRowIndex}: Mã '{row.NhanHieuId}' sai định dạng (Phải là NH + 4 số, vd: NH0001).");
                            continue;
                        }

                        // Kiểm tra tồn tại trong DB (Quan trọng)
                        if (!validBrandIds.Contains(row.NhanHieuId.Trim()))
                        {
                            result.ErrorLogs.Add($"Dòng {excelRowIndex}: Mã nhãn hiệu '{row.NhanHieuId}' không tồn tại trong hệ thống.");
                            continue;
                        }

                        // Check trùng tên SP trong file Excel
                        if (excelNameCheck.Contains(row.TenSanPham))
                        {
                            result.ErrorLogs.Add($"Dòng {excelRowIndex}: Sản phẩm '{row.TenSanPham}' bị lặp lại trong file.");
                            continue;
                        }
                        excelNameCheck.Add(row.TenSanPham);

                        // Check trùng tên SP trong Database
                        if (_context.SanPhams.Any(x => x.ten == row.TenSanPham && !x.isDelete))
                        {
                            result.ErrorLogs.Add($"Dòng {excelRowIndex}: Sản phẩm '{row.TenSanPham}' đã có trong hệ thống.");
                            continue;
                        }

                        // --- XỬ LÝ SẢN PHẨM ---
                        currentSpId++;
                        string newSpId = "SP" + currentSpId.ToString().PadLeft(4, '0'); // Tạo SPxxxx

                        var sanPham = new SanPham
                        {
                            id = newSpId,
                            ten = row.TenSanPham.Trim(),
                            nhanHieuId = row.NhanHieuId.Trim().ToUpper(), // Dùng luôn ID người dùng nhập
                            moTa = row.MoTa,
                            isDelete = false
                        };
                        _context.SanPhams.Add(sanPham);
                    }

                    // 4. Kết thúc
                    if (result.ErrorLogs.Count > 0)
                    {
                        transaction.Rollback();
                        result.IsSuccess = false;
                        result.Message = $"Phát hiện {result.ErrorLogs.Count} lỗi. Đã hủy toàn bộ thao tác.";
                    }
                    else
                    {
                        _context.SaveChanges();
                        transaction.Commit();
                        result.IsSuccess = true;
                        result.Message = $"Đã nhập thành công {rows.Count} sản phẩm!";
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    result.IsSuccess = false;
                    result.Message = "Lỗi hệ thống: " + ex.Message;
                }
            }

            return result;
        }
        public ImportResult ImportSanPhamDonVi(string filePath)
        {
            var result = new ImportResult();

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var rows = MiniExcel.Query<SanPhamDonViImportDto>(filePath).ToList();
                    if (rows == null || rows.Count == 0)
                    {
                        result.IsSuccess = false;
                        result.Message = "File Excel rỗng!";
                        return result;
                    }

                    // 1. CACHE DỮ LIỆU
                    var validSpIds = new HashSet<string>(_context.SanPhams.Where(x => !x.isDelete).Select(x => x.id).ToList(), StringComparer.OrdinalIgnoreCase);
                    var validDvIds = new HashSet<string>(_context.DonViDoLuongs.Where(x => !x.isDelete).Select(x => x.id).ToList(), StringComparer.OrdinalIgnoreCase);

                    var existingPairs = _context.SanPhamDonVis
                                        .Where(x => !x.isDelete)
                                        .Select(x => new { x.sanPhamId, x.donViId })
                                        .ToList()
                                        .Select(x => $"{x.sanPhamId}_{x.donViId}".ToUpper())
                                        .ToHashSet();

                    int currentId = GetCurrentMaxIdNumeric<SanPhamDonVi>("SPDV");
                    var excelDuplicateCheck = new HashSet<string>();
                    int excelRowIndex = 1;

                    foreach (var row in rows)
                    {
                        excelRowIndex++;

                        // --- VALIDATION CƠ BẢN ---
                        if (string.IsNullOrWhiteSpace(row.SanPhamId) || string.IsNullOrWhiteSpace(row.DonViId))
                        {
                            result.ErrorLogs.Add($"Dòng {excelRowIndex}: Mã SP hoặc Mã Đơn vị bị trống.");
                            continue;
                        }

                        // --- VALIDATION TRẠNG THÁI (MỚI) ---
                        // Chuẩn hóa: Nếu trống -> Còn hàng. Nếu có nhập -> Cắt khoảng trắng
                        string status = string.IsNullOrWhiteSpace(row.TrangThai) ? "Còn hàng" : row.TrangThai.Trim();

                        // Kiểm tra chỉ cho phép 2 giá trị
                        if (status != "Còn hàng" && status != "Hết hàng")
                        {
                            result.ErrorLogs.Add($"Dòng {excelRowIndex}: Trạng thái '{row.TrangThai}' không hợp lệ. Chỉ chấp nhận 'Còn hàng' hoặc 'Hết hàng'.");
                            continue;
                        }

                        if (row.HeSoQuyDoi <= 0)
                        {
                            result.ErrorLogs.Add($"Dòng {excelRowIndex}: Hệ số quy đổi phải lớn hơn 0.");
                            continue;
                        }
                        if (row.GiaBan < 0)
                        {
                            result.ErrorLogs.Add($"Dòng {excelRowIndex}: Giá bán không được âm.");
                            continue;
                        }

                        // Kiểm tra tồn tại Mã SP & Đơn vị
                        if (!validSpIds.Contains(row.SanPhamId.Trim()))
                        {
                            result.ErrorLogs.Add($"Dòng {excelRowIndex}: Mã SP '{row.SanPhamId}' không tồn tại.");
                            continue;
                        }
                        if (!validDvIds.Contains(row.DonViId.Trim()))
                        {
                            result.ErrorLogs.Add($"Dòng {excelRowIndex}: Mã Đơn vị '{row.DonViId}' không tồn tại.");
                            continue;
                        }

                        // Kiểm tra trùng lặp
                        string pairKey = $"{row.SanPhamId.Trim()}_{row.DonViId.Trim()}".ToUpper();
                        if (excelDuplicateCheck.Contains(pairKey))
                        {
                            result.ErrorLogs.Add($"Dòng {excelRowIndex}: Cặp '{row.SanPhamId} - {row.DonViId}' bị lặp lại trong file.");
                            continue;
                        }
                        excelDuplicateCheck.Add(pairKey);

                        if (existingPairs.Contains(pairKey))
                        {
                            result.ErrorLogs.Add($"Dòng {excelRowIndex}: Sản phẩm '{row.SanPhamId}' với đơn vị '{row.DonViId}' đã có trong hệ thống.");
                            continue;
                        }

                        // --- INSERT ---
                        currentId++;
                        string newId = "SPDV" + currentId.ToString().PadLeft(4, '0');

                        var spdv = new SanPhamDonVi
                        {
                            id = newId,
                            sanPhamId = row.SanPhamId.Trim(),
                            donViId = row.DonViId.Trim(),
                            heSoQuyDoi = row.HeSoQuyDoi,
                            giaBan = row.GiaBan,
                            trangThai = status, // Sử dụng biến status đã chuẩn hóa
                            isDelete = false
                        };

                        _context.SanPhamDonVis.Add(spdv);
                    }

                    if (result.ErrorLogs.Count > 0)
                    {
                        transaction.Rollback();
                        result.IsSuccess = false;
                        result.Message = $"Có {result.ErrorLogs.Count} lỗi dữ liệu. Đã hủy thao tác.";
                    }
                    else
                    {
                        _context.SaveChanges();
                        transaction.Commit();
                        result.IsSuccess = true;
                        result.Message = $"Nhập thành công {rows.Count} quy cách sản phẩm!";
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    string msg = ex.Message;
                    if (ex.InnerException != null) msg += "\nSQL Error: " + ex.InnerException.Message;
                    result.IsSuccess = false;
                    result.Message = "Lỗi hệ thống: " + msg;
                }
            }
            return result;
        }
    }
}