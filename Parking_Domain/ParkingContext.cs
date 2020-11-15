using Microsoft.EntityFrameworkCore;
using Parking_Domain.ParkingEntities;
using Parking_Domain.ParkingLevels;
using Parking_Domain.ParkingSpaces;

namespace Parking_Domain
{
    public class ParkingContext : DbContext
    {
        public DbSet<Parking> ParkingEntities { get; set; }

        public DbSet<ParkingLevel> ParkingLevels { get; set; }

        public DbSet<ParkingSpace> ParkingSpaces { get; set; }

        public ParkingContext(DbContextOptions<ParkingContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parking>(x =>
            {
                x.ToTable("Parking").HasKey(p => p.Id);
                x.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd();
                x.OwnsOne(p => p.Address, p =>
                {
                    p.Property(pp => pp.City).HasColumnName("City");
                    p.Property(pp => pp.Country).HasColumnName("Country");
                    p.Property(pp => pp.Street).HasColumnName("Street");
                });
                x.HasMany(p => p.ParkingLevels).WithOne(p => p.Parking)
                    .OnDelete(DeleteBehavior.Cascade)
                    .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
            });

            modelBuilder.Entity<ParkingLevel>(x =>
            {
                x.ToTable("ParkingLevel").HasKey(p => p.Id);
                x.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd();
                x.Property(p => p.Floor).HasColumnName("Floor");
                x.HasOne(p => p.Parking).WithMany(p => p.ParkingLevels);
                x.HasMany(p => p.ParkingSpaces).WithOne(p => p.ParkingLevel)
                    .OnDelete(DeleteBehavior.Cascade)
                    .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
            });

            modelBuilder.Entity<ParkingSpace>(x =>
            {
                x.ToTable("ParkingSpace").HasKey(p => p.Id);
                x.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd();
                x.Property(p => p.Number).HasColumnName("Number");
                x.OwnsOne(p => p.Vehicle, p =>
                {
                    p.Property(pp => pp.Weight).HasColumnName("VehicleWeight");
                    p.Property(pp => pp.LicensePlate).HasColumnName("VehicleLicensePlate")
                        .HasConversion(c => c.Value, c => LicensePlate.Create(c).Value);
                });
                x.HasOne(p => p.ParkingLevel).WithMany(p => p.ParkingSpaces);
            });
        }
    }
}