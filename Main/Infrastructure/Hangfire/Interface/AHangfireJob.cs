using Hangfire;
using Hangfire.Server;

namespace LC.Infrastructure.Hangfire.Interface
{
    /// <summary>
    /// Hangfire工作的抽象類別
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AHangfireJob<T> : IHangfireJob where T : IHangfireJob
    {
        /// <inheritdoc />
        public abstract string JobId { get; }
        /// <inheritdoc />
        public abstract string CronExpression { get; }
        /// <summary>
        /// 取消權杖
        /// </summary>
        protected IJobCancellationToken _cancellationToken;
        /// <summary>
        /// PerformContext
        /// </summary>
        protected PerformContext _context;
        /// <summary>
        /// 工作的實際執行內容
        /// </summary>
        protected abstract Task PerformJobTasks();

        /// <inheritdoc />
        [DisableConcurrentExecution(0)]
        public abstract Task Execute(IJobCancellationToken cancellationToken, PerformContext context);

        /// <inheritdoc />
        public void Schedule()
        {
            if (string.IsNullOrWhiteSpace(CronExpression))
            {
                RecurringJob.RemoveIfExists(JobId);
            }

            RecurringJob.AddOrUpdate<T>(
                JobId,
                x => x.Execute(JobCancellationToken.Null, null),
                CronExpression,
                new RecurringJobOptions
                {
                    TimeZone = TimeZoneInfo.Local
                }
            );
        }
    }
}
