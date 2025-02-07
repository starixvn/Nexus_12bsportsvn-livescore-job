namespace SabaNews.Livescore.ScheduledTasks.Workers
{
    using Constants;
    using Helpers;
    using Models;
    using SabaNews.Livescore.Core.Services;
    using SabaNews.Livescore.SyncData.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SyncStageListByLastUpdatedTimeWorker : WorkerBase
    {
        private readonly ISyncSeasonService syncSeasonService;

        public SyncStageListByLastUpdatedTimeWorker(
            ISyncSeasonService syncSeasonService,
            List<ScheduledTaskSettings> jobSettings,
            ILogServiceProvider logService) : base(logService)
        {
            this.syncSeasonService = syncSeasonService;
            jobSetting = ScheduledTaskHelper.GetJobConfig(jobSettings, JobName.SyncStageListByLastUpdatedTime);
        }

        public override async Task DoWorkAsync()
        {
            await syncSeasonService.SyncStageListByLastUpdatedTimeAsync();
        }
    }
}