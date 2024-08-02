using LC.Infrastructure._Base;
using Serilog;
using System.Reflection;

namespace LC.Infrastructure
{
    /// <summary>
    /// 基礎設施容器
    /// </summary>
    public class ContainerInfrastructure
    {
        private List<InfrastructureEnum> _infraList = [];
        
        /// <summary>
        /// 加入基礎設施
        /// </summary>
        public ContainerInfrastructure AddInfrastructure(InfrastructureEnum infrastructure)
        {
            _infraList.Add(infrastructure);
            return this;
        }

        /// <summary>
        /// 運行
        /// </summary>
        /// <param name="args"></param>
        public void Run(string[] args)
        {
            try
            {
                var builder = WebApplication
                    .CreateBuilder(args);

                //取得目前正在執行之程式碼的組件
                var assembly = Assembly.GetExecutingAssembly();
                var infraAssembly = assembly.GetTypes()
                    .Where(t => t.Name.EndsWith("FeatureInfrastructure") && !t.IsInterface && !t.IsAbstract)
                    .ToList();

                var instanceContainer = new List<AbstractFeatureInfrastructure>();
                foreach(var infra in infraAssembly)
                {
                    var instance = Activator.CreateInstance(infra) as AbstractFeatureInfrastructure;
                    if (_infraList.Contains(instance.Name))
                    {
                        instanceContainer.Add(instance);
                    }
                }

                //排序Infrastructure順序
                instanceContainer = instanceContainer.OrderBy(x => x.Name).ToList();

                foreach (var instance in instanceContainer)
                {
                    instance.ConfigureServices(builder);
                }

                //=========================================

                var app = builder.Build();

                foreach (var instance in instanceContainer)
                {
                    instance.Configure(app);
                }

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
