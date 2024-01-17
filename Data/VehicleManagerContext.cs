using Microsoft.EntityFrameworkCore;
using VehicleManager.Models;

namespace VehicleManager.Data
{
    public class VehicleManagerContext : DbContext
    {
        public VehicleManagerContext (DbContextOptions<VehicleManagerContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicle { get; set; }

        public DbSet<Maintenance> Maintenances { get; set; }

        public DbSet<MaintenanceItem> MaintenanceItems { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Maintenance>()
                .HasMany(m => m.MaintenanceItems)
                .WithOne(i => i.Maintenance)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MaintenanceItem>()
            .HasOne(mi => mi.Maintenance)
            .WithMany(m => m.MaintenanceItems)
            .HasForeignKey(mi => mi.MaintenanceId);
        }
    }
}
