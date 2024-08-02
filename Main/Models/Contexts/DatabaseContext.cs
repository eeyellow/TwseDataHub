using LC.Infrastructure.Database.Interface;
using LC.Infrastructure.Database.Extensions;
using LC.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LC.Models.Contexts
{
    public partial class DatabaseContext : IdentityDbContext<IdentityUser>
    {
        private static readonly bool[] MigratedRecord = { false };
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<County> County { get; set; }
        public virtual DbSet<Town> Town { get; set; }
        public virtual DbSet<Village> Village { get; set; }

        public virtual DbSet<Stocks> Stocks { get; set; }
        public virtual DbSet<StockDaily> StockDaily { get; set; }

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

            // Infrastructure/Database/Extensions
            SoftDeleteQueryFilter.Apply(modelBuilder, typeof(IDelete));
            CustomDataTypeAttributeConvention.Apply(modelBuilder);
            DecimalPrecisionAttributeConvention.Apply(modelBuilder);
            SqlDefaultValueAttributeConvention.Apply(modelBuilder);

            // Infrastructure/Seeds
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());            
        }
        
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    } 
}
