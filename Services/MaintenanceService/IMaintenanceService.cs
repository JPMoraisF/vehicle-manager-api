using VehicleManager.Models;
using VehicleManager.Models.DTOs;

namespace VehicleManager.Services.MaintenanceService
{
    public interface IMaintenanceService
    {
        Task<Maintenance> GetMaintenanceAsync(Maintenance vehicle);
        Task<Maintenance>? GetMaintenanceAsync(int id);
        Task<List<Maintenance>> GetVehicleMaintenancesAsync(int vehicleId);
        Task<Maintenance> AddMaintenanceAsync(MaintenanceCreateDto maintenanceCreateDto);
        Task<Maintenance> UpdateMaintenanceAsync(int maintenanceId, MaintenanceUpdateDto maintenanceUpdateDto);
        Task DeleteMaintenanceAsync(Maintenance vehicle);
        Task DeleteMaintenanceAsync(int id);

        Task<List<MaintenanceItem>> GetMaintenanceItemsByMaintenanceId(int maintenanceId);
    }
}
