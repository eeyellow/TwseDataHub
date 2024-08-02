using LC.Infrastructure._Base;
using LC.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LC.Infrastructure.Database
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class MainFeatureInfrastructure : AbstractFeatureInfrastructure
    {
        /// <inheritdoc />
        public override InfrastructureEnum Name => InfrastructureEnum.MainDatabase;

        /// <inheritdoc />
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services
                .AddDbContext<DatabaseContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("MainSqlServer"),
                        x => x.UseNetTopologySuite().UseDateOnlyTimeOnly()
                    );
                });
        }

        /// <inheritdoc />
        public override void Configure(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<DatabaseContext>();
                //確認資料庫建立
                db.Database.EnsureCreated();
            }
        }
    }
}
