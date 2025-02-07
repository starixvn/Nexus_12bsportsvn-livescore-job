namespace SabaNews.Livescore.ScheduledTasks.Workers
{
    using Constants;
    using Helpers;
    using Models;
    using SabaNews.Livescore.Core.Services;
    using SabaNews.Livescore.SyncData.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SyncTeamListByLastUpdatedTimeWorker : WorkerBase
    {
        private readonly ISyncTeamService syncTeamService;

        public SyncTeamListByLastUpdatedTimeWorker(
            ISyncTeamService syncTeamService,
            List<ScheduledTaskSettings> jobSettings,
            ILogServiceProvider logService) : base(logService)
        {
            this.syncTeamService = syncTeamService;
            jobSetting = ScheduledTaskHelper.GetJobConfig(jobSettings, JobName.SyncTeamListByLastUpdatedTime);
        }

        public override async Task DoWorkAsync()
        {
            await syncTeamService.SyncTeamListByLastUpdatedTimeAsync();
        }
    }
}