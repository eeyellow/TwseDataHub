using Microsoft.EntityFrameworkCore;

namespace LC.Models.Contexts
{
    public partial class LoggerContext : DbContext
    {        
        public LoggerContext(DbContextOptions<LoggerContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
