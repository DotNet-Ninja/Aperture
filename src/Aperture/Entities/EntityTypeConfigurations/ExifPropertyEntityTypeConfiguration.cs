using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aperture.Entities.EntityTypeConfigurations;

public class ExifPropertyEntityTypeConfiguration: IEntityTypeConfiguration<ExifProperty>
{
    public void Configure(EntityTypeBuilder<ExifProperty> builder)
    {
        builder.HasKey(entity => entity.Id).IsClustered().HasName("PK_ExifProperties");
        builder.Property(entity => entity.Id).ValueGeneratedOnAdd();
        builder.Property(entity => entity.Name).IsRequired().HasMaxLength(128);
        builder.Property(entity => entity.Value).IsRequired().HasMaxLength(128);
        builder.Property(entity => entity.DisplayName).HasMaxLength(128);
        builder.Property(entity => entity.DisplayValue).HasMaxLength(128);
    }
}