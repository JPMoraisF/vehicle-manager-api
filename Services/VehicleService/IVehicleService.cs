using VehicleManager.Models;
using VehicleManager.Models.DTOs;

namespace VehicleManager.Services.VehicleService
{
    public interface IVehicleService
    {
        Task<Vehicle> GetVehicleAsync(Vehicle vehicle);
        Task<Vehicle>? GetVehicleAsync(int id);
        Task<List<Vehicle>> GetVehiclesAsync();
        Task<Vehicle> AddVehicleAsync(VehicleCreateDto vehicleCreateDto);
        Task<Vehicle> UpdateVehicleAsync(int vehicleId, VehicleUpdateDto vehicle);
        Task DeleteVehicleAsync(Vehicle vehicle);
        Task DeleteVehicleAsync(int id);
    }
}
