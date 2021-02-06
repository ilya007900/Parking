using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ParkingService.Domain.Entities;

namespace ParkingService.Persistence
{
    public class ParkingDbContext : DbContext
    {
        public const string DefaultSchema = "dbo";

        public DbSet<Parking> Parkings { get; set; }

        public ParkingDbContext(DbContextOptions<ParkingDbContext> contextOptions) : base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ParkingDbContext)));
        }
    }
}
