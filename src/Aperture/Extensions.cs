using System.Security.Claims;
using Aperture.Configuration;

namespace Aperture;

public static class Extensions
{
    public static string AvatarId(this ClaimsPrincipal principal)
    {
        return principal?.FindFirst(Avatars.ClaimKey)?.Value ?? string.Empty;
    }
}