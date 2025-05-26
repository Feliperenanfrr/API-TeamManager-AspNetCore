using TeamManager.Domain.Common;
using TeamManager.Domain.DTOs;
using TeamManager.Domain.Enum;

namespace TeamManager.Domain.Interfaces.Services;

public interface IAthleteService
{
    Task<PaginationResponse<AthleteResponseDto>> GetAllAthletesAsync(PaginationRequest request);
    Task<AthleteResponseDto?> GetAthleteByIdAsync(int id);
    Task<AthleteResponseDto> CreateAthleteAsync(AthleteCreateDto athleteDto);
    Task<AthleteResponseDto> UpdateAthleteAsync(int id, AthleteUpdateDto athleteDto);
    Task<bool> DeleteAthleteAsync(int id);
    Task<IEnumerable<AthleteResponseDto>> GetAthletesByPositionAsync(Positions position);
    Task<int> GetAthletesCountAsync();
}
