using DotNetNinja.AutoBoundConfiguration;

namespace Aperture.Configuration;

[AutoBind("Admin")]
public class AdminSettings
{
    public int UsersPerPage { get; set; } = 12;
}