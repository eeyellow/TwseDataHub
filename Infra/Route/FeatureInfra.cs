using LC.Infra._Base;
using Newtonsoft.Json.Serialization;

namespace LC.Infra.Route
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class FeatureInfra : AbstractFeatureInfra
    {
        /// <inheritdoc />
        public override InfraEnum Name => InfraEnum.Route;

        /// <inheritdoc />
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services
                .AddMvc().AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            builder.Services
                .AddControllersWithViews()
                .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

            builder.Services
                .AddRazorPages()
                .AddRazorRuntimeCompilation()
                .AddMvcOptions(options =>
                {
                    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(
                        (x, y) => "此欄位格式錯誤");
                    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                        _ => "此欄位為必填欄位");
                });
        }

        /// <inheritdoc />
        public override void Configure(WebApplication app)
        {
            app.UseRouting();

            app.UseHttpsRedirection();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );
        }
    }
}
