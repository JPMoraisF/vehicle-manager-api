namespace VehicleManager.Models.DTOs
{
    public class VehicleUpdateDto
    {
        public string? Make { get; set; }
        public string? ModelName { get; set; }
        public int? ModelYear { get; set; }
        public string Image { get; set; }
        public string? VIN { get; set; }
        public int KilometersDriven { get; set; }
        public string? LicensePlate { get; set; }
        public string? Color { get; set; }
        public string? Notes { get; set; }
    }
}
