using LC.Infrastructure._Base;
using LC.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LC.Infrastructure.Database
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class TaskFeatureInfrastructure : AbstractFeatureInfrastructure
    {
        /// <inheritdoc />
        public override InfrastructureEnum Name => InfrastructureEnum.TaskDatabase;

        /// <inheritdoc />
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services
                .AddDbContext<TaskContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("TaskSqlServer"),
                        x => x.UseNetTopologySuite().UseDateOnlyTimeOnly()
                    );
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
