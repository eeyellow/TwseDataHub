using LC.Models.Contexts;

namespace DataShareHub.Services
{
    public class AuthResponse
    {
        public string Name { get; set; }
        public string? AccessToken { get; set; }
    }

    public interface IJwtService
    {
        /// <summary>
        /// 驗證登入者
        /// </summary>
        /// <param name="vm_login">登入資訊</param>
        /// <returns>驗證結果</returns>
        AuthResponse Authenticate(VM_Login vm_login);

        /// <summary>
        /// 建立JWT Token
        /// </summary>
        /// <param name="claims">聲明</param>
        /// <returns>JWT Token</returns>
        public string GenerateAccessToken(IEnumerable<Claim> claims);
    }

    public class JwtService : IJwtService
    {
        DatabaseContext _context;
        IConfiguration _config;

        /// <summary>
        /// JwtService 建構子
        /// </summary>
        /// <param name="context">資料庫上下文</param>
        /// <param name="config">設定檔</param>
        public JwtService(DatabaseContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        /// <summary>
        /// 驗證登入者
        /// </summary>
        /// <param name="vm_login">登入資訊</param>
        /// <returns>驗證結果</returns>
        public AuthResponse Authenticate(VM_Login vm_login)
        {
            var user = _context.UserProfile.Where(x => !x.IsDelete && x.Account == vm_login.Account && x.Password == vm_login.Password).FirstOrDefault();
            if (user is null)
            {
                throw new Exception("未授權帳號");
            }

            var clames = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Name),
                };
            var accessToken = GenerateAccessToken(clames);

            return new AuthResponse
            {
                Name = user.Name,
                AccessToken = accessToken
            };
        }

        /// <summary>
        /// 建立JWT Token
        /// </summary>
        /// <param name="claims">聲明</param>
        /// <returns>JWT Token</returns>
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            //獲取 SecurityKey
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JwtSetting:SignKey").Value!));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                //籤發者
                Issuer = _config.GetSection("JwtSetting:Issuer").Value!,
                //用戶
                Audience = "DataShareHubUser",
                Expires = DateTime.Now.AddSeconds(_config.GetValue<int>("JwtSetting:ExpireSeconds")),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }
    }
}
