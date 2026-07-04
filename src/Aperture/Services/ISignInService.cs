using Microsoft.AspNetCore.Authentication;

namespace Aperture.Services;

public interface ISignInService
{
    Task ChallengeAsync(string scheme, AuthenticationProperties properties);
    Task SignOutAsync(string scheme, AuthenticationProperties properties);
    Task SignOutAsync(string scheme);
}