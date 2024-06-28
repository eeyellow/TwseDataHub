using LC.Infra.AppSettings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;

namespace TwseDataHub.Controllers
{
    /// <summary>
    /// Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : BaseController<TestController>
    {


        public TestController(ILogger<TestController> logger,
                              IDiagnosticContext diagnosticContext,
                              IWebHostEnvironment webHostEnvironment,
                              IOptions<AppSettingsOptions> options) 
            : base(logger, diagnosticContext, webHostEnvironment, options)
        {

        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(123);
        }

        //[HttpGet]
        //[Route("TestAuth")]
        //[Authorize]
        //public IActionResult TestAuth()
        //{
        //    return Ok("TestAuth Success");
        //}

        //[HttpGet]
        //[Route("Error")]
        //public IActionResult Error()
        //{
        //    throw new NotImplementedException("未實現");
        //}
    }
}
