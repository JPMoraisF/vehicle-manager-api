using VehicleManager.Models;
using VehicleManager.Models.DTOs;
using VehicleManager.Repository.MaintenanceRepository;

namespace VehicleManager.Services.MaintenanceService
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly IMaintenanceRepository _maintenanceRepository;

        public MaintenanceService(IMaintenanceRepository maintenanceRepository)
        {
            _maintenanceRepository = maintenanceRepository;
        }
        public async Task<Maintenance> AddMaintenanceAsync(MaintenanceCreateDto maintenanceCreateDto)
        {
            var maintenance = new Maintenance
            {
                VehicleId = maintenanceCreateDto.VehicleId,
                MaintenanceDate = maintenanceCreateDto.MaintenanceDate,
                KilometersDriven = maintenanceCreateDto.KilometersDriven,
                Notes = maintenanceCreateDto.Notes,
                IsDealershipService = maintenanceCreateDto.IsDealershipService,
                MaintenanceItems = new List<MaintenanceItem>(),
            };

            maintenanceCreateDto.MaintenanceItems.ForEach(maintenanceItem =>
            {
                var newItem = new MaintenanceItem
                {
                    Description = maintenanceItem.Description,
                    UnitCost = maintenanceItem.UnitCost,
                    Quantity = maintenanceItem.Quantity
                };

                maintenance.MaintenanceItems.Add(newItem);
            });

            maintenance.CalculateTotalCost();
            return await _maintenanceRepository.AddMaintenanceAsync(maintenance);
        }

        public Task DeleteMaintenanceAsync(Maintenance vehicle)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteMaintenanceAsync(int id)
        {
            var existingMaintenance = await _maintenanceRepository.GetMaintenanceByIdAsync(id) ?? throw new Exception("Maintenance not found");
            //  We must delete the maintenance items as well
            await _maintenanceRepository.DeleteMaintenanceAsync(id);
        }

        public Task<Maintenance> GetMaintenanceAsync(Maintenance vehicle)
        {
            throw new NotImplementedException();
        }

        public async Task<Maintenance>? GetMaintenanceAsync(int id)
        {
            return await _maintenanceRepository.GetMaintenanceByIdAsync(id);
        }

        public async Task<List<MaintenanceItem>> GetMaintenanceItemsByMaintenanceId(int maintenanceId)
        {
            return await _maintenanceRepository.GetMaintenanceItemsByMaintenanceId(maintenanceId);
        }

        public async Task<List<Maintenance>> GetVehicleMaintenancesAsync(int vehicleId)
        {
            return await _maintenanceRepository.GetVehicleMaintenance(vehicleId);
        }

        public async Task<Maintenance> UpdateMaintenanceAsync(int maintenanceId, MaintenanceUpdateDto maintenanceUpdateDto)
        {
            var existingMaintenance = await _maintenanceRepository.GetMaintenanceByIdAsync(maintenanceId) ?? throw new Exception("Maintenance not found");
            existingMaintenance.MaintenanceDate = maintenanceUpdateDto.MaintenanceDate;
            existingMaintenance.KilometersDriven = maintenanceUpdateDto.KilometersDriven;
            existingMaintenance.Notes = maintenanceUpdateDto.Notes;
            existingMaintenance.IsDealershipService = maintenanceUpdateDto.IsDealershipService;
            return await _maintenanceRepository.UpdateMaintenanceAsync(existingMaintenance);
        }
    }
}
