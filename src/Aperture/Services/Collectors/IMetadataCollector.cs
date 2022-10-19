using Aperture.Entities;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

namespace Aperture.Services.Collectors;

public interface IMetadataCollector
{
    public void Collect(IReadOnlyCollection<IExifValue> values, List<Property> metadata);
}