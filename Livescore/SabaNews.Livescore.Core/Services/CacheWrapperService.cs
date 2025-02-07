namespace SabaNews.Livescore.Core.Services
{
    using Fanex.Caching;
    using Newtonsoft.Json;
    using System.Threading.Tasks;

    public interface ICacheWrapperService
    {
        Task<T> GetAsync<T>(string key);

        Task SetAsync<T>(string key, T data, CacheItemOptions options);
    }

    public class CacheWrapperService : ICacheWrapperService
    {
        private readonly ICacheService cacheService;

        public CacheWrapperService(ICacheService cacheService)
        {
            this.cacheService = cacheService;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            string jsonResult = await cacheService.GetAsync<string>(key);

            if (string.IsNullOrWhiteSpace(jsonResult))
            {
                return default;
            }

            var result = JsonConvert.DeserializeObject<T>(jsonResult);
            return result;
        }

        public async Task SetAsync<T>(string key, T data, CacheItemOptions options)
        {
            await cacheService.SetAsync(key, JsonConvert.SerializeObject(data), options);
        }
    }
}