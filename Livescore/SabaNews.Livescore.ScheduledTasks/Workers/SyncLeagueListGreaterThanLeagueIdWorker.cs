namespace SabaNews.Livescore.ScheduledTasks.Workers
{
    using Constants;
    using Helpers;
    using Models;
    using SabaNews.Livescore.Core.Services;
    using SabaNews.Livescore.SyncData.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SyncLeagueListGreaterThanLeagueIdWorker : WorkerBase
    {
        private readonly ISyncLeagueService syncLeagueService;

        public SyncLeagueListGreaterThanLeagueIdWorker(
            ISyncLeagueService syncLeagueService,
            List<ScheduledTaskSettings> jobSettings,
            ILogServiceProvider logService) : base(logService)
        {
            this.syncLeagueService = syncLeagueService;
            jobSetting = ScheduledTaskHelper.GetJobConfig(jobSettings, JobName.SyncLeagueListGreaterThanLeagueId);
        }

        public override async Task DoWorkAsync()
        {
            await syncLeagueService.SyncLeagueListGreaterThanLeagueIdAsync();
        }
    }
}