using DotNetNinja.AutoBoundConfiguration;

namespace Aperture.Configuration;

[AutoBind("Site")]
public class SiteSettings
{
    public string Name { get; set; } = "Aperture";
    public CopyrightSettings Copyright { get; set; } = new();
}