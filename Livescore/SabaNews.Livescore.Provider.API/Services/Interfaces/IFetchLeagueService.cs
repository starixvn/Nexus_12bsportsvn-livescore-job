namespace SabaNews.Livescore.Provider.API.Services
{
    using Models.League;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFetchLeagueService
    {
        Task<List<LeagueListResponse>> FetchLeagueListAsync(FetchLeagueListParams leagueParams);
    }
}