using JudgeBot.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JudgeBot.Infrastructure.Database.Configuration;

public class JuryToActConfiguration : IEntityTypeConfiguration<JuryToAct>
{
    public void Configure(EntityTypeBuilder<JuryToAct> entity)
    {
        entity.HasKey(x => x.Uid);
        entity.ToTable("jury_to_act");

        entity.Property(x => x.Uid)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("uid");
        entity.Property(x => x.ActUid)
            .HasColumnName("act_uid");
        entity.Property(x => x.UserId)
            .HasColumnName("user_id");
    }
}