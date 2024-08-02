using Hangfire;
using Hangfire.Console;
using LC.Infrastructure._Base;

namespace LC.Infrastructure.Hangfire
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class FeatureInfrastructure : AbstractFeatureInfrastructure
    {
        /// <inheritdoc />
        public override InfrastructureEnum Name => InfrastructureEnum.Hangfire;

        /// <inheritdoc />
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services
                .AddHangfire(configuration => configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(builder.Configuration.GetConnectionString("TaskSqlServer"))
                    .UseConsole()
                )
                .AddHangfireServer();
        }

        /// <inheritdoc />
        public override void Configure(WebApplication app)
        {
            app.MapHangfireDashboard();
            app.UseHangfireDashboard("/Hangfires");

            // Hangfire註冊排程
            HangfireScheduler.RecurringTasks(app.Services);
        }
    }
}
