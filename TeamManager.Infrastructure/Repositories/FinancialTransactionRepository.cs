using Microsoft.EntityFrameworkCore;
using TeamManager.Domain.DTOs;
using TeamManager.Domain.Interfaces.Repoitories;
using TeamManager.Domain.Model;
using TeamManager.Infrastructure.Data;

namespace TeamManager.Infrastructure.Repositories;

public class FinancialTransactionRepository : IFinancialTransactionRepository
{
    private readonly AppDbContext _context;

    public FinancialTransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FinancialTransaction>> GetAllAsync()
    {
        return await _context
            .FinancialTransactions.AsNoTracking()
            .OrderByDescending(t => t.Date)
            .ThenByDescending(t => t.Id)
            .ToListAsync();
    }

    public async Task<FinancialTransaction?> GetByIdAsync(int id)
    {
        return await _context
            .FinancialTransactions.AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<FinancialTransaction> CreateAsync(FinancialTransaction transaction)
    {
        _context.FinancialTransactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<FinancialTransaction> UpdateAsync(FinancialTransaction transaction)
    {
        _context.Entry(transaction).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var transaction = await _context.FinancialTransactions.FindAsync(id);
        if (transaction == null)
            return false;

        _context.FinancialTransactions.Remove(transaction);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.FinancialTransactions.AsNoTracking().AnyAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<FinancialTransaction>> GetByTypeAsync(bool typeTransaction)
    {
        return await _context
            .FinancialTransactions.AsNoTracking()
            .Where(t => t.TypeTransaction == typeTransaction)
            .OrderByDescending(t => t.Date)
            .ThenByDescending(t => t.Id)
            .ToListAsync();
    }

    public async Task<IEnumerable<FinancialTransaction>> GetByDateRangeAsync(
        DateTime startDate,
        DateTime endDate
    )
    {
        return await _context
            .FinancialTransactions.AsNoTracking()
            .Where(t => t.Date >= startDate && t.Date <= endDate)
            .OrderByDescending(t => t.Date)
            .ThenByDescending(t => t.Id)
            .ToListAsync();
    }
}
