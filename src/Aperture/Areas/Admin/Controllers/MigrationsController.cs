using Aperture.Controllers;
using Aperture.Data;
using Aperture.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aperture.Areas.Admin.Controllers;

[Area("Admin")]
public class MigrationsController : MvcController<MigrationsController>
{
    private readonly IMigrationsService _migrations;
    private readonly IRepository _repository;

    public MigrationsController(IMvcContext<MigrationsController> context, IMigrationsService migrations) : base(context)
    {
        _migrations = migrations;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
