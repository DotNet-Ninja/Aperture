using Microsoft.AspNetCore.Mvc;

namespace Aperture.Controllers;

public class HomeController : MvcController
{
    public HomeController(ILogger<HomeController> logger):base(logger)
    {
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
