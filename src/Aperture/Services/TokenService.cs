using Aperture.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aperture.Models;

namespace Aperture.Services;

public class TokenService : ITokenService
{
    private readonly JwtOptions _options;

    public TokenService(JwtOptions options)
    {
        _options = options;
    }

    public (string AccessToken, DateTimeOffset ExpiresAt) CreateToken(AuthenticationResult user)
    {
        var now = DateTimeOffset.UtcNow;
        var expiresAt = now.AddMinutes(_options.ExpirationMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(Avatars.ClaimKey, user.AvatarId),
            new(ClaimTypes.Name, user.DisplayName)
        };

        foreach (var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_options.SigningKey));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _options.Issuer,
            Audience = _options.Audience,
            NotBefore = now.UtcDateTime,
            Expires = expiresAt.UtcDateTime,
            SigningCredentials = credentials
        };

        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(tokenDescriptor);

        return (AccessToken: handler.WriteToken(token), ExpiresAt: expiresAt);
    }
}