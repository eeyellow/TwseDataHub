using Autofac;
using System.Reflection;

namespace LC.Infrastructure.DI
{
    /// <summary>
    /// Autofac Module
    /// </summary>
    public class AutofacModule : Autofac.Module
    {
        /// <summary>
        /// 註冊 至 Container
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            //取得目前正在執行之程式碼的組件
            var assembly = Assembly.GetExecutingAssembly();

            //使用 Assembly 掃描並註冊
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            //使用 Assembly 掃描並註冊
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            //使用 Assembly 掃描並註冊
            builder.RegisterAssemblyTypes(assembly)
                   .Where(t => t.Name.EndsWith("HangfireJob"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            // // https://graphql-dotnet.github.io/docs/getting-started/dependency-injection/
            // builder
            //         .Register(c => new FuncServiceProvider(c.Resolve<IComponentContext>().Resolve))
            //         .As<IServiceProvider>()
            //         .InstancePerDependency();
        }
    }
}
