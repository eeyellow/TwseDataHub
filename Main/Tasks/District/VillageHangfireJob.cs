using LC.Infrastructure.Hangfire.Interface;
using Hangfire.Server;
using LC.Models.Entities;

namespace TwseDataHub.Tasks.District
{
    /// <summary>
    /// 村里資料的 Hangfire 工作
    /// </summary>
    public class VillageHangfireJob : AHangfireJob<VillageHangfireJob>
    {
        /// <summary> 工作的唯一識別碼 </summary>
        public override string JobId => "21ff050a-12f0-401c-937d-c30ed79394c4";
        /// <summary> 工作的 CRON 表達式 </summary>
        public override string CronExpression => "15 3 * * *";
        private readonly IDistrictVillageService<List<Village>?> _service;
        private readonly ILogger<VillageHangfireJob> _logger;
        
        /// <summary> 建構式 </summary>
        public VillageHangfireJob(
            ILogger<VillageHangfireJob> logger,
            IDistrictVillageService<List<Village>?> service)
        {
            _logger = logger;
            _service = service;
        }

        /// <inheritdoc />
        [DisplayName("更新村里資料")]
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
