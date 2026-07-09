using Aperture.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Aperture.Controllers;

public class HomeController : MvcController<HomeController>
{
    private readonly INavigationService _navigationService;

    public HomeController(IMvcContext<HomeController> context, INavigationService navigationService) : base(context)
    {
        _navigationService = navigationService;
    }

    [AllowAnonymous]
    public IActionResult Index([FromQuery] int? statusCode)
    {
        return View();
    }
}
