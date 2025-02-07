namespace SabaNews.Livescore.Provider.API.Services
{
    using Models.Season;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFetchSeasonService
    {
        Task<List<SeasonListResponse>> FetchSeasonListAsync(FetchSeasonListParams seasonParams);

        Task<List<StageListResponse>> FetchStageListAsync(FetchSeasonListParams seasonParams);

        Task<List<T>> FetchSeasonStandingsAsync<T>(FetchSeasonStandingsParams seasonStadingsParams);

        Task<List<TeamHonorResponse>> FetchTeamHonorAsync(FetchTeamHonorParams teamHonorParams);

        Task<List<PlayerHonorResponse>> FetchPlayerHonorAsync(FetchTeamHonorParams teamHonorParams);
    }
}