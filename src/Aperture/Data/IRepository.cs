namespace Aperture.Data;

public interface IRepository
{
    Task<List<MenuItem>> GetMenuItemsAsync();

    Task<ApplicationUser?> GetUserByEmailAsync(string email);

    Task<ApplicationUser> AddUserAsync(ApplicationUser user);

    Task<int> GetAdminCountAsync();

    IRepository Attach<TEntity>(TEntity entity) where TEntity : class;

    IRepository AttachRole(string roleName);

    Task SeedDataAsync();
    Task<List<MigrationStatus>> GetMigrationStatusAsync();
    Task MigrateDatabaseAsync();
}