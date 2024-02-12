using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VehicleManager.Models;
using VehicleManager.Models.DTOs;
using VehicleManager.Services.MaintenanceService;
using VehicleManager.Services.VehicleService;

namespace VehicleManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMaintenanceService _maintenanceService;
        private readonly ILogger<VehicleController> _logger;

        public VehicleController(
            IVehicleService vehicleService,
            IMaintenanceService maintenanceService,
            ILogger<VehicleController> logger)
        {
            _vehicleService = vehicleService;
            _maintenanceService = maintenanceService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet(Name = "GetAllVehicles")]
        public async Task<ActionResult<ServiceResponse<List<Vehicle>>>> Get()
        {
            var serviceResponse = new ServiceResponse<List<Vehicle>>();
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if(string.IsNullOrEmpty(userId))
                {
                    serviceResponse.ErrorList.Add($"User Id {userId} not found! ");
                    return NotFound(serviceResponse);
                }
                var vehicles = await _vehicleService.GetVehiclesAsync(userId);
                serviceResponse.Data = vehicles;
                return Ok(serviceResponse);
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorList.Add(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, serviceResponse);
            }
        }

        [HttpGet("{id}", Name = "GetVehicle")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<Vehicle>>> GetVehicle(int id)
        {
            var serviceResponse = new ServiceResponse<Vehicle>();
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    serviceResponse.ErrorList.Add($"User Id {userId} incorrect or missing");
                    return NotFound(serviceResponse);
                }
                var vehicle = await _vehicleService.GetVehicleAsync(id, userId);
                if (vehicle == null)
                {
                    serviceResponse.ErrorList.Add($"Vehicle {id} not found ");
                    return NotFound(serviceResponse);
                }
                serviceResponse.Data = vehicle;
                return Ok(serviceResponse);
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorList.Add(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, serviceResponse);
            }
        }

        [Authorize]
        [HttpPost(Name = "AddVehicle")]
        public async Task<ActionResult<ServiceResponse<Vehicle>>> Post(VehicleCreateDto vehicleCreateDto)
        {
            var serviceResponse = new ServiceResponse<Vehicle>();
            try
            {
                if (vehicleCreateDto == null)
                {
                    return BadRequest();
                }
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    serviceResponse.ErrorList.Add($"User Id {userId} incorrect or missing");
                    return NotFound(serviceResponse);
                }
                var createdVehicle = await _vehicleService.AddVehicleAsync(vehicleCreateDto, userId);
                serviceResponse.Data = createdVehicle;
                return Ok(serviceResponse);
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorList.Add(ex.Message );
                return StatusCode(StatusCodes.Status500InternalServerError, serviceResponse);
            }
        }

        [Authorize]
        [HttpPut("{id}", Name = "UpdateVehicle")]
        public async Task<ActionResult<ServiceResponse<Vehicle>>> Put(int id, VehicleUpdateDto vehicle)
        {
            var serviceResponse = new ServiceResponse<Vehicle>();
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    serviceResponse.ErrorList.Add($"User Id {userId} incorrect or missing");
                    return NotFound(serviceResponse);
                }
                var updatedVehicle = await _vehicleService.UpdateVehicleAsync(id, userId, vehicle);
                if (updatedVehicle == null)
                {
                    serviceResponse.ErrorList.Add("Vehicle not found");
                    return NotFound(serviceResponse);
                }
                serviceResponse.Data = updatedVehicle;
                return Ok(serviceResponse);
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorList.Add(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, serviceResponse);
            }
        }

        [Authorize]
        [HttpGet("{vehicleId}/maintenances")]
        public async Task<ActionResult<ServiceResponse<List<Maintenance>>>> GetMaintenancesByVehicleId(int vehicleId)
        {
            var serviceResponse = new ServiceResponse<List<Maintenance>>();
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    serviceResponse.ErrorList.Add($"User Id {userId} incorrect or missing");
                    return NotFound(serviceResponse);
                }
                var maintenances = await _maintenanceService.GetVehicleMaintenancesAsync(vehicleId);
                if (maintenances == null || !maintenances.Any())
                {
                    serviceResponse.ErrorList.Add($"No maintenances for vehicle {vehicleId} found.");
                    return NotFound(serviceResponse);
                }
                serviceResponse.Data = maintenances;
                return Ok(serviceResponse);
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorList.Add(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, serviceResponse);
                throw;
            }

        }

        [Authorize]
        [HttpDelete("{vehicleId}")]
        public async Task<ActionResult<ServiceResponse<Vehicle>>> Delete(int vehicleId)
        {
            var serviceResponse = new ServiceResponse<Vehicle>();
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    serviceResponse.ErrorList.Add($"User Id {userId} incorrect or missing");
                    return NotFound(serviceResponse);
                }
                var vehicleToDelete = await _vehicleService.GetVehicleAsync(vehicleId, userId);
                if (vehicleToDelete == null)
                {
                    serviceResponse.ErrorList.Add("Vehicle not found");
                    return NotFound(serviceResponse);
                }
                await _vehicleService.DeleteVehicleAsync(vehicleToDelete);
                return Ok(serviceResponse);
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorList.Add(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, serviceResponse);
            }
        }
    }
}
