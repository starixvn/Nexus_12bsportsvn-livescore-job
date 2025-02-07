namespace SabaNews.Livescore.ScheduledTasks.Workers
{
    using Constants;
    using Helpers;
    using Models;
    using SabaNews.Livescore.Core.Services;
    using SabaNews.Livescore.SyncData.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SyncLeagueListByLastUpdatedTimeWorker : WorkerBase
    {
        private readonly ISyncLeagueService syncLeagueService;

        public SyncLeagueListByLastUpdatedTimeWorker(
            ISyncLeagueService syncLeagueService,
            List<ScheduledTaskSettings> jobSettings,
            ILogServiceProvider logService) : base(logService)
        {
            this.syncLeagueService = syncLeagueService;
            jobSetting = ScheduledTaskHelper.GetJobConfig(jobSettings, JobName.SyncLeagueListByLastUpdatedTime);
        }

        public override async Task DoWorkAsync()
        {
            await syncLeagueService.SyncLeagueListByLastUpdatedTimeAsync();
        }
    }
}