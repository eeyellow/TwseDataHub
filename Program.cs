using LC.Infra;
using LC.Models.Contexts;
using Microsoft.EntityFrameworkCore;

var container = new ContainerInfra();
container
    .AddInfrastructure(InfraEnum.DI)
    //.AddInfrastructure(InfraEnum.HealthCheck)
    //.AddInfrastructure(InfraEnum.AppSettings)
    //.AddInfrastructure(InfraEnum.Identity)
    //.AddInfrastructure(InfraEnum.Authentication)
    //.AddInfrastructure(InfraEnum.Authorization)
    .AddInfrastructure(InfraEnum.Route)
    //.AddInfrastructure(InfraEnum.Cors)
    //.AddInfrastructure(InfraEnum.Graphql)
    //.AddInfrastructure(InfraEnum.Hangfire)
    //.AddInfrastructure(InfraEnum.Swagger)
    //.AddInfrastructure(InfraEnum.Mvc)
    //.AddInfrastructure(InfraEnum.HttpClient)
    //.AddInfrastructure(InfraEnum.Session)
    //.AddInfrastructure(InfraEnum.Cache)
    //.AddInfrastructure(InfraEnum.MainDatabase)
    //.AddInfrastructure(InfraEnum.LogDatabase)
    //.AddInfrastructure(InfraEnum.TaskDatabase)
    .AddInfrastructure(InfraEnum.Logger)
    //.AddInfrastructure(InfraEnum.Files)
    //.AddInfrastructure(InfraEnum.Security)
    .Run(args);

//var builder = WebApplication.CreateBuilder(args);
//builder.Services
//    .AddDbContext<DatabaseContext>(options =>
//    {
//        options
//            .UseNpgsql(builder.Configuration.GetConnectionString("MainDatabase"))
//            .UseSnakeCaseNamingConvention();;
//    })
//    .AddDbContext<LogContext>(options =>
//    {
//        options
//            .UseNpgsql(builder.Configuration.GetConnectionString("LogDatabase"))
//            .UseSnakeCaseNamingConvention();;
//    })
//    .AddDbContext<TaskContext>(options =>
//    {
//        options
//            .UseNpgsql(builder.Configuration.GetConnectionString("TaskDatabase"))
//            .UseSnakeCaseNamingConvention(); ;
//    });

//var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var dbMain = scope.ServiceProvider.GetService<DatabaseContext>();
//    //確認資料庫建立
//    dbMain?.Database.EnsureCreated();

//    var dbLog = scope.ServiceProvider.GetService<LogContext>();
//    //確認資料庫建立
//    dbLog?.Database.EnsureCreated();

//    var taskLog = scope.ServiceProvider.GetService<TaskContext>();
//    //確認資料庫建立
//    taskLog?.Database.EnsureCreated();
//}

//app.MapGet("/", () => "Hello World!");

//app.Run();
