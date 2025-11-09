using Domain.Contracts;
using ServiceAbstraction.Contracts;

namespace Service.Implementations
{
    public class CacheService(ICacheRepository _cacheRepository) : ICacheService
    {
        public async Task<string> GetCachedValueAsync(string key)
            => await _cacheRepository.GetAsync(key);

        public async Task SetCachedValueAsync(string key, object value, TimeSpan duration)
            => await _cacheRepository.SetAsync(key, value, duration);
    }
}
