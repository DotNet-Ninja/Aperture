using Microsoft.AspNetCore.Mvc;

namespace Aperture.Controllers;
public class ErrorController : MvcController
{
    public ErrorController(ILogger<ErrorController> logger) : base(logger)
    {
    }

    [HttpGet]
    public ViewResult ServerError()
    {
        return InternalServerErrorView();
    }

    [HttpGet]
    public ViewResult AccessDenied()
    {
        return ForbiddenView();
    }
}
