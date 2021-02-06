using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingService.Domain;
using ParkingService.Domain.Entities;

namespace ParkingService.Persistence.Configurations
{
    internal class ParkingSpaceEfConfiguration : IEntityTypeConfiguration<ParkingSpace>
    {
        public void Configure(EntityTypeBuilder<ParkingSpace> builder)
        {
            builder.ToTable("ParkingSpace").HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(p => p.Number).HasColumnName("Number");
            builder.OwnsOne(p => p.Vehicle, p =>
            {
                p.Property(pp => pp.Weight).HasColumnName("VehicleWeight");
                p.Property(pp => pp.LicensePlate).HasColumnName("VehicleLicensePlate")
                    .HasConversion(c => c.Value, c => LicensePlate.Create(c).Value);
            });
        }
    }
}
