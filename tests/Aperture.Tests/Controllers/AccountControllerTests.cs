using System.Security.Claims;
using Aperture.Controllers;
using Aperture.Services;
using Auth0.AspNetCore.Authentication;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols;
using Moq;

namespace Aperture.Tests.Controllers;

public class AccountControllerTests
{
    private readonly Mock<ILogger<AccountController>> _mockLogger = new();
    private readonly Mock<IWebHostEnvironment> _mockEnvironment = new();
    private readonly Mock<ISignInService> _mockSignInService = new();

    [Fact]
    public async Task LogIn_ShouldIssueChallengeWithExpectedParameters()
    {
        _mockSignInService.Setup(mock => mock.ChallengeAsync(It.IsAny<string>(), It.IsAny<AuthenticationProperties>()))
            .Returns(Task.CompletedTask);
        var controller = CreateController();

        await controller.LogIn("/Home");

        _mockSignInService.Verify(mock =>
            mock.ChallengeAsync(It.Is<string>(s => s == Auth0Constants.AuthenticationScheme),
                It.Is<AuthenticationProperties>(s => s.RedirectUri == "/Home")));
    }

    [Fact]
    public async Task LogOut_ShouldLogOutFromAuth0Scheme()
    {
        _mockSignInService.Setup(mock => mock.SignOutAsync(It.IsAny<string>(), It.IsAny<AuthenticationProperties>()))
            .Returns(Task.CompletedTask);
        var controller = CreateController();

        await controller.LogOut();

        _mockSignInService.Verify(mock =>
            mock.SignOutAsync(It.Is<string>(s => s == Auth0Constants.AuthenticationScheme),
                It.Is<AuthenticationProperties>(s => s.RedirectUri == "/")));
    }

    [Fact]
    public async Task LogOut_ShouldLogOutFromCookieScheme()
    {
        _mockSignInService.Setup(mock => mock.SignOutAsync(It.IsAny<string>(), It.IsAny<AuthenticationProperties>()))
            .Returns(Task.CompletedTask);
        var controller = CreateController();

        await controller.LogOut();

        _mockSignInService.Verify(mock =>
            mock.SignOutAsync(It.Is<string>(s => s == CookieAuthenticationDefaults.AuthenticationScheme)));
    }

    [Fact]
    public void Diagnostics_WhenRunningInDevelopment_ShouldReturnViewWithUserClaims()
    {
        var user = CreateAuthenticatedUser();
        _mockEnvironment.Setup(mock => mock.EnvironmentName).Returns("Development");
        var controller = CreateControllerWithUser(user);

        var result = controller.Diagnostics();

        result.Should().BeOfType<ViewResult>();
        var view = (ViewResult)result;
        var model = view.Model as IEnumerable<Claim>;
        model.Should().NotBeNull();
        model.Should().HaveCount(3);
    }


    [Fact]
    public void Diagnostics_WhenRunningInProduction_ShouldReturnNotFound()
    {
        var user = CreateAuthenticatedUser();
        _mockEnvironment.Setup(mock => mock.EnvironmentName).Returns("Production");
        var controller = CreateControllerWithUser(user);

        var result = controller.Diagnostics();

        result.Should().BeOfType<NotFoundResult>();
    }

    private AccountController CreateController()
    {
        return CreateController(_mockLogger.Object, _mockEnvironment.Object, _mockSignInService.Object);
    }

    private AccountController CreateController(ILogger<AccountController> logger, IWebHostEnvironment environment, ISignInService signinService)
    {
        var controller = new AccountController(logger, environment, signinService);
        return controller;
    }

    private AccountController CreateControllerWithUser(ClaimsPrincipal user)
    {
        var controller = CreateController();
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext
            {
                User = user
            }
        };
        return controller;
    }

    private ClaimsPrincipal CreateAuthenticatedUser()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "John Doe"),
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, "Member")
        };
        return new ClaimsPrincipal(new ClaimsIdentity(claims, "password", ClaimTypes.Name, ClaimTypes.Role));
    }
}
