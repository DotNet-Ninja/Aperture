using Aperture.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Aperture.Controllers;

public class HomeController : MvcController
{
    private readonly ApertureDb _db;

    public HomeController(ILogger<HomeController> logger, ApertureDb db):base(logger)
    {
        _db = db;
    }

    [HttpGet]
    public ViewResult Index()
    {
        var photos = _db.Photos.ToList();
        return View();
    }
}
