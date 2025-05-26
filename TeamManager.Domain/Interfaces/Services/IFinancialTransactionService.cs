using TeamManager.Domain.Common;
using TeamManager.Domain.DTOs;

namespace TeamManager.Domain.Interfaces.Services;

public interface IFinancialTransactionService
{
    Task<IEnumerable<FinancialTransactionResponseDto>> GetAllAsync();

    Task<FinancialTransactionResponseDto?> GetByIdAsync(int id);

    Task<FinancialTransactionResponseDto> CreateAsync(FinancialTransactionCreateDto createDto);

    Task<FinancialTransactionResponseDto?> UpdateAsync(
        int id,
        FinancialTransactionUpdateDto updateDto
    );

    Task<bool> DeleteAsync(int id);

    Task<IEnumerable<FinancialTransactionResponseDto>> GetByTypeAsync(bool typeTransaction);

    Task<IEnumerable<FinancialTransactionResponseDto>> GetByDateRangeAsync(
        DateTime startDate,
        DateTime endDate
    );

    Task<double> GetBalanceAsync();

    Task<double> GetTotalIncomeAsync();

    Task<double> GetTotalExpenseAsync();
}
