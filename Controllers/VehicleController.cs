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
        public async Task<ActionResult<ServiceResponse<List<Vehicle>>>> GetVehicles()
        {
            var serviceResponse = new ServiceResponse<List<Vehicle>>();
            try
            {
                var userId = GetUserIdOrThrow();
                if(string.IsNullOrEmpty(userId))
                {
                    return NotFound(serviceResponse);
                }
                serviceResponse.Data = await _vehicleService.GetVehiclesAsync(userId);;
                return Ok(serviceResponse);
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorList.Add(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, serviceResponse);
            }
        }

        [HttpGet("{licensePlate}", Name = "GetVehicleDetails")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<Vehicle>>> GetVehicleDetails(string licensePlate)
        {
            var serviceResponse = new ServiceResponse<Vehicle>();
            try
            {
                var userId = GetUserIdOrThrow();                
                if (string.IsNullOrEmpty(userId))
                {
                    return NotFound(serviceResponse);
                }
                var vehicle = await _vehicleService.GetVehicleDetailsAsync(licensePlate);
                if (vehicle == null)
                {
                    serviceResponse.ErrorList.Add($"Vehicle {licensePlate} not found ");
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
        public async Task<ActionResult<ServiceResponse<Vehicle>>> AddVehicleAsync(VehicleCreateDto vehicleCreateDto)
        {
            var serviceResponse = new ServiceResponse<Vehicle>();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var userId = GetUserIdOrThrow();
                if (string.IsNullOrEmpty(userId))
                {
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
        [HttpPut("{licensePlate}", Name = "UpdateVehicle")]
        public async Task<ActionResult<ServiceResponse<Vehicle>>> UpdateVehicleAsync(string licensePlate, VehicleUpdateDto vehicleUpdateDto)
        {
            var serviceResponse = new ServiceResponse<Vehicle>();
            try
            {
                var userId = GetUserIdOrThrow();
                if (string.IsNullOrEmpty(userId))
                {
                    return NotFound(serviceResponse);
                }
                var updatedVehicle = await _vehicleService.UpdateVehicleAsync(licensePlate, userId, vehicleUpdateDto);
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
        [HttpGet("{licensePlate}/maintenances")]
        public async Task<ActionResult<ServiceResponse<List<Maintenance>>>> GetMaintenancesByVehicleId(string licensePlate)
        {
            var serviceResponse = new ServiceResponse<List<Maintenance>>();
            try
            {
                var userId = GetUserIdOrThrow();
                if (string.IsNullOrEmpty(userId))
                {
                    return NotFound(serviceResponse);
                }
                var maintenances = await _maintenanceService.GetVehicleMaintenancesAsync(licensePlate);
                if (maintenances == null || !maintenances.Any())
                {
                    serviceResponse.ErrorList.Add($"No maintenances for vehicle {licensePlate} found.");
                    return NotFound(serviceResponse);
                }
                serviceResponse.Data = maintenances;
                return Ok(serviceResponse);
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorList.Add(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, serviceResponse);
            }
        }

        [Authorize]
        [HttpDelete("{licensePlate}")]
        public async Task<ActionResult<ServiceResponse<Vehicle>>> DeleteVehicle(string licensePlate)
        {
            var serviceResponse = new ServiceResponse<Vehicle>();
            try
            {
                var userId = GetUserIdOrThrow();
                if (string.IsNullOrEmpty(userId))
                {
                    return NotFound(serviceResponse);
                }
                var result = await _vehicleService.DeleteVehicleAsync(licensePlate, userId);
                if (result) return Ok(serviceResponse);
                serviceResponse.ErrorList.Add($"Vehicle {licensePlate} not found");
                return NotFound(serviceResponse);
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorList.Add(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, serviceResponse);
            }
        }
        
        private string GetUserIdOrThrow()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User ID is missing.");
            return userId;
        }
    }
}
