using Aperture.Data;

namespace Aperture.Models;

public class AuthenticationResult
{
    public AuthenticationResult(ApplicationUser user)
    {
        Id = user.Id;
        DisplayName = user.DisplayName;
        Email = user.Email;
        AvatarId = user.AvatarId;
        Roles = user.Roles.Select(r => r.Name).ToList();
        IsAuthenticated = true;
    }

    public AuthenticationResult(){}

    public bool IsAuthenticated { get; } = false;

    public int Id { get; } = 0;
    public string DisplayName { get; } = string.Empty;
    public string Email { get; } = string.Empty;
    public string AvatarId { get; } = string.Empty;

    public List<string> Roles { get; } = new();
}