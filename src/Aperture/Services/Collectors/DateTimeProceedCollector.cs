using Aperture.Constants;
using Aperture.Entities;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

namespace Aperture.Services.Collectors;

public class DateTimeProceedCollector : CollectorBase
{
    public override void Collect(IReadOnlyCollection<IExifValue> values, List<Property> metadata)
    {
        var data = ReadValue(values, ExifTag.DateTime);
        var offsetValue = ReadValue(values, ExifTag.DateTime);
        if (data != null && offsetValue != null)
        {
            var delimiters = new char[] { ':', ' ' };
            var segments = data.Split(delimiters).Select(int.Parse).ToArray();
            if (segments.Length == 6 && TimeSpan.TryParse(offsetValue, out var offset))
            {
                var date = new DateTimeOffset(segments[0], segments[1], segments[2], segments[3], segments[4], segments[5], offset);
                metadata.Add(new Property
                {
                    Name = "Date Processed",
                    Tag = MetadataTag.DateTimeCaptured,
                    Value = date.ToString()
                });
            }
        }
    }
}