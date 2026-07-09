namespace Aperture.Data;

public interface IRepository
{
    Task<List<MenuItem>> GetMenuItemsAsync();
    Task SeedDataAsync();
    Task<List<MigrationStatus>> GetMigrationStatusAsync();
    Task MigrateDatabaseAsync();
}