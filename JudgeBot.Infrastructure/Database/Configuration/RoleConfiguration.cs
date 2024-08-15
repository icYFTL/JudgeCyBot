using JudgeBot.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JudgeBot.Infrastructure.Database.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> entity)
    {
        entity.HasKey(x => x.Uid);
        entity.ToTable("roles");

        entity.Property(x => x.Uid)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("uid");
        entity.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(50);
        entity.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(3000);
    }
}