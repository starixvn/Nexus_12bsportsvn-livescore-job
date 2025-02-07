using SabaNews.Livescore.Core.Models;

namespace SabaNews.Livescore.ScheduledTasks.Workers
{
    using Constants;
    using Core.Enum;
    using Helpers;
    using Models;

    using SabaNews.Livescore.Core.Services;
    using SabaNews.Livescore.Models.Synchronous;
    using SabaNews.Livescore.Provider.API.Models.Matches;
    using SabaNews.Livescore.Provider.API.Services;
    using SabaNews.Livescore.SyncData.Services;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SyncMatchByMaxMatchIdWorker : WorkerBase
    {
        private readonly IFetchMatchService fetchMatchService;
        private readonly ISyncMatchService syncMatchService;
        private readonly AppSettings appSettings;

        public SyncMatchByMaxMatchIdWorker(
            ISyncMatchService syncMatchService,
            IFetchMatchService fetchMatchService,
            AppSettings appSettings,
            List<ScheduledTaskSettings> jobSettings,
            ILogServiceProvider logService)
                : base(logService)
        {
            this.fetchMatchService = fetchMatchService;
            this.syncMatchService = syncMatchService;
            this.appSettings = appSettings;
            jobSetting = ScheduledTaskHelper.GetJobConfig(jobSettings, JobName.SyncAllMatchByMaxId);
        }

        public override async Task DoWorkAsync()
        {
            long maxMatchId = await syncMatchService.GetMaxMatchIdAsync();
            var param = new SyncMatchParams
            {
                SportId = (int)SportType.Soccer,
                MaxMatchId = maxMatchId,
                RowLimit = appSettings.BatchSizeSyncMatches
            };
            var matches = await fetchMatchService.FetchMatchListAsync(param);

            await syncMatchService.SyncMatchByLastUpdatedTimeAsync(matches);

            await SyncMatchDetais(matches);
        }

        private async Task SyncMatchDetais(List<MatchResponse> matches)
        {
            var matchIds = matches.Select(m => m.MatchId).ToArray();

            await SyncMatchDetailByIdsAsync(matchIds);

            var syncLineupMatchIds = matches.Where(m => m.IsLineUp).Select(i => i.MatchId).ToArray();

            await SyncLinupAsync(syncLineupMatchIds);
            await SyncIncidents(matchIds);
        }

        public async Task SyncMatchDetailByIdsAsync(long[] matchIds)
        {
            var fetchMatchDetailParam = new SyncMatchParams
            {
                MatchIds = matchIds,
                RowLimit = appSettings.BatchSizeSyncMatches
            };
            var matches = await fetchMatchService.FetchMatchDetailAsync(fetchMatchDetailParam);

            await syncMatchService.SyncMatchStatisticAsync(matches);
        }

        private async Task SyncLinupAsync(long[] matchIds)
        {
            var lineups = await fetchMatchService.FetchMatchLineupAsync(matchIds);

            await syncMatchService.SyncMatchLineupByIdsAsync(lineups);
        }

        private async Task SyncIncidents(long[] matchIds)
        {
            foreach (var matchId in matchIds)
            {
                var incidents = await fetchMatchService.FetchMatchIncidentAsync((int)SportType.Soccer, matchId);
                await syncMatchService.SyncMatchIncidentAsync(matchId, incidents);
            }
        }
    }
}