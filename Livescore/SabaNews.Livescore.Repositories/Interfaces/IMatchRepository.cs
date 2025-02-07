namespace SabaNews.Livescore.Repositories
{
    using Fanex.Data.Repository;
    using Models.Match;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMatchRepository
    {
        Task<IList<Match>> GetMatchByDateAsync(ICriteria criteria);

        Task<Match> GetMatchByIdAsync(ICriteria criteria);

        Task<HeadToHeadDetail> GetHeadToHeadAsync(ICriteria criteria);

        Task<IList<Match>> GetMatchByIdsAsync(ICriteria criteria);

        Task<IList<Match>> GetMatchBySeasonFilterAsync(ICriteria criteria);

        Task<MatchUpdate> GetMatchByIdUpdateAsync(ICriteria criteria);

        Task<Match> GetSummaryMatchByIdAsync(ICriteria criteria);

        Task<IList<Match>> GetTodayMatchesAsync(ICriteria criteria);
    }
}