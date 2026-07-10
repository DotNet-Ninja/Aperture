using Aperture.Constants;
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

    public Task<ApplicationUser?> GetUserByEmailAsync(string email)
    {
        return _db.Users.AsNoTracking()
                .Include(u => u.Roles)
                .SingleOrDefaultAsync(u => u.Email == email);
    }

    public async Task<ApplicationUser> AddUserAsync(ApplicationUser user)
    {
        if(user.Id != 0)
        {
            throw new ArgumentException("User ID must be 0 when adding a new user.");
        }
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public Task<int> GetAdminCountAsync()
    {
        return _db.Users.CountAsync(u => u.Roles.Any(r => r.Name == AppRoles.Owner));
    }

    public IRepository Attach<TEntity>(TEntity entity) where TEntity : class
    {
        _db.Attach(entity);
        return this;
    }

    public IRepository AttachRole(string roleName)
    {
        var role = new Role { Name = roleName };
        _db.Attach(role);
        return this;
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