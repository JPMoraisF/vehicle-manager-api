namespace VehicleManager.Models.DTOs
{
    public class MaintenanceCreateDto
    {
        public int VehicleId { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public int KilometersDriven { get; set; }
        public string? Notes { get; set; }
        public bool IsDealershipService { get; set; } = false;
        public List<MaintenanceItemCreateDto>? MaintenanceItems { get; set; } = new List<MaintenanceItemCreateDto>();
    }
}
