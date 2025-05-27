using TeamManager.Domain.DTOs;

namespace TeamManager.Domain.Interfaces.Services;

public interface ICoachService
{
    Task<IEnumerable<CoachResponseDto>> GetAllAsync();
    Task<CoachResponseDto?> GetByIdAsync(int id);
    Task<CoachResponseDto> CreateAsync(CoachCreateDto createDto);
    Task<CoachResponseDto?> UpdateAsync(int id, CoachUpdateDto updateDto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<CoachResponseDto>> GetByRoleAsync(string role);
    Task<int> GetCountAsync();
}
