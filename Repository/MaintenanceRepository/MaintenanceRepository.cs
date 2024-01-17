using Microsoft.EntityFrameworkCore;
using VehicleManager.Data;
using VehicleManager.Models;

namespace VehicleManager.Repository.MaintenanceRepository
{
    public class MaintenanceRepository : IMaintenanceRepository
    {
        private readonly VehicleManagerContext _context;

        public MaintenanceRepository(VehicleManagerContext context)
        {
            _context = context;
        }

        public async Task<Maintenance> AddMaintenanceAsync(Maintenance maintenance)
        {
            _context.Maintenances.Add(maintenance);
            await _context.SaveChangesAsync();
            return maintenance;
        }

        public async Task DeleteMaintenanceAsync(int id)
        {
            var maintenance = await _context.Maintenances.FindAsync(id);
            _context.Maintenances.Remove(maintenance);
            await _context.SaveChangesAsync();
        }

        public Task<Maintenance>? GetMaintenanceByIdAsync(int id)
        {
            return _context.Maintenances
                .Include(x => x.MaintenanceItems)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<MaintenanceItem>> GetMaintenanceItemsByMaintenanceId(int maintenanceId)
        {
            var maintenanceItem = _context.MaintenanceItems
                .Where(x => x.MaintenanceId == maintenanceId);
            return maintenanceItem.ToListAsync();
        }

        public Task<List<Maintenance>> GetVehicleMaintenance(int vehicleId)
        {
            var maintenances = _context.Maintenances
                .Where(x => x.VehicleId == vehicleId)
                .Include(x => x.MaintenanceItems)
                .ToList();
            return Task.FromResult(maintenances);
        }

        public async Task<Maintenance> UpdateMaintenanceAsync(Maintenance maintenance)
        {
            _context.Entry(maintenance).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return maintenance;
        }
    }
}
