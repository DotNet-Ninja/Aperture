using DotNetNinja.AutoBoundConfiguration;

namespace Aperture.Configuration;

[AutoBind("Databases")]
public class DatabaseSettings: Dictionary<string, DbConfiguration>
{
}