using Aperture.Constants;

namespace Aperture.Entities;

public class Property: IEntity
{
    public int Id { get; set; } = 0;
 
    public MetadataTag Tag { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Value { get; set; } = string.Empty;
}