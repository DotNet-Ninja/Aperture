using DotNetNinja.AutoBoundConfiguration;

namespace Aperture.Configuration;

[AutoBind("DbSettings:SeedData")]
public class SeedSettings
{
    public int PhotoCount { get; set; } = 25;
}