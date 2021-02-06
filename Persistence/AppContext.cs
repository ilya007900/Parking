using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace ParkingService.Persistence
{
    public class AppContext : DbContext
    {
        public DbSet<Domain.Entities.Parking> Parkings { get; set; }

        public AppContext(DbContextOptions<AppContext> contextOptions) : base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(AppContext)));
        }
    }
}
