using VehicleManager.Models;
using VehicleManager.Models.DTOs;

namespace VehicleManager.Repository.VehicleRepository
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicleAsync(Vehicle vehicle);
        Task<Vehicle>? GetVehicleAsync(int id);
        Task<List<Vehicle>> GetVehiclesAsync();
        Task<Vehicle> AddVehicleAsync(Vehicle vehicle);
        Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle);
        Task DeleteVehicleAsync(Vehicle vehicle);

        Task<Vehicle>? FindByLicensePlateOrVINAsync(string? licensePlate, string? vin);
    }
}
