using Aperture.Data;

namespace Aperture.Services;

public interface IMigrationsService
{
    bool ValidatePasskey(string key);
    Task<List<MigrationStatus>> GetMigrationStatusAsync(string key);
    Task ApplyMigrationAsync(string key);
}