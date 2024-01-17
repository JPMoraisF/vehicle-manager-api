namespace VehicleManager.Models.DTOs
{
    public class MaintenanceItemCreateDto
    {
        public string Description { get; set; }
        public double UnitCost { get; set; }
        public int Quantity { get; set; }
    }
}
