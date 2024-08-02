using LC.Infrastructure._Base;

namespace LC.Infrastructure.HealthCheck
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class FeatureInfrastructure : AbstractFeatureInfrastructure
    {
        /// <inheritdoc />
        public override InfrastructureEnum Name => InfrastructureEnum.HealthCheck;

        /// <inheritdoc />
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services
                .AddHealthChecks();
        }
    }
}
