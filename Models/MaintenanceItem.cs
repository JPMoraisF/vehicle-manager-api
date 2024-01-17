using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;

namespace VehicleManager.Models
{
    public class MaintenanceItem
    {
        private double _totalAmount;

        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required] 
        public double UnitCost{ get; set; }
        [Required]
        public int Quantity { get; set; }

        [JsonIgnore] // Prevents circular reference
        public int MaintenanceId { get; set; }
        [JsonIgnore]
        public Maintenance Maintenance { get; set; }

        public double TotalAmount 
        {
            get { return _totalAmount; }
            set { _totalAmount = value; }
        }

        public void CalculateTotalAmount()
        {
            TotalAmount = UnitCost * Quantity;
        }

    }
}
