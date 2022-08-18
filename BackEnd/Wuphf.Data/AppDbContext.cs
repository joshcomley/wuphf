using Microsoft.EntityFrameworkCore;
using Wuphf.Data.Models;

namespace Wuphf.Data;

public class AppDbContext : DbContext
{
    public DbSet<Server> Servers { get; set; } = null!;
    public DbSet<AuditLog> AuditLogs { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var maxNameLength = 50;
        modelBuilder.Entity<Server>()
            .Property(_ => _.Name)
            .HasMaxLength(maxNameLength);
        modelBuilder.Entity<Server>()
            .Property(_ => _.UserNameLastAcquired)
            .HasMaxLength(maxNameLength);
        modelBuilder.Entity<AuditLog>()
            .Property(_ => _.FromUserName)
            .HasMaxLength(maxNameLength);
        modelBuilder.Entity<AuditLog>()
            .Property(_ => _.ToUserName)
            .HasMaxLength(maxNameLength);
        base.OnModelCreating(modelBuilder);
    }
}