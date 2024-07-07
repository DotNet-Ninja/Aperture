using Aperture.Configuration;
using Aperture.Services;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aperture.TagHelpers;

[HtmlTargetElement("copyright", TagStructure = TagStructure.WithoutEndTag)]
public class CopyrightTagHelper : TagHelper
{
    private readonly CopyrightSettings _settings;
    private readonly ITimeProvider _time;

    public CopyrightTagHelper(SiteSettings settings, ITimeProvider time)
    {
        _settings = settings.Copyright;
        _time = time;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var currentYear = _time.Now.Year;
        var displayYear = (_settings.StartYear.HasValue && currentYear > _settings.StartYear.Value)
            ? $"{_settings.StartYear} - {currentYear}"
            : currentYear.ToString();

        output.TagName = null;
        output.Content.SetHtmlContent($"&copy; {displayYear} {_settings.Notice}".Trim());
        base.Process(context, output);
    }
}