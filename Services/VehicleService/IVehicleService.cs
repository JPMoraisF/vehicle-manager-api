using VehicleManager.Models;
using VehicleManager.Models.DTOs;

namespace VehicleManager.Services.VehicleService
{
    public interface IVehicleService
    {
        Task<Vehicle> GetVehicleAsync(Vehicle vehicle);
        Task<Vehicle>? GetVehicleAsync(int id, string userId);
        Task<List<Vehicle>> GetVehiclesAsync(string userId);
        Task<Vehicle> AddVehicleAsync(VehicleCreateDto vehicleCreateDto, string userId);
        Task<Vehicle> UpdateVehicleAsync(int vehicleId, string userId, VehicleUpdateDto vehicle);
        Task DeleteVehicleAsync(Vehicle vehicle);
        Task DeleteVehicleAsync(int id, string userId);
    }
}
