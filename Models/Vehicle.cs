using System.ComponentModel.DataAnnotations;

namespace VehicleManager.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Make { get; set; } = string.Empty;
        [Required]
        public string ModelName { get; set; } = string.Empty;
        public int KilometersDriven { get; set; }
        public int ModelYear { get; set; }
        public string? VIN { get; set; } = string.Empty;
        public string? Image { get; set; }
        [Required]
        public string LicensePlate { get; set; } = string.Empty;
        public string? Color { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;




        public ICollection<Maintenance>? MaintenanceList { get; set; } = new List<Maintenance>();
    }
}
