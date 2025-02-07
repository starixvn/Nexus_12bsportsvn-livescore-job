using SabaNews.Livescore.Core.Models;

namespace SabaNews.Livescore.ScheduledTasks.Workers
{
    using Constants;
    using Helpers;
    using Models;
    using SabaNews.Livescore.Core.Extensions;

    using SabaNews.Livescore.Core.Services;
    using SabaNews.Livescore.SyncData.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SyncMatchByUpdatedTimeFromCurrentToFutureWorker : WorkerBase
    {
        private readonly ISyncMatchService syncMatchService;
        private readonly AppSettings appSettings;

        public SyncMatchByUpdatedTimeFromCurrentToFutureWorker(
            ISyncMatchService syncMatchService,
            List<ScheduledTaskSettings> jobSettings,
            AppSettings appSettings,
            ILogServiceProvider logService)
                : base(logService)
        {
            this.syncMatchService = syncMatchService;
            this.appSettings = appSettings;
            jobSetting = ScheduledTaskHelper.GetJobConfig(jobSettings, JobName.SyncMatchByUpdatedTimeFromCurrentToFuture);
        }

        public override async Task DoWorkAsync()
        {
            var startTime = DateTimeUtilize.UtcNow().Date;
            var endTime = startTime.AddDays(appSettings.NumOfDayFutureToSyncMatch + 1).AddSeconds(-1);

            var lastUpdatedTime = await syncMatchService.GetMatchLastUpdatedTimeInDateRangeAsync(startTime, endTime);

            await syncMatchService.SyncAllMatchInfoByStartTimeRangeAndUpdatedTimeAsync(startTime, endTime, lastUpdatedTime);
        }
    }
}