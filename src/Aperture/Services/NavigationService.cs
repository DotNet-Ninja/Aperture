using Aperture.Data;

namespace Aperture.Services;

public class NavigationService : INavigationService
{
    private readonly IRepository _repository;

    public NavigationService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<MenuItem>> GetMenuItemsAsync()
    {
        return await _repository.GetMenuItemsAsync();
    }

    public async Task SeedDataAsync()
    {
        await _repository.SeedDataAsync();
    }
}