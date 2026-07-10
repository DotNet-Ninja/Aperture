using Aperture.Models;

namespace Aperture.Services;

public interface IAccountService
{
    Task<AuthenticationResult> AuthenticateAsync(string email, string password);
    Task<AuthenticationResult> RegisterAsync(string displayName, string email, string password);
}