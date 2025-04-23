using Microsoft.Extensions.Caching.Distributed;

namespace TeamManager.Data;

public class CachingService : ICachingService
{
    private readonly IDistributedCache _cache;
    
    public CachingService(IDistributedCache  cache)
    {
        _cache = cache;
    }
    
    public Task SetAsync(string key, object value)
    {
        return await _cache.SetStringAsync(key);
    }

    public Task<string> GetAsync(string key)
    {
        throw new NotImplementedException();
    }
}