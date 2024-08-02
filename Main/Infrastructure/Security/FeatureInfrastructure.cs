using LC.Infrastructure._Base;

namespace LC.Infrastructure.Security
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class FeatureInfrastructure : AbstractFeatureInfrastructure
    {
        /// <inheritdoc />
        public override InfrastructureEnum Name => InfrastructureEnum.Security;

        /// <inheritdoc />
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            
        }

        /// <inheritdoc />
        public override void Configure(WebApplication app)
        {
            if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("DevelopmentDocker"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
        }
    }
}
