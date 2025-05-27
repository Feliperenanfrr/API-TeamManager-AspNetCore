using System.ComponentModel.DataAnnotations;
using AutoMapper;
using TeamManager.Domain.DTOs;
using TeamManager.Domain.Enum;
using TeamManager.Domain.Exceptions;
using TeamManager.Domain.Interfaces.Repoitories;
using TeamManager.Domain.Interfaces.Services;
using TeamManager.Domain.Model;

namespace TeamManager.Application.Services;

public class TrainService : ITrainService
{
    private readonly ITrainRepository _trainRepository;
    private readonly IMapper _mapper;

    public TrainService(ITrainRepository trainRepository, IMapper mapper)
    {
        _trainRepository = trainRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TrainResponseDto>> GetAllAsync()
    {
        var trains = await _trainRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<TrainResponseDto>>(trains);
    }

    public async Task<TrainResponseDto?> GetByIdAsync(int id)
    {
        var train = await _trainRepository.GetByIdAsync(id);
        return train != null ? _mapper.Map<TrainResponseDto>(train) : null;
    }

    public async Task<TrainResponseDto> CreateAsync(TrainCreateDto createDto)
    {
        var train = _mapper.Map<Train>(createDto);
        var createdTrain = await _trainRepository.CreateAsync(train);
        return _mapper.Map<TrainResponseDto>(createdTrain);
    }

    public async Task<TrainResponseDto?> UpdateAsync(int id, TrainUpdateDto updateDto)
    {
        var existingTrain = await _trainRepository.GetByIdAsync(id);
        if (existingTrain == null)
            throw new NotFoundException($"Treino com ID {id} não encontrado");

        _mapper.Map(updateDto, existingTrain);
        var updatedTrain = await _trainRepository.UpdateAsync(existingTrain);
        return _mapper.Map<TrainResponseDto>(updatedTrain);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exists = await _trainRepository.ExistsAsync(id);
        if (!exists)
            throw new NotFoundException($"Treino com ID {id} não encontrado");

        return await _trainRepository.DeleteAsync(id);
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var exists = await _trainRepository.ExistsAsync(id);
        if (!exists)
            throw new NotFoundException($"Treino com ID {id} não encontrado");

        return await _trainRepository.SoftDeleteAsync(id);
    }

    public async Task<int> GetCountAsync()
    {
        return await _trainRepository.GetCountAsync();
    }

    public async Task<IEnumerable<TrainResponseDto>> GetActiveTrainsAsync()
    {
        var trains = await _trainRepository.GetActiveTrainsAsync();
        return _mapper.Map<IEnumerable<TrainResponseDto>>(trains);
    }

    public async Task<IEnumerable<TrainResponseDto>> GetTrainsByDateRangeAsync(
        DateTime startDate,
        DateTime endDate
    )
    {
        if (startDate > endDate)
            throw new ValidationException("Data de início deve ser anterior à data de fim");

        var trains = await _trainRepository.GetTrainsByDateRangeAsync(startDate, endDate);
        return _mapper.Map<IEnumerable<TrainResponseDto>>(trains);
    }

    public async Task<IEnumerable<TrainResponseDto>> GetTrainsByTypeAsync(TypeTrain typeTrain)
    {
        var trains = await _trainRepository.GetTrainsByTypeAsync(typeTrain);
        return _mapper.Map<IEnumerable<TrainResponseDto>>(trains);
    }

    public async Task<IEnumerable<TrainResponseDto>> GetTrainsFromTodayAsync()
    {
        var trains = await _trainRepository.GetTrainsFromTodayAsync();
        return _mapper.Map<IEnumerable<TrainResponseDto>>(trains);
    }

    public async Task<IEnumerable<TrainResponseDto>> GetUpcomingTrainsAsync()
    {
        var trains = await _trainRepository.GetUpcomingTrainsAsync();
        return _mapper.Map<IEnumerable<TrainResponseDto>>(trains);
    }
}
