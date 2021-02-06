using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingService.Domain.Entities;

namespace ParkingService.Persistence.Configurations
{
    internal class ParkingEfConfiguration : IEntityTypeConfiguration<Parking>
    {
        public void Configure(EntityTypeBuilder<Parking> builder)
        {
            builder.ToTable("Parking", ParkingDbContext.DefaultSchema).HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd();

            builder.Ignore(x => x.ParkingSpaces);
            builder.Ignore(x => x.State);

            builder.OwnsOne(p => p.Address, p =>
            {
                p.Property(pp => pp.City).HasColumnName("City");
                p.Property(pp => pp.Country).HasColumnName("Country");
                p.Property(pp => pp.Street).HasColumnName("Street");
            });

            builder.HasMany(p => p.Floors).WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
