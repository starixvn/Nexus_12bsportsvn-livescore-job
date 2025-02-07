namespace SabaNews.Livescore.ScheduledTasks.Workers
{
    using Constants;
    using Helpers;
    using Models;
    using SabaNews.Livescore.Core.Services;
    using SabaNews.Livescore.Provider.API.Services;
    using SabaNews.Livescore.SyncData.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SyncDeleledMatchesWorker : WorkerBase
    {
        private readonly IFetchMatchService fetchMatchService;
        private readonly ISyncMatchService syncMatchService;

        public SyncDeleledMatchesWorker(
            ISyncMatchService syncMatchService,
            IFetchMatchService fetchMatchService,
            List<ScheduledTaskSettings> jobSettings,
            ILogServiceProvider logService)
                : base(logService)
        {
            this.fetchMatchService = fetchMatchService;
            this.syncMatchService = syncMatchService;
            jobSetting = ScheduledTaskHelper.GetJobConfig(jobSettings, JobName.SyncDeletedMatchesInTwoWeek);
        }

        public override async Task DoWorkAsync()
        {
            var delMatches = await fetchMatchService.FetchDeletedMatchAsync();

            await syncMatchService.SyncDeletedMatchesAsync(delMatches);
        }
    }
}