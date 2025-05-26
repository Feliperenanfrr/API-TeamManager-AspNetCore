using TeamManager.Domain.Model;

namespace TeamManager.Domain.Interfaces.Repoitories;

public interface IFinancialTransactionRepository
{
    Task<IEnumerable<FinancialTransaction>> GetAllAsync();

    Task<FinancialTransaction?> GetByIdAsync(int id);

    Task<FinancialTransaction> CreateAsync(FinancialTransaction transaction);

    Task<FinancialTransaction> UpdateAsync(FinancialTransaction transaction);

    Task<bool> DeleteAsync(int id);

    Task<bool> ExistsAsync(int id);

    Task<IEnumerable<FinancialTransaction>> GetByTypeAsync(bool typeTransaction);

    Task<IEnumerable<FinancialTransaction>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);

}