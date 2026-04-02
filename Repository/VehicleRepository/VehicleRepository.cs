using Microsoft.EntityFrameworkCore;
using VehicleManager.Data;
using VehicleManager.Models;

namespace VehicleManager.Repository.VehicleRepository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VehicleManagerContext _context;

        public VehicleRepository(VehicleManagerContext context)
        {
            _context = context;
        }

        public async Task<Vehicle> GetVehicleAsync(Vehicle vehicle)
        {
            return await _context.Vehicle.FindAsync(vehicle.Id);
        }

        public async Task<Vehicle?> GetVehicleDetailsAsync(string licensePlate)
        {
            return await _context.Vehicle.Include(x => x.MaintenanceList).FirstOrDefaultAsync((x => x.LicensePlate == licensePlate));
        }

        public async Task<List<Vehicle>> GetVehiclesAsync()
        {
            return await _context.Vehicle.ToListAsync();
        }

        public async Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicle.Add(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }

        public async Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle)
        {
            _context.Entry(vehicle).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return vehicle;
        }

        public async Task DeleteVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicle.Remove(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVehicleAsync(int id)
        {
            var vehicle = await _context.Vehicle.FindAsync(id);
            _context.Vehicle.Remove(vehicle);
            await _context.SaveChangesAsync();
        }
    }
}
