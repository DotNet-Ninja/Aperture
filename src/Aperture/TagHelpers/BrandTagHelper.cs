using Aperture.Configuration;
using Aperture.Constants;
using Humanizer;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aperture.TagHelpers;

[HtmlTargetElement("brand")]
public class BrandTagHelper: TagHelper
{
    private readonly AppSettings _settings;

    public BrandTagHelper(AppSettings settings)
    {
        _settings = settings;
    }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if(!string.IsNullOrWhiteSpace(_settings.SiteLogoImageUrl))
        {
            output.TagName = "img";
            output.Attributes.SetAttribute("src", _settings.SiteLogoImageUrl);
            output.Attributes.SetAttribute("alt", _settings.SiteName);
        }
        else
        {
            output.TagName = null; // Remove the <brand> tag
            if (_settings.BrandIcon != BootstrapIcon.NotSet)
            {
                var icon = _settings.BrandIcon.Humanize();
                output.Content.AppendHtml($"<i class=\"bi bi-{icon} me-2\"></i>");
            }

            output.Content.Append(_settings.SiteName);
        }
        return base.ProcessAsync(context, output);
    }
}