using Aperture.Constants;

namespace Aperture.Models;

public class PreviousNextPagerViewModel
{
    private readonly Page _page;

    public PreviousNextPagerViewModel(Page page)
    {
        _page = page;
    }

    public string Caption => $"Page {_page.Number} of {_page.Count}";

    public bool CanGoBack => _page.Number > 1;
    public bool CanGoForward => _page.Number < _page.Count;

    public string PreviousText { get; set; } = "Back";
    public string NextText { get; set; } = "Next";
    public bool HasData => _page.Count > 0;
    public string PreviousClass => (CanGoBack)? "page-item" : "page-item disabled";
    public string NextClass => (CanGoForward) ? "page-item" : "page-item disabled";

    public string PreviousUrl { get; set; } = WellKnownEndpoint.ApplicationRoot;
    public string NextUrl { get; set; } = WellKnownEndpoint.ApplicationRoot;
}