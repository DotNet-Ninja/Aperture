using Aperture.Models;

namespace Aperture.Services;

public interface ITokenService
{
    (string AccessToken, DateTimeOffset ExpiresAt) CreateToken(AuthenticationResult user);
}