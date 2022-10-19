using System.Text;
using Aperture.Configuration;
using Aperture.Constants;
using Aperture.Entities;
using Aperture.Exceptions;
using Aperture.Extensions;
using Aperture.Models;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Aperture.Services;

public class PhotoService : IPhotoService
{
    private readonly ILogger<PhotoService> _logger;
    private readonly ApertureDb _db;
    private readonly ITimeProvider _time;
    private readonly IStorageProvider _storage;
    private readonly PhotoSettings _settings;
    private readonly IExifToMetadataConverter _converter;

    public PhotoService(ILogger<PhotoService> logger, ApertureDb db, ITimeProvider time, IStorageProvider storage, 
                        PhotoSettings settings, IExifToMetadataConverter converter)
    {
        _logger = logger;
        _db = db;
        _time = time;
        _storage = storage;
        _settings = settings;
        _converter = converter;
    }

    public Task<Page<Photo>> FindPhotosAsync(PhotoSearchFilter filter)
    {
        var query = _db.Photos.Include(p => p.Metadata).Include(p => p.Tags).AsQueryable();
        if (!string.IsNullOrWhiteSpace(filter.SearchQuery))
        {
            query = query.Where(q =>
                q.Title.Contains(filter.SearchQuery) || q.Tags.Any(t => t.Name == filter.SearchQuery));
        }

        return query.OrderByDescending(p=>p.DateUploaded).ToPageAsync(filter);
    }

    public async Task<Photo> AddPhotoAsync(NewPhoto photo)
    {
        if (photo.File == null)
        {
            throw new ArgumentNullException(nameof(photo.File));
        }

        var fileName = photo.File.FileName;
        var extension = Path.GetExtension(fileName);
        extension = (extension.StartsWith(".")) ? extension.Substring(1) : extension;
        if (!(ContentType.SupportedImageContentTypes.Contains(photo.File.ContentType, StringComparer.CurrentCultureIgnoreCase) 
                && ContentType.SupportedFileExtensions.Contains(extension, StringComparer.CurrentCultureIgnoreCase)))
        {
            throw UnsupportedFileTypeException.FromExtensionAndContentType(extension, photo.File.ContentType);
        }

        // Get file contents
        var upload = await Image.LoadAsync(photo.File.OpenReadStream());
        
        // Get ExifData
        var properties = _converter.Convert(upload.Metadata.ExifProfile.Values);

        // Scale and save images
        var urls = new Dictionary<PhotoSize, Uri>();
        foreach (PhotoSize size in Enum.GetValues(typeof(PhotoSize)))
        {
            var uri = await SaveScaledPhotoFileAsync(upload, size, photo.Slug, photo.File.ContentType, photo.File.FileName);
            urls.Add(size, uri);
        }

        // Create Photo entity
        var entity = new Photo
        {
            Title = photo.Title,
            FileName = photo.File.FileName,
            Caption = photo.Caption ?? string.Empty,
            ContentType = photo.File.ContentType,
            DateUploaded = _time.RequestTime,
            Metadata = properties,
            Orientation = upload.GetOrientation(),
            Slug = photo.Slug,
            FullUrl = urls[PhotoSize.Full],
            LargeUrl = urls[PhotoSize.Large],
            SmallUrl = urls[PhotoSize.Small],
            ThumbnailUrl = urls[PhotoSize.Thumb],
            IconUrl = urls[PhotoSize.Icon]
        };
        entity.DateCreated = GetDateTaken(entity.Metadata) ?? _time.RequestTime;
        entity.ExposureSummary = BuildExposureSummary(entity.Metadata);

        // Save Entity
        _db.Add(entity);
        try
        {
            await _db.SaveChangesAsync();

        }
        catch (DbUpdateException ex)
        {
            _logger.LogWarning(ex, $"Exception commiting new photo '{entity.Slug}'.");
            throw PhotoExistsException.FromDbUpdateException(entity, ex);
        }

        return entity;
    }

    private async Task<Uri> SaveScaledPhotoFileAsync(Image image, PhotoSize size, string slug, string contentType, string fileName)
    {
        var orientation = image.GetOrientation();
        Image scaledImage;
        if (size == PhotoSize.Icon || size == PhotoSize.Thumb)
        {
            scaledImage = CreateThumbnail(image, _settings.GetWidth(orientation, size), orientation);
        }
        else
        {
            scaledImage = ScaleProportionally(image, _settings.GetWidth(orientation, size));
        }
        
        var name = PrefixFileNameWithDate($"{slug}.{fileName}");
        var data = await scaledImage.ToByteArrayAsync(contentType);
        var result = await _storage.StoreAsync(size.ToString(), name, contentType, data);
        return result;
    }

    private static Image ScaleProportionally(Image image, int width)
    {
        return image.Clone(x => x.Resize(width, 0));
    }

    private static Image CreateThumbnail(Image image, int width, Orientation orientation)
    {
        switch (orientation)
        {
            case Orientation.Landscape:
                var landscape = image.Clone(x => x.Resize(0, width));
                var landscapeRectangle = new Rectangle((landscape.Width - width) / 2, 0, width, width);
                landscape.Mutate(x => x.Crop(landscapeRectangle));
                return landscape;
            case Orientation.Portrait:
                var portrait = image.Clone(x => x.Resize(width, 0));
                var portraitRectangle = new Rectangle(0, (portrait.Height - width) / 2, width, width);
                portrait.Mutate(x => x.Crop(portraitRectangle));
                return portrait;
            default:
                return image.Clone(x => x.Resize(width, width));
        }
    }

    private string PrefixFileNameWithDate(string fileName)
    {
        var date = _time.RequestTime.Date;
        return $"{date.Year}.{date.Month.ToString().PadLeft(2, '0')}.{date.Day.ToString().PadLeft(2, '0')}.{fileName}";
    }

    private DateTimeOffset? GetDateTaken(List<Property> properties)
    {
        var date = properties.FirstOrDefault(p => p.Tag == MetadataTag.DateTimeCaptured);
        if (date != null && DateTimeOffset.TryParse(date.Value, out var result))
        {
            return result;
        }
        return null;
    }

    private string BuildExposureSummary(List<Property> properties)
    {
        var exposure = properties.SingleOrDefault(p => p.Tag == MetadataTag.ExposureTime)?.Value?.Trim();
        var aperture = properties.SingleOrDefault(p => p.Tag == MetadataTag.FNumber)?.Value?.Trim();
        var iso = properties.SingleOrDefault(p => p.Tag == MetadataTag.IsoSpeed)?.Value?.Trim();
        var focalLength = properties.SingleOrDefault(p => p.Tag == MetadataTag.FocalLength)?.Value?.Trim();
        var lens = properties.SingleOrDefault(p => p.Tag == MetadataTag.LensModel)?.Value?.Trim();
        var builder = new StringBuilder(exposure ?? string.Empty);
        if (builder.Length > 0)
        {
            if (!string.IsNullOrWhiteSpace(aperture))
            {
                builder.Append($" @ {aperture}");
            }
        }
        else
        {
            builder.Append(aperture ?? string.Empty);
        }
        if (builder.Length > 0)
        {
            if (!string.IsNullOrWhiteSpace(iso))
            {
                builder.Append($", ISO {iso}");
            }
        }
        else if (!string.IsNullOrWhiteSpace(iso))
        {
            builder.Append($"ISO {iso}");
        }

        if (builder.Length > 0 && !string.IsNullOrWhiteSpace(focalLength))
        {
            builder.Append($" - {focalLength}");
        } 
        else
        {
            builder.Append(focalLength ?? string.Empty);
        }

        if (!string.IsNullOrWhiteSpace(lens))
        {
            builder.Append($" ({lens})");
        }
        return builder.ToString().Trim();
    }
}


// TODO: ExposureSummary