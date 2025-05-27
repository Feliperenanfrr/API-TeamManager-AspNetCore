using TeamManager.Domain.DTOs;
using TeamManager.Domain.Enum;

namespace TeamManager.Domain.Interfaces.Services;

public interface ITrainService
{
    Task<IEnumerable<TrainResponseDto>> GetAllAsync();
    Task<TrainResponseDto?> GetByIdAsync(int id);
    Task<TrainResponseDto> CreateAsync(TrainCreateDto createDto);
    Task<TrainResponseDto?> UpdateAsync(int id, TrainUpdateDto updateDto);
    Task<bool> DeleteAsync(int id);
    Task<bool> SoftDeleteAsync(int id);
    Task<int> GetCountAsync();
    Task<IEnumerable<TrainResponseDto>> GetActiveTrainsAsync();
    Task<IEnumerable<TrainResponseDto>> GetTrainsByDateRangeAsync(
        DateTime startDate,
        DateTime endDate
    );
    Task<IEnumerable<TrainResponseDto>> GetTrainsByTypeAsync(TypeTrain typeTrain);
    Task<IEnumerable<TrainResponseDto>> GetTrainsFromTodayAsync();
    Task<IEnumerable<TrainResponseDto>> GetUpcomingTrainsAsync();
}
