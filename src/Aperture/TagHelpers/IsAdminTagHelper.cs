using Aperture.Constants;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aperture.TagHelpers;


[HtmlTargetElement("*", Attributes = "is-admin")]
public class IsAdminTagHelper: TagHelper
{
    private readonly HttpContext _context;

    public IsAdminTagHelper(IHttpContextAccessor accessor)
    {
        _context = accessor.HttpContext!;
    }

    [HtmlAttributeName("is-admin")]
    public bool IsAdmin { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var user = _context.User;
        bool isAuthenticated = user.Identity!.IsAuthenticated;
        bool hasRole = user.IsInRole(AppRoles.Owner) || user.IsInRole(AppRoles.Contributor);
        bool isAdmin = isAuthenticated && hasRole;
        if (IsAdmin != isAdmin)
        {
            output.SuppressOutput();
        }
    }
}