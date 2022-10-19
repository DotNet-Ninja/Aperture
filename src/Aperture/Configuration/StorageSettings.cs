using DotNetNinja.AutoBoundConfiguration;

namespace Aperture.Configuration;

[AutoBind("Storage")]
public class StorageSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public int CacheControlTimeout { get; set; } = 45;
}