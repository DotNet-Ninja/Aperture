using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aperture.Entities.EntityTypeConfigurations;

public class TagEntityTypeConfiguration: IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(entity => entity.Name).IsClustered().HasName("PK_Tags");
        builder.Property(entity=>entity.Name).IsRequired().HasMaxLength(64);

        builder.HasMany(entity => entity.Photos).WithMany(entity => entity.Tags);
    }
}