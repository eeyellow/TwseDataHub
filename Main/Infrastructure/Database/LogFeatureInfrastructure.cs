using LC.Infrastructure._Base;
using LC.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LC.Infrastructure.Database
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class LogFeatureInfrastructure : AbstractFeatureInfrastructure
    {
        /// <inheritdoc />
        public override InfrastructureEnum Name => InfrastructureEnum.LogDatabase;

        /// <inheritdoc />
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services
                .AddDbContext<LoggerContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("LogSqlServer"),
                        x => x.UseNetTopologySuite().UseDateOnlyTimeOnly()
                    );
                });
        }

        /// <inheritdoc />
        public override void Configure(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<LoggerContext>();
                //確認資料庫建立
                db.Database.EnsureCreated();
            }
        }
    }
}
