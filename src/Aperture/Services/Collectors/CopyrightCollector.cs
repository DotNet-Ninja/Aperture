using Aperture.Constants;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

namespace Aperture.Services.Collectors;

public class CopyrightCollector: DefaultCollectorBase
{
    protected override MetadataTag OutputTag => MetadataTag.Copyright;
    protected override ExifTag ExifTag => ExifTag.Copyright;
    protected override string Name => "Copyright";
}