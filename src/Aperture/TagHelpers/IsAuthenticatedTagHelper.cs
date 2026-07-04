using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aperture.TagHelpers;

[HtmlTargetElement("*", Attributes = "is-authenticated")]
public class IsAuthenticatedTagHelper : TagHelper
{
    private readonly HttpContext _context;

    public IsAuthenticatedTagHelper(IHttpContextAccessor accessor)
    {
        _context = accessor.HttpContext!;
    }

    [HtmlAttributeName("is-authenticated")]
    public bool IsAuthenticated { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (_context.User.Identity!.IsAuthenticated != IsAuthenticated)
        {
            output.SuppressOutput();
        }
    }
}