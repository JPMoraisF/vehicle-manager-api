using Microsoft.AspNetCore.Mvc;
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

        [HttpGet(Name = "GetAllVehicles")]
        public async Task<ActionResult<List<Vehicle>>> Get()
        {
            try
            {
                var vehicles = await _vehicleService.GetVehiclesAsync();
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Error in GetVehicles");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in GetVehicles");
            }
        }

        [HttpGet("{id}", Name = "GetVehicle")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleAsync(id);
                if (vehicle == null)
                {
                    return NotFound();
                }
                return Ok(vehicle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Error in GetVehicle");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in GetVehicle");
            }
        }

        [HttpPost(Name = "AddVehicle")]
        public async Task<ActionResult<Vehicle>> Post(VehicleCreateDto vehicleCreateDto)
        {
            try
            {
                if (vehicleCreateDto == null)
                {
                        return BadRequest();
                }
                var createdVehicle = await _vehicleService.AddVehicleAsync(vehicleCreateDto);
                return createdVehicle;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AddVehicle");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}", Name = "UpdateVehicle")]
        public async Task<ActionResult<Vehicle>> Put(int id, VehicleUpdateDto vehicle)
        {
            try
            {
                var updatedVehicle = await _vehicleService.UpdateVehicleAsync(id, vehicle);
                if (updatedVehicle == null)
                {
                    return NotFound();
                }
                return Ok(updatedVehicle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in UpdateVehicle: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in UpdateVehicle");
            }
        }

        [HttpGet("{vehicleId}/maintenances")]
        public async Task<ActionResult<IEnumerable<Maintenance>>> GetMaintenancesByVehicleId(int vehicleId)
        {
            var maintenances = await _maintenanceService.GetVehicleMaintenancesAsync(vehicleId);

            if (maintenances == null || !maintenances.Any())
            {
                return NotFound("No maintenances found for the specified vehicle.");
            }

            return Ok(maintenances);
        }

        [HttpDelete("{vehicleId}")]
        public async Task<ActionResult<Vehicle>> Delete(int vehicleId)
        {
            try
            {
                var vehicleToDelete = await _vehicleService.GetVehicleAsync(vehicleId);
                if (vehicleToDelete == null)
                {
                    return NotFound();
                }
                await _vehicleService.DeleteVehicleAsync(vehicleToDelete);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in DeleteVehicle: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
