using LC.Infrastructure.Hangfire.Interface;
using Hangfire.Server;
using LC.Models.Entities;
using DataShareHub.Services.TWSE;

namespace DataShareHub.Tasks.TWSE
{
    /// <summary>
    /// 上市個股資訊的 Hangfire 工作
    /// </summary>
    public class StocksHangfireJob : AHangfireJob<StocksHangfireJob>
    {
        /// <summary> 工作的唯一識別碼 </summary>
        public override string JobId => "4a0ea595-a83a-4b40-bc50-b13e8f36aeb9";
        /// <summary> 工作的 CRON 表達式 </summary>
        public override string CronExpression => "0 15 * * *";
        private readonly IStocksService<(List<Stocks>?, List<StockDaily>?)> _service;
        private readonly ILogger<StocksHangfireJob> _logger;
        
        /// <summary> 建構式 </summary>
        public StocksHangfireJob(
            ILogger<StocksHangfireJob> logger,
            IStocksService<(List<Stocks>?, List<StockDaily>?)> service)
        {
            _logger = logger;
            _service = service;
        }

        /// <inheritdoc />
        [DisplayName("更新上市個股資訊")]
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
