using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class ParkingLevelEfConfiguration : IEntityTypeConfiguration<ParkingLevel>
    {
        public void Configure(EntityTypeBuilder<ParkingLevel> builder)
        {
            builder.ToTable("ParkingLevel").HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(p => p.Floor).HasColumnName("Floor");
            builder.HasMany(p => p.ParkingSpaces).WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
