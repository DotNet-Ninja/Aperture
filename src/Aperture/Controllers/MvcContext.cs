using Aperture.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aperture.Controllers;

public class MvcContext<TController> : IMvcContext<TController> where TController: Controller
{
    public MvcContext(ILogger<TController> logger, ITimeProvider time, IWebHostEnvironment host)
    {
        Logger = logger;
        Time = time;
        Host = host;
    }

    public ILogger Logger { get; }
    public ITimeProvider Time { get; }
    public IWebHostEnvironment Host { get; }
}