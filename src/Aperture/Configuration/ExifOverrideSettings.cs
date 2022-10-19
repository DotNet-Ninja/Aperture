using Aperture.Constants;
using Aperture.Entities;
using DotNetNinja.AutoBoundConfiguration;

namespace Aperture.Configuration;

[AutoBind("ExifOverrides")]
public class ExifOverrideSettings
{
    public Dictionary<MetadataTag, Dictionary<string, string>> Mappings { get; set; } = new();

    public void OverrideWithMappings(Property property)
    {
        if (Mappings.ContainsKey(property.Tag) && Mappings[property.Tag].ContainsKey(property.Value))
        {
            property.Value = Mappings[property.Tag][property.Value];
        }
    }
}