using Aperture.Data;

namespace Aperture.Services;

public interface INavigationService
{
    Task<List<MenuItem>> GetMenuItemsAsync();
    Task SeedDataAsync();
}