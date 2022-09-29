using System.Security.Claims;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aperture.Controllers;
public class AccountController : Controller
{
    [HttpGet]
    public async Task Login()
    {
        var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
            .WithRedirectUri("/")
            .Build();

        await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    }
    
    [HttpGet, Authorize]
    public IActionResult Profile()
    {
        return View(User.Claims);
    }

    [HttpGet, Authorize]
    public async Task Logout()
    {
        var redirect = Url.Action("Index", "Home") ?? "/";
        var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
            .WithRedirectUri(redirect)
            .Build();

        await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}


// TODO: Return Redirects
// TODO: Navigation
// TODO: Make Profile only available in development
// TODO: Configure Auth0 Login & Sign Up
