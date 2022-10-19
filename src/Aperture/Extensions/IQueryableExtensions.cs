using Aperture.Models;

namespace Aperture.Extensions;

public static class IQueryableExtensions
{
    public static Page<TEntity> ToPage<TEntity>(this IQueryable<TEntity> entities, int page, int size)
        where TEntity : class, new()
    {
        return PageFilter<TEntity>.ApplyPaging(entities, page, size);
    }

    public static Task<Page<TEntity>> ToPageAsync<TEntity>(this IQueryable<TEntity> entities, int page, int size)
        where TEntity : class, new()
    {
        return PageFilter<TEntity>.ApplyPagingAsync(entities, page, size);
    }

    public static Page<TEntity> ToPage<TEntity>(this IQueryable<TEntity> entities, PageFilter filter)
        where TEntity : class, new()
    {
        return PageFilter<TEntity>.ApplyPaging(entities, filter.PageNumber, filter.PageSize);
    }

    public static Task<Page<TEntity>> ToPageAsync<TEntity>(this IQueryable<TEntity> entities, PageFilter filter)
        where TEntity : class, new()
    {
        return PageFilter<TEntity>.ApplyPagingAsync(entities, filter.PageNumber, filter.PageSize);
    }
}