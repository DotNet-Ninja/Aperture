namespace Aperture.Configuration;

public class Theme
{
    public string Name { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = false;
    public List<string> Stylesheets { get; set; } = new();
    public string NavbarClasses { get; set; } = string.Empty;
}