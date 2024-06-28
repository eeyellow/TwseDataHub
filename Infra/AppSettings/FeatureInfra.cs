using LC.Infra._Base;

namespace LC.Infra.AppSettings
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class FeatureInfra : AbstractFeatureInfra
    {
        /// <inheritdoc />
        public override InfraEnum Name => InfraEnum.AppSettings;

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
