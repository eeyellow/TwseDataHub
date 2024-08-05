using LC.Infrastructure.Hangfire.Interface;
using Hangfire.Server;
using LC.Models.Entities;

namespace TwseDataHub.Tasks.District
{
    /// <summary>
    /// 鄉鎮市區資料的 Hangfire 工作
    /// </summary>
    public class TownHangfireJob : AHangfireJob<TownHangfireJob>
    {
        /// <summary> 工作的唯一識別碼 </summary>
        public override string JobId => "a4d0458b-bb34-4480-a706-f6ba59f8c7fd";
        /// <summary> 工作的 CRON 表達式 </summary>
        public override string CronExpression => "10 3 * * *";
        private readonly IDistrictTownService<List<Town>?> _service;
        private readonly ILogger<TownHangfireJob> _logger;
        
        /// <summary> 建構式 </summary>
        public TownHangfireJob(
            ILogger<TownHangfireJob> logger,
            IDistrictTownService<List<Town>?> service)
        {
            _logger = logger;
            _service = service;
        }

        /// <inheritdoc />
        [DisplayName("更新鄉鎮市區資料")]
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
