using Aperture.Entities;
using Aperture.Models;

namespace Aperture.Services;

public interface IPhotoService
{
    Task<Page<Photo>> FindPhotosAsync(PhotoSearchFilter filter);
    Task<Photo> AddPhotoAsync(NewPhoto photo);
    Task<Photo?> GetPhotoAsync(string slug);
    Task<List<Tag>> GetAllTagsAsync();
    Task<List<Property>> GetMetadataAsync(int photoId);
}