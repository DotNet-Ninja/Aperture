using Aperture.Models;

namespace Aperture.Data;

public interface IRepository
{
    // Navigation
    Task<List<MenuItem>> GetMenuItemsAsync();

    // User management
    Task<ApplicationUser?> GetUserByEmailAsync(string email);

    Task<ApplicationUser?> GetUserByIdAsync(int id);

    Task<ApplicationUser> AddUserAsync(ApplicationUser user);

    Task UpdateUserAsync(ApplicationUser user);

    Task<Page<ApplicationUser>> PageUsersAsync(int page, int size);

    Task<List<Role>> GetRolesAsync();

    Task<int> GetAdminCountAsync();

    // General Data Management/Access
    IRepository Attach<TEntity>(TEntity entity) where TEntity : class;

    IRepository AttachRole(string roleName);

    Task SeedDataAsync();
    Task<List<MigrationStatus>> GetMigrationStatusAsync();
    Task MigrateDatabaseAsync();
}