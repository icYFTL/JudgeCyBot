using JudgeBot.Core.Models;

namespace JudgeBot.Infrastructure.Database;

public sealed partial class ApplicationContext
{
    private void _fillStatuses()
    {
        var actStatusType = new StatusType
        {
            Name = "Act statuses",
            Uid = new Guid("00000000-0000-0000-0000-000000000001")
        };
        StatusTypes.Add(actStatusType);

        SaveChanges();

        Statuses.Add(new Status
        {
            Name = "Created",
            Description = "Act created, but not started",
            StatusTypeUid = actStatusType.Uid,
            Uid = new Guid("00000000-0000-0000-0000-000000000001")
        });
        
        Statuses.Add(new Status
        {
            Name = "In progress",
            Description = "Act in progress. There's no any resolution",
            StatusTypeUid = actStatusType.Uid,
            Uid = new Guid("00000000-0000-0000-0000-000000000002")
        });
        
        Statuses.Add(new Status
        {
            Name = "Declined",
            Description = "Act declined",
            StatusTypeUid = actStatusType.Uid,
            Uid = new Guid("00000000-0000-0000-0000-000000000003")
        });
        
        Statuses.Add(new Status
        {
            Name = "Closed",
            Description = "Act closed with resolution",
            StatusTypeUid = actStatusType.Uid,
            Uid = new Guid("00000000-0000-0000-0000-000000000004")
        });

        SaveChanges();
    }

    private void _fillRolesAndPermissions()
    {
        Roles.Add(new Role
        {
            Uid = new Guid("00000000-0000-0000-0000-000000000001"),
            Name = "Administrator"
        });
        
        Roles.Add(new Role
        {
            Uid = new Guid("00000000-0000-0000-0000-000000000002"),
            Name = "Magistrate",
            Description = "Magistrate in an act"
        });

        SaveChanges();
    }
}