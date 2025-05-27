using System.Collections;
using TeamManager.Domain.Enum;
using TeamManager.Domain.Model;

namespace TeamManager.Domain.Interfaces.Repoitories;

public interface ICoachRepository
{
    Task<IEnumerable<Coach>> GetAllAsync();
    Task<Coach?> GetByIdAsync(int id);
    Task<Coach> CreateAsync(Coach coach);
    Task<Coach> UpdateAsync(Coach coach);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<IEnumerable<Coach>> GetByRoleAsync(CoachRole role);
    Task<int> GetCountAsync();
}
