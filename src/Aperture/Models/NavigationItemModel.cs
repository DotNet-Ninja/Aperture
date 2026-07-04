namespace Aperture.Models;

public class NavigationItemModel
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = string.Empty;
    public bool OpenInNewTab { get; set; } = false;

    public List<NavigationItemModel> Children { get; set; } = new();
}