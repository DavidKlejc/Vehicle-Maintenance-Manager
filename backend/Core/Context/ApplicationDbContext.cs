using backend.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend.Core.Context
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        // Define relations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehicle>()
                // A vehicle has one owner
                .HasOne(vehicle => vehicle.Owner)
                // An owner has many vehicles
                .WithMany(owner => owner.Vehicles)
                // A vehicle has a foreign key of OwnerId
                .HasForeignKey(vehicle => vehicle.OwnerId);

            modelBuilder.Entity<Owner>()
                // A owner has one user
                .HasOne(owner => owner.User)
                // A user has many owners
                .WithMany()
                // A owner has a foreign key of UserId
                .HasForeignKey(owner => owner.UserId);
        }
    }
}
