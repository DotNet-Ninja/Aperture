namespace Aperture.Entities;

public class Tag
{
    public string Name { get; set; } = string.Empty;
    public List<Photo>? Photos { get; set; } 
}
