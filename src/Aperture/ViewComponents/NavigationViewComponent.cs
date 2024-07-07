using Aperture.Configuration;
using Aperture.Models;
using Aperture.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aperture.ViewComponents;

public class NavigationViewComponent : ViewComponent
{
    private readonly IThemeService _themes;
    private readonly SiteSettings _settings;

    public NavigationViewComponent(IThemeService themes, SiteSettings settings)
    {
        _themes = themes;
        _settings = settings;
    }

    public IViewComponentResult Invoke()
    {
        var model = new NavigationModel
        {
            BrandName = _settings.Name,
            NavbarClasses = _themes.GetActiveTheme().NavbarClasses
        };

        return View(model);
    }
}