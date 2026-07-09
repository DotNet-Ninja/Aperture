using Aperture.Configuration;
using Aperture.Data;

namespace Aperture.Services;

public class MigrationsService : IMigrationsService
{
    private readonly IRepository _repository;
    private readonly AppSettings _settings;

    public MigrationsService(IRepository repository, AppSettings settings)
    {
        _repository = repository;
        _settings = settings;
    }

    public bool ValidatePasskey(string key)
    {
        return key == _settings.MigrationsPasskey;
    }

    public async Task<List<MigrationStatus>> GetMigrationStatusAsync(string key)
    {
        if (!ValidatePasskey(key))
        {
            throw new UnauthorizedAccessException("Invalid passkey.");
        }

        var status = await _repository.GetMigrationStatusAsync();
        return status;
    }

    public async Task ApplyMigrationAsync(string key)
    {
        if (!ValidatePasskey(key))
        {
            throw new UnauthorizedAccessException("Invalid passkey.");
        }

        await _repository.MigrateDatabaseAsync();
    }
}