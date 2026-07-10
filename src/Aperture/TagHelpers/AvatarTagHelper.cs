using Aperture.Configuration;
using Aperture.Services;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aperture.TagHelpers;

[HtmlTargetElement("avatar")]
public class AvatarTagHelper: TagHelper
{
    private readonly IHttpContextAccessor _accessor;
    private readonly AvatarSettings _settings;
    private readonly IAvatarService _avatars;

    public AvatarTagHelper(IHttpContextAccessor accessor, AvatarSettings settings, IAvatarService avatars)
    {
        _accessor = accessor;
        _settings = settings;
        _avatars = avatars;
    }

    [HtmlAttributeName("size")]
    public int Size { get; set; } = 0;

    [HtmlAttributeName("id")]
    public string Id { get; set; } = string.Empty;

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var size = Size==0? _settings.Size : Size;
        var id = string.IsNullOrWhiteSpace(Id) ? _accessor.HttpContext?.User?.AvatarId(): Id;

        if (string.IsNullOrWhiteSpace(id))
        {
            output.SuppressOutput();
        }
        else
        {
            output.TagName = "img";
            output.TagMode = TagMode.SelfClosing;
            output.Attributes.Add("src", _avatars.GetAvatarUrl(id, size));
            output.Attributes.Add("alt", _accessor.HttpContext?.User?.Identity?.Name ?? "Avatar");
        }

        return base.ProcessAsync(context, output);
    }
}