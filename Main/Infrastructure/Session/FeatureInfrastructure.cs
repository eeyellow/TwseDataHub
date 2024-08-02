using LC.Infrastructure._Base;

namespace LC.Infrastructure.Session
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class FeatureInfrastructure : AbstractFeatureInfrastructure
    {
        /// <inheritdoc />
        public override InfrastructureEnum Name => InfrastructureEnum.Session;

        /// <inheritdoc />
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            //從組態讀取逾時時間設定
            var _cookieName = builder.Configuration.GetValue<string>("SystemName");
            var _cookieExpireMinute = builder.Configuration.GetValue<double>("CookieExpireMinute");
            var _sessionExpireMinute = builder.Configuration.GetValue<double>("SessionExpireMinute");

            builder.Services
                .AddSession(option =>
                {
                    option.Cookie.IsEssential = true;
                    //option.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    option.Cookie.Name = _cookieName;
                    option.IdleTimeout = TimeSpan.FromMinutes(_sessionExpireMinute);   //設定session能活多久
                });
        }
    }
}
