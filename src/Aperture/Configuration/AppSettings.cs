using Aperture.Constants;
using DotNetNinja.AutoBoundConfiguration;

namespace Aperture.Configuration;

[AutoBind("App")]
public class AppSettings
{
    public string SiteName { get; set; } = string.Empty;
    public string SiteLogoImageUrl { get; set; } = string.Empty;
    public string CopyrightOwner { get; set; } = string.Empty;
    public BootstrapIcon BrandIcon { get; set; } = BootstrapIcon.NotSet;
}