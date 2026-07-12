using Aperture.Constants;
using Aperture.Data;

namespace Aperture.Services;

public class NavigationService : INavigationService
{
    private readonly IRepository _repository;

    public NavigationService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<MenuItem>> GetMenuItemsAsync(MenuLocation location)
    {
        if (location == MenuLocation.Admin)
        {
            return AdminMenu.Items;
        }
        return await _repository.GetMenuItemsAsync();
    }

    public async Task SeedDataAsync()
    {
        await _repository.SeedDataAsync();
    }
}