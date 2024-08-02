using LC.Infrastructure._Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LC.Infrastructure.Authenticate
{
    /// <summary>
    /// 功能基礎設施
    /// </summary>
    public class FeatureInfrastructure : AbstractFeatureInfrastructure
    {
        /// <inheritdoc />
        public override InfrastructureEnum Name => InfrastructureEnum.Authentication;

        /// <inheritdoc />
        public override void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtSetting:SignKey").Value!));
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        // 透過這項宣告，就可以從 "NAME" 取值
                        NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                        // 透過這項宣告，就可以從 "Role" 取值，並可讓 [Authorize] 判斷角色
                        RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                        // 接收者
                        ValidateAudience = false,
                        // 簽發者
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration.GetSection("JwtSetting:Issuer").Value!,
                        // Token有效時間，驗證過期時間
                        ValidateLifetime = true,
                        // 如果 Token 中包含 key 才需要驗證，一般都只有簽章而已
                        ValidateIssuerSigningKey = false,
                        // key
                        IssuerSigningKey = key,
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }

        /// <inheritdoc />
        public override void Configure(WebApplication app)
        {
            app.UseAuthentication();
        }
    }
}
