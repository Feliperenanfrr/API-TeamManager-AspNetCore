using Microsoft.EntityFrameworkCore;
using TeamManager.Domain.Enum;
using TeamManager.Domain.Interfaces.Repoitories;
using TeamManager.Domain.Model;
using TeamManager.Infrastructure.Data;

namespace TeamManager.Infrastructure.Repositories;

public class WorkoutRepository : IWorkoutRepository
{
    private readonly AppDbContext _context;

    public WorkoutRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Workout>> GetAllAsync()
    {
        return await _context.Trains.ToListAsync();
    }

    public async Task<Workout?> GetByIdAsync(int id)
    {
        return await _context.Trains.FindAsync(id);
    }

    public async Task<Workout> CreateAsync(Workout workout)
    {
        workout.CreatedAt = DateTime.UtcNow;
        workout.IsActive = true;

        _context.Trains.Add(workout);
        await _context.SaveChangesAsync();
        return workout;
    }

    public async Task<Workout> UpdateAsync(Workout workout)
    {
        workout.UpdatedAt = DateTime.UtcNow;

        _context.Trains.Update(workout);
        await _context.SaveChangesAsync();
        return workout;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var train = await _context.Trains.FindAsync(id);
        if (train == null)
            return false;

        _context.Trains.Remove(train);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var train = await _context.Trains.FindAsync(id);
        if (train == null)
            return false;

        train.IsActive = false;
        train.UpdatedAt = DateTime.UtcNow;

        _context.Trains.Update(train);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Trains.AnyAsync(t => t.Id == id);
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.Trains.CountAsync();
    }

    public async Task<IEnumerable<Workout>> GetActiveTrainsAsync()
    {
        return await _context.Trains.Where(t => t.IsActive).OrderBy(t => t.Date).ToListAsync();
    }

    public async Task<IEnumerable<Workout>> GetTrainsByDateRangeAsync(
        DateTime startDate,
        DateTime endDate
    )
    {
        return await _context
            .Trains.Where(t => t.IsActive && t.Date >= startDate && t.Date <= endDate)
            .OrderBy(t => t.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Workout>> GetTrainsByTypeAsync(TypeWorkout typeWorkou)
    {
        return await _context
            .Trains.Where(t => t.IsActive && t.TypeWorkou == typeWorkou)
            .OrderBy(t => t.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Workout>> GetTrainsFromTodayAsync()
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);

        return await _context
            .Trains.Where(t => t.IsActive && t.Date >= today && t.Date < tomorrow)
            .OrderBy(t => t.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Workout>> GetUpcomingTrainsAsync()
    {
        var now = DateTime.Now;

        return await _context
            .Trains.Where(t => t.IsActive && t.Date > now)
            .OrderBy(t => t.Date)
            .ToListAsync();
    }
}
