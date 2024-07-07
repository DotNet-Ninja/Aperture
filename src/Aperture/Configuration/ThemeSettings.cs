using DotNetNinja.AutoBoundConfiguration;

namespace Aperture.Configuration;

[AutoBind("Themes")]
public class ThemeSettings
{
    public string ActiveTheme { get; set; } = "default";
    public List<Theme> Themes { get; set; } = new();
}