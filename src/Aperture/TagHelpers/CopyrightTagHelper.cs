using Aperture.Configuration;
using Aperture.Constants;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aperture.TagHelpers;

[HtmlTargetElement("copyright")]
public class CopyrightTagHelper: TagHelper
{
    private readonly AppSettings _settings;

    public CopyrightTagHelper(AppSettings settings)
    {
        _settings = settings;
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        //get value inside existing <copyright> tag, if any
        var existingContent = (await output.GetChildContentAsync())?.GetContent()??string.Empty;
        var siteName = string.IsNullOrWhiteSpace(_settings.SiteName) ? Defaults.SiteName : _settings.SiteName;
        var owner = string.IsNullOrWhiteSpace(_settings.CopyrightOwner) ? siteName : _settings.CopyrightOwner;
        output.TagName = null; // Remove the <copyright> tag
        output.Content?.SetHtmlContent($"&copy; {DateTime.Now.Year} {owner} {existingContent}");
    }
}