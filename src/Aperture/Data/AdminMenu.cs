namespace Aperture.Data;

public static class AdminMenu
{
    public static readonly List<MenuItem> Items = new()
    {
        new MenuItem
        {
            Text = "Users",
            Href = "/Admin/Users",
            SortIndex = 10
        }
    };
}