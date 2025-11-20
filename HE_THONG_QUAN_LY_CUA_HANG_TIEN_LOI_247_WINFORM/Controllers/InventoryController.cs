using System;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.DTO;
using System.Collections.Generic;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers
{
    public class InventoryController : IDisposable
    {
        private readonly InventoryService _inventoryService;

        public InventoryController()
        {
            _inventoryService = new InventoryService();
        }
        public List<InventoryDetailDto> GetAllInventory()
        {
            return _inventoryService.GetAllInventory();
        }
        public List<InventoryDetailDto> SearchInventory(string keyword)
        {
            return _inventoryService.SearchInventory(keyword);
        }

        public dynamic GetProductLocations(string sanPhamDonViId)
        {
            return _inventoryService.GetProductLocations(sanPhamDonViId);
        }

        public (bool success, string message) UpdateInventoryQuantity(string tonKhoId, int newQuantity)
        {
            if (string.IsNullOrEmpty(tonKhoId))
                return (false, "Mã tồn kho không hợp lệ");

            if (newQuantity < 0)
                return (false, "Số lượng không được âm");

            return _inventoryService.UpdateInventoryQuantity(tonKhoId, newQuantity);
        }

        public dynamic GetInventoryStatistics()
        {
            return _inventoryService.GetInventoryStatistics();
        }

        public void Dispose()
        {
            _inventoryService?.Dispose();
        }
    }
}
