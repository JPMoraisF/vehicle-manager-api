namespace VehicleManager.Models.DTOs
{
    public class MaintenanceUpdateDto
    {
        public DateTime MaintenanceDate { get; set; }
        public int KilometersDriven { get; set; }
        public string? Notes { get; set; }
        public bool IsDealershipService { get; set; } = false;
    }
}
