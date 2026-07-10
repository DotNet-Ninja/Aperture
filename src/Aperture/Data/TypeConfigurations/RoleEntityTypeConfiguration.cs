using Aperture.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aperture.Data.TypeConfigurations;

public class RoleEntityTypeConfiguration: IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(b => b.Name).IsClustered().HasName("PK_Role");
        builder.Property(b => b.Name).IsRequired().HasMaxLength(64);

        builder.HasMany(b => b.Users)
               .WithMany(b => b.Roles)
               .UsingEntity(j => j.ToTable("UserRoles"));

        builder.HasData(
            new Role { Name = AppRoles.Owner },
            new Role { Name = AppRoles.Contributor },
            new Role { Name = AppRoles.FamilyMember },
            new Role { Name = AppRoles.Friend }
        );
    }
}