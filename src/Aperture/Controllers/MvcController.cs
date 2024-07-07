using Aperture.Constants;
using Microsoft.AspNetCore.Mvc;

namespace Aperture.Controllers;

public abstract class MvcController: Controller
{
    protected ILogger Logger { get; }

    protected MvcController(ILogger logger)
    {
        Logger = logger;
    }

    protected ViewResult ViewWithStatus(int status)
    {
        var result = View();
        result.StatusCode = status;
        return result;
    }

    protected ViewResult ViewWithStatus(int status, string view)
    {
        var result = View(view);
        result.StatusCode = status;
        return result;
    }

    protected ViewResult InternalServerErrorView()
    {
        return ViewWithStatus(HttpStatus.InternalServerError, "~/Views/Error/ServerError.cshtml");
    }

    protected ViewResult ForbiddenView()
    {
        return ViewWithStatus(HttpStatus.Forbidden, "~/Views/Error/AccessDenied.cshtml");
    }
}