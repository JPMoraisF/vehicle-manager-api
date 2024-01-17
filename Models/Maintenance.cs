using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VehicleManager.Models
{
    public class Maintenance
    {
        public double _totalCost;

        [Key]
        public int Id { get; set; }

        //public string? Name { get; set; }

        [Required]
        public DateTime MaintenanceDate { get; set; } = DateTime.UtcNow;

        [Required]
        public int KilometersDriven { get; set; }

        public string? Notes { get; set; } = string.Empty;

        public bool IsDealershipService { get; set; } = false;
        
        public double TotalCost { get { return _totalCost; } set { _totalCost = value; } }

        public ICollection<MaintenanceItem>? MaintenanceItems { get; set; } = new List<MaintenanceItem>();
        [JsonIgnore]
        public virtual Vehicle Vehicle { get; set; }

        public int VehicleId { get; set; }

        public void CalculateTotalCost()
        {
            MaintenanceItems.ToList().ForEach(item => item.CalculateTotalAmount());
            TotalCost = MaintenanceItems.Sum(item => item.TotalAmount);
        }
    }
}
