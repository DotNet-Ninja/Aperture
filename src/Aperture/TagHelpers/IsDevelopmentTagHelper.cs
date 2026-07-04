using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aperture.TagHelpers;

[HtmlTargetElement("*", Attributes = "is-development")]
public class IsDevelopmentTagHelper : TagHelper
{
    private readonly IWebHostEnvironment _environment;

    public IsDevelopmentTagHelper(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    [HtmlAttributeName("is-development")]
    public bool IsDevelopment { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (_environment.IsDevelopment() != IsDevelopment)
        {
            output.SuppressOutput();
        }
    }
}