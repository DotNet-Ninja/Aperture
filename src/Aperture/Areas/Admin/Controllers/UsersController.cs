using Aperture.Configuration;
using Aperture.Constants;
using Aperture.Controllers;
using Aperture.Models;
using Aperture.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aperture.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = AppRoles.Owner)]
public class UsersController: MvcController<UsersController>
{
    private readonly IAccountService _accounts;
    private readonly AdminSettings _adminSettings;

    public UsersController(IMvcContext<UsersController> context, IAccountService accounts, AdminSettings adminSettings) : base(context)
    {
        _accounts = accounts;
        _adminSettings = adminSettings;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromQuery]int page = 1, [FromQuery]int? size = null)
    {
        var pageSize = size ?? _adminSettings.UsersPerPage;
        var users = await _accounts.PageUsersAsync(page, pageSize);
        var model = new Page<UserModel>(users.Size, users.Number, users.Items.Select(u => new UserModel(u)), users.TotalCount);
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit([FromRoute]int id)
    {
        var user = await _accounts.GetUserAsync(id);
        var allRoles = await _accounts.GetRolesAsync();
        if (user == null)
        {
            return NotFound();
        }
        var model = new UserEditModel(new UserModel(user), allRoles.Select(r => r.Name).ToList());
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UserEditModel model)
    {
        var user = await _accounts.GetUserAsync(model.User.Id);
        if (user == null)
        {
            return NotFound();
        }

        user.DisplayName = model.User.DisplayName;
        user.Email = model.User.Email;
        user.AvatarId = model.User.AvatarId;
        user.Roles = (model.User.Roles ?? []).Select(role => new Aperture.Data.Role { Name = role }).ToList();

        await _accounts.UpdateUserAsync(user);
        return RedirectToAction(nameof(Edit), new { id = model.User.Id });
    }
}