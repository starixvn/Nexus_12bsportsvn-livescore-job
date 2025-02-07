namespace SabaNews.Livescore.Provider.API.Services
{
    using Models.Country;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFetchCountryService
    {
        Task<List<CountryListResponse>> FetchCountryListAsync();
    }
}