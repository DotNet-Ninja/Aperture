using Aperture.Configuration;
using Aperture.Constants;
using Aperture.Entities;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

namespace Aperture.Services.Collectors;

public class LensModelCollector:CollectorBase
{
    private readonly ExifOverrideSettings _overrides;

    public LensModelCollector(ExifOverrideSettings overrides)
    {
        _overrides = overrides;
    }

    public override void Collect(IReadOnlyCollection<IExifValue> values, List<Property> metadata)
    {
        var value = ReadValue(values, ExifTag.LensModel);
        if (value != null)
        {
            var property = new Property
            {
                Name = "Lens Model",
                Value = value,
                Tag = MetadataTag.LensModel
            };
            _overrides.OverrideWithMappings(property);
            metadata.Add(property);
        }
    }
}