namespace SabaNews.Livescore.ScheduledTasks.Workers
{
    using Constants;
    using Helpers;
    using Models;
    using SabaNews.Livescore.Core.Services;
    using SabaNews.Livescore.SyncData.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SyncTeamListGreaterThanTeamIdWorker : WorkerBase
    {
        private readonly ISyncTeamService syncTeamService;

        public SyncTeamListGreaterThanTeamIdWorker(
            ISyncTeamService syncTeamService,
            List<ScheduledTaskSettings> jobSettings,
            ILogServiceProvider logService) : base(logService)
        {
            this.syncTeamService = syncTeamService;
            jobSetting = ScheduledTaskHelper.GetJobConfig(jobSettings, JobName.SyncTeamListGreaterThanTeamId);
        }

        public override async Task DoWorkAsync()
        {
            await syncTeamService.SyncTeamListGreaterThanTeamIdAsync();
        }
    }
}