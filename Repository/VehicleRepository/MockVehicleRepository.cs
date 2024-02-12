using VehicleManager.Models;
using VehicleManager.Models.DTOs;

namespace VehicleManager.Repository.VehicleRepository
{
    public class MockVehicleRepository : IVehicleRepository
    {
        public MockVehicleRepository() { }

        public Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
        {
            return Task.FromResult(new Vehicle()
            {
                Id = 3,
                Make = "Hyundai",
                ModelName = "Creta",
                ModelYear = 2021,
                Color = "Silver",
            });
        }

        public Task DeleteVehicleAsync(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public Task DeleteVehicleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Vehicle>? FindByLicensePlateOrVINAsync(string? licensePlate, string? vin, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Vehicle> GetVehicleAsync(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public Task<Vehicle>? GetVehicleAsync(int id)
        {
            if (id == 1)
            {
                return Task.FromResult(new Vehicle()
                {
                    Id = 1,
                    Make = "Ford",
                    ModelName = "Mustang",
                    ModelYear = 2019,
                    Color = "Black",
                });
            }
            else return Task.FromResult(new Vehicle());
            
        }

        public Task<List<Vehicle>> GetVehiclesAsync()
        {
            return Task.FromResult(new List<Vehicle>()
            {
                new Vehicle()
                {
                    Id = 1,
                    Make = "Ford",
                    ModelName = "Mustang",
                    ModelYear = 2019,
                    Color = "Black",
                },
                new Vehicle()
                {
                    Id = 2,
                    Make = "Chevrolet",
                    ModelName = "Silverado",
                    ModelYear = 2020,
                    Color = "White",
                }
            });
        }

        public Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle)
        {
            return Task.FromResult(new Vehicle()
            {
                Id = 2,
                Make = "Chevrolet",
                ModelName = "Silverado",
                ModelYear = 2020,
                Color = "White",
            });
        }

        public Task<Vehicle> UpdateVehicleAsync(int vehicleId, VehicleCreateDto vehicle)
        {
            throw new NotImplementedException();
        }

        public Task<Vehicle> UpdateVehicleAsync(int vehicleId, VehicleUpdateDto vehicle)
        {
            throw new NotImplementedException();
        }
    }
}
