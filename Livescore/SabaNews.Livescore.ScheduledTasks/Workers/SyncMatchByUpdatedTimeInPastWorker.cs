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

    public class SyncMatchByUpdatedTimeInPastWorker : WorkerBase
    {
        private readonly ISyncMatchService syncMatchService;
        private readonly AppSettings appSettings;

        public SyncMatchByUpdatedTimeInPastWorker(
            ISyncMatchService syncMatchService,
            List<ScheduledTaskSettings> jobSettings,
            AppSettings appSettings,
            ILogServiceProvider logService)
                : base(logService)
        {
            this.syncMatchService = syncMatchService;
            this.appSettings = appSettings;
            jobSetting = ScheduledTaskHelper.GetJobConfig(jobSettings, JobName.SyncMatchByUpdatedTimeInPast);
        }

        public override async Task DoWorkAsync()
        {
            var endTime = DateTimeUtilize.UtcNow().Date.AddSeconds(-1);
            var startTime = endTime.AddDays(appSettings.NumOfDayPastToSyncMatch);

            var lastUpdatedTime = await syncMatchService.GetMatchLastUpdatedTimeInDateRangeAsync(startTime, endTime, true);
            await syncMatchService.SyncAllMatchInfoByStartTimeRangeAndUpdatedTimeAsync(startTime, endTime, lastUpdatedTime);
        }
    }
}