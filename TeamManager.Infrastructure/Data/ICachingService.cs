namespace TeamManager.Infrastructure.Data;

public interface ICachingService
{
    Task SetAsync(string key, object value);
    
    Task<String>  GetAsync(string key);
}