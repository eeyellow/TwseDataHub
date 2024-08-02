// Licensed to the .NET Foundation under one or more agreements.


// Licensed to the .NET Foundation under one or more agreements.

using LC.Infrastructure.Hangfire.Interface;

namespace LC.Infrastructure.Hangfire
{
    /// <summary>
    /// Hangfire排程器
    /// </summary>
    public class HangfireScheduler
    {
        /// <summary>
        /// 執行定期任務
        /// </summary>
        public static void RecurringTasks(IServiceProvider serviceProvider)
        {
            try
            {
                var hangfireJobs = serviceProvider.GetServices<IHangfireJob>();

                foreach (var hangfireJob in hangfireJobs)
                {
                    hangfireJob.Schedule();
                }
            }
            catch (Exception e)
            {
                // 處理例外狀況，如果在動態載入類型時發生錯誤。
            }
        }
    }
}
