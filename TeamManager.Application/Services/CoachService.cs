using AutoMapper;
using TeamManager.Domain.DTOs;
using TeamManager.Domain.Enum;
using TeamManager.Domain.Exceptions;
using TeamManager.Domain.Interfaces.Repoitories;
using TeamManager.Domain.Interfaces.Services;
using TeamManager.Domain.Model;

namespace TeamManager.Application.Services;

public class CoachService : ICoachService
{
    private readonly ICoachRepository _coachRepository;
    private readonly IMapper _mapper;

    public CoachService(ICoachRepository coachRepository, IMapper mapper)
    {
        _coachRepository = coachRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CoachResponseDto>> GetAllAsync()
    {
        var coaches = await _coachRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CoachResponseDto>>(coaches);
    }

    public async Task<CoachResponseDto?> GetByIdAsync(int id)
    {
        var coach = await _coachRepository.GetByIdAsync(id);
        return coach != null ? _mapper.Map<CoachResponseDto>(coach) : null;
    }

    public async Task<CoachResponseDto> CreateAsync(CoachCreateDto createDto)
    {
        var coach = _mapper.Map<Coach>(createDto);
        var createdCoach = await _coachRepository.CreateAsync(coach);
        return _mapper.Map<CoachResponseDto>(createdCoach);
    }

    public async Task<CoachResponseDto?> UpdateAsync(int id, CoachUpdateDto updateDto)
    {
        var existingCoach = await _coachRepository.GetByIdAsync(id);
        if (existingCoach == null)
            throw new NotFoundException($"Técnico com ID {id} não encontrado");

        _mapper.Map(updateDto, existingCoach);
        var updatedCoach = await _coachRepository.UpdateAsync(existingCoach);
        return _mapper.Map<CoachResponseDto>(updatedCoach);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exists = await _coachRepository.ExistsAsync(id);
        if (!exists)
            throw new NotFoundException($"Técnico com ID {id} não encontrado.");

        return await _coachRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<CoachResponseDto>> GetByRoleAsync(string role)
    {
        if (!Enum.TryParse<CoachRole>(role, true, out var parsedRole))
            throw new ArgumentException($"Role inválido: {role}");

        var coaches = await _coachRepository.GetByRoleAsync(parsedRole);
        return _mapper.Map<IEnumerable<CoachResponseDto>>(coaches);
    }

    public async Task<int> GetCountAsync()
    {
        return await _coachRepository.GetCountAsync();
    }
}
