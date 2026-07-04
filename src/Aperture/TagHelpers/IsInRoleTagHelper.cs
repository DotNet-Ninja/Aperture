using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aperture.TagHelpers;

[HtmlTargetElement(Attributes = "is-in-role")]
public class IsInRoleTagHelper: TagHelper
{
    [ViewContext]
    public ViewContext ViewContext { get; set; } = null!;

    [HtmlAttributeName("is-in-role")]
    public string Role { get; set; } = null!;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (!ViewContext.HttpContext.User.IsInRole(Role))
        {
            output.SuppressOutput();
        }
    }
}