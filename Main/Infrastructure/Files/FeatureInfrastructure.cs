using LC.Infrastructure._Base;

namespace LC.Infrastructure.Files
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class FeatureInfrastructure : AbstractFeatureInfrastructure
    {
        /// <inheritdoc />
        public override InfrastructureEnum Name => InfrastructureEnum.Files;

        /// <inheritdoc />
        public override void Configure(WebApplication app)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
