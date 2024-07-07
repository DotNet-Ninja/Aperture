using Aperture.Configuration;

namespace Aperture.Services;

public class ThemeService : IThemeService
{
    private readonly ThemeSettings _settings;

    public ThemeService(ThemeSettings settings)
    {
        _settings = settings;
    }

    private Theme? _active = null;

    public Theme GetActiveTheme()
    {
        if(_active is null || _active.Name != _settings.ActiveTheme || !_active.IsEnabled)
        {
            _active = _settings.Themes.SingleOrDefault(t => t.IsEnabled && t.Name == _settings.ActiveTheme) ??
                      _settings.Themes.First(theme => theme.IsEnabled);
        }
        return _active;
    }

    public List<string> ListThemes()
    {
        return _settings.Themes.Where(theme => theme.IsEnabled).Select(theme => theme.Name).ToList();
    }
}