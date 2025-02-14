namespace LearnEase_Api.LearnEase.Core.IServices
{
    public interface IRedisCacheService
    {
        Task SetAsync<T>(string key, T value, TimeSpan expiration);
        Task<T?> GetAsync<T>(string key);
        Task RemoveAsync(string key);
    }
}
