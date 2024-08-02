using LC.Infrastructure.AppSettings;

namespace DataShareHub.Controllers
{
    /// <summary>
    /// 權限驗證 controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : BaseController<AuthenticateController>
    {
        private readonly IAuthenticateService _authenticateService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="diagnosticContext"></param>
        /// <param name="webHostEnvironment"></param>
        /// <param name="options"></param>
        /// <param name="authenticateService"></param>
        /// <returns></returns>
        public AuthenticateController(ILogger<AuthenticateController> logger,
                                      IDiagnosticContext diagnosticContext,
                                      IWebHostEnvironment webHostEnvironment,
                                      IOptions<AppSettingsOptions> options,
                                      IAuthenticateService authenticateService) : base(logger, diagnosticContext, webHostEnvironment, options)
        {
            _authenticateService = authenticateService;
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _authenticateService.LoginAsync(model);
            
            if (!string.IsNullOrEmpty(result))
            {    
                return Ok(result);
            }
            
            return Unauthorized();
        }

        /// <summary>
        /// 註冊
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _authenticateService.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            }

            var result = await _authenticateService.RegisterAsync(model);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = string.Join(",", errors) });
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        /// <summary>
        /// 註冊管理者
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _authenticateService.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });
            }

            var result = await _authenticateService.RegisterAsync(model);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = string.Join(",", errors) });
            }

            await _authenticateService.CreateRoleAsync();

            await _authenticateService.AddToRoleAsync(model.Username);

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
    }
}
