using Aperture.Constants;

namespace Aperture.Entities;

public class Photo: IEntity
{
    public static readonly Uri DefaultUri = new ("/", UriKind.Relative);
    private string _title = string.Empty;
    private string _caption = string.Empty;

    public int Id { get; set; } = 0;
    public string Slug { get; set; } = string.Empty;
    public string Title
    {
        get => (string.IsNullOrWhiteSpace(_title)) ? FileName : _title;
        set => _title = value;
    }
    public string FileName { get; set; } = string.Empty;

    public string Caption
    {
        get => (string.IsNullOrWhiteSpace(_caption)) ? ExposureSummary : _caption;
        set => _caption = value;
    }
    public string ExposureSummary { get; set; } = string.Empty;

    public Uri FullUrl { get; set; } = DefaultUri;
    public Uri LargeUrl { get; set; } = DefaultUri;
    public Uri SmallUrl { get; set; } = DefaultUri;
    public Uri ThumbnailUrl { get; set; } = DefaultUri;
    public Uri IconUrl { get; set; } = DefaultUri;

    public Orientation Orientation { get; set; } = Orientation.Landscape;
    public string ContentType { get; set; } = string.Empty;
    public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.MinValue;
    public DateTimeOffset DateUploaded { get; set; } = DateTimeOffset.MinValue;

    public List<Property>? Metadata { get; set; }
    public List<Tag>? Tags { get; set; }
}