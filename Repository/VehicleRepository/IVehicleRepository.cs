using VehicleManager.Models;
using VehicleManager.Models.DTOs;

namespace VehicleManager.Repository.VehicleRepository
{
    public interface IVehicleRepository
    {
        Task<Vehicle?> GetVehicleDetailsAsync(string licensePlate);
        Task<List<Vehicle>> GetVehiclesAsync();
        Task<Vehicle> AddVehicleAsync(Vehicle vehicle);
        Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle);
        Task DeleteVehicleAsync(Vehicle vehicle);
    }
}
