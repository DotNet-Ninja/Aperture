using Aperture.Constants;
using Aperture.Entities;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

namespace Aperture.Services.Collectors;

public abstract class DefaultCollectorBase: CollectorBase
{
    protected abstract MetadataTag OutputTag { get; }

    protected abstract ExifTag ExifTag { get; }

    protected abstract string Name { get; }

    public override void Collect(IReadOnlyCollection<IExifValue> values, List<Property> metadata)
    {
        var data = ReadValue(values, ExifTag);
        if (data!=null && data.Trim().Length>0)
        {
            metadata.Add(new Property
            {
                Name = Name,
                Tag = OutputTag,
                Value = data
            });
        }
    }
}