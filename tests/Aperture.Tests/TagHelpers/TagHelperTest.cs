using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aperture.Tests.TagHelpers;

public abstract class TagHelperTest
{
    protected TagHelperOutputBuilder OutputBuilder { get; set; } = new();
    protected TagHelperContextBuilder ContextBuilder { get; set; } = new();

    
    protected string RenderOutput(TagHelperOutput output)
    {
        using var writer = new StringWriter();
        output.Content.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }
}