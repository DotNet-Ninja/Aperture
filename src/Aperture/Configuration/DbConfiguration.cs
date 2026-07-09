using Aperture.Constants;

namespace Aperture.Configuration;

public class DbConfiguration
{
    public string Name { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
    public DbType DbType { get; set; } = DbType.SqlServer;
}