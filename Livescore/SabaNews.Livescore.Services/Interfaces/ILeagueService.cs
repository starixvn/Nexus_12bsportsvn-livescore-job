using SabaNews.Livescore.Core.Models;

namespace SabaNews.Livescore.Services
{
    using Criteria.League;
    using Models.League;
    using Models.Match;
    using Models.Team;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILeagueService
    {
        Task<IList<League>> GetMajorLeagueAsync();

        Task<IList<League>> GetLeagueByIdsAsync(GetLeagueByIdsCriteria criteria);

        Task<League> GetLeagueByIdAsync(long leagueId);

        Task<IList<League>> GetLeaguesAsync();

        Task<IList<League>> GetAllLeaguesAsync();

        Task<CommandResult> UpdateLeaguesExtraAsync(League league);

        Task<CommandResult> DeleteLeaguesExtraAsync(long leagueId);

        Task<CommandResult> InsertLeaguesExtraAsync(long leagueId);

        Task<CommandResult> UpdateLeaguesAsync(UpdateLeagueCriteria updateLeague);

        Task<IList<SeasonStandings>> GetLeagueSeasonStanding(GetLeagueSeasonStandingCriteria criteria);

        Task<(League, IList<SeasonStandings>)> GetLeagueAndSeasonStandingAsync(GetLeagueAndSeasonStandingsCriteria criteria);

        Task<IList<Player>> GetPlayerByIdTeamAsync(long teamId);

        Task<IList<TopScorers>> GetTop10ScorersAsync(GetTop10ScorersCriteria top10ScorersCriteria);

        Task<IList<TopScorers>> GetAllTopScorersAsync(long leagueId, long seasonId);

        Task<IList<TopScorers>> GetTopScorersByRowNumAsync(int rowNum, long leagueId, long seasonId);

        Task<CommandResult> InsertTopScorerAsync(TopScorers topScorers);

        Task<CommandResult> UpdateTopScorerAsync(TopScorers topScorers);

        Task<CommandResult> DeleteTopScorerAsync(int rowNum, long leagueId, long seasonId);

        Task<IList<Season>> GetSeasonByLeagueIdAsync(long leagueId);

        Task<IEnumerable<MiniGame>> GetDataMinigameAsync(GetDataMinigameCriteria criteria);

        Task<IList<MiniGame>> GetDetailMinigameAsync(int UserId);

        Task<CommandResult> InsertMinigameAsync(MiniGame miniGame);

        Task<SeasonStandingUpdate> GetLeagueSeasonStandingDetailAsync(GetLeagueSeasonStandingDetailCriteria criteria);

        Task<CommandResult> UpdateStandingAsync(StandingUpdateModel model);

        Task<CommandResult> RemoveStandingCustomAsync(long seasonId);
    }
}