using LC.Infrastructure._Base;

namespace LC.Infrastructure.Cors
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class FeatureInfrastructure : AbstractFeatureInfrastructure
    {
        /// <inheritdoc />
        public override InfrastructureEnum Name => InfrastructureEnum.Cors;

        /// <inheritdoc />
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services
                .AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        policy =>
                        {
                            var allowHosts = builder.Configuration.GetSection("AllowHosts").Get<string[]>() ?? new List<string>().ToArray();
                            if (allowHosts.Any())
                            {
                                policy.WithOrigins(allowHosts);
                            }
                            else
                            {
                                policy.AllowAnyOrigin();
                            }
                            policy.AllowAnyMethod().AllowAnyHeader();
                        });
                });
        }

        /// <inheritdoc />
        public override void Configure(WebApplication app)
        {
            app.UseCors();
        }
    }
}
