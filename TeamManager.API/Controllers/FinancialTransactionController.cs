using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamManager.Domain.DTOs;
using TeamManager.Domain.Exceptions;
using TeamManager.Domain.Interfaces.Repoitories;
using TeamManager.Domain.Interfaces.Services;
using TeamManager.Domain.Model;
using TeamManager.Infrastructure.Data;

namespace TeamManager.Controllers;

[ApiController]
[Route("api/financialtransaction")]
public class FinancialTransactionController : ControllerBase
{
    private readonly IFinancialTransactionService _financialTransactionService;

    public FinancialTransactionController(IFinancialTransactionService financialTransactionService)
    {
        _financialTransactionService = financialTransactionService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FinancialTransactionResponseDto>>> GetAll()
    {
        var transactions = await _financialTransactionService.GetAllAsync();
        return Ok(transactions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FinancialTransactionResponseDto>> GetById(int id)
    {
        if (id <= 0)
            return BadRequest("ID deve ser maior que zero");

        var transaction = await _financialTransactionService.GetByIdAsync(id);

        if (transaction == null)
            return NotFound($"Transação com ID {id} não encontrado");

        return Ok(transaction);
    }

    [HttpPost]
    public async Task<ActionResult<FinancialTransactionResponseDto>> Create(
        [FromBody] FinancialTransactionCreateDto createDto
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var createdTransaction = await _financialTransactionService.CreateAsync(createDto);
            return CreatedAtAction(
                nameof(GetById),
                new { id = createdTransaction.Id },
                createdTransaction
            );
        }
        catch (Exception e)
        {
            return BadRequest($"Erro ao criar transação: {e.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<FinancialTransactionResponseDto>> Update(
        int id,
        [FromBody] FinancialTransactionUpdateDto updateDto
    )
    {
        if (id <= 0)
            return BadRequest("ID deve ser maior que zero");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var updatedTransaction = await _financialTransactionService.UpdateAsync(id, updateDto);
            return Ok(updatedTransaction);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("type/{isIncome}")]
    public async Task<ActionResult<IEnumerable<FinancialTransactionResponseDto>>> GetByType(
        bool isIncome
    )
    {
        var transactions = await _financialTransactionService.GetByTypeAsync(isIncome);
        return Ok(transactions);
    }

    [HttpGet("date-range")]
    public async Task<ActionResult<IEnumerable<FinancialTransactionResponseDto>>> GetByDateRange(
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate
    )
    {
        if (startDate > endDate)
            return BadRequest("Data inicial deve ser anterior à data final");

        var transactions = await _financialTransactionService.GetByDateRangeAsync(
            startDate,
            endDate
        );
        return Ok(transactions);
    }

    [HttpGet("balance")]
    public async Task<ActionResult<double>> GetBalance()
    {
        var balance = await _financialTransactionService.GetBalanceAsync();
        return Ok(new { Balance = balance });
    }

    [HttpGet("income/total")]
    public async Task<ActionResult<double>> GetTotalIncome()
    {
        var totalIncome = await _financialTransactionService.GetTotalIncomeAsync();
        return Ok(new { TotalIncome = totalIncome });
    }

    [HttpGet("expenses/total")]
    public async Task<ActionResult<double>> GetTotalExpenses()
    {
        var totalExpenses = await _financialTransactionService.GetTotalExpenseAsync();
        return Ok(new { TotalExpenses = totalExpenses });
    }
}
