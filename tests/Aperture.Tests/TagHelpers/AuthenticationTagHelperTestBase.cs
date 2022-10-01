using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Aperture.Tests.TagHelpers;

public class AuthenticationTagHelperTestBase: TagHelperTest
{

    protected Mock<IHttpContextAccessor> SetUpContextAccessor(ClaimsPrincipal user)
    {
        var accessor = new Mock<IHttpContextAccessor>();
        var context = new Mock<HttpContext>();
        context.Setup(mock => mock.User).Returns(user);
        accessor.Setup(x => x.HttpContext).Returns(context.Object);
        return accessor;
    }

    protected ClaimsPrincipal CreateAuthenticatedUser()
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, "John Doe"),
            new (ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new (ClaimTypes.Role, "Member")
        };
        return CreateAuthenticatedUser(claims);
    }

    protected ClaimsPrincipal CreateAuthenticatedUser(IEnumerable<Claim> claims)
    {
        var identity = new ClaimsIdentity(claims, "password", ClaimTypes.Name, ClaimTypes.Role);
        return new ClaimsPrincipal(identity);
    }

    protected ClaimsPrincipal CreateUnauthenticatedUser()
    {
        return new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()));
    }
}