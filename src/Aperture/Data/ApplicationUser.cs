namespace Aperture.Data;

public class ApplicationUser
{
    public int Id { get; set; } = 0;
    public string DisplayName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string AvatarId { get; set; } = string.Empty;

    public List<Role> Roles { get; set; } = new();
}