using Aperture.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aperture.Controllers;

[AllowAnonymous]
[Route("error")]
public class ErrorController : MvcController<ErrorController>
{
    private readonly IErrorPageService _errorPageService;

    public ErrorController(IMvcContext<ErrorController> context, IErrorPageService errorPageService)
        : base(context)
    {
        _errorPageService = errorPageService;
    }

    [HttpGet("{statusCode:int}")]
    public IActionResult Handle(int statusCode)
    {
        Response.StatusCode = statusCode;
        var model = _errorPageService.GetErrorPageModel(statusCode);
        // If status code == 0 then the specific error was not recognized.
        // Set Status Code to actual so it is correct.
        if (model.StatusCode == 0) model.StatusCode = statusCode;

        return View(model);
    }
}
