using Aperture.Entities;
using Aperture.Services.Collectors;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

namespace Aperture.Services;

public class ExifToMetadataConverter : IExifToMetadataConverter
{
    private readonly IEnumerable<IMetadataCollector> _collectors;

    public ExifToMetadataConverter(IEnumerable<IMetadataCollector> collectors)
    {
        _collectors = collectors;
    }

    public List<Property> Convert(IReadOnlyCollection<IExifValue> exif)
    {
        var properties=  new List<Property>();
        foreach (var collector in _collectors)
        {
            collector.Collect(exif, properties);
        }
        return properties;
    }
}
