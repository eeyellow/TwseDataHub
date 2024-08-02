using LC.Infrastructure._Base;

namespace LC.Infrastructure.AppSettings
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class FeatureInfrastructure : AbstractFeatureInfrastructure
    {
        /// <inheritdoc />
        public override InfrastructureEnum Name => InfrastructureEnum.AppSettings;

        /// <inheritdoc />
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services
                .Configure<AppSettingsOptions>(
                    builder.Configuration.GetSection(AppSettingsOptions.AppSettings)
                );
        }
    }
}
