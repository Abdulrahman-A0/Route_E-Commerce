namespace ServiceAbstraction.Contracts
{
    public interface ICacheService
    {
        Task<string> GetCachedValueAsync(string key);
        Task SetCachedValueAsync(string key, object value, TimeSpan duration);
    }
}
