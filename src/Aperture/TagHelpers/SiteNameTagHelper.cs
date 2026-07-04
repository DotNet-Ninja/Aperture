using Aperture.Configuration;
using Aperture.Constants;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aperture.TagHelpers;

[HtmlTargetElement("site-name")]
public class SiteNameTagHelper: TagHelper
{
    private readonly AppSettings _settings;
    public SiteNameTagHelper(AppSettings settings)
    {
        _settings = settings;
    }
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var name = string.IsNullOrWhiteSpace(_settings.SiteName) ? Defaults.SiteName : _settings.SiteName;
        output.TagName = null; // Remove the <site-name> tag
        output.Content.SetContent(name);
    }
}