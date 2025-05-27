using Microsoft.EntityFrameworkCore;
using TeamManager.Domain.Enum;
using TeamManager.Domain.Interfaces.Repoitories;
using TeamManager.Domain.Model;
using TeamManager.Infrastructure.Data;

namespace TeamManager.Infrastructure.Repositories;

public class TrainRepository :  ITrainRepository
{
    private readonly AppDbContext _context;

    public TrainRepository(AppDbContext context)
    {
        _context = context;
    }
    
    
    
    public async Task<IEnumerable<Train>> GetAllAsync()
    {
        return await _context.Trains.ToListAsync();
    }

    public async Task<Train?> GetByIdAsync(int id)
    {
        return await _context.Trains.FindAsync(id);
    }

    public async Task<Train> CreateAsync(Train train)
    {
        train.CreatedAt = DateTime.UtcNow;
        train.IsActive = true;
        
        _context.Trains.Add(train);
        await _context.SaveChangesAsync();
        return train;
        
    }

    public async Task<Train> UpdateAsync(Train train)
    {
        train.UpdatedAt = DateTime.UtcNow;

        _context.Trains.Update(train);
        await _context.SaveChangesAsync();
        return train;
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

    public async Task<IEnumerable<Train>> GetActiveTrainsAsync()
    {
        return await _context.Trains
            .Where(t => t.IsActive)
            .OrderBy(t => t.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Train>> GetTrainsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Trains
            .Where(t => t.IsActive && t.Date >= startDate && t.Date <= endDate)
            .OrderBy(t => t.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Train>> GetTrainsByTypeAsync(TypeTrain typeTrain)
    {
        return await _context.Trains
            .Where(t => t.IsActive && t.TypeTrain == typeTrain)
            .OrderBy(t => t.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Train>> GetTrainsFromTodayAsync()
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);
        
        return await _context.Trains
            .Where(t => t.IsActive && t.Date >= today && t.Date < tomorrow)
            .OrderBy(t => t.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Train>> GetUpcomingTrainsAsync()
    {
        var now = DateTime.Now;
        
        return await _context.Trains
            .Where(t => t.IsActive && t.Date > now)
            .OrderBy(t => t.Date)
            .ToListAsync();
    }
}