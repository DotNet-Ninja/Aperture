namespace Aperture.Models;

public abstract class Page
{
    public int ItemCount { get; set; }
    public int Number { get; set; }
    public int Size { get; set; }

    public int Count
    {
        get
        {
            if (Size == 0) return 0;
            return (ItemCount % Size == 0) ? ItemCount / Size : ItemCount / Size + 1;
        }
    }
}

public class Page<T> : Page where T : class
{
    public Page()
    {
        Items=new List<T>();
    }

    public Page(int itemCount, int number, int size)
    {
        ItemCount = itemCount;
        Number = number;
        Size = size;
        Items = new List<T>();
    }

    public Page(int itemCount, int number, int size, List<T> items)
    {
        ItemCount = itemCount;
        Number = number;
        Size = size;
        Items = items;
    }

    public List<T> Items { get; set; }
}