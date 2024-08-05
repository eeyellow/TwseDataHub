using LC.Infrastructure.AppSettings;

namespace TwseDataHub.Controllers
{
    /// <summary>
    /// Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : BaseController<TestController>
    {
        private readonly IJwtService _jwtService;
        private readonly ITestService _testService;

        public TestController(ILogger<TestController> logger,
                              IDiagnosticContext diagnosticContext,
                              IWebHostEnvironment webHostEnvironment,
                              IOptions<AppSettingsOptions> options,
                              IJwtService jwtService,
                              ITestService testService) : base(logger, diagnosticContext, webHostEnvironment, options)
        {
            _jwtService = jwtService;
            _testService = testService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_testService.GetTestNumber());
        }

        [HttpGet]
        [Route("TestAuth")]
        [Authorize]
        public IActionResult TestAuth()
        {
            return Ok("TestAuth Success");
        }

        [HttpGet]
        [Route("Error")]
        public IActionResult Error()
        {
            throw new NotImplementedException("未實現");
        }
    }
}
