using JudgeBot.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JudgeBot.Infrastructure.Database.Configuration;

public class StatusTypeConfiguration : IEntityTypeConfiguration<StatusType>
{
    public void Configure(EntityTypeBuilder<StatusType> entity)
    {
        entity.HasKey(x => x.Uid);
        entity.ToTable("status_types");

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