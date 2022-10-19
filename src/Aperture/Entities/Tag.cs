namespace Aperture.Entities;

public class Tag: IEntity
{
    public string Name { get; set; } = string.Empty;
    public List<Photo>? Photos { get; set; } 
}
