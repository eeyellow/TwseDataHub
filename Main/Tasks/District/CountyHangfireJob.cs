using LC.Infrastructure.Hangfire.Interface;
using Hangfire.Server;
using LC.Models.Entities;

namespace DataShareHub.Tasks.District
{
    /// <summary>
    /// 縣市資料的 Hangfire 工作
    /// </summary>
    public class CountyHangfireJob : AHangfireJob<CountyHangfireJob>
    {
        /// <summary> 工作的唯一識別碼 </summary>
        public override string JobId => "0155a73c-882a-46ac-8a6d-f92630468813";
        /// <summary> 工作的 CRON 表達式 </summary>
        public override string CronExpression => "0 3 * * *";
        private readonly IDistrictCountyService<List<County>?> _service;
        private readonly ILogger<CountyHangfireJob> _logger;
        
        /// <summary> 建構式 </summary>
        public CountyHangfireJob(
            ILogger<CountyHangfireJob> logger,
            IDistrictCountyService<List<County>?> service)
        {
            _logger = logger;
            _service = service;
        }

        /// <inheritdoc />
        [DisplayName("更新縣市資料")]
        public override async Task Execute(IJobCancellationToken cancellationToken, PerformContext context)
        {
            _cancellationToken = cancellationToken;
            _context = context;
            await PerformJobTasks();
        }

        /// <inheritdoc />
        protected override async Task PerformJobTasks()
        {
            await _service.ExecuteAsync();
        }

        
    }
}
