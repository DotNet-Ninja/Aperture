using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Aperture.Constants;
using Aperture.Entities;

namespace Aperture.Models
{
    public class UpdatePhotoModel: UpdatePhoto
    {
        public string FileName { get; set; } = string.Empty;

        public Uri FullUrl { get; set; } = Photo.DefaultUri;
        public Uri LargeUrl { get; set; } = Photo.DefaultUri;
        public Orientation Orientation { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateUploaded { get; set; }
        public List<Property> Metadata { get; set; } = new();
        
        public static UpdatePhotoModel FromEntity(Photo photo)
        {
            return new UpdatePhotoModel
            {
                Caption = photo.Caption,

                ContentType = photo.ContentType,
                DateCreated = photo.DateCreated,
                DateUploaded = photo.DateUploaded,
                ExposureSummary = photo.ExposureSummary,
                FileName = photo.FileName,
                FullUrl = photo.FullUrl,
                Id = photo.Id,
                LargeUrl = photo.LargeUrl,
                Metadata = photo.Metadata ?? new List<Property>(),
                Orientation = photo.Orientation,
                Slug = photo.Slug,
                Title = photo.Title,
                Tags = JsonSerializer.Serialize(photo.Tags?.Select(t => t.Name)?.ToList() ?? new List<string>())
            };
        }
    }
}

namespace Aperture.Models
{
    public class UpdatePhoto
    {
        [Required] public int Id { get; set; }
        [Required, StringLength(128)] public string Title { get; set; } = string.Empty;
        [Required, StringLength(128)] public string Slug { get; set; } = string.Empty;
        [StringLength(256)] public string Caption { get; set; } = string.Empty;
        [Required, StringLength(256)] public string ExposureSummary { get; set; } = string.Empty;
        public string Tags { get; set; } = "[]";
        
        public void UpdateEntity(Photo photo)
        {
            photo.Caption = Caption;
            photo.ExposureSummary = ExposureSummary;
            photo.Slug = Slug;
            photo.Title = Title;
        }
    }
}