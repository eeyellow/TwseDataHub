using Autofac;
using Autofac.Extensions.DependencyInjection;
using LC.Infrastructure._Base;

namespace LC.Infrastructure.DI
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class FeatureInfrastructure : AbstractFeatureInfrastructure
    {
        /// <inheritdoc />
        public override InfrastructureEnum Name => InfrastructureEnum.DI;

        /// <inheritdoc />
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(
                    builder => builder.RegisterModule(new AutofacModule())
                );
        }
    }
}
