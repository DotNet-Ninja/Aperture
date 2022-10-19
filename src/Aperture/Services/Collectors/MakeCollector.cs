using Aperture.Constants;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

namespace Aperture.Services.Collectors;

public class MakeCollector: DefaultCollectorBase
{
    protected override MetadataTag OutputTag => MetadataTag.Make;
    protected override ExifTag ExifTag => ExifTag.Make;
    protected override string Name => "Make";
}