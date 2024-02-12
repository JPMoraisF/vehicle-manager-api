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
            var vehicleExists = await _vehicleRepository.FindByLicensePlateOrVINAsync(vehicleCreateDto.LicensePlate, vehicleCreateDto.VIN, userId);
            if(vehicleExists != null)
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

        public async Task DeleteVehicleAsync(int id, string userId)
        {
            var vehicleExists = await this.GetVehicleAsync(id, userId) ?? throw new Exception($"Vehicle with id {id} does not exist. ");
            await _vehicleRepository.DeleteVehicleAsync(vehicleExists);
        }

        public async Task DeleteVehicleAsync(Vehicle vehicle)
        {
            await _vehicleRepository.DeleteVehicleAsync(vehicle);
        }

        public Task<Vehicle> GetVehicleAsync(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public async Task<Vehicle>? GetVehicleAsync(int id, string userId)
        {
            var repoVehicle = await _vehicleRepository.GetVehicleAsync(id);
            if (repoVehicle.UserId == userId)
            {
                return repoVehicle;
            }
            else return null;
        }

        public async Task<List<Vehicle>> GetVehiclesAsync(string userId)
        {
            var vehicles = await _vehicleRepository.GetVehiclesAsync();
            return vehicles.Where(v => v.UserId == userId).ToList();
        }

        public async Task<Vehicle>? UpdateVehicleAsync(int vehicleId, string userId, VehicleUpdateDto updateDto)
        {
            var existingVehicle = await GetVehicleAsync(vehicleId, userId);
            
            if (existingVehicle == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(updateDto.Make))
            {
                existingVehicle.Make = updateDto.Make;
            }

            if (!string.IsNullOrEmpty(updateDto.ModelName))
            {
                existingVehicle.ModelName = updateDto.ModelName;
            }

            if (updateDto.ModelYear.HasValue)
            {
                existingVehicle.ModelYear = updateDto.ModelYear.Value;
            }

            if(!string.IsNullOrEmpty(updateDto.Image))
            {
                existingVehicle.Image = updateDto.Image;
            }

            if (!string.IsNullOrEmpty(updateDto.VIN))
            {
                existingVehicle.VIN = updateDto.VIN;
            }

            if (!string.IsNullOrEmpty(updateDto.LicensePlate))
            {
                existingVehicle.LicensePlate = updateDto.LicensePlate;
            }

            if (!string.IsNullOrEmpty(updateDto.Color))
            {
                existingVehicle.Color = updateDto.Color;
            }

            if (!string.IsNullOrEmpty(updateDto.Notes))
            {
                existingVehicle.Notes = updateDto.Notes;
            }

            if(updateDto.KilometersDriven > 0)
            {
                existingVehicle.KilometersDriven = updateDto.KilometersDriven;
            }

            existingVehicle.DateUpdated = DateTime.UtcNow;
            return await _vehicleRepository.UpdateVehicleAsync(existingVehicle);
        }
    }
}
