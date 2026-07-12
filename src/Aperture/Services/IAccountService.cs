using Aperture.Data;
using Aperture.Models;

namespace Aperture.Services;

public interface IAccountService
{
    Task<AuthenticationResult> AuthenticateAsync(string email, string password);
    Task<AuthenticationResult> RegisterAsync(string displayName, string email, string password);
    Task<ApplicationUser?> GetUserAsync(int id);
    Task UpdateUserAsync(ApplicationUser user);
    Task<Page<ApplicationUser>> PageUsersAsync(int page, int size);

    Task<List<Role>> GetRolesAsync();
}