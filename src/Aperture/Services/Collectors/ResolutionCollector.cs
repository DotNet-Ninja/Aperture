using Aperture.Constants;
using Aperture.Entities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

namespace Aperture.Services.Collectors;

public class ResolutionCollector: IMetadataCollector
{
    public void Collect(IReadOnlyCollection<IExifValue> values, List<Property> metadata)
    {
        var xResolutionTag = values.SingleOrDefault(v => v.Tag == ExifTag.PixelXDimension);
        var yResolutionTag = values.SingleOrDefault(v => v.Tag == ExifTag.PixelYDimension);
        var x = (xResolutionTag != null) ? (Number)xResolutionTag.GetValue() : 0;
        var y = (yResolutionTag != null) ? (Number)yResolutionTag.GetValue() : 0;
        if (x > 0 && y > 0)
        {
            metadata.Add(new Property
            {
                Name = "Resolution",
                Tag = MetadataTag.Resolution,
                Value = $"{x} x {y} pixels"
            });
        }
    }
}