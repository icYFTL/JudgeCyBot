using JudgeBot.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JudgeBot.Infrastructure.Database.Configuration;

public class ActResolutionConfiguration : IEntityTypeConfiguration<ActResolution>
{
    public void Configure(EntityTypeBuilder<ActResolution> entity)
    {
        entity.HasKey(x => x.Uid);
        entity.ToTable("act_resolutions");

        entity.Property(x => x.Uid)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("uid");
        entity.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(50);
        entity.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(3000);
        entity.Property(x => x.IsPositive)
            .HasColumnName("is_positive");
    }
}