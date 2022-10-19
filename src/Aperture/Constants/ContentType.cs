namespace Aperture.Constants;

public static class ContentType
{
    public const string Jpeg = "image/jpeg";
    public const string Png = "image/png";
    public const string Bmp = "image/bmp";
    public const string Tiff = "image/tiff";
    public const string Gif = "image/gif";
    public const string Webp = "image/webp";

    public static readonly string[] SupportedImageContentTypes = new[]
    {
        Jpeg,
        Png,
        Gif
    };

    public static readonly string[] SupportedFileExtensions = new[]
    {
        "jpg",
        "jpeg",
        "png",
        "gif"
    };

    public static string GetExtension(string contentType)
    {
        switch (contentType.ToLower())
        {
            case Jpeg:
                return "jpg";
            case Png:
                return "png";
            case Bmp:
                return "bmp";
            case Tiff:
                return "tiff";
            case Gif:
                return "gif";
            case Webp: 
                return "webp";
            default:
                throw new ArgumentOutOfRangeException(nameof(contentType));
        }

    }
}
