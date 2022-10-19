using Aperture.Constants;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;

namespace Aperture.Extensions;

public static class ImageExtensions
{
    public static Orientation GetOrientation(this Image image)
    {
        if (image.Width == image.Height) return Orientation.Square;
        return image.Width > image.Height ? Orientation.Landscape : Orientation.Portrait;
    }

    public static async Task<byte[]> ToByteArrayAsync(this Image source, string contentType)
    {
        await using var stream = new MemoryStream();
        await source.SaveAsync(stream, GetEncoder(contentType));
        byte[] result = stream.ToArray();
        return result;
    }

    private static IImageEncoder GetEncoder(string contentType)
    {
        return contentType switch
        {
            ContentType.Png => new PngEncoder(),
            ContentType.Jpeg => new JpegEncoder(),
            ContentType.Gif => new GifEncoder(),
            _ => new JpegEncoder()
        };
    }
}