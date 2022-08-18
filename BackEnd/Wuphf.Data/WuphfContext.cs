using Microsoft.EntityFrameworkCore;
using Wuphf.Data.Models;

namespace Wuphf.Data
{
    public class WuphfContext : DbContext
    {
        public DbSet<Server> Servers { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}