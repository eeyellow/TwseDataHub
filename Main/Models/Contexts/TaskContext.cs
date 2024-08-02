using Microsoft.EntityFrameworkCore;

namespace LC.Models.Contexts
{
    public partial class TaskContext : DbContext
    {        
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
