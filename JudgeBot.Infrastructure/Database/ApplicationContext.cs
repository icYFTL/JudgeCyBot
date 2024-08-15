using System.Reflection;
using JudgeBot.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JudgeBot.Infrastructure.Database;

public sealed partial class ApplicationContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<User> Users { get; init; }
    public DbSet<Act> Acts { get; init; }
    public DbSet<ActResolution> ActResolutions { get; init; }
    public DbSet<JuryToAct> JuryToActs { get; init; }
    public DbSet<Status> Statuses { get; init; }
    public DbSet<StatusType> StatusTypes { get; init; }
    public DbSet<Role> Roles { get; init; }
    public DbSet<Permission> Permissions { get; init; }
    public DbSet<UserInRole> UserInRoles { get; init; }
    public DbSet<PermissionInRole> PermissionInRoles { get; init; }


    public ApplicationContext(IConfiguration configuration)
    {
        _configuration = configuration;
        if (Database.EnsureCreated())
            _fillEmptyDatabase();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Postgres"));
    }

    private void _fillEmptyDatabase()
    {
        _fillStatuses();
        _fillRolesAndPermissions();
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}