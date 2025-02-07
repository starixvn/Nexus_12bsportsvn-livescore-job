namespace SabaNews.Livescore.ScheduledTasks.Workers
{
    using Constants;
    using Helpers;
    using Models;
    using SabaNews.Livescore.Core.Services;
    using SabaNews.Livescore.SyncData.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SyncSeasonPlayerHonorByLastUpdatedTimeWorker : WorkerBase
    {
        private readonly ISyncSeasonService syncSeasonService;

        public SyncSeasonPlayerHonorByLastUpdatedTimeWorker(
            ISyncSeasonService syncSeasonService,
            List<ScheduledTaskSettings> jobSettings,
            ILogServiceProvider logService) : base(logService)
        {
            this.syncSeasonService = syncSeasonService;
            jobSetting = ScheduledTaskHelper.GetJobConfig(jobSettings, JobName.SyncSeasonPlayerHonorByLastUpdatedTime);
        }

        public override async Task DoWorkAsync()
        {
            await syncSeasonService.SyncSeasonPlayerHonorByLastUpdatedTimeAsync();
        }
    }
}