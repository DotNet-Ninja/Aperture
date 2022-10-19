using Aperture.Entities;
using Aperture.Exceptions;
using Aperture.Extensions;
using Aperture.Models;
using Aperture.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;

namespace Aperture.Controllers;

[Authorize(Roles= "Aperture:Administrator")]
public class LibraryController : MvcController
{
    private readonly IPhotoService _service;

    public LibraryController(ILogger<LibraryController> logger, IPhotoService service) : base(logger)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery]PhotoSearchFilter filter)
    {
        var photos = await _service.FindPhotosAsync(filter);

        var model = new LibraryViewModel(photos, filter)
        {
            SearchQuery = filter.SearchQuery
        };

        if (filter.PageNumber > model.Count && string.IsNullOrWhiteSpace(filter.SearchQuery))
        {
            return NotFoundView();
        }
        return View(model);
    }

    [HttpGet]
    public ViewResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(NewPhoto model)
    {
        if (!ModelState.IsValid || model.File==null)
        {
            return View(model);
        }

        try
        {
            await _service.AddPhotoAsync(model);
            return RedirectToAction("Index");
        }
        catch (StorageOperationException sx)
        {
            Logger.LogWarning(sx, $"Error saving image.");
            ModelState.AddModelError("ExceptionMessage", "An error occurred saving your photo. See logs for more details.");
            return View(model);
        }
        catch (PhotoExistsException dx)
        {
            Logger.LogError(dx, "Error adding photo.");
            ModelState.AddModelError("ExceptionMessage", $"A photo with the slug '{model.Slug}' already exists.");
            return View(model);
        }
    }
}
