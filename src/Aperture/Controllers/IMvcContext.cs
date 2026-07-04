using Aperture.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aperture.Controllers;

public interface IMvcContext
{
    ILogger Logger { get; }
    ITimeProvider Time { get; }
    IWebHostEnvironment Host { get; }
}

public interface IMvcContext<TController> : IMvcContext where TController : Controller
{

}