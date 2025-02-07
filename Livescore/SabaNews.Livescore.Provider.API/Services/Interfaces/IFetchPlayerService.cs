namespace SabaNews.Livescore.Provider.API.Services
{
    using Models.Player;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFetchPlayerService
    {
        Task<IDictionary<string, List<PlayerListResponse>>> FetchPlayerListAsync(FetchPlayerListParams playerParams);
    }
}