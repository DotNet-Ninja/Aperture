namespace Aperture.Data;

public class MenuItem
{
    public int Id { get; set; } = 0;
    public string Text { get; set; } = string.Empty;
    public string Href { get; set; } = string.Empty;
    public bool OpensInNewTab { get; set; } = false;
    public int SortIndex { get; set; } = 0;

    public List<MenuItem> Children { get; set; } = new ();

    public int? ParentId { get; set; }
}