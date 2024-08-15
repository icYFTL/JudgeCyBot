using JudgeBot.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JudgeBot.Infrastructure.Database.Configuration;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> entity)
    {
        entity.HasKey(x => x.Uid);
        entity.ToTable("permissions");

        entity.Property(x => x.Uid)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("uid");
        entity.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(50);
        entity.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(3000);
        
        entity.HasMany(x => x.Roles)
            .WithMany(x => x.Permissions)
            .UsingEntity<PermissionInRole>();
    }
}