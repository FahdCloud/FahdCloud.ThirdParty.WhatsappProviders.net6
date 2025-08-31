using FahdCloud.ThirdParty.WhatsappProviders.Interfaces._00_Shared;
using Microsoft.Extensions.Caching.Memory;

namespace FahdCloud.ThirdParty.WhatsappProviders.Services._00_Shared
{
    internal class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(40);

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        #region Members

        #endregion Members

        #region Methods

        public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> fetchDataFunction, TimeSpan? absoluteExpiration = null)
        {
            var data = GetData<T>(key);

            if (data is not null)
                return data;
            else
            {
                data = await fetchDataFunction();

                if (data != null)
                    SetData(key, data);

                return data;
            }
        }

        public T? GetData<T>(string key)
        {
            return _cache.TryGetValue(key, out T? data) ? data : default;
        }

        public T SetData<T>(string key, T data, TimeSpan? absoluteExpiration = null)
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(absoluteExpiration ?? _defaultExpiration);

            return _cache.Set(key, data, cacheOptions);
        }

        public void RemoveData(string key)
            => _cache.Remove(key);

        #endregion Methods
    }
}