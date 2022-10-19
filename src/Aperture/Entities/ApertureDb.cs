using Aperture.Configuration;
using Aperture.Entities.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Aperture.Entities;

public class ApertureDb: DbContext, ISeedable
{
    private readonly SeedSettings _settings;

    public ApertureDb(DbContextOptions<ApertureDb> options, SeedSettings settings) : base(options)
    {
        _settings = settings;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StartUp).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Photo> Photos => Set<Photo>();
    public DbSet<Property> Properties => Set<Property>();
    public DbSet<Tag> Tags => Set<Tag>();

    public void SeedData(DateTimeOffset date)
    {
        SampleData.AddSamplePhotos(this, _settings.PhotoCount);
    }
}

// Add Sample Photo Count to DB Settings and inject