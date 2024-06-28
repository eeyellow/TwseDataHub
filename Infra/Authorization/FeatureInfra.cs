using LC.Infra._Base;

namespace LC.Infra.Authorization
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class FeatureInfra : AbstractFeatureInfra
    {
        /// <inheritdoc />
        public override InfraEnum Name => InfraEnum.Authorization;

        /// <inheritdoc />
        public override void Configure(WebApplication app)
        {
            app.UseAuthorization();
        }
    }
}
