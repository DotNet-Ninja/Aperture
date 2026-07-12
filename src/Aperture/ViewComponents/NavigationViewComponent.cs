using Aperture.Constants;
using Aperture.Models;
using Aperture.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aperture.ViewComponents;

public class NavigationViewComponent: ViewComponent
{
    private readonly INavigationService _service;

    public NavigationViewComponent(INavigationService service)
    {
        _service = service;
    }

    public async Task<IViewComponentResult> InvokeAsync(MenuLocation location)
    {
        var items = await _service.GetMenuItemsAsync(location);
        var model = items.Select(x => new NavigationItemModel(x)).ToList();
        return View(model);
    }
}