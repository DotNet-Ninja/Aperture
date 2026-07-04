using Aperture.Services;
using Aperture.Models;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aperture.Controllers;

public class AccountController : MvcController<AccountController>
{
    private readonly ISignInService _signInService;

    public AccountController(IMvcContext<AccountController> context, ISignInService signInService) : base(context)
    {
        _signInService = signInService;
    }

    [HttpGet, AllowAnonymous]
    public async Task LogIn(string returnUrl = "/")
    {
        var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
            .WithRedirectUri(returnUrl)
            .Build();

        await _signInService.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    }

    [HttpGet]
    public IActionResult Diagnostics()
    {
        if (!Host.IsDevelopment())
        {
            return NotFound();
        }
        var model = new AccountDiagnosticsModel
        {
            Claims = User.Claims.ToList()
        };
        return View(model);
    }

    [HttpGet]
    public async Task LogOut()
    {
        var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
            .WithRedirectUri("/")
            .Build();

        await _signInService.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        await _signInService.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}