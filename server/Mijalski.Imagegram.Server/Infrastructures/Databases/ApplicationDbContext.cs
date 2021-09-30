using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mijalski.Imagegram.Server.Infrastructures.Generics;
using Mijalski.Imagegram.Server.Modules.Accounts.Databases;
using Mijalski.Imagegram.Server.Modules.Comments.Databases;
using Mijalski.Imagegram.Server.Modules.Posts.Databases;

namespace Mijalski.Imagegram.Server.Infrastructures.Databases;

class ApplicationDbContext : IdentityDbContext<DbAccount, IdentityRole<Guid>, Guid>
{
    public DbSet<DbAccount> Accounts => Set<DbAccount>();
    public DbSet<DbPost> Posts => Set<DbPost>();
    public DbSet<DbComment> Comments => Set<DbComment>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges()
    {
        SetAuditedColumns();

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetAuditedColumns();

        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetAuditedColumns()
    {
        var entriesCreated = ChangeTracker
            .Entries()
            .Where(e => e.Entity is ICreationAudited && e.State == EntityState.Added);

        foreach (var entityEntry in entriesCreated)
        {
            ((ICreationAudited)entityEntry.Entity).CreationDateTime = DateTime.UtcNow; // TODO Postgre does not support DateTimeOffset 
        }
    }
}