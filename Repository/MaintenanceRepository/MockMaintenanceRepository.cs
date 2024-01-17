using VehicleManager.Models;

namespace VehicleManager.Repository.MaintenanceRepository
{
    public class MockMaintenanceRepository : IMaintenanceRepository
    {
        public Task<Maintenance> AddMaintenanceAsync(Maintenance maintenance)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMaintenanceAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Maintenance>? GetMaintenanceByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<MaintenanceItem>> GetMaintenanceItemsByMaintenanceId(int maintenanceId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Maintenance>> GetVehicleMaintenance(int vehicleId)
        {
            throw new NotImplementedException();
        }

        public Task<Maintenance> UpdateMaintenanceAsync(Maintenance maintenance)
        {
            throw new NotImplementedException();
        }
    }
}
