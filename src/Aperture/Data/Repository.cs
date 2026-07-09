using Microsoft.EntityFrameworkCore;

namespace Aperture.Data;

public class Repository : IRepository
{
    private readonly ApertureDb _db;

    public Repository(ApertureDb db)
    {
        _db = db;
    }

    public Task<List<MenuItem>> GetMenuItemsAsync()
    {
        return _db.MenuItems.Where(m => m.ParentId == null)
                    .OrderBy(m => m.SortIndex)
                    .Include(m => m.Children)
                    .OrderBy(m=>m.SortIndex)
                    .ToListAsync();
    }

    public async Task SeedDataAsync()
    {
        await _db.SeedDataAsync();
    }


    public async Task<List<MigrationStatus>> GetMigrationStatusAsync()
    {
        var result = (await _db.Database.GetAppliedMigrationsAsync())
            .Select(x => new MigrationStatus { IsApplied = true, Name = x }).ToList();
        var pending = await _db.Database.GetPendingMigrationsAsync();
        result.AddRange(pending.Select(x => new MigrationStatus { IsApplied = false, Name = x }));
        return result;
    }

    public Task MigrateDatabaseAsync()
    {
        return _db.Database.MigrateAsync();
    }
}