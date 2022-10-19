using Microsoft.EntityFrameworkCore;

namespace Aperture.Models;

public class PageFilter
{
    public const string PageParameterName = "pagenumber";
    public const string SizeParameterName = "pagesize";

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 24;
}

public class PageFilter<TEntity>: PageFilter where TEntity: class, new()
{
    public Page<TEntity> Apply(IQueryable<TEntity> entities)
    {
        return ApplyPaging(entities, PageNumber, PageSize);
    }

    public async Task<Page<TEntity>> ApplyAsync(IQueryable<TEntity> entities)
    {
        return await ApplyPagingAsync(entities, PageNumber, PageSize);
    }

    public static Page<TEntity> ApplyPaging(IQueryable<TEntity> entities, int page, int size)
    {
        var items = entities.Skip((page - 1) * size).Take(size).ToList();
        var count = entities.Count();
        return new Page<TEntity>
        {
            Size = size,
            Number = page,
            ItemCount = count,
            Items = items
        };
    }

    public static async Task<Page<TEntity>> ApplyPagingAsync(IQueryable<TEntity> entities, int page, int size)
    {
        var items = await entities.Skip((page - 1) * size).Take(size).ToListAsync();
        var count = await entities.CountAsync();
        return new Page<TEntity>
        {
            Size = size,
            Number = page,
            ItemCount = count,
            Items = items
        };
    }
}



