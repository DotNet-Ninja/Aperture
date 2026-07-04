using Aperture.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aperture.ViewComponents;

public class NavigationViewComponent: ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = new List<NavigationItemModel>()
        {
            new NavigationItemModel()
            {
                TargetUrl = "/Home",
                Text = "Home",
                Children = new()
                {
                    new NavigationItemModel()
                    {
                        TargetUrl = "/Home/Test1",
                        Text = "Test 1",
                    },
                    new NavigationItemModel()
                    {
                        TargetUrl = "/Home/Test2",
                        Text = "Test 2",
                    }
                }
            },
            new NavigationItemModel()
            {
                TargetUrl = "/About",
                Text = "About",
            }
        };
        return View(model);
    }
}