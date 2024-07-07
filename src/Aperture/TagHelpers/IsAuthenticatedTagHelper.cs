using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aperture.TagHelpers;

[HtmlTargetElement(Attributes = "is-authenticated")]
public class IsAuthenticatedTagHelper : TagHelper
{
    public IsAuthenticatedTagHelper(IHttpContextAccessor contextAccessor)
    {
        Context = contextAccessor.HttpContext;
    }

    [HtmlAttributeName("is-authenticated")]
    public bool IsAuthenticated { get; set; } = true;

    protected internal HttpContext? Context { get; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (IsAuthenticated != (Context?.User.Identity?.IsAuthenticated ?? false))
        {
            output.Content.Clear();
            output.TagName = null;
        }
        return base.ProcessAsync(context, output);
    }
}