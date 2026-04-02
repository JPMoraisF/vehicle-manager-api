using VehicleManager.Models;
using VehicleManager.Models.DTOs;
using VehicleManager.Repository.VehicleRepository;

namespace VehicleManager.Services.VehicleService
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }
        public async Task<Vehicle> AddVehicleAsync(VehicleCreateDto vehicleCreateDto, string userId)
        {
            var vehicleExists = await _vehicleRepository.GetVehicleDetailsAsync(vehicleCreateDto.LicensePlate);
            if(vehicleExists != null && vehicleExists.UserId == userId)
            {
                throw new Exception($"Vehicle already exists. ");
            }
            var vehicle = new Vehicle
            {
                Make = vehicleCreateDto.Make,
                ModelName = vehicleCreateDto.ModelName,
                ModelYear = vehicleCreateDto.ModelYear,
                VIN = vehicleCreateDto.VIN,
                LicensePlate = vehicleCreateDto.LicensePlate,
                Color = vehicleCreateDto.Color,
                Notes = vehicleCreateDto.Notes,
                DateAdded = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow,
                KilometersDriven = vehicleCreateDto.KilometersDriven,
                UserId = userId,
            };
            
            return await _vehicleRepository.AddVehicleAsync(vehicle);
        }

        public async Task<bool> DeleteVehicleAsync(string licensePlate, string userId)
        {
            var vehicleExists = await this.GetVehicleDetailsAsync(licensePlate);
            if (vehicleExists == null || vehicleExists.UserId != userId)
            {
                return false;
            }
            await _vehicleRepository.DeleteVehicleAsync(vehicleExists);
            return true;
        }
 
        public async Task<Vehicle?> GetVehicleDetailsAsync(string licensePlate)
        {
            return await _vehicleRepository.GetVehicleDetailsAsync(licensePlate);
        }

        public async Task<List<Vehicle>> GetVehiclesAsync(string userId)
        {
            var vehicles = await _vehicleRepository.GetVehiclesAsync();
            return vehicles.Where(v => v.UserId == userId).ToList();
        }

        public async Task<Vehicle?> UpdateVehicleAsync(string licensePlate, string userId, VehicleUpdateDto updateDto)
        {
            var existingVehicle = await GetVehicleDetailsAsync(licensePlate);
            
            if (existingVehicle == null || existingVehicle.UserId != userId)
            {
                return null;
            }

            existingVehicle.Make = string.IsNullOrWhiteSpace(updateDto.Make) ? existingVehicle.Make : updateDto.Make;
            existingVehicle.ModelName = string.IsNullOrWhiteSpace(updateDto.ModelName) ? existingVehicle.ModelName : updateDto.ModelName;
            existingVehicle.ModelYear = updateDto.ModelYear ?? existingVehicle.ModelYear;
            existingVehicle.Image = string.IsNullOrWhiteSpace(updateDto.Image) ? existingVehicle.Image : updateDto.Image;
            existingVehicle.VIN = string.IsNullOrWhiteSpace(updateDto.VIN) ? existingVehicle.VIN : updateDto.VIN;
            existingVehicle.LicensePlate = string.IsNullOrWhiteSpace(updateDto.LicensePlate) ? existingVehicle.LicensePlate : updateDto.LicensePlate;
            existingVehicle.Color = string.IsNullOrWhiteSpace(updateDto.Color) ? existingVehicle.Color : updateDto.Color;
            existingVehicle.Notes = string.IsNullOrWhiteSpace(updateDto.Notes) ? existingVehicle.Notes : updateDto.Notes;
    
            if (updateDto.KilometersDriven.HasValue && updateDto.KilometersDriven.Value >= 0)
            {
                existingVehicle.KilometersDriven = updateDto.KilometersDriven.Value;
            }

            existingVehicle.DateUpdated = DateTime.UtcNow;
            return await _vehicleRepository.UpdateVehicleAsync(existingVehicle);
        }
    }
}
