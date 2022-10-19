using Aperture.Entities;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

namespace Aperture.Services;

public interface IExifToMetadataConverter
{
    List<Property> Convert(IReadOnlyCollection<IExifValue> exif);
}