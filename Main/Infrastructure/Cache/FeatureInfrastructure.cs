using LC.Infrastructure._Base;

namespace LC.Infrastructure.Cache
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class FeatureInfrastructure : AbstractFeatureInfrastructure
    {
        /// <inheritdoc />
        public override InfrastructureEnum Name => InfrastructureEnum.Cache;

        /// <inheritdoc />
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services
                .AddDistributedMemoryCache();
        }

    }
}
