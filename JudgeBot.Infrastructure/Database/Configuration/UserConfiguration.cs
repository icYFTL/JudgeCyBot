using JudgeBot.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JudgeBot.Infrastructure.Database.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(x => x.Id);
        entity.ToTable("users");

        entity.Property(x => x.Id)
            .HasColumnName("id");
        entity.Property(x => x.MenuMessageId)
            .HasColumnName("menu_message_id");
        entity.Property(x => x.LanguageId)
            .HasColumnName("language_id")
            .HasDefaultValue("ru");

        entity.HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity<UserInRole>();
    }
}