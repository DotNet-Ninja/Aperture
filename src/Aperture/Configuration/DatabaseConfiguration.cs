namespace Aperture.Configuration;

public class DatabaseConfiguration
{
    public string Name { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
    public bool EnableAutomaticMigrations { get; set; } = true;
    public bool EnableDataSeeding { get; set; } = true;
}