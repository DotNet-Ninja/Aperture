using Aperture.Configuration;
using Aperture.Services;
using Microsoft.EntityFrameworkCore;

namespace Aperture.Entities.Migrations;

public class SqlDbMigrator<TContext> : IDbMigrator<TContext> where TContext : DbContext, ISeedable
{
    private readonly TContext _db;
    private readonly ITimeProvider _time;
    private readonly DatabaseConfiguration _configuration;

    public SqlDbMigrator(TContext db, DbSettings settings, ITimeProvider time)
    {
        _db = db;
        _time = time;
        var contextName = typeof(TContext).Name;
        _configuration = settings.Contexts[contextName];
    }

    public IDbMigrator<TContext> Migrate()
    {
        if (_configuration.EnableAutomaticMigrations)
        {
            _db.Database.Migrate();
        }

        return this;
    }

    public IDbMigrator<TContext> SeedData()
    {
        if (_configuration.EnableDataSeeding)
        {
            _db.SeedData(_time.RequestTime);
        }

        return this;
    }
}