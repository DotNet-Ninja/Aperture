using Aperture.Constants;

namespace Aperture.Models;

public class ErrorAction
{
    public string Text { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public BootstrapIcon Icon { get; set; } = BootstrapIcon.NotSet;
    public string CssClass { get; set; } = "primary";
}