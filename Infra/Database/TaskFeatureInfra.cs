using LC.Infra._Base;
using LC.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LC.Infra.Database
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class TaskFeatureInfra : AbstractFeatureInfra
    {
        /// <inheritdoc />
        public override InfraEnum Name => InfraEnum.TaskDatabase;

        /// <inheritdoc />
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services
                .AddDbContext<TaskContext>(options =>
                {
                    options
                        .UseNpgsql(builder.Configuration.GetConnectionString("TaskDatabase"))
                        .UseSnakeCaseNamingConvention();
                });
        }

        /// <inheritdoc />
        public override void Configure(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<TaskContext>();
                //確認資料庫建立
                db.Database.EnsureCreated();
            }
        }
    }
}
