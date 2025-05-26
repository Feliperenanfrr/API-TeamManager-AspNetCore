using Microsoft.EntityFrameworkCore;
using TeamManager.Domain.Common;
using TeamManager.Domain.Enum;
using TeamManager.Domain.Interfaces.Repoitories;
using TeamManager.Domain.Model;
using TeamManager.Infrastructure.Data;

namespace TeamManager.Infrastructure.Repositories;

public class AthleteRepository : IAthleteRepository
{
    private readonly AppDbContext _context;

    public AthleteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PaginationResponse<Athlete>> GetAllAsync(PaginationRequest paginationRequest)
    {
        var query = _context.Athletes.Where(a => a.IsActive).AsNoTracking();

        if (!string.IsNullOrWhiteSpace(paginationRequest.Search))
        {
            query = query.Where(a => a.Name.Contains(paginationRequest.Search));
        }

        query = paginationRequest.SortBy?.ToLower() switch
        {
            "name" => paginationRequest.SortDescending
                ? query.OrderByDescending(a => a.Name)
                : query.OrderBy(a => a.Name),
            "age" => paginationRequest.SortDescending
                ? query.OrderByDescending(a => a.BirthDay)
                : query.OrderBy(a => a.BirthDay),
            "position" => paginationRequest.SortDescending
                ? query.OrderByDescending(a => a.Position)
                : query.OrderBy(a => a.Position),
            _ => query.OrderBy(a => a.Name),
        };

        var totalRecords = await query.CountAsync();

        var athletes = await query
            .Skip((paginationRequest.Page - 1) * paginationRequest.PageSize)
            .Take(paginationRequest.PageSize)
            .ToListAsync();

        var totalPages = (int)Math.Ceiling((double)totalRecords / paginationRequest.PageSize);

        return new PaginationResponse<Athlete>(
            athletes,
            totalRecords,
            paginationRequest.Page,
            paginationRequest.PageSize,
            totalPages
        );
    }

    public async Task<Athlete?> GetByIdAsync(int id)
    {
        return await _context
            .Athletes.AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id && a.IsActive);
    }

    public async Task<Athlete> CreateAsync(Athlete athlete)
    {
        athlete.CreatedAt = DateTime.UtcNow;
        athlete.IsActive = true;

        _context.Athletes.Add(athlete);
        await _context.SaveChangesAsync();

        return athlete;
    }

    public async Task<Athlete> UpdateAsync(Athlete athlete)
    {
        athlete.UpdatedAt = DateTime.UtcNow;

        _context.Athletes.Update(athlete);
        await _context.SaveChangesAsync();

        return athlete;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var athlete = await _context.Athletes.FindAsync(id);

        if (athlete == null || !athlete.IsActive)
            return false;

        athlete.IsActive = false;
        athlete.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Athletes.AnyAsync(a => a.Id == id && a.IsActive);
    }

    public async Task<IEnumerable<Athlete>> GetByPositionsAsync(Positions position)
    {
        return await _context
            .Athletes.Where(a => a.Position == position && a.IsActive)
            .AsNoTracking()
            .OrderBy(a => a.Name)
            .ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.Athletes.CountAsync(a => a.IsActive);
    }
}
