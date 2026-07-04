using DotNetNinja.AutoBoundConfiguration;

namespace Aperture.Configuration;

[AutoBind("Auth0")]
public class Auth0Settings
{
    public string Domain { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
}