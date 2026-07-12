namespace Aperture.Services;

public interface IAvatarService
{
    string GetAvatarUrl(string avatarId, int? size = null);
    string GetAvatarId(string email);
}