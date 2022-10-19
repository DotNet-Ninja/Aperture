using Aperture.Constants;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

namespace Aperture.Services.Collectors;

public class ArtistCollector: DefaultCollectorBase
{
    protected override MetadataTag OutputTag => MetadataTag.Artist;
    protected override ExifTag ExifTag => ExifTag.Artist;
    protected override string Name => "Photographer";
}