using System.ComponentModel.DataAnnotations;

namespace VehicleManager.Models.DTOs
{
    public class VehicleCreateDto
    {
        [Required]
        public string Make { get; set; } = string.Empty;
        [Required]
        public string ModelName { get; set; } = string.Empty;
        public int KilometersDriven { get; set; }
        public int ModelYear { get; set; }
        public string Image { get; set; } = string.Empty;
        public string? VIN { get; set; } = string.Empty;
        [Required]
        public string LicensePlate { get; set; } = string.Empty;
        public string? Color { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;
    }
}
