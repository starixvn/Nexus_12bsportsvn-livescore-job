namespace SabaNews.Livescore.Services.Interfaces
{
    using Models.Match;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPushSignalService
    {
        public Task PushMatchListAsync(IEnumerable<Match> matches);

        public Task PushMatchDetailcAsync(long matchId, MatchDetail detail);
    }
}