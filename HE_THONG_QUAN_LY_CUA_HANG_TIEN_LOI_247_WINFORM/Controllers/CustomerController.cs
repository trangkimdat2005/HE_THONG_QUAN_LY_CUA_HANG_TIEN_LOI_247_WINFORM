using System;
using System.Collections.Generic;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers
{
    /// <summary>
    /// Controller điều phối các thao tác liên quan đến Khách hàng
    /// </summary>
    public class CustomerController
    {
        private readonly CustomerService _customerService;

        public CustomerController()
        {
            _customerService = new CustomerService();
        }

        /// <summary>
        /// Lấy tất cả khách hàng
        /// </summary>
        public List<KhachHang> GetAllCustomers()
        {
            try
            {
                return _customerService.GetAllCustomers();
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
            // Bỏ try-catch đi, chỉ gọi hàm.
            // Nếu có lỗi hệ thống (mất mạng, sai SQL), để tầng UI cao nhất xử lý hoặc dùng try-catch bao quanh ở tầng UI.
            return _customerService.GetCustomerById(id);
        }

        /// <summary>
        /// Tìm kiếm khách hàng
        /// </summary>
        public List<KhachHang> SearchCustomers(string keyword)
        {
            try
            {
                return _customerService.SearchCustomers(keyword);
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
                return _customerService.AddCustomer(customer);
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
                return _customerService.UpdateCustomer(customer);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi cập nhật khách hàng: {ex.Message}");
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
                return (false, $"Lỗi khi xóa khách hàng: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy danh sách khách hàng VIP
        /// </summary>
        public List<KhachHang> GetVIPCustomers()
        {
            try
            {
                return _customerService.GetVIPCustomers();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách khách hàng VIP: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy thông tin chi tiết khách hàng
        /// </summary>
        public CustomerDetailDto GetCustomerDetail(string customerId)
        {
            try
            {
                return _customerService.GetCustomerDetail(customerId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thông tin chi tiết khách hàng: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy lịch sử mua hàng
        /// </summary>


        /// <summary>
        /// Lấy thẻ thành viên
        /// </summary>
        public TheThanhVien GetMemberCard(string customerId)
        {
            try
            {
                return _customerService.GetMemberCard(customerId);
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
                return _customerService.UpdateMemberCard(memberCard);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi cập nhật thẻ thành viên: {ex.Message}");
            }
        }

        /// <summary>
        /// Tính điểm tích lũy
        /// </summary>
        public int CalculateLoyaltyPoints(decimal totalPurchase)
        {
            try
            {
                return _customerService.CalculateLoyaltyPoints(totalPurchase);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tính điểm tích lũy: {ex.Message}");
            }
        }

        /// <summary>
        /// Xác định hạng thẻ
        /// </summary>
        public string DetermineMemberRank(int points)
        {
            try
            {
                return _customerService.DetermineMemberRank(points);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xác định hạng thẻ: {ex.Message}");
            }
        }
        public List<LichSuMuaHang> GetPurchaseHistoryByDate(string customerId, DateTime from, DateTime to)
        {
            // Chỉ việc gọi Service
            return _customerService.GetPurchaseHistoryByDate(customerId, from, to);
        }
    }
}
