using Aperture.Entities;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

namespace Aperture.Services.Collectors;

public abstract class CollectorBase: IMetadataCollector
{
    public abstract void Collect(IReadOnlyCollection<IExifValue> values, List<Property> metadata);

    protected string? ReadValue(IReadOnlyCollection<IExifValue> values, ExifTag tag)
    {
        return values.FirstOrDefault(x => x.Tag == tag)?.GetValue()?.ToString();
    }
}