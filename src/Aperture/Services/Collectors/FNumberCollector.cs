using Aperture.Constants;
using Aperture.Entities;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

namespace Aperture.Services.Collectors;

public class FNumberCollector: CollectorBase
{
    public override void Collect(IReadOnlyCollection<IExifValue> values, List<Property> metadata)
    {
        var value = ReadValue(values, ExifTag.FNumber);
        if (value != null)
        {
            metadata.Add(new Property
            {
                Name = "Aperture",
                Value = (!value.StartsWith("f", StringComparison.CurrentCultureIgnoreCase)) ? $"f/{value}" : value,
                Tag = MetadataTag.FNumber
            });
        }
    }
}