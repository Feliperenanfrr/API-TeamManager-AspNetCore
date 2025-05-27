using TeamManager.Domain.Enum;
using TeamManager.Domain.Model;

namespace TeamManager.Domain.Interfaces.Repoitories;

public interface IWorkoutRepository
{
    Task<IEnumerable<Workout>> GetAllAsync();
    Task<Workout?> GetByIdAsync(int id);
    Task<Workout> CreateAsync(Workout workout);
    Task<Workout> UpdateAsync(Workout workout);
    Task<bool> DeleteAsync(int id);
    Task<bool> SoftDeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<int> GetCountAsync();
    Task<IEnumerable<Workout>> GetActiveTrainsAsync();
    Task<IEnumerable<Workout>> GetTrainsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<Workout>> GetTrainsByTypeAsync(TypeWorkout typeWorkou);
    Task<IEnumerable<Workout>> GetTrainsFromTodayAsync();
    Task<IEnumerable<Workout>> GetUpcomingTrainsAsync();
}
