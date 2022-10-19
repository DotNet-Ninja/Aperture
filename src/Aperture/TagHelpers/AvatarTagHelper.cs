using System.Security.Claims;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aperture.TagHelpers;

[HtmlTargetElement("avatar", TagStructure = TagStructure.WithoutEndTag)]
public class AvatarTagHelper: TagHelper
{
    private readonly ClaimsPrincipal _user;

    public AvatarTagHelper(IHttpContextAccessor accessor)
    {
        _user = accessor?.HttpContext?.User ?? new ClaimsPrincipal();
    }

    [HtmlAttributeName("size")]
    public int Size { get; set; }

   

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var url = GetAvatarUrl(GetUserAvatarId());
        var userName = GetUserName();
        output.TagName = "img";
        output.Attributes.Add("alt", userName);
        output.Attributes.Add("title", userName);
        output.Attributes.Add("src", url);
        return base.ProcessAsync(context, output);
    }

    private string GetUserName()
    {
        if (_user.Identity?.IsAuthenticated ?? false)
        {
            return _user.Identity.Name ?? "Unknown";
        }

        return "Anonymous";
    }

    private string GetUserAvatarId()
    {
        if (!_user.Identity?.IsAuthenticated ?? false)
        {
            return string.Empty;
        }

        var claim = _user.Claims.FirstOrDefault(claim => claim.Type == "picture");
        if (claim != null && !string.IsNullOrWhiteSpace(claim.Value))
        {
            var match = Regex.Match(claim.Value, @"/avatar/[a-f0-9]{32}");
            if (match.Success)
            {
                return match.Groups[0].Value.Substring(8);
            }
        }
        return string.Empty;
    }

    private string GetAvatarUrl(string id)
    {
        return $"https://s.gravatar.com/avatar/{id}?s={Size}&d=mp&r=pg";
    }
}