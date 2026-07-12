using Aperture.Models;
using Microsoft.EntityFrameworkCore;

namespace Aperture.Data;

public static class QueryableExtensions
{
    public static Page<T> ToPage<T>(this IQueryable<T> source, int page, int size)
        where T : class
    {
        var count = source.Count();
        var items = source.Skip((page - 1) * size).Take(size).ToList();
        return new Page<T>(size, page, items, count);
    }

    public static async Task<Page<T>> ToPageAsync<T>(
        this IQueryable<T> source,
        int page,
        int size,
        CancellationToken cancellationToken = default)
        where T : class
    {
        var count = await source.CountAsync(cancellationToken);
        var items = await source.Skip((page - 1) * size).Take(size).ToListAsync(cancellationToken);
        return new Page<T>(size, page, items, count);
    }
}
