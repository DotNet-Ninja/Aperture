using System.Security.Cryptography;
using Aperture.Configuration;

namespace Aperture.Services;

public class GravatarService : IAvatarService
{
    private readonly AvatarSettings _settings;

    public GravatarService(AvatarSettings settings)
    {
        _settings = settings;
    }

    public string GetAvatarUrl(string avatarId, int? size = null)
    {
        var avatarSize = size ?? _settings.Size;
        return $"https://www.gravatar.com/avatar/{avatarId}?s={avatarSize}&d={_settings.Default}&r={_settings.Rating}";
    }

    public string GetAvatarId(string email)
    {
        using var hasher = SHA256.Create();
        var hash = hasher.ComputeHash(System.Text.Encoding.UTF8.GetBytes(email.Trim().ToLower()));
        var hashString = BitConverter.ToString(hash).Replace("-", "").ToLower();
        return hashString;
    }
}