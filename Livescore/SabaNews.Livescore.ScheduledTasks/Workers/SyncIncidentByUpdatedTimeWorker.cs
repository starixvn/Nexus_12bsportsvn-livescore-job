namespace SabaNews.Livescore.ScheduledTasks.Workers
{
    using Constants;
    using Helpers;
    using Models;
    using SabaNews.Livescore.Core.Services;
    using SabaNews.Livescore.SyncData.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SyncIncidentByUpdatedTimeWorker : WorkerBase
    {
        private readonly ISyncMatchService syncMatchService;

        public SyncIncidentByUpdatedTimeWorker(
            ISyncMatchService syncMatchService,
            List<ScheduledTaskSettings> jobSettings,
            ILogServiceProvider logService) : base(logService)
        {
            this.syncMatchService = syncMatchService;
            jobSetting = ScheduledTaskHelper.GetJobConfig(jobSettings, JobName.SyncIncidentByUpdatedTime);
        }

        public override async Task DoWorkAsync()
        {
            await syncMatchService.SyncIncidentByUpdatedTimeAsync();
        }
    }
}