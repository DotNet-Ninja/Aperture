using Aperture.Constants;

namespace Aperture.Entities;

public class Photo
{
    private static readonly Uri DefaultUri = new ("/", UriKind.Relative);
    private string _name = string.Empty;
    private string _caption = string.Empty;

    public int Id { get; set; } = 0;

    public string Name
    {
        get => (string.IsNullOrWhiteSpace(_name)) ? FileName : _name;
        set => _name = value;
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

    public Orientation Orientation { get; set; } = Orientation.Landscape;
    public string ContentType { get; set; } = string.Empty;
    public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.MinValue;

    public List<ExifProperty>? ExifProperties { get; set; }
    public List<Tag>? Tags { get; set; }
}