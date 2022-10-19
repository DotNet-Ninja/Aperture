using Aperture.Constants;
using DotNetNinja.AutoBoundConfiguration;

namespace Aperture.Configuration;

[AutoBind("Photos")]
public class PhotoSettings
{
    public Dictionary<Orientation, Dictionary<PhotoSize, int>> Sizing { get; set; } = new();

    public int GetWidth(Orientation orientation, PhotoSize size)
    {
        if (Sizing.ContainsKey(orientation) && Sizing[orientation].ContainsKey(size))
        {
            return Sizing[orientation][size];
        }
        return Defaults.PhotoWidth;
    }
}