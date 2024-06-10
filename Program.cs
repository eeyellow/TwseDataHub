using LC.Models.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddDbContext<DatabaseContext>(options =>
    {
        options
            .UseNpgsql(builder.Configuration.GetConnectionString("MainSqlServer"))
            .UseSnakeCaseNamingConvention();;
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetService<DatabaseContext>();
    //確認資料庫建立
    db?.Database.EnsureCreated();
}

app.MapGet("/", () => "Hello World!");

app.Run();
