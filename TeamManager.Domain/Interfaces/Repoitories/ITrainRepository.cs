using TeamManager.Domain.Enum;
using TeamManager.Domain.Model;

namespace TeamManager.Domain.Interfaces.Repoitories;

public interface ITrainRepository
{
    Task<IEnumerable<Train>> GetAllAsync();
    Task<Train?> GetByIdAsync(int id);
    Task<Train> CreateAsync(Train train);
    Task<Train> UpdateAsync(Train train);
    Task<bool> DeleteAsync(int id);
    Task<bool> SoftDeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<int> GetCountAsync();
    Task<IEnumerable<Train>> GetActiveTrainsAsync();
    Task<IEnumerable<Train>> GetTrainsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<Train>> GetTrainsByTypeAsync(TypeTrain typeTrain);
    Task<IEnumerable<Train>> GetTrainsFromTodayAsync();
    Task<IEnumerable<Train>> GetUpcomingTrainsAsync();
}