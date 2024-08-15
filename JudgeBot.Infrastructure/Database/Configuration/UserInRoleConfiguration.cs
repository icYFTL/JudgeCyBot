using JudgeBot.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JudgeBot.Infrastructure.Database.Configuration;

public class UserInRoleConfiguration : IEntityTypeConfiguration<UserInRole>
{
    public void Configure(EntityTypeBuilder<UserInRole> entity)
    {
        entity.HasKey(x => x.Uid);
        entity.ToTable("user_in_role");

        entity.Property(x => x.Uid)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("uid");
        entity.Property(x => x.RoleUid)
            .HasColumnName("role_uid");
        entity.Property(x => x.UserId)
            .HasColumnName("user_id");
    }
}