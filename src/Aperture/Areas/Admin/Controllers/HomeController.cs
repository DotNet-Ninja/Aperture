using Aperture.Constants;
using Aperture.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aperture.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = AppRoles.Admin)]
public class HomeController: MvcController<HomeController>
{
    public HomeController(IMvcContext<HomeController> context) : base(context)
    {
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}