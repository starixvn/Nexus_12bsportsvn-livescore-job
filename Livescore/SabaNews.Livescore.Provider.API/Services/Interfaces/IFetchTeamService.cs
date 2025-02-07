namespace SabaNews.Livescore.Provider.API.Services
{
    using Models.Team;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFetchTeamService
    {
        Task<List<TeamListResponse>> FetchTeamListAsync(FetchTeamListParams teamParams);

        Task<HeadToHeadResponse> FetchHeadToHeadAsync(long matchId, long homeTeamId, long awayTeamId);
    }
}