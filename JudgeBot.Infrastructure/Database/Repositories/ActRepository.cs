using System.Linq.Expressions;
using JudgeBot.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace JudgeBot.Infrastructure.Database.Repositories;

public class ActRepository(ApplicationContext db)
{
    public async Task<Act> CreateActAsync(Act act, CancellationToken cancellationToken)
    {
        db.Acts.Add(act);
        await db.SaveChangesAsync(cancellationToken);

        return act;
    }

    public async Task<List<Act>> GetUsersActsAsync(long userId, Expression<Func<Act, long?>> filterProperty)
    {
        var parameter = Expression.Parameter(typeof(Act), "x");
        var property = Expression.Invoke(filterProperty, parameter);
        var userIdValue = Expression.Constant(userId, typeof(long));
        var body = Expression.Equal(property, userIdValue);

        var predicate = Expression.Lambda<Func<Act, bool>>(body, parameter);
        
        return await db.Acts
            .OrderByDescending(x => x.CreatedAt)
            .Where(predicate)
            .ToListAsync();
    }
}