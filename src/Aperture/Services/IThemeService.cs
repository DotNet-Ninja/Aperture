using Aperture.Configuration;

namespace Aperture.Services;

public interface IThemeService
{
    Theme GetActiveTheme();
    List<string> ListThemes();
}