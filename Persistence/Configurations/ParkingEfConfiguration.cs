using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class ParkingEfConfiguration : IEntityTypeConfiguration<Parking>
    {
        public void Configure(EntityTypeBuilder<Parking> builder)
        {
            builder.ToTable("Parking").HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd();

            builder.OwnsOne(p => p.Address, p =>
            {
                p.Property(pp => pp.City).HasColumnName("City");
                p.Property(pp => pp.Country).HasColumnName("Country");
                p.Property(pp => pp.Street).HasColumnName("Street");
            });

            builder.HasMany(p => p.ParkingLevels).WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
