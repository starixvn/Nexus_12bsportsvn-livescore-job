namespace SabaNews.Livescore.ScheduledTasks.Workers
{
    using Constants;
    using Helpers;
    using Models;
    using SabaNews.Livescore.Core.Services;
    using SabaNews.Livescore.SyncData.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SyncPlayerListByLastUpdatedTimeWorker : WorkerBase
    {
        private readonly ISyncPlayerService syncPlayerService;

        public SyncPlayerListByLastUpdatedTimeWorker(
            ISyncPlayerService syncPlayerService,
            List<ScheduledTaskSettings> jobSettings,
            ILogServiceProvider logService) : base(logService)
        {
            this.syncPlayerService = syncPlayerService;
            jobSetting = ScheduledTaskHelper.GetJobConfig(jobSettings, JobName.SyncPlayerListByLastUpdatedTime);
        }

        public override async Task DoWorkAsync()
        {
            await syncPlayerService.SyncPlayerListByLastUpdatedTimeAsync();
        }
    }
}