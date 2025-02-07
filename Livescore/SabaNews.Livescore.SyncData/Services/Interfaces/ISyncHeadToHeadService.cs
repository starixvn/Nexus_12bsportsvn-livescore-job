namespace SabaNews.Livescore.SyncData.Services.Interfaces
{
    using Models.HeadToHead;
    using SabaNews.Livescore.Provider.API.Models.Team;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISyncHeadToHeadService
    {
        public Task<IEnumerable<MatchToGetH2H>> GetMatchToSyncHeadToHeadByDateRange(DateTime from, DateTime to, int limit);

        public Task UpdateSyncCompletedHeadToHead(long matchId);

        Task SyncHeadToHeadAsync(HeadToHeadResponse h2hResponse);
    }
}