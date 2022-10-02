using Microsoft.EntityFrameworkCore;

namespace Aperture.Entities.Migrations;

public interface IDbMigrator<TContext> where TContext: DbContext, ISeedable
{
    IDbMigrator<TContext> Migrate();
    IDbMigrator<TContext> SeedData();
}