using LC.Infrastructure;

namespace DataShareHub
{
    /// <summary>Program</summary>
    public class Program
    {
        /// <summary>
        /// 程式進入點
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        { 

            var container = new ContainerInfrastructure();
            container
                .AddInfrastructure(InfrastructureEnum.DI)
                .AddInfrastructure(InfrastructureEnum.HealthCheck)
                .AddInfrastructure(InfrastructureEnum.AppSettings)
                .AddInfrastructure(InfrastructureEnum.Identity)
                .AddInfrastructure(InfrastructureEnum.Authentication)
                .AddInfrastructure(InfrastructureEnum.Authorization)
                .AddInfrastructure(InfrastructureEnum.Route)
                .AddInfrastructure(InfrastructureEnum.Cors)
                .AddInfrastructure(InfrastructureEnum.Graphql)
                .AddInfrastructure(InfrastructureEnum.Hangfire)
                .AddInfrastructure(InfrastructureEnum.Swagger)
                .AddInfrastructure(InfrastructureEnum.Mvc)
                .AddInfrastructure(InfrastructureEnum.HttpClient)
                .AddInfrastructure(InfrastructureEnum.Session)
                .AddInfrastructure(InfrastructureEnum.Cache)
                .AddInfrastructure(InfrastructureEnum.MainDatabase)
                .AddInfrastructure(InfrastructureEnum.LogDatabase)
                .AddInfrastructure(InfrastructureEnum.TaskDatabase)
                .AddInfrastructure(InfrastructureEnum.Logger)
                .AddInfrastructure(InfrastructureEnum.Files)
                .AddInfrastructure(InfrastructureEnum.Security)
                .Run(args);
        }
    }
}
