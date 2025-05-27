using System.ComponentModel.DataAnnotations;
using AutoMapper;
using TeamManager.Domain.DTOs;
using TeamManager.Domain.Enum;
using TeamManager.Domain.Exceptions;
using TeamManager.Domain.Interfaces.Repoitories;
using TeamManager.Domain.Interfaces.Services;
using TeamManager.Domain.Model;

namespace TeamManager.Application.Services;

public class WorkoutService : IWorkoutService
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly IMapper _mapper;

    public WorkoutService(IWorkoutRepository workoutRepository, IMapper mapper)
    {
        _workoutRepository = workoutRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TrainResponseDto>> GetAllAsync()
    {
        var trains = await _workoutRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<TrainResponseDto>>(trains);
    }

    public async Task<TrainResponseDto?> GetByIdAsync(int id)
    {
        var train = await _workoutRepository.GetByIdAsync(id);
        return train != null ? _mapper.Map<TrainResponseDto>(train) : null;
    }

    public async Task<TrainResponseDto> CreateAsync(TrainCreateDto createDto)
    {
        var train = _mapper.Map<Workout>(createDto);
        var createdTrain = await _workoutRepository.CreateAsync(train);
        return _mapper.Map<TrainResponseDto>(createdTrain);
    }

    public async Task<TrainResponseDto?> UpdateAsync(int id, TrainUpdateDto updateDto)
    {
        var existingTrain = await _workoutRepository.GetByIdAsync(id);
        if (existingTrain == null)
            throw new NotFoundException($"Treino com ID {id} não encontrado");

        _mapper.Map(updateDto, existingTrain);
        var updatedTrain = await _workoutRepository.UpdateAsync(existingTrain);
        return _mapper.Map<TrainResponseDto>(updatedTrain);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exists = await _workoutRepository.ExistsAsync(id);
        if (!exists)
            throw new NotFoundException($"Treino com ID {id} não encontrado");

        return await _workoutRepository.DeleteAsync(id);
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var exists = await _workoutRepository.ExistsAsync(id);
        if (!exists)
            throw new NotFoundException($"Treino com ID {id} não encontrado");

        return await _workoutRepository.SoftDeleteAsync(id);
    }

    public async Task<int> GetCountAsync()
    {
        return await _workoutRepository.GetCountAsync();
    }

    public async Task<IEnumerable<TrainResponseDto>> GetActiveTrainsAsync()
    {
        var trains = await _workoutRepository.GetActiveTrainsAsync();
        return _mapper.Map<IEnumerable<TrainResponseDto>>(trains);
    }

    public async Task<IEnumerable<TrainResponseDto>> GetTrainsByDateRangeAsync(
        DateTime startDate,
        DateTime endDate
    )
    {
        if (startDate > endDate)
            throw new ValidationException("Data de início deve ser anterior à data de fim");

        var trains = await _workoutRepository.GetTrainsByDateRangeAsync(startDate, endDate);
        return _mapper.Map<IEnumerable<TrainResponseDto>>(trains);
    }

    public async Task<IEnumerable<TrainResponseDto>> GetTrainsByTypeAsync(TypeWorkout typeWorkou)
    {
        var trains = await _workoutRepository.GetTrainsByTypeAsync(typeWorkou);
        return _mapper.Map<IEnumerable<TrainResponseDto>>(trains);
    }

    public async Task<IEnumerable<TrainResponseDto>> GetTrainsFromTodayAsync()
    {
        var trains = await _workoutRepository.GetTrainsFromTodayAsync();
        return _mapper.Map<IEnumerable<TrainResponseDto>>(trains);
    }

    public async Task<IEnumerable<TrainResponseDto>> GetUpcomingTrainsAsync()
    {
        var trains = await _workoutRepository.GetUpcomingTrainsAsync();
        return _mapper.Map<IEnumerable<TrainResponseDto>>(trains);
    }
}
