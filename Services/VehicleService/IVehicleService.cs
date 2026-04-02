using VehicleManager.Models;
using VehicleManager.Models.DTOs;

namespace VehicleManager.Services.VehicleService
{
    public interface IVehicleService
    {
        Task<Vehicle?> GetVehicleDetailsAsync(string licensePlate);
        Task<List<Vehicle>> GetVehiclesAsync(string userId);
        Task<Vehicle> AddVehicleAsync(VehicleCreateDto vehicleCreateDto, string userId);
        Task<Vehicle?> UpdateVehicleAsync(string licensePlate, string userId, VehicleUpdateDto vehicle);
        Task<bool> DeleteVehicleAsync(string licensePlate, string userId);
    }
}
