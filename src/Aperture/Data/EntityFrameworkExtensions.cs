using Microsoft.EntityFrameworkCore;

namespace Aperture.Data;

public static class EntityFrameworkExtensions
{
    public static ModelBuilder ApplyTypeConfigurationsFromAssemblyOf<T>(this ModelBuilder modelBuilder)
    {
        var assembly = typeof(T).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        return modelBuilder;
    }
}