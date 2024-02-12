using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Maintenance>>> CreateMaintenance([FromBody] MaintenanceCreateDto maintenanceCreateDto)
        {
            var serviceResponse = new ServiceResponse<Maintenance>();
            try
            {
                var createdMaintenance = await _maintenanceService.AddMaintenanceAsync(maintenanceCreateDto);
                serviceResponse.Data = createdMaintenance;
                return Ok(serviceResponse);
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorList.Add(ex.Message);    
                return StatusCode(StatusCodes.Status500InternalServerError, serviceResponse);
            }
        }

        [Authorize]
        [HttpGet("{maintenanceId}")]
        public async Task<ActionResult<ServiceResponse<Maintenance>>> GetMaintenanceByMaintenanceId(int maintenanceId)
        {
            var serviceResponse = new ServiceResponse<Maintenance>();
            try
            {
                var services = await _maintenanceService.GetMaintenanceAsync(maintenanceId);
                serviceResponse.Data = services;
                return Ok(serviceResponse);
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorList.Add(ex.Message);  
                return StatusCode(StatusCodes.Status500InternalServerError, serviceResponse);
            }
        }

        [Authorize]
        [HttpDelete("{maintenanceId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> Delete(int maintenanceId)
        {
            var serviceResponse = new ServiceResponse<Maintenance>();
            try
            {
                await _maintenanceService.DeleteMaintenanceAsync(maintenanceId);
                return Ok(serviceResponse);
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorList.Add(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, serviceResponse);
            }
        }

        [Authorize]
        [HttpPut("{maintenanceId}", Name = "UpdateMaintenance")]
        public async Task<ActionResult<ServiceResponse<Maintenance>>> UpdateMaintenance(int maintenanceId, [FromBody] MaintenanceUpdateDto maintenance)
        {
            var serviceResponse = new ServiceResponse<Maintenance>();
            try
            {
                var updatedMaintenance = await _maintenanceService.UpdateMaintenanceAsync(maintenanceId, maintenance);
                serviceResponse.Data = updatedMaintenance;
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
