using System;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;
using System.Collections.Generic;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers
{
    public class EmployeeAccountController
    {
        private readonly EmployeeAccountService _service;

        public EmployeeAccountController()
        {
            _service = new EmployeeAccountService();
        }

        public dynamic GetAllEmployeeAccounts()
        {
            return _service.GetAllEmployeeAccounts();
        }

        public dynamic SearchEmployeeAccounts(string keyword)
        {
            return _service.SearchEmployeeAccounts(keyword);
        }

        public List<NhanVien> GetEmployeesWithoutAccount()
        {
            return _service.GetEmployeesWithoutAccount();
        }

        public (bool success, string message, TaiKhoan account) CreateEmployeeAccount(
            string employeeId, 
            string username, 
            string password, 
            string email)
        {
            return _service.CreateEmployeeAccount(employeeId, username, password, email);
        }

        public (bool success, string message) ResetPassword(string accountId, string newPassword)
        {
            return _service.ResetPassword(accountId, newPassword);
        }

        public (bool success, string message) ToggleAccountStatus(string accountId)
        {
            return _service.ToggleAccountStatus(accountId);
        }

        public (bool success, string message) DeleteEmployeeAccount(string employeeId)
        {
            return _service.DeleteEmployeeAccount(employeeId);
        }

        public void Dispose()
        {
            _service?.Dispose();
        }
    }
}
