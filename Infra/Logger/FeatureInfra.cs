using LC.Infra._Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL;
using Serilog.Ui.PostgreSqlProvider;
using Serilog.Ui.Web;

namespace LC.Infra.Logger
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class FeatureInfra : AbstractFeatureInfra
    {
        /// <inheritdoc />
        public override InfraEnum Name => InfraEnum.Logger;

        private const string connectionstring = "LogDatabase";
        private const string tableName = "logs";
        /// <inheritdoc />
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog();

            builder.Services.AddSerilogUi(options =>
                //options.UseNpgSql(builder.Configuration.GetConnectionString("LogSqlServer"), "Logs", "dbo")
                options.UseNpgSql(
                    PostgreSqlSinkType.SerilogSinksPostgreSQL,
                    builder.Configuration.GetConnectionString(connectionstring),
                    tableName
                )
            );
        }

        /// <inheritdoc />
        public override void Configure(WebApplication app)
        {
            app.UseSerilogUi(options =>
            {
                options.HomeUrl = "/";
                options.RoutePrefix = "SeriLogs";
            });



            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(app.Configuration)
            //    .Filter.ByExcluding("RequestPath = '/'")
            //    .Filter.ByExcluding("RequestPath = '/index.html'")
            //    .Filter.ByExcluding("RequestPath = '/favicon.png'")
            //    .Filter.ByExcluding("RequestPath like '%_framework%'")
            //    .Filter.ByExcluding("RequestPath like '/SeriLogs%'")
            //    .Filter.ByExcluding("RequestPath = '/Swagger/index.html'")
            //    .Filter.ByExcluding("RequestPath = '/Swagger/v1/swagger.json'")
            //    .CreateLogger();

            var columnWriters = new Dictionary<string, ColumnWriterBase>
            {
                {"message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
                {"message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
                {"level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
                {"raise_date", new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
                {"exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
                {"properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) },
                {"props_test", new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
                {"machine_name", new SinglePropertyColumnWriter("MachineName", PropertyWriteMethod.ToString, NpgsqlDbType.Text, "l") }
            };

            Log.Logger = new LoggerConfiguration()
                    .WriteTo.PostgreSQL(
                        app.Configuration.GetConnectionString(connectionstring) , 
                        tableName, 
                        columnWriters
                    )
                    .CreateLogger();
        }
    }
}
