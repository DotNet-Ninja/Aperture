namespace Aperture.Models;

public class Page<T> where T : class
{
    public Page(int size, int number, IEnumerable<T> items, int totalCount)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(size, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(number, 1);
        Number = number;
        Size = size;
        Items = items.ToList();
        TotalCount = totalCount;
        PageCount = (int)Math.Ceiling(totalCount / (double)size);
    }

    public int Number { get; }
    public int Size { get; }
    public int PageCount { get; }
    public int TotalCount { get; }
    public bool CanGoForward => Number < PageCount;
    public bool CanGoBack => Number > 1;
    public IReadOnlyList<T> Items { get; }
}
