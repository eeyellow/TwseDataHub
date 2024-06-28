using LC.Infra._Base;
using LC.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LC.Infra.Database
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class MainFeatureInfra : AbstractFeatureInfra
    {
        /// <inheritdoc />
        public override InfraEnum Name => InfraEnum.MainDatabase;

        /// <inheritdoc />
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services
                .AddDbContext<DatabaseContext>(options =>
                {
                    options
                        .UseNpgsql(builder.Configuration.GetConnectionString("MainDatabase"))
                        .UseSnakeCaseNamingConvention();
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
