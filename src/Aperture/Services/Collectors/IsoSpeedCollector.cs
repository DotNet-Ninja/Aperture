using Aperture.Constants;
using Aperture.Entities;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

namespace Aperture.Services.Collectors;

public class IsoSpeedCollector : CollectorBase
{
    public override void Collect(IReadOnlyCollection<IExifValue> values, List<Property> metadata)
    {
        var value = values.SingleOrDefault(v => v.Tag == ExifTag.ISOSpeedRatings);
        var iso = (value?.GetValue() as short[])?.FirstOrDefault();
        string result = iso?.ToString()??string.Empty;
        if (string.IsNullOrWhiteSpace(result))
        {
            result = ReadValue(values, ExifTag.ISOSpeed) ?? string.Empty;
        } 
        if (!string.IsNullOrWhiteSpace(result))
        {
            metadata.Add(new Property
            {
                Name = "ISO Speed",
                Tag = MetadataTag.IsoSpeed,
                Value = result
            });
        }
    }
}