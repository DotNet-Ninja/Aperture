using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aperture.TagHelpers;

[HtmlTargetElement("user-name")]
public class UserNameTagHelper: TagHelper
{
    private readonly IHttpContextAccessor _accessor;

    public UserNameTagHelper(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    [HtmlAttributeName("anonymous")]
    public string AnonymousUserName { get; set; } = "Anonymous";

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var user = _accessor.HttpContext?.User;
        if (user == null)
        {
            output.TagName = null;
            output.Content.SetContent(AnonymousUserName);
        }
        else
        {
            output.TagName = null;
            output.Content.SetContent(user.Identity?.Name ?? AnonymousUserName);
        }
        return base.ProcessAsync(context, output);
    }
}