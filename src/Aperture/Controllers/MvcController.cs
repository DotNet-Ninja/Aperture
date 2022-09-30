using Microsoft.AspNetCore.Mvc;

namespace Aperture.Controllers;
public abstract class MvcController : Controller
{
    protected ILogger Logger { get; }

    protected MvcController(ILogger logger)
    {
        Logger = logger;
    }
}
