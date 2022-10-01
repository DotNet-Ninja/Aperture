using Microsoft.AspNetCore.Mvc;

namespace Aperture.Controllers;

public class HomeController : MvcController
{
    public HomeController(ILogger<HomeController> logger):base(logger)
    {
    }

    [HttpGet]
    public ViewResult Index()
    {
        return View();
    }
}
