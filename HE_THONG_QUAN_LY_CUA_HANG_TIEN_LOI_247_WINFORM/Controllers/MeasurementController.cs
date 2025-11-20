using System;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.BLL.Services;
using HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Models;

namespace HE_THONG_QUAN_LY_CUA_HANG_TIEN_LOI_247_WINFORM.Controllers
{
    public class MeasurementController : IDisposable
    {
        private readonly MeasurementService _measurementService;

        public MeasurementController()
        {
            _measurementService = new MeasurementService();
        }

        public dynamic GetAllMeasurements()
        {
            return _measurementService.GetAllMeasurements();
        }


        public dynamic SearchMeasurements(string keyword)
        {
            return _measurementService.SearchMeasurements(keyword);
        }

        public DonViDoLuong GetMeasurementById(string id)
        {
            return _measurementService.GetMeasurementById(id);
        }

        public int GetProductCount(string unitId)
        {
            return _measurementService.GetProductCount(unitId);
        }

        // Gọi hàm sinh mã
        public string GenerateNewMeasurementId()
        {
            return _measurementService.GenerateNewMeasurementId();
        }

        public (bool success, string message, DonViDoLuong unit) AddMeasurement(DonViDoLuong unit)
        {
            return _measurementService.AddMeasurement(unit);
        }

        public (bool success, string message) UpdateMeasurement(DonViDoLuong unit)
        {
            return _measurementService.UpdateMeasurement(unit);
        }

        public (bool success, string message) DeleteMeasurement(string unitId)
        {
            return _measurementService.DeleteMeasurement(unitId);
        }

        public void Dispose()
        {
            _measurementService?.Dispose();
        }
    }
}