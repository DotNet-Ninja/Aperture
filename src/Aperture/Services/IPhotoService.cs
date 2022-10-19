using Aperture.Entities;
using Aperture.Models;

namespace Aperture.Services;

public interface IPhotoService
{
    Task<Page<Photo>> FindPhotosAsync(PhotoSearchFilter filter);
    Task<Photo> AddPhotoAsync(NewPhoto photo);
}