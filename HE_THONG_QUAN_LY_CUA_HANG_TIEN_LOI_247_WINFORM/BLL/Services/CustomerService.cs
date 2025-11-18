using System;
using System.Collections.Generic;
using System.Linq;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM
{
    /// <summary>
    /// Service x? lý logic nghi?p v? cho Khách hàng
    /// </summary>
    public class CustomerService
    {
        private readonly AppDbContext _context;

        public CustomerService()
        {
            _context = new AppDbContext();
        }

        /// <summary>
        /// L?y t?t c? khách hàng
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
                throw new Exception($"L?i khi l?y danh sách khách hàng: {ex.Message}");
            }
        }

        /// <summary>
        /// L?y khách hàng theo ID
        /// </summary>
        public KhachHang GetCustomerById(string id)
        {
            try
            {
                var customer = _context.KhachHangs
                    .FirstOrDefault(k => k.id == id && !k.isDelete);

                if (customer == null)
                {
                    throw new Exception("Không tìm th?y khách hàng.");
                }

                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception($"L?i khi l?y thông tin khách hàng: {ex.Message}");
            }
        }

        /// <summary>
        /// Tìm ki?m khách hàng
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
                throw new Exception($"L?i khi tìm ki?m khách hàng: {ex.Message}");
            }
        }

        /// <summary>
        /// Thêm khách hàng m?i
        /// </summary>
        public (bool success, string message) AddCustomer(KhachHang customer)
        {
            try
            {
                // Ki?m tra s? ?i?n tho?i ?ã t?n t?i
                var existingCustomer = _context.KhachHangs
                    .FirstOrDefault(k => k.soDienThoai == customer.soDienThoai && !k.isDelete);

                if (existingCustomer != null)
                {
                    return (false, "S? ?i?n tho?i ?ã ???c ??ng ký.");
                }

                // T?o ID m?i
                customer.id = GenerateNewCustomerId();
                customer.ngayDangKy = DateTime.Now;
                customer.isDelete = false;

                _context.KhachHangs.Add(customer);
                _context.SaveChanges();

                return (true, "Thêm khách hàng thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"L?i khi thêm khách hàng: {ex.Message}");
            }
        }

        /// <summary>
        /// C?p nh?t thông tin khách hàng
        /// </summary>
        public (bool success, string message) UpdateCustomer(KhachHang customer)
        {
            try
            {
                var existingCustomer = _context.KhachHangs
                    .FirstOrDefault(k => k.id == customer.id);

                if (existingCustomer == null)
                {
                    return (false, "Không tìm th?y khách hàng.");
                }

                // Ki?m tra s? ?i?n tho?i trùng v?i khách hàng khác
                var duplicatePhone = _context.KhachHangs
                    .FirstOrDefault(k => k.soDienThoai == customer.soDienThoai &&
                                        k.id != customer.id && !k.isDelete);

                if (duplicatePhone != null)
                {
                    return (false, "S? ?i?n tho?i ?ã ???c s? d?ng b?i khách hàng khác.");
                }

                existingCustomer.hoTen = customer.hoTen;
                existingCustomer.soDienThoai = customer.soDienThoai;
                existingCustomer.email = customer.email;
                existingCustomer.diaChi = customer.diaChi;
                existingCustomer.gioiTinh = customer.gioiTinh;
                existingCustomer.trangThai = customer.trangThai;
                existingCustomer.anhId = customer.anhId;

                _context.SaveChanges();

                return (true, "C?p nh?t khách hàng thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"L?i khi c?p nh?t khách hàng: {ex.Message}");
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
                    return (false, "Không tìm th?y khách hàng.");
                }

                customer.isDelete = true;
                _context.SaveChanges();

                return (true, "Xóa khách hàng thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"L?i khi xóa khách hàng: {ex.Message}");
            }
        }

        /// <summary>
        /// L?y danh sách khách hàng VIP (khách hàng có th? h?ng Vàng)
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
                throw new Exception($"L?i khi l?y danh sách khách hàng VIP: {ex.Message}");
            }
        }

        /// <summary>
        /// L?y thông tin chi ti?t khách hàng
        /// </summary>
        public CustomerDetailDto GetCustomerDetail(string customerId)
        {
            try
            {
                var customer = GetCustomerById(customerId);

                if (customer == null)
                {
                    throw new Exception("Không tìm th?y khách hàng.");
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
                throw new Exception($"L?i khi l?y thông tin chi ti?t khách hàng: {ex.Message}");
            }
        }

        /// <summary>
        /// L?y l?ch s? mua hàng
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
                throw new Exception($"L?i khi l?y l?ch s? mua hàng: {ex.Message}");
            }
        }

        /// <summary>
        /// L?y th? thành viên
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
                throw new Exception($"L?i khi l?y thông tin th? thành viên: {ex.Message}");
            }
        }

        /// <summary>
        /// C?p nh?t th? thành viên
        /// </summary>
        public (bool success, string message) UpdateMemberCard(TheThanhVien memberCard)
        {
            try
            {
                var existingCard = _context.TheThanhViens
                    .FirstOrDefault(t => t.khachHangId == memberCard.khachHangId && !t.isDelete);

                if (existingCard == null)
                {
                    // T?o m?i th? thành viên
                    memberCard.id = GenerateNewMemberCardId();
                    memberCard.isDelete = false;
                    _context.TheThanhViens.Add(memberCard);
                }
                else
                {
                    // C?p nh?t th? thành viên
                    existingCard.hang = memberCard.hang;
                    existingCard.diemTichLuy = memberCard.diemTichLuy;
                    existingCard.ngayCap = memberCard.ngayCap;
                }

                _context.SaveChanges();

                return (true, "C?p nh?t th? thành viên thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"L?i khi c?p nh?t th? thành viên: {ex.Message}");
            }
        }

        /// <summary>
        /// Tính ?i?m tích l?y (1 ?i?m cho m?i 10,000 VN?)
        /// </summary>
        public int CalculateLoyaltyPoints(decimal totalPurchase)
        {
            try
            {
                return (int)(totalPurchase / 10000);
            }
            catch (Exception ex)
            {
                throw new Exception($"L?i khi tính ?i?m tích l?y: {ex.Message}");
            }
        }

        /// <summary>
        /// Xác ??nh h?ng th? d?a trên ?i?m tích l?y
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
                    return "B?c";
                }
                else
                {
                    return "??ng";
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"L?i khi xác ??nh h?ng th?: {ex.Message}");
            }
        }

        /// <summary>
        /// T?o ID m?i cho khách hàng
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

                // L?y ph?n s? t? ID cu?i cùng
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

        /// <summary>
        /// T?o ID m?i cho th? thành viên
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

                // L?y ph?n s? t? ID cu?i cùng
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
