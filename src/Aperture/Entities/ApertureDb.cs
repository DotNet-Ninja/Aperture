using Aperture.Constants;
using Aperture.Entities.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Aperture.Entities;

public class ApertureDb: DbContext, ISeedable
{
    public ApertureDb(DbContextOptions<ApertureDb> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StartUp).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Photo> Photos => Set<Photo>();
    public DbSet<ExifProperty> ExifProperties => Set<ExifProperty>();
    public DbSet<Tag> Tags => Set<Tag>();

    public void SeedData(DateTimeOffset date)
    {
        if (Photos.Any())
        {
            return;
        }

        var tag = new Tag
        {
            Name = "Samples"
        };
        Tags.Add(tag);

        var photos = new List<Photo>
        {
            new Photo
            {
                Name = "First Sample",
                Orientation = Orientation.Landscape,
                Caption = "My First Sample Photograph",
                ContentType = "image/jpeg",
                DateCreated = date,
                ExposureSummary = "Canon EF-M 15-45mm f/3.5-6.3 @ 45mm - 1/60 sec. @ f/8, ISO 100",
                FileName = "IMG_1101.jpg",
                FullUrl = new Uri("/images/samples/IMG_1101.full.jpg", UriKind.Relative),
                LargeUrl = new Uri("/images/samples/IMG_1101.large.jpg", UriKind.Relative),
                SmallUrl = new Uri("/images/samples/IMG_1101.small.jpg", UriKind.Relative),
                ThumbnailUrl = new Uri("/images/samples/IMG_1101.thumb.jpg", UriKind.Relative),
                Tags = new List<Tag>
                {
                    tag
                },
                ExifProperties = new List<ExifProperty>
                {
                    new ExifProperty
                    {
                        Name = "make",
                        Value = "Canon"
                    }
                }
            },            new Photo
            {
                Name = "Second Sample",
                Orientation = Orientation.Landscape,
                Caption = "My Second Sample Photograph",
                ContentType = "image/jpeg",
                DateCreated = date,
                ExposureSummary = "Canon EF-M 15-45mm f/3.5-6.3 @ 45mm - 1/60 sec. @ f/8, ISO 100",
                FileName = "IMG_1102.jpg",
                FullUrl = new Uri("/images/samples/IMG_1102.full.jpg", UriKind.Relative),
                LargeUrl = new Uri("/images/samples/IMG_1102.large.jpg", UriKind.Relative),
                SmallUrl = new Uri("/images/samples/IMG_1102.small.jpg", UriKind.Relative),
                ThumbnailUrl = new Uri("/images/samples/IMG_1102.thumb.jpg", UriKind.Relative),
                Tags = new List<Tag>
                {
                    tag
                },
                ExifProperties = new List<ExifProperty>
                {
                    new ExifProperty
                    {
                        Name = "make",
                        Value = "Canon"
                    }
                }
            }
        };
        Photos.AddRange(photos);

        SaveChanges();
    }
}