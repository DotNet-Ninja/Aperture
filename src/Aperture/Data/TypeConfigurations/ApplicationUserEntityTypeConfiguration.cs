using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aperture.Data.TypeConfigurations;

public class ApplicationUserEntityTypeConfiguration: IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(b=>b.Id).IsClustered().HasName("PK_ApplicationUser");
        builder.Property(b=>b.DisplayName).IsRequired().HasMaxLength(64);
        builder.Property(b=>b.Email).IsRequired().HasMaxLength(384);
        builder.Property(b=>b.Password).IsRequired().HasMaxLength(128);
        builder.Property(b=>b.AvatarId).IsRequired().HasMaxLength(128);

        builder.HasIndex(b => b.Email).IsUnique().HasDatabaseName("IX_ApplicationUser_Email");

        builder.HasMany(b=>b.Roles)
               .WithMany(b=>b.Users)
               .UsingEntity(j => j.ToTable("UserRoles"));
    }
}