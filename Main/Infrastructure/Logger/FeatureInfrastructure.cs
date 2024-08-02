using LC.Infrastructure._Base;
using Serilog;
using Serilog.Ui.MsSqlServerProvider;
using Serilog.Ui.Web;

namespace LC.Infrastructure.Logger
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class FeatureInfrastructure : AbstractFeatureInfrastructure
    {
        /// <inheritdoc />
        public override InfrastructureEnum Name => InfrastructureEnum.Logger;

        /// <inheritdoc />
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog();

            builder.Services.AddSerilogUi(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("LogSqlServer"), "Logs", "dbo")
            );
        }

        /// <inheritdoc />
        public override void Configure(WebApplication app)
        {
            app.UseSerilogRequestLogging(options =>
            {
                // 如果要自訂訊息的範本格式，可以修改這裡，但修改後並不會影響結構化記錄的屬性
                //options.MessageTemplate = "Handled {RequestPath}";

                // 預設輸出的紀錄等級為 Information，你可以在此修改記錄等級
                // options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

                // 你可以從 httpContext 取得 HttpContext 下所有可以取得的資訊！
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                    diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                    diagnosticContext.Set("UserID", httpContext.User.Identity?.Name);
                };
            }).UseSerilogUi(options =>
            {
                options.HomeUrl = "/";
                options.RoutePrefix = "SeriLogs";
            });



            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(app.Configuration)
                .Filter.ByExcluding("RequestPath = '/'")
                .Filter.ByExcluding("RequestPath = '/index.html'")
                .Filter.ByExcluding("RequestPath = '/favicon.png'")
                .Filter.ByExcluding("RequestPath like '%_framework%'")
                .Filter.ByExcluding("RequestPath like '/SeriLogs%'")
                .Filter.ByExcluding("RequestPath = '/Swagger/index.html'")
                .Filter.ByExcluding("RequestPath = '/Swagger/v1/swagger.json'")
                .CreateLogger();
        }
    }
}
