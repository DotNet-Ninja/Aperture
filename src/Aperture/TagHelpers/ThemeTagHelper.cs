using Aperture.Services;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aperture.TagHelpers;

[HtmlTargetElement("theme", TagStructure = TagStructure.WithoutEndTag)]
public class ThemeTagHelper: TagHelper
{
    private readonly IThemeService _themes;

    public ThemeTagHelper(IThemeService themes)
    {
        _themes = themes;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var theme = _themes.GetActiveTheme();
        output.TagName = null;
        output.Content.Clear();
        foreach(var css in theme.Stylesheets)
        {
            output.Content.AppendHtml($"<link rel=\"stylesheet\" href=\"{css}\" />");
        }
        base.Process(context, output);
    }
}