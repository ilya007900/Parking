using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parking_Domain;
using Parking_Domain.Entities;

namespace Persistence.Configurations
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
