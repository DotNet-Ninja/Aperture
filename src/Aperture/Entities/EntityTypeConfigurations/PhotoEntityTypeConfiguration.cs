using Aperture.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aperture.Entities.EntityTypeConfigurations;

public class PhotoEntityTypeConfiguration: IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.HasKey(entity => entity.Id).IsClustered().HasName("PK_Photos");
        builder.Property(entity => entity.Id).ValueGeneratedOnAdd();
        builder.Property(entity => entity.Slug).HasMaxLength(128);
        builder.HasIndex(entity => entity.Slug).IsUnique().HasDatabaseName("UK_Photo_Slug");
        builder.Property(entity => entity.Title).HasMaxLength(128);
        builder.Property(entity => entity.FileName).IsRequired().HasMaxLength(128);
        builder.Property(entity => entity.Caption).HasMaxLength(256);
        builder.Property(entity => entity.ContentType).IsRequired().HasMaxLength(256);
        builder.Property(entity => entity.ExposureSummary).IsRequired().HasMaxLength(256); 
        builder.Property(e => e.Orientation).IsRequired().HasMaxLength(32)
                                            .HasConversion<EnumToStringConverter<Orientation>>();
        builder.Property(e => e.DateCreated).IsRequired();
        builder.Property(e => e.DateUploaded).IsRequired();
        builder.Property(e => e.FullUrl).IsRequired().HasMaxLength(2048).HasConversion<UriToStringConverter>();
        builder.Property(e => e.LargeUrl).IsRequired().HasMaxLength(2048).HasConversion<UriToStringConverter>();
        builder.Property(e => e.SmallUrl).IsRequired().HasMaxLength(2048).HasConversion<UriToStringConverter>();
        builder.Property(e => e.ThumbnailUrl).IsRequired().HasMaxLength(2048).HasConversion<UriToStringConverter>();
        builder.Property(e => e.IconUrl).IsRequired().HasMaxLength(2048).HasConversion<UriToStringConverter>();

        builder.HasMany(entity => entity.Metadata).WithOne().HasForeignKey(p=>p.PhotoId);
        builder.HasMany(entity => entity.Tags).WithMany(entity => entity.Photos);
    }
}