using Autofac;
using Autofac.Extensions.DependencyInjection;
using LC.Infra._Base;

namespace LC.Infra.DI
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class FeatureInfra : AbstractFeatureInfra
    {
        /// <inheritdoc />
        public override InfraEnum Name => InfraEnum.DI;

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
