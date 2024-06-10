using Microsoft.EntityFrameworkCore;
using LC.Models.Entities;

namespace LC.Models.Contexts
{
    public partial class DatabaseContext : DbContext
    {
        private static readonly bool[] MigratedRecord = { false };
        public virtual DbSet<Stocks> Stocks { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            // 自動更新資料庫結構
            if (!MigratedRecord[0])
            {
                lock (MigratedRecord)
                {
                    if (!MigratedRecord[0])
                    {
                        this.Database.Migrate();
                        MigratedRecord[0] = true;
                    }
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}