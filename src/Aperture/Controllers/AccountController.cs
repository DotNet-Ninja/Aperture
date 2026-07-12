using Aperture.Constants;
using Aperture.Models;
using Aperture.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aperture.Controllers;

public class AccountController : MvcController<AccountController>
{
    private readonly IAccountService _accountService;
    private readonly ITokenService _tokenService;

    public AccountController(IMvcContext<AccountController> context, IAccountService accountService, ITokenService tokenService) : base(context)
    {
        _accountService = accountService;
        _tokenService = tokenService;
    }

    [HttpGet, AllowAnonymous]
    public IActionResult LogIn(string returnUrl = "/")
    {
        var model = new LoginModel();

        return View(model);
    }

    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> LogIn(LoginModel model, string returnUrl = "/")
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _accountService.AuthenticateAsync(model.Email, model.Password);
        if (!result.IsAuthenticated)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        return LoginAndRedirect(result, returnUrl);
    }

    [HttpGet, AllowAnonymous]
    public IActionResult Register()
    {
        var model = new RegistrationModel();
        return View(model);
    }

    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> Register(RegistrationModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _accountService.RegisterAsync(model.DisplayName, model.Email, model.Password);
        if (!result.IsAuthenticated)
        {
            ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
            return View(model);
        }
        return LoginAndRedirect(result, "/");
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
    public IActionResult LogOut()
    {
        Response.Cookies.Delete(JwtCookie.Name);

        return RedirectToAction("Index", "Home");
    }


    private IActionResult LoginAndRedirect(AuthenticationResult result, string returnUrl)
    {
        var tokenData = _tokenService.CreateToken(result);

        Response.Cookies.Append(JwtCookie.Name, tokenData.AccessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = Request.IsHttps,
            SameSite = SameSiteMode.Lax,
            Expires = tokenData.ExpiresAt,
            IsEssential = true
        });

        if (!Url.IsLocalUrl(returnUrl))
        {
            returnUrl = "/";
        }

        return Redirect(returnUrl);
    }
}