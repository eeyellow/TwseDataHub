using Hangfire;
using Hangfire.Server;

namespace LC.Infrastructure.Hangfire.Interface
{
    /// <summary>
    /// Hangfire工作的介面
    /// </summary>
    public interface IHangfireJob
    {
        /// <summary>
        /// 工作的唯一識別碼
        /// </summary>
        string JobId { get; }

        /// <summary>
        /// 工作的 CRON 表達式
        /// </summary>
        string CronExpression { get; }

        /// <summary>
        /// 執行工作
        /// </summary>
        /// <param name="cancellationToken">工作的取消權杖</param>
        /// <param name="context">工作的執行上下文</param>
        Task Execute(IJobCancellationToken cancellationToken, PerformContext context);

        /// <summary>
        /// 將工作加入排程
        /// </summary>
        void Schedule();
    }
}
