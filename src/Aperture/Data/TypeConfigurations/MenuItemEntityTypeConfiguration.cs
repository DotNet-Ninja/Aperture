using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aperture.Data.TypeConfigurations;

public class MenuItemEntityTypeConfiguration: IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.HasKey(m => m.Id).IsClustered().HasName("PK_MenuItem");
        builder.Property(m => m.Id).ValueGeneratedOnAdd();
        builder.Property(m => m.Text).IsRequired().HasMaxLength(128);
        builder.Property(m => m.Href).IsRequired().HasMaxLength(512);
        builder.Property(m => m.SortIndex).IsRequired();

        builder.HasMany(m => m.Children).WithOne().HasForeignKey(m => m.ParentId);
    }   
}