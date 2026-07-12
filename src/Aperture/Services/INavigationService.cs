using Aperture.Constants;
using Aperture.Data;

namespace Aperture.Services;

public interface INavigationService
{
    Task<List<MenuItem>> GetMenuItemsAsync(MenuLocation location);
    Task SeedDataAsync();
}