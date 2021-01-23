using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Domain.Entities;

namespace Persistence
{
    public class AppContext : DbContext
    {
        public DbSet<Parking> Parkings { get; set; }

        public AppContext(DbContextOptions<AppContext> contextOptions) : base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(AppContext)));
        }
    }
}
