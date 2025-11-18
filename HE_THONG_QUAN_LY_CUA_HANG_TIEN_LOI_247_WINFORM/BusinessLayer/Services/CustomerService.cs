using System;
using System.Collections.Generic;
using System.Linq;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM._01_DataAccessLayer.Repositories;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BusinessLayer.Services
{
    /// <summary>
    /// Service x? lý business logic cho Khách hàng
    /// </summary>
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService()
        {
            _customerRepository = new CustomerRepository();
        }

        /// <summary>
        /// L?y t?t c? khách hàng
        /// </summary>
        public List<KhachHang> GetAllCustomers()
        {
            return _customerRepository.GetAllCustomers();
        }

        /// <summary>
        /// L?y khách hàng theo ID
        /// </summary>
        public KhachHang GetCustomerById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("ID khách hàng không ???c ?? tr?ng.");

            return _customerRepository.GetCustomerById(id);
        }

        /// <summary>
        /// Tìm ki?m khách hàng
        /// </summary>
        public List<KhachHang> SearchCustomers(string keyword)
        {
            return _customerRepository.SearchCustomers(keyword);
        }

        /// <summary>
        /// Thêm khách hàng m?i
        /// </summary>
        public (bool success, string message) AddCustomer(KhachHang customer)
        {
            // Validate
            var validationResult = ValidateCustomer(customer);
            if (!validationResult.isValid)
                return (false, validationResult.message);

            // Ki?m tra s? ?i?n tho?i ?ã t?n t?i ch?a
            var existingCustomer = _customerRepository.GetCustomerByPhone(customer.soDienThoai);
            if (existingCustomer != null)
                return (false, "S? ?i?n tho?i ?ã ???c s? d?ng.");

            // Thêm khách hàng
            bool result = _customerRepository.AddCustomer(customer);
            return result
                ? (true, "Thêm khách hàng thành công.")
                : (false, "Có l?i x?y ra khi thêm khách hàng.");
        }

        /// <summary>
        /// C?p nh?t thông tin khách hàng
        /// </summary>
        public (bool success, string message) UpdateCustomer(KhachHang customer)
        {
            // Validate
            var validationResult = ValidateCustomer(customer);
            if (!validationResult.isValid)
                return (false, validationResult.message);

            // Ki?m tra s? ?i?n tho?i ?ã ???c s? d?ng b?i khách hàng khác ch?a
            var existingCustomer = _customerRepository.GetCustomerByPhone(customer.soDienThoai);
            if (existingCustomer != null && existingCustomer.id != customer.id)
                return (false, "S? ?i?n tho?i ?ã ???c s? d?ng b?i khách hàng khác.");

            // C?p nh?t
            bool result = _customerRepository.UpdateCustomer(customer);
            return result
                ? (true, "C?p nh?t khách hàng thành công.")
                : (false, "Có l?i x?y ra khi c?p nh?t khách hàng.");
        }

        /// <summary>
        /// Xóa khách hàng
        /// </summary>
        public (bool success, string message) DeleteCustomer(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return (false, "ID khách hàng không h?p l?.");

            var customer = _customerRepository.GetCustomerById(id);
            if (customer == null)
                return (false, "Không tìm th?y khách hàng.");

            bool result = _customerRepository.DeleteCustomer(id);
            return result
                ? (true, "Xóa khách hàng thành công.")
                : (false, "Có l?i x?y ra khi xóa khách hàng.");
        }

        /// <summary>
        /// L?y th?ng kê khách hàng VIP
        /// </summary>
        public List<KhachHang> GetVIPCustomers()
        {
            return _customerRepository.GetVIPCustomers();
        }

        /// <summary>
        /// L?y thông tin chi ti?t khách hàng bao g?m l?ch s? mua hàng
        /// </summary>
        public CustomerDetailDto GetCustomerDetail(string customerId)
        {
            var customer = _customerRepository.GetCustomerById(customerId);
            if (customer == null)
                return null;

            var purchaseHistory = _customerRepository.GetPurchaseHistory(customerId);
            var memberCard = _customerRepository.GetMemberCard(customerId);
            var totalPurchase = _customerRepository.GetCustomerTotalPurchase(customerId);
            var purchaseCount = _customerRepository.GetCustomerPurchaseCount(customerId);

            return new CustomerDetailDto
            {
                Customer = customer,
                PurchaseHistory = purchaseHistory,
                MemberCard = memberCard,
                TotalPurchase = totalPurchase,
                PurchaseCount = purchaseCount
            };
        }

        /// <summary>
        /// L?y l?ch s? mua hàng c?a khách hàng
        /// </summary>
        public List<LichSuMuaHang> GetPurchaseHistory(string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
                throw new ArgumentException("ID khách hàng không ???c ?? tr?ng.");

            return _customerRepository.GetPurchaseHistory(customerId);
        }

        /// <summary>
        /// L?y th? thành viên
        /// </summary>
        public TheThanhVien GetMemberCard(string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
                throw new ArgumentException("ID khách hàng không ???c ?? tr?ng.");

            return _customerRepository.GetMemberCard(customerId);
        }

        /// <summary>
        /// C?p nh?t th? thành viên
        /// </summary>
        public (bool success, string message) UpdateMemberCard(TheThanhVien memberCard)
        {
            if (memberCard == null)
                return (false, "Thông tin th? thành viên không h?p l?.");

            if (string.IsNullOrWhiteSpace(memberCard.khachHangId))
                return (false, "ID khách hàng không h?p l?.");

            // Ki?m tra khách hàng có t?n t?i không
            var customer = _customerRepository.GetCustomerById(memberCard.khachHangId);
            if (customer == null)
                return (false, "Không tìm th?y khách hàng.");

            bool result = _customerRepository.SaveMemberCard(memberCard);
            return result
                ? (true, "C?p nh?t th? thành viên thành công.")
                : (false, "Có l?i x?y ra khi c?p nh?t th? thành viên.");
        }

        /// <summary>
        /// Tính ?i?m tích l?y t? t?ng ti?n mua hàng
        /// </summary>
        public int CalculateLoyaltyPoints(decimal totalPurchase)
        {
            // Quy t?c: 1,000 VND = 1 ?i?m
            return (int)(totalPurchase / 1000);
        }

        /// <summary>
        /// Xác ??nh h?ng th? thành viên d?a trên ?i?m tích l?y
        /// </summary>
        public string DetermineMemberRank(int points)
        {
            if (points >= 1000)
                return "Vàng";
            else if (points >= 500)
                return "B?c";
            else
                return "??ng";
        }

        /// <summary>
        /// Validate thông tin khách hàng
        /// </summary>
        private (bool isValid, string message) ValidateCustomer(KhachHang customer)
        {
            if (customer == null)
                return (false, "Thông tin khách hàng không h?p l?.");

            if (string.IsNullOrWhiteSpace(customer.hoTen))
                return (false, "H? tên không ???c ?? tr?ng.");

            if (string.IsNullOrWhiteSpace(customer.soDienThoai))
                return (false, "S? ?i?n tho?i không ???c ?? tr?ng.");

            if (customer.soDienThoai.Length < 10 || customer.soDienThoai.Length > 11)
                return (false, "S? ?i?n tho?i không h?p l?.");

            if (!string.IsNullOrWhiteSpace(customer.email))
            {
                if (!IsValidEmail(customer.email))
                    return (false, "Email không h?p l?.");
            }

            if (string.IsNullOrWhiteSpace(customer.diaChi))
                return (false, "??a ch? không ???c ?? tr?ng.");

            if (string.IsNullOrWhiteSpace(customer.trangThai))
                customer.trangThai = "Active";

            return (true, "");
        }

        /// <summary>
        /// Ki?m tra email h?p l?
        /// </summary>
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }

    /// <summary>
    /// DTO cho thông tin chi ti?t khách hàng
    /// </summary>
    public class CustomerDetailDto
    {
        public KhachHang Customer { get; set; }
        public List<LichSuMuaHang> PurchaseHistory { get; set; }
        public TheThanhVien MemberCard { get; set; }
        public decimal TotalPurchase { get; set; }
        public int PurchaseCount { get; set; }
    }
}
