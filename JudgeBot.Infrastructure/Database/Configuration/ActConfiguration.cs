using JudgeBot.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JudgeBot.Infrastructure.Database.Configuration;

public class ActConfiguration : IEntityTypeConfiguration<Act>
{
    public void Configure(EntityTypeBuilder<Act> entity)
    {
        entity.HasKey(x => x.Uid);
        entity.ToTable("acts");

        entity.Property(x => x.Uid)
            .HasDefaultValueSql("gen_random_uuid()")
            .HasColumnName("uid");
        entity.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(50);
        entity.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(3000);
        entity.Property(x => x.AccuserId)
            .HasColumnName("accuser_id");
        entity.Property(x => x.CreatedAt)
            .HasColumnName("created_at");
        entity.Property(x => x.EditedAt)
            .HasColumnName("edited_at");
        entity.Property(x => x.EditUserId)
            .HasColumnName("edit_user_id");
        entity.Property(x => x.CreateUserId)
            .HasColumnName("create_user_id");
        entity.Property(x => x.StatusUid)
            .HasColumnName("status_uid");
        entity.Property(x => x.DefendantId)
            .HasColumnName("defendant_id");
        entity.Property(x => x.MagistrateId)
            .HasColumnName("magistrate_id");
        entity.Property(x => x.VictimId)
            .HasColumnName("victim_id");
        entity.Property(x => x.ResolutionUid)
            .HasColumnName("resolution_uid");

        entity.HasMany(x => x.Jury)
            .WithMany(x => x.ActsAsJury)
            .UsingEntity<JuryToAct>();

        entity.HasOne(x => x.Accuser).WithMany(x => x.ActsAsAccuser).HasForeignKey(x => x.AccuserId);
        entity.HasOne(x => x.Defendant).WithMany(x => x.ActsAsDefendant).HasForeignKey(x => x.DefendantId);
        entity.HasOne(x => x.Magistrate).WithMany(x => x.ActsAsMagistrate).HasForeignKey(x => x.MagistrateId);
        entity.HasOne(x => x.Victim).WithMany(x => x.ActsAsVictim).HasForeignKey(x => x.VictimId);
        entity.HasOne(x => x.CreateUser).WithMany(x => x.ActsCreated).HasForeignKey(x => x.CreateUserId);
        entity.HasOne(x => x.EditUser).WithMany(x => x.ActsEdited).HasForeignKey(x => x.EditUserId);
    }
}