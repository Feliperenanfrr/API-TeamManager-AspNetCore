using AutoMapper;
using TeamManager.Domain.Common;
using TeamManager.Domain.DTOs;
using TeamManager.Domain.Enum;
using TeamManager.Domain.Exceptions;
using TeamManager.Domain.Interfaces.Repoitories;
using TeamManager.Domain.Interfaces.Services;
using TeamManager.Domain.Model;

namespace TeamManager.Application.Services;

public class AthleteService : IAthleteService
{
    private readonly IAthleteRepository _athleteRepository;
    private readonly IMapper _mapper;

    public AthleteService(IAthleteRepository athleteRepository, IMapper mapper)
    {
        _athleteRepository = athleteRepository;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<AthleteResponseDto>> GetAllAthletesAsync(
        PaginationRequest paginationRequest
    )
    {
        var athletesPage = await _athleteRepository.GetAllAsync(paginationRequest);

        var athleteDto = _mapper.Map<IEnumerable<AthleteResponseDto>>(athletesPage.Data);

        return new PaginationResponse<AthleteResponseDto>(
            athleteDto,
            athletesPage.TotalRecords,
            athletesPage.Page,
            athletesPage.PageSize,
            athletesPage.TotalPages
        );
    }

    public async Task<AthleteResponseDto?> GetAthleteByIdAsync(int id)
    {
        var athlete = await _athleteRepository.GetByIdAsync(id);
        return athlete != null ? _mapper.Map<AthleteResponseDto>(athlete) : null;
    }

    public async Task<AthleteResponseDto> CreateAthleteAsync(AthleteCreateDto athleteDto)
    {
        ValidateAthleteAge(athleteDto.BirthDay);

        var athlete = _mapper.Map<Athlete>(athleteDto);
        var createdAthlete = await _athleteRepository.CreateAsync(athlete);

        return _mapper.Map<AthleteResponseDto>(createdAthlete);
    }

    public async Task<AthleteResponseDto> UpdateAthleteAsync(int id, AthleteUpdateDto athleteDto)
    {
        var existingAthlete = await _athleteRepository.GetByIdAsync(id);

        if (existingAthlete == null)
            throw new NotFoundException($"Athlete {id} not found");

        ValidateAthleteAge(athleteDto.BirthDay);

        _mapper.Map(athleteDto, existingAthlete);
        var updatedAthlete = await _athleteRepository.UpdateAsync(existingAthlete);

        return _mapper.Map<AthleteResponseDto>(updatedAthlete);
    }

    public async Task<bool> DeleteAthleteAsync(int id)
    {
        var exists = await _athleteRepository.ExistsAsync(id);

        if (!exists)
            throw new NotFoundException($"Athlete {id} not found");

        return await _athleteRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<AthleteResponseDto>> GetAthletesByPositionAsync(
        Positions position
    )
    {
        var athletes = await _athleteRepository.GetByPositionsAsync(position);
        return _mapper.Map<IEnumerable<AthleteResponseDto>>(athletes);
    }

    public async Task<int> GetAthletesCountAsync()
    {
        return await _athleteRepository.GetCountAsync();
    }

    private static void ValidateAthleteAge(DateTime birthDay)
    {
        var age = DateTime.Now.Year - birthDay.Year;

        if (age < 16)
            throw new BusinessException("Atleta deve ter pelo menos 16 anos");

        if (age > 55)
            throw new BusinessException("Atleta não pode ter mais de 55 anos");
    }
}
