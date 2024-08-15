using JudgeBot.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace JudgeBot.Infrastructure.Database.Repositories;

public class UserRepository(ApplicationContext db)
{
    public async Task<long?> AllocMagistrateAsync(CancellationToken cancellationToken)
    {
        return (await (from u in db.Users
            join a in db.Acts on u.Id equals a.MagistrateId
            group u by u.Id
            into g
            orderby g.Count() ascending 
            select new {Key = g.Key, Count = g.Count()}).FirstOrDefaultAsync(cancellationToken))?.Key;
    }

    public async Task<User?> GetUserByIdAsync(long userId, CancellationToken cancellationToken)
    {
        return await db.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
    }

    public async Task ChangeLanguageAsync(User user, string newLanguage, CancellationToken cancellationToken)
    {
        db.Attach(user);
        user.LanguageId = newLanguage;
        
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task<User> AddUserAsync(User user, CancellationToken cancellationToken)
    {
        await db.Users.AddAsync(user, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
        return user;
    }

    public async Task<bool> IsUserExistsAsync(long userId, CancellationToken cancellationToken)
    {
        return await db.Users.AnyAsync(x => x.Id == userId, cancellationToken);
    }
}