using Aperture.Configuration;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aperture.TagHelpers;

[HtmlTargetElement("site-name", TagStructure = TagStructure.WithoutEndTag)]
public class SiteNameTagHelper : TagHelper
{
    public SiteNameTagHelper(SiteSettings settings)
    {
        _settings = settings;
    }

    private readonly SiteSettings _settings;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var name = (string.IsNullOrWhiteSpace(_settings.Name)) ? "Aperture" : _settings.Name;
        output.TagName = null;
        output.Content.Clear();
        output.Content.SetHtmlContent(name);
        base.Process(context, output);
    }
}