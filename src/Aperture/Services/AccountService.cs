using Aperture.Constants;
using Aperture.Data;
using Aperture.Models;
using Microsoft.AspNetCore.Identity;

namespace Aperture.Services;

public class AccountService : IAccountService
{
    private readonly IRepository _repository;
    private readonly IPasswordHasher<ApplicationUser> _hasher;
    private readonly IAvatarService _avatars;

    public AccountService(IRepository repository, IPasswordHasher<ApplicationUser> hasher, IAvatarService avatars)
    {
        _repository = repository;
        _hasher = hasher;
        _avatars = avatars;
    }

    public async Task<AuthenticationResult> AuthenticateAsync(string email, string password)
    {
        var user = await _repository.GetUserByEmailAsync(email);
        if (user == null)
        {
            return new AuthenticationResult();
        }
        var verificationResult = _hasher.VerifyHashedPassword(user, user.Password, password);
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            return new AuthenticationResult();
        }
        return new AuthenticationResult(user);
    }

    public async Task<AuthenticationResult> RegisterAsync(string displayName, string email, string password)
    {
        var adminCount = await _repository.GetAdminCountAsync();
        List<Role> roles = new();
        if(adminCount==0)
        {
            var role = new Role { Name = AppRoles.Owner };
            _repository.Attach(role);
            roles.Add(role);
        }
        var user = new ApplicationUser
        {
            DisplayName = displayName,
            Email = email,
            Password = _hasher.HashPassword(null, password),
            AvatarId = _avatars.GetAvatarId(email),
            Roles = roles 
        };
        await _repository.AddUserAsync(user);
        return new AuthenticationResult(user);
    }

    public Task<ApplicationUser?> GetUserAsync(int id)
    {
        return _repository.GetUserByIdAsync(id);
    }

    public Task UpdateUserAsync(ApplicationUser user)
    {
        foreach(var role in user.Roles)
        {
            _repository.Attach(role);
        }
        return _repository.UpdateUserAsync(user);
    }

    public Task<List<Role>> GetRolesAsync()
    {
        return _repository.GetRolesAsync();
    }

    public Task<Page<ApplicationUser>> PageUsersAsync(int page, int size)
    {
        return _repository.PageUsersAsync(page, size);
    }
}