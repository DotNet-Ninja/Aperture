namespace Aperture.Models;

public class PhotoSearchFilter: PageFilter
{
    public string SearchQuery { get; set; } = string.Empty;
}