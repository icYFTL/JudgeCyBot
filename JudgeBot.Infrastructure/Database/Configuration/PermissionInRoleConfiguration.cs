using JudgeBot.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JudgeBot.Infrastructure.Database.Configuration;

public class PermissionInRoleConfiguration : IEntityTypeConfiguration<PermissionInRole>
{
    public void Configure(EntityTypeBuilder<PermissionInRole> entity)
    {
        entity.HasKey(x => x.Uid);
        entity.ToTable("permission_in_role");

        entity.Property(x => x.Uid)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("uid");
        entity.Property(x => x.PermissionUid)
            .HasColumnName("permission_uid");
        entity.Property(x => x.RoleUid)
            .HasColumnName("role_uid");
    }
}