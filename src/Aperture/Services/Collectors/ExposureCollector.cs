using Aperture.Constants;
using Aperture.Entities;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

namespace Aperture.Services.Collectors;

public class ExposureCollector: CollectorBase
{
    public override void Collect(IReadOnlyCollection<IExifValue> values, List<Property> metadata)
    {
        var value = ReadValue(values, ExifTag.ExposureTime);
        if (value != null)
        {
            if (value.Contains("/"))
            {
                var parts = value.Split('/');
                if (parts.Length == 2 && int.TryParse(parts[0], out var numerator) 
                                      && int.TryParse(parts[1], out var denominator) && numerator > denominator)
                {
                    var number = (float)numerator / (float)denominator;
                    value = $"{number:n1}";
                }
            }
            metadata.Add(new Property
            {
                Name = "Exposure",
                Value = $"{value} sec.",
                Tag = MetadataTag.ExposureTime
            });
        }
    }
}