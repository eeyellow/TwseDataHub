using LC.Infrastructure._Base;

namespace LC.Infrastructure.Route
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class FeatureInfrastructure : AbstractFeatureInfrastructure
    {
        /// <inheritdoc />
        public override InfrastructureEnum Name => InfrastructureEnum.Route;

        /// <inheritdoc />
        public override void Configure(WebApplication app)
        {
            app.UseRouting();

            app.UseHttpsRedirection();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );
        }
    }
}
