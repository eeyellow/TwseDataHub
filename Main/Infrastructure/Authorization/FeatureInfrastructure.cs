using LC.Infrastructure._Base;

namespace LC.Infrastructure.Authorization
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class FeatureInfrastructure : AbstractFeatureInfrastructure
    {
        /// <inheritdoc />
        public override InfrastructureEnum Name => InfrastructureEnum.Authorization;

        /// <inheritdoc />
        public override void Configure(WebApplication app)
        {
            app.UseAuthorization();
        }
    }
}
