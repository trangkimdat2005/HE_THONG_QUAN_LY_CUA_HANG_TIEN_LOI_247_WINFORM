using System;
using System.Collections.Generic;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BusinessLayer.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers
{
    /// <summary>
    /// Controller ?i?u ph?i các thao tác liên quan ??n Khách hàng
    /// </summary>
    public class CustomerController
    {
        private readonly CustomerService _customerService;

        public CustomerController()
        {
            _customerService = new CustomerService();
        }

        /// <summary>
        /// L?y t?t c? khách hàng
        /// </summary>
        public List<KhachHang> GetAllCustomers()
        {
            try
            {
                return _customerService.GetAllCustomers();
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
                return _customerService.GetCustomerById(id);
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
                return _customerService.SearchCustomers(keyword);
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
                return _customerService.AddCustomer(customer);
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
                return _customerService.UpdateCustomer(customer);
            }
            catch (Exception ex)
            {
                return (false, $"L?i khi c?p nh?t khách hàng: {ex.Message}");
            }
        }

        /// <summary>
        /// Xóa khách hàng
        /// </summary>
        public (bool success, string message) DeleteCustomer(string id)
        {
            try
            {
                return _customerService.DeleteCustomer(id);
            }
            catch (Exception ex)
            {
                return (false, $"L?i khi xóa khách hàng: {ex.Message}");
            }
        }

        /// <summary>
        /// L?y danh sách khách hàng VIP
        /// </summary>
        public List<KhachHang> GetVIPCustomers()
        {
            try
            {
                return _customerService.GetVIPCustomers();
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
                return _customerService.GetCustomerDetail(customerId);
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
                return _customerService.GetPurchaseHistory(customerId);
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
                return _customerService.GetMemberCard(customerId);
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
                return _customerService.UpdateMemberCard(memberCard);
            }
            catch (Exception ex)
            {
                return (false, $"L?i khi c?p nh?t th? thành viên: {ex.Message}");
            }
        }

        /// <summary>
        /// Tính ?i?m tích l?y
        /// </summary>
        public int CalculateLoyaltyPoints(decimal totalPurchase)
        {
            try
            {
                return _customerService.CalculateLoyaltyPoints(totalPurchase);
            }
            catch (Exception ex)
            {
                throw new Exception($"L?i khi tính ?i?m tích l?y: {ex.Message}");
            }
        }

        /// <summary>
        /// Xác ??nh h?ng th?
        /// </summary>
        public string DetermineMemberRank(int points)
        {
            try
            {
                return _customerService.DetermineMemberRank(points);
            }
            catch (Exception ex)
            {
                throw new Exception($"L?i khi xác ??nh h?ng th?: {ex.Message}");
            }
        }
    }
}
