using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehicleManager.Models;

namespace VehicleManager.Data
{
    public class VehicleManagerContext : IdentityDbContext<User>
    {
        public VehicleManagerContext (DbContextOptions<VehicleManagerContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicle { get; set; }

        public DbSet<Maintenance> Maintenances { get; set; }

        public DbSet<MaintenanceItem> MaintenanceItems { get; set;}

        public DbSet<User> Users { get; set; }

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

            modelBuilder.Entity<Vehicle>()
                .HasOne(ve => ve.User)
                .WithMany(u => u.Vehicles)
                .HasForeignKey(ve => ve.UserId);

            modelBuilder.Entity<User>()
                .HasMany(v => v.Vehicles)
                .WithOne(u => u.User)
                .HasForeignKey(v => v.UserId);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(p => new { p.LoginProvider, p.ProviderKey });
        }
    }
}
