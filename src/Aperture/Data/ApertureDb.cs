using Microsoft.EntityFrameworkCore;

namespace Aperture.Data;

public class ApertureDb: DbContext
{
    public ApertureDb(DbContextOptions<ApertureDb> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyTypeConfigurationsFromAssemblyOf<ApertureDb>();
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<MenuItem> MenuItems => Set<MenuItem>();

    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();

    public DbSet<Role> Roles => Set<Role>();

    public async Task SeedDataAsync()
    {
        if (!MenuItems.Any())
        {
            var menuItems = new List<MenuItem>
            {
                new MenuItem { Text = "Home", Href = "/", SortIndex = 1 },
                new MenuItem { Text = "Posts", Href = "/posts", SortIndex = 2 },
                new MenuItem { Text = "Galleries", Href = "/galleries", SortIndex = 3 },
                new MenuItem { Text = "About", Href = "/about", SortIndex = 4, Children = new List<MenuItem> 
                {
                    new MenuItem { Text = "Contact", Href = "/about/contact", SortIndex = 1}
                }},
            };
            MenuItems.AddRange(menuItems);
            await SaveChangesAsync();
        }
    }
}