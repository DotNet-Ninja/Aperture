using Aperture.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aperture.Entities.EntityTypeConfigurations;

public class PropertyEntityTypeConfiguration: IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.HasKey(entity => entity.Id).IsClustered().HasName("PK_Properties");
        builder.Property(entity => entity.Id).ValueGeneratedOnAdd();
        builder.Property(entity => entity.Tag).IsRequired().HasMaxLength(32).HasConversion<EnumToStringConverter<MetadataTag>>();
        builder.Property(entity=>entity.Name).IsRequired().HasMaxLength(128);
        builder.Property(entity => entity.Value).IsRequired().HasMaxLength(128);
    }
}