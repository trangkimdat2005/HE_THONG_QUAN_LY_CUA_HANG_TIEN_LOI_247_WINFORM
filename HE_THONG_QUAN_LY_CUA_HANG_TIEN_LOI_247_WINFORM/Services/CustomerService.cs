using System;
using System.Collections.Generic;
using System.Linq;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM
{
    /// <summary>
    /// Service xử lý logic nghiệp vụ cho Khách hàng
    /// </summary>
    public class CustomerService
    {
        private readonly AppDbContext _context;

        public CustomerService()
        {
            _context = new AppDbContext();
        }

        /// <summary>
        /// Lấy tất cả khách hàng
        /// </summary>
        public List<KhachHang> GetAllCustomers()
        {
            try
            {
                return _context.KhachHangs
                    .Where(k => !k.isDelete)
                    .OrderByDescending(k => k.ngayDangKy)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách khách hàng: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy khách hàng theo ID
        /// </summary>
        public KhachHang GetCustomerById(string id)
        {
            // Hàm này chỉ có nhiệm vụ: Tìm và trả về kết quả (hoặc null).
            // Không nên tự ý ném lỗi (throw exception) ở đây.

            return _context.KhachHangs
                           .FirstOrDefault(k => k.id == id && !k.isDelete);
        }

        /// <summary>
        /// Tìm kiếm khách hàng
        /// </summary>
        public List<KhachHang> SearchCustomers(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    return GetAllCustomers();
                }

                keyword = keyword.ToLower().Trim();

                return _context.KhachHangs
                    .Where(k => !k.isDelete &&
                        (k.hoTen.ToLower().Contains(keyword) ||
                         k.soDienThoai.Contains(keyword) ||
                         k.email.ToLower().Contains(keyword)))
                    .OrderBy(k => k.hoTen)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tìm kiếm khách hàng: {ex.Message}");
            }
        }

        /// <summary>
        /// Thêm khách hàng mới
        /// </summary>
        public (bool success, string message) AddCustomer(KhachHang customer)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(customer.hoTen))
                {
                    return (false, "Tên khách hàng không được để trống.");
                }

                if (string.IsNullOrWhiteSpace(customer.soDienThoai))
                {
                    return (false, "Số điện thoại không được để trống.");
                }

                // Kiểm tra số điện thoại đã tồn tại
                var existingCustomer = _context.KhachHangs
                    .FirstOrDefault(k => k.soDienThoai == customer.soDienThoai && !k.isDelete);

                if (existingCustomer != null)
                {
                    return (false, "Số điện thoại đã được đăng ký.");
                }

                // Tạo ID mới
                customer.id = GenerateNewCustomerId();
                customer.ngayDangKy = DateTime.Now;
                customer.isDelete = false;
                
                // Đảm bảo các trường bắt buộc có giá trị
                if (string.IsNullOrWhiteSpace(customer.diaChi))
                {
                    customer.diaChi = "Chưa cập nhật";
                }
                
                if (string.IsNullOrWhiteSpace(customer.trangThai))
                {
                    customer.trangThai = "Active";
                }
                
                if (string.IsNullOrWhiteSpace(customer.email))
                {
                    customer.email = "";
                }

                _context.KhachHangs.Add(customer);
                _context.SaveChanges();

                return (true, "Thêm khách hàng thành công.");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                // Log chi tiết lỗi validation
                var errorMessages = new System.Text.StringBuilder();
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessages.AppendLine($"- {validationError.PropertyName}: {validationError.ErrorMessage}");
                    }
                }
                return (false, $"Lỗi validation:\n{errorMessages.ToString()}");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi thêm khách hàng: {ex.Message}");
            }
        }

        /// <summary>
        /// Cập nhật thông tin khách hàng
        /// </summary>
        public (bool success, string message) UpdateCustomer(KhachHang customer)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(customer.hoTen))
                {
                    return (false, "Tên khách hàng không được để trống.");
                }

                if (string.IsNullOrWhiteSpace(customer.soDienThoai))
                {
                    return (false, "Số điện thoại không được để trống.");
                }

                var existingCustomer = _context.KhachHangs
                    .FirstOrDefault(k => k.id == customer.id);

                if (existingCustomer == null)
                {
                    return (false, "Không tìm thấy khách hàng.");
                }

                // Kiểm tra số điện thoại trùng với khách hàng khác
                var duplicatePhone = _context.KhachHangs
                    .FirstOrDefault(k => k.soDienThoai == customer.soDienThoai &&
                                        k.id != customer.id && !k.isDelete);

                if (duplicatePhone != null)
                {
                    return (false, "Số điện thoại đã được sử dụng bởi khách hàng khác.");
                }

                // Cập nhật thông tin
                existingCustomer.hoTen = customer.hoTen;
                existingCustomer.soDienThoai = customer.soDienThoai;
                existingCustomer.email = string.IsNullOrWhiteSpace(customer.email) ? "" : customer.email;
                existingCustomer.diaChi = string.IsNullOrWhiteSpace(customer.diaChi) ? "Chưa cập nhật" : customer.diaChi;
                existingCustomer.gioiTinh = customer.gioiTinh;
                existingCustomer.trangThai = string.IsNullOrWhiteSpace(customer.trangThai) ? "Active" : customer.trangThai;
                existingCustomer.anhId = customer.anhId;

                _context.SaveChanges();

                return (true, "Cập nhật khách hàng thành công.");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                // Log chi tiết lỗi validation
                var errorMessages = new System.Text.StringBuilder();
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessages.AppendLine($"- {validationError.PropertyName}: {validationError.ErrorMessage}");
                    }
                }
                return (false, $"Lỗi validation:\n{errorMessages.ToString()}");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi cập nhật khách hàng: {ex.Message}");
            }
        }

        /// <summary>
        /// Xóa khách hàng (soft delete)
        /// </summary>
        public (bool success, string message) DeleteCustomer(string id)
        {
            try
            {
                var customer = _context.KhachHangs
                    .FirstOrDefault(k => k.id == id);

                if (customer == null)
                {
                    return (false, "Không tìm thấy khách hàng.");
                }

                customer.isDelete = true;
                _context.SaveChanges();

                return (true, "Xóa khách hàng thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi xóa khách hàng: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy danh sách khách hàng VIP (khách hàng có thẻ hạng Vàng)
        /// </summary>
        public List<KhachHang> GetVIPCustomers()
        {
            try
            {
                return _context.KhachHangs
                    .Where(k => !k.isDelete &&
                        k.TheThanhViens.Any(t => !t.isDelete && t.hang == "Vàng"))
                    .OrderBy(k => k.hoTen)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách khách hàng VIP: {ex.Message}");
            }
        }
        /// <summary>
        /// Lấy lịch sử mua hàng
        /// </summary>
        public List<LichSuMuaHang> GetPurchaseHistory(string customerId)
        {
            try
            {
                return _context.LichSuMuaHangs
                    .Where(l => l.khachHangId == customerId && !l.isDelete)
                    .OrderByDescending(l => l.ngayMua)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy lịch sử mua hàng: {ex.Message}");
            }
        }
        /// <summary>
        /// Lấy thông tin chi tiết khách hàng
        /// </summary>
        public CustomerDetailDto GetCustomerDetail(string customerId)
        {
            try
            {
                var customer = GetCustomerById(customerId);

                if (customer == null)
                {
                    throw new Exception("Không tìm thấy khách hàng.");
                }

                var purchaseHistory = GetPurchaseHistory(customerId);
                var memberCard = GetMemberCard(customerId);

                return new CustomerDetailDto
                {
                    Customer = customer,
                    TotalPurchase = purchaseHistory.Sum(p => p.tongTien),
                    PurchaseCount = purchaseHistory.Count,
                    MemberCard = memberCard,
                    PurchaseHistory = purchaseHistory
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thông tin chi tiết khách hàng: {ex.Message}");
            }
        }



        /// <summary>
        /// Lấy thẻ thành viên
        /// </summary>
        public TheThanhVien GetMemberCard(string customerId)
        {
            try
            {
                return _context.TheThanhViens
                    .FirstOrDefault(t => t.khachHangId == customerId && !t.isDelete);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thông tin thẻ thành viên: {ex.Message}");
            }
        }

        /// <summary>
        /// Cập nhật thẻ thành viên
        /// </summary>
        public (bool success, string message) UpdateMemberCard(TheThanhVien memberCard)
        {
            try
            {
                var existingCard = _context.TheThanhViens
                    .FirstOrDefault(t => t.khachHangId == memberCard.khachHangId && !t.isDelete);

                if (existingCard == null)
                {
                    // Tạo mới thẻ thành viên
                    memberCard.id = GenerateNewMemberCardId();
                    memberCard.isDelete = false;
                    _context.TheThanhViens.Add(memberCard);
                }
                else
                {
                    // Cập nhật thẻ thành viên
                    existingCard.hang = memberCard.hang;
                    existingCard.diemTichLuy = memberCard.diemTichLuy;
                    existingCard.ngayCap = memberCard.ngayCap;
                }

                _context.SaveChanges();

                return (true, "Cập nhật thẻ thành viên thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi cập nhật thẻ thành viên: {ex.Message}");
            }
        }

        /// <summary>
        /// Tính điểm tích lũy (1 điểm cho mỗi 10,000 VNĐ)
        /// </summary>
        public int CalculateLoyaltyPoints(decimal totalPurchase)
        {
            try
            {
                return (int)(totalPurchase / 10000);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tính điểm tích lũy: {ex.Message}");
            }
        }

        /// <summary>
        /// Xác định hạng thẻ dựa trên điểm tích lũy
        /// </summary>
        public string DetermineMemberRank(int points)
        {
            try
            {
                if (points >= 1000)
                {
                    return "Vàng";
                }
                else if (points >= 500)
                {
                    return "Bạc";
                }
                else
                {
                    return "Đồng";
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xác định hạng thẻ: {ex.Message}");
            }
        }

        /// <summary>
        /// Tạo ID mới cho khách hàng
        /// </summary>
        private string GenerateNewCustomerId()
        {
            try
            {
                var lastCustomer = _context.KhachHangs
                    .OrderByDescending(k => k.id)
                    .FirstOrDefault();

                if (lastCustomer == null || string.IsNullOrEmpty(lastCustomer.id))
                {
                    return "KH00001";
                }

                // Lấy phần số từ ID cuối cùng
                var lastIdNumber = lastCustomer.id.Substring(2);
                if (int.TryParse(lastIdNumber, out int number))
                {
                    number++;
                    return $"KH{number:D5}";
                }

                return "KH00001";
            }
            catch (Exception)
            {
                return "KH00001";
            }
        }
        public List<LichSuMuaHang> GetPurchaseHistoryByDate(string customerId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                // Lọc ngay tại nguồn dữ liệu
                return _context.LichSuMuaHangs
                    .Where(l => l.khachHangId == customerId
                             && !l.isDelete
                             && l.ngayMua >= fromDate
                             && l.ngayMua <= toDate) // Logic lọc nằm ở đây
                    .OrderByDescending(l => l.ngayMua)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lọc lịch sử mua hàng: {ex.Message}");
            }
        }
        /// <summary>
        /// Tạo ID mới cho thẻ thành viên
        /// </summary>
        private string GenerateNewMemberCardId()
        {
            try
            {
                var lastCard = _context.TheThanhViens
                    .OrderByDescending(t => t.id)
                    .FirstOrDefault();

                if (lastCard == null || string.IsNullOrEmpty(lastCard.id))
                {
                    return "TTV00001";
                }

                // Lấy phần số từ ID cuối cùng
                var lastIdNumber = lastCard.id.Substring(3);
                if (int.TryParse(lastIdNumber, out int number))
                {
                    number++;
                    return $"TTV{number:D5}";
                }

                return "TTV00001";
            }
            catch (Exception)
            {
                return "TTV00001";
            }
        }
    }
}
