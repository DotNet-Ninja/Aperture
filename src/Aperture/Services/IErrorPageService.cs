using Aperture.Models;

namespace Aperture.Services;

public interface IErrorPageService
{
    ErrorPageModel GetErrorPageModel(int statusCode);
}