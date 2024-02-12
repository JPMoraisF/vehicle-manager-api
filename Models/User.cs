using Microsoft.AspNetCore.Identity;

namespace VehicleManager.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public IEnumerable<Vehicle>? Vehicles { get; set; } = new List<Vehicle>();
    }
}
