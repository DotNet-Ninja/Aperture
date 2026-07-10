using DotNetNinja.AutoBoundConfiguration;

namespace Aperture.Configuration;

[AutoBind("Avatars")]
public class AvatarSettings
{
    public string Rating { get; set; } = Avatars.Rating;
    public int Size { get; set; } = Avatars.Size;
    public string Default { get; set; } = Avatars.Default;
}