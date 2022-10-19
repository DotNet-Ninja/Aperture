using Aperture.Entities;

namespace Aperture.Models;

public class LibraryListItem
{
    public int Id { get; set; } = 0;
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public Uri ThumbnailUrl { get; set; } = Photo.DefaultUri;

    public static LibraryListItem FromEntity(Photo photo)
    {
        return new LibraryListItem
        {
            Slug = photo.Slug,
            Title = photo.Title,
            Id = photo.Id,
            ThumbnailUrl = photo.ThumbnailUrl
        };
    }
}