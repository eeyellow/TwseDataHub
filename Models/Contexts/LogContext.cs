using Microsoft.EntityFrameworkCore;
using LC.Models.Entities;

namespace LC.Models.Contexts
{
    public partial class LogContext : DbContext
    {
        public LogContext(DbContextOptions<LogContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}