using VehicleManager.Models;

namespace VehicleManager.Repository.MaintenanceRepository
{
    public interface IMaintenanceRepository
    {
        Task<Maintenance>? GetMaintenanceByIdAsync(int id);
        Task<List<Maintenance>> GetVehicleMaintenance(int vehicleId);
        Task<Maintenance> AddMaintenanceAsync(Maintenance maintenance);
        Task<List<MaintenanceItem>> GetMaintenanceItemsByMaintenanceId(int maintenanceId);
        Task DeleteMaintenanceAsync(int id);

        Task<Maintenance> UpdateMaintenanceAsync(Maintenance maintenance);
    }
}
