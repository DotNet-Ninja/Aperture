using Aperture.Constants;
using Aperture.Models;
using Microsoft.AspNetCore.Mvc;

namespace Aperture.ViewComponents;

public class PreviousNextPagerViewComponent: ViewComponent
{
    public string? Controller => RouteData.Values["controller"]?.ToString();
    public string? Action => RouteData.Values["action"]?.ToString();

    public IViewComponentResult Invoke(Page page)
    {
        var baseUrl = Url.ActionLink(Action, Controller, RouteData.Values) ?? WellKnownEndpoint.ApplicationRoot;
        var model = new PreviousNextPagerViewModel(page);
        model.PreviousUrl = (model.CanGoBack) ? $"{baseUrl}?{BuildQueryString(page.Number - 1, page.Size)}" : string.Empty;
        model.NextUrl = (model.CanGoForward) ? $"{baseUrl}?{BuildQueryString(page.Number + 1, page.Size)}" : string.Empty;
        return View(model);
    }

    private string BuildQueryString(string page, string size)
    {
        var query = Request.Query.Where(k=>
                !k.Key.Equals(PageFilter.PageParameterName, StringComparison.CurrentCultureIgnoreCase)
                && !k.Key.Equals(PageFilter.SizeParameterName, StringComparison.CurrentCultureIgnoreCase))
            .Select(k=> new KeyValuePair<string, string>(k.Key, k.Value)).ToList();
        query.Add(new KeyValuePair<string, string>(PageFilter.PageParameterName, page));
        query.Add(new KeyValuePair<string, string>(PageFilter.SizeParameterName, size));
        var result = string.Join("&", query.Select(k => $"{k.Key}={k.Value}"));
        return result;
    }

    private string BuildQueryString(int page, int size)
    {
        return BuildQueryString(page.ToString(), size.ToString());
    }
}