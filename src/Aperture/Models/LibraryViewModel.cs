using Aperture.Entities;

namespace Aperture.Models;

public class LibraryViewModel: Page<Photo>
{
    public LibraryViewModel()
    {
    }

    public LibraryViewModel(Page<Photo> page, PhotoSearchFilter filter)
    {
        Number = page.Number;
        Size = page.Size;
        Items = page.Items;
        ItemCount = page.ItemCount;
        SearchQuery = filter.SearchQuery;
    }

    public string SearchQuery { get; set; } = string.Empty;
}