using AutoMapper;
using TeamManager.Domain.DTOs;
using TeamManager.Domain.Exceptions;
using TeamManager.Domain.Interfaces.Repoitories;
using TeamManager.Domain.Interfaces.Services;
using TeamManager.Domain.Model;

namespace TeamManager.Application.Services;

public class FinancialTransactionService : IFinancialTransactionService
{
    private readonly IFinancialTransactionRepository _financialTransactionRepository;
    private readonly IMapper _mapper;

    public FinancialTransactionService(
        IFinancialTransactionRepository financialTransactionRepository,
        IMapper mapper
    )
    {
        _financialTransactionRepository = financialTransactionRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FinancialTransactionResponseDto>> GetAllAsync()
    {
        var transactions = await _financialTransactionRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<FinancialTransactionResponseDto>>(transactions);
    }

    public async Task<FinancialTransactionResponseDto?> GetByIdAsync(int id)
    {
        var transaction = await _financialTransactionRepository.GetByIdAsync(id);
        return transaction != null
            ? _mapper.Map<FinancialTransactionResponseDto>(transaction)
            : null;
    }

    public async Task<FinancialTransactionResponseDto> CreateAsync(
        FinancialTransactionCreateDto createDto
    )
    {
        var transaction = _mapper.Map<FinancialTransaction>(createDto);
        var createdTransaction = await _financialTransactionRepository.CreateAsync(transaction);

        return _mapper.Map<FinancialTransactionResponseDto>(createdTransaction);
    }

    public async Task<FinancialTransactionResponseDto?> UpdateAsync(
        int id,
        FinancialTransactionUpdateDto transactionDto
    )
    {
        var existingTransaction = await _financialTransactionRepository.GetByIdAsync(id);

        if (existingTransaction == null)
            throw new NotFoundException($"Transação com ID {id} não foi encontrada");

        _mapper.Map(transactionDto, existingTransaction);
        var updatedTransaction = await _financialTransactionRepository.UpdateAsync(
            existingTransaction
        );

        return _mapper.Map<FinancialTransactionResponseDto>(updatedTransaction);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exists = await _financialTransactionRepository.ExistsAsync(id);

        if (!exists)
            throw new NotFoundException($"Transação com ID {id} não foi encontrada");

        return await _financialTransactionRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<FinancialTransactionResponseDto>> GetByTypeAsync(
        bool typeTransaction
    )
    {
        var transactions = await _financialTransactionRepository.GetByTypeAsync(typeTransaction);
        return _mapper.Map<IEnumerable<FinancialTransactionResponseDto>>(transactions);
    }

    public async Task<IEnumerable<FinancialTransactionResponseDto>> GetByDateRangeAsync(
        DateTime startDate,
        DateTime endDate
    )
    {
        var transactions = await _financialTransactionRepository.GetByDateRangeAsync(
            startDate,
            endDate
        );
        return _mapper.Map<IEnumerable<FinancialTransactionResponseDto>>(transactions);
    }

    public async Task<double> GetBalanceAsync()
    {
        var transactions = await _financialTransactionRepository.GetAllAsync();
        var income = transactions.Where(t => t.TypeTransaction).Sum(t => t.Amount);
        var expenses = transactions.Where(t => !t.TypeTransaction).Sum(t => t.Amount);

        return income - expenses;
    }

    public async Task<double> GetTotalIncomeAsync()
    {
        var incomeTransactions = await _financialTransactionRepository.GetByTypeAsync(true);
        return incomeTransactions.Sum(t => t.Amount);
    }

    public async Task<double> GetTotalExpenseAsync()
    {
        var expenseTransactions = await _financialTransactionRepository.GetByTypeAsync(false);
        return expenseTransactions.Sum(t => t.Amount);
    }
}
