using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aperture.TagHelpers;

[HtmlTargetElement(Attributes = "is-in-role")]
public class IsInRoleTagHelper : TagHelper
{
    public IsInRoleTagHelper(IHttpContextAccessor contextAccessor)
    {
        Context = contextAccessor.HttpContext;
    }

    [HtmlAttributeName("is-in-role")]
    public string IsInRole { get; set; } = string.Empty;

    protected internal HttpContext? Context { get; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (!Context?.User.IsInRole(IsInRole) ?? false)
        {
            output.Content.Clear();
            output.TagName = null;
        }
        return base.ProcessAsync(context, output);
    }
}