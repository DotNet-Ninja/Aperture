namespace Aperture.Data;

public class Role
{
    public string Name { get; set; } = string.Empty;

    public List<ApplicationUser> Users { get; set; } = new();
}