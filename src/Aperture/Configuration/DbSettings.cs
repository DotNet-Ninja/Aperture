using DotNetNinja.AutoBoundConfiguration;

namespace Aperture.Configuration;

[AutoBind("DbSettings")]
public class DbSettings
{
    public Dictionary<string, DatabaseConfiguration> Contexts { get; set; } = new();
}