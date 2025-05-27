using Microsoft.EntityFrameworkCore;
using TeamManager.Domain.Enum;
using TeamManager.Domain.Interfaces.Repoitories;
using TeamManager.Domain.Model;
using TeamManager.Infrastructure.Data;

namespace TeamManager.Infrastructure.Repositories;

public class CoachRepository : ICoachRepository
{
    private readonly AppDbContext _context;

    public CoachRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Coach>> GetAllAsync()
    {
        return await _context.Coaches.ToListAsync();
    }

    public async Task<Coach?> GetByIdAsync(int id)
    {
        return await _context.Coaches.FindAsync(id);
    }

    public async Task<Coach> CreateAsync(Coach coach)
    {
        _context.Coaches.Add(coach);
        await _context.SaveChangesAsync();
        return coach;
    }

    public async Task<Coach> UpdateAsync(Coach coach)
    {
        _context.Coaches.Update(coach);
        await _context.SaveChangesAsync();
        return coach;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var coach = await _context.Coaches.FindAsync(id);
        if (coach == null)
            return false;

        _context.Coaches.Remove(coach);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Coaches.AnyAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Coach>> GetByRoleAsync(CoachRole role)
    {
        return await _context.Coaches.Where(c => c.Role == role).ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.Coaches.CountAsync();
    }
}
