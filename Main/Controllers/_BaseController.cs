﻿using LC.Infrastructure.AppSettings;

namespace TwseDataHub.Controllers
{
    /// <summary>
    /// 基底控制器-介面
    /// </summary>
    public interface IBaseController
    {
       
    }
    
    /// <summary>
    /// 基底控制器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseController<T> : ControllerBase where T : BaseController<T>
    {
        private readonly ILogger<T> _logger;
        private readonly IDiagnosticContext _diagnosticContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IOptions<AppSettingsOptions> _options;

        /// <summary>
        /// 建構子
        /// </summary>
        public BaseController(ILogger<T> logger,
                              IDiagnosticContext diagnosticContext,
                              IWebHostEnvironment webHostEnvironment,
                              IOptions<AppSettingsOptions> options)
        {
            _logger = logger;
            _diagnosticContext = diagnosticContext;
            _webHostEnvironment = webHostEnvironment;
            _options = options;
        }
    }
}
