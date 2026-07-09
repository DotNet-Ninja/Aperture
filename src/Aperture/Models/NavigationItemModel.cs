using Aperture.Data;

namespace Aperture.Models;

public class NavigationItemModel
{
    public NavigationItemModel()
    {
    }

    public NavigationItemModel(MenuItem item)
    {
        Id = item.Id;
        Text = item.Text;
        TargetUrl = item.Href;
        OpenInNewTab = item.OpensInNewTab;
        Children = item.Children.Select(c => new NavigationItemModel(c)).ToList();
    }

    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = string.Empty;
    public bool OpenInNewTab { get; set; } = false;

    public List<NavigationItemModel> Children { get; set; } = new();
}