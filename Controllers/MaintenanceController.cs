using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VehicleManager.Models;
using VehicleManager.Models.DTOs;
using VehicleManager.Services.MaintenanceService;

namespace VehicleManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceService _maintenanceService;
        private readonly ILogger<MaintenanceController> _logger;

        public MaintenanceController(IMaintenanceService maintenanceService, ILogger<MaintenanceController> logger)
        {
            _maintenanceService = maintenanceService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<Maintenance>> CreateMaintenance([FromBody] MaintenanceCreateDto maintenanceCreateDto)
        {
            var createdMaintenance = await _maintenanceService.AddMaintenanceAsync(maintenanceCreateDto);
            return Ok(createdMaintenance);
        }

        [HttpGet("{maintenanceId}")]
        public async Task<ActionResult<Maintenance>> GetMaintenanceByMaintenanceId(int maintenanceId)
        {
            try
            {
                var services = await _maintenanceService.GetMaintenanceAsync(maintenanceId);
                return Ok(services);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Error in GetServices");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in GetServices");
            }
        }

        [HttpDelete("{maintenanceId}")]
        public async Task<ActionResult> Delete(int maintenanceId)
        {
            try
            {
                await _maintenanceService.DeleteMaintenanceAsync(maintenanceId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, $"Error in DeleteMaintenance: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in DeleteMaintenance");
            }
        }

        [HttpPut("{maintenanceId}", Name = "UpdateMaintenance")]
        public async Task<ActionResult<Maintenance>> UpdateMaintenance(int maintenanceId, [FromBody] MaintenanceUpdateDto maintenance)
        {
            var updatedMaintenance = await _maintenanceService.UpdateMaintenanceAsync(maintenanceId, maintenance);
            return Ok(updatedMaintenance);
        }
    }
}
