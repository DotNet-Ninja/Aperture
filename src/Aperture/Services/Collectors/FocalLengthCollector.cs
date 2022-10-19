using Aperture.Constants;
using Aperture.Entities;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

namespace Aperture.Services.Collectors;

public class FocalLengthCollector: CollectorBase
{
    public override void Collect(IReadOnlyCollection<IExifValue> values, List<Property> metadata)
    {
        var value = ReadValue(values, ExifTag.FocalLength);
        if (value != null)
        {
            metadata.Add(new Property
            {
                Name = "Focal Length",
                Value = (value.EndsWith("mm", StringComparison.CurrentCultureIgnoreCase)) ? value : $"{value}mm",
                Tag = MetadataTag.FocalLength
            });
        }
    }
}