namespace TwseDataHub.Services
{
    /// <summary>
    /// 身分驗證服務 - 介面
    /// </summary>
    public interface IAuthenticateService
    {
        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<string> LoginAsync(LoginModel model);

        /// <summary>
        /// 以使用者名稱尋找使用者
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<IdentityUser?> FindByNameAsync(string username);

        /// <summary>
        /// 註冊
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IdentityResult> RegisterAsync(RegisterModel model);

        /// <summary>
        /// 建立角色
        /// </summary>
        /// <returns></returns>
        Task CreateRoleAsync();

        /// <summary>
        /// 將使用者加入角色
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task AddToRoleAsync(string username);
    }

    /// <summary>
    /// 身分驗證服務
    /// </summary>
    public class AuthenticateService : IAuthenticateService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IJwtService _jwtService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="configuration"></param>
        /// <param name="jwtService"></param>
        public AuthenticateService(UserManager<IdentityUser> userManager,
                                   RoleManager<IdentityRole> roleManager,
                                   IConfiguration configuration,
                                   IJwtService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _jwtService = jwtService;
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> LoginAsync(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

               var token = _jwtService.GenerateAccessToken(authClaims);

               return token;
            }

            return string.Empty;
        }

        /// <summary>
        /// 以使用者名稱尋找使用者
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<IdentityUser?> FindByNameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        /// <summary>
        /// 註冊
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IdentityResult> RegisterAsync(RegisterModel model)
        {
            var user = new IdentityUser
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            return result;
        }

        /// <summary>
        /// 建立角色
        /// </summary>
        /// <returns></returns>
        public async Task CreateRoleAsync()
        {            
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }
        }

        /// <summary>
        /// 將使用者加入角色
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task AddToRoleAsync(string username)
        {
            var user = await FindByNameAsync(username);

            if (user == null)
            {
                return;
            }
            
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
        }
    }
}
