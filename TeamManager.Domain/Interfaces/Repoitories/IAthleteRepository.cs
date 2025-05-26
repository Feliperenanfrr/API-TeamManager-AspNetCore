using TeamManager.Domain.Common;
using TeamManager.Domain.Enum;
using TeamManager.Domain.Model;

namespace TeamManager.Domain.Interfaces.Repoitories;

public interface IAthleteRepository
{
    Task<PaginationResponse<Athlete>> GetAllAsync(PaginationRequest paginationRequest);
    Task<Athlete?> GetByIdAsync(int id);
    Task<Athlete> CreateAsync(Athlete athlete);
    Task<Athlete> UpdateAsync(Athlete athlete);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<IEnumerable<Athlete>> GetByPositionsAsync(Positions positions);
    Task<int> GetCountAsync();
}
