using Aperture.Configuration;
using Aperture.Constants;
using Aperture.Entities;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

namespace Aperture.Services.Collectors;

public class ModelCollector: CollectorBase
{
    private readonly ExifOverrideSettings _overrides;

    public ModelCollector(ExifOverrideSettings overrides)
    {
        _overrides = overrides;
    }

    public override void Collect(IReadOnlyCollection<IExifValue> values, List<Property> metadata)
    {
        var model = ReadValue(values, ExifTag.Model);
        var make = ReadValue(values, ExifTag.Make);

        if (model != null)
        {
            var data = (make != null && model.StartsWith(make, StringComparison.CurrentCultureIgnoreCase))
                ? model.Substring(make.Length).Trim()
                : model;
            var property = new Property
            {
                Name = "Model",
                Tag = MetadataTag.Model,
                Value = data
            };
            _overrides.OverrideWithMappings(property);
            metadata.Add(property);
        }
    }
}