using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aperture.Controllers;
public class AccountController : MvcController
{
    private readonly IWebHostEnvironment _environment;

    public AccountController(ILogger<AccountController> logger, IWebHostEnvironment environment) : base(logger)
    {
        _environment = environment;
    }

    [HttpGet]
    public async Task LogIn(string returnUrl = "/")
    {
        var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
            .WithRedirectUri(returnUrl)
            .Build();

        await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    }
    
    [HttpGet, Authorize]
    public IActionResult Diagnostics()
    {
        if (!_environment.IsDevelopment())
        {
            return NotFound();
        }
        return View(User.Claims);
    }

    [HttpGet, Authorize]
    public async Task LogOut()
    {
        var redirect = Url.Action("Index", "Home") ?? "/";
        var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
            .WithRedirectUri(redirect)
            .Build();

        await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

}
