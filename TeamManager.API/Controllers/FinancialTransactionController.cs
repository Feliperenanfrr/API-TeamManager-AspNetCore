using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamManager.Domain.Model;
using TeamManager.Infrastructure.Data;

namespace TeamManager.Controllers;

[ApiController]
[Route("api/transaction")]
public class FinancialTransactionController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<FinancialTransaction>>> GetAllTransactions()
    {
        IEnumerable<FinancialTransaction> transactions =
            await context.FinancialTransactions.ToListAsync();
        return Ok(transactions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FinancialTransaction>> GetTransaction(int id)
    {
        var transaction = await context.FinancialTransactions.FindAsync(id);

        if (transaction == null)
        {
            return NotFound();
        }
        return Ok(transaction);
    }

    [HttpPost]
    public async Task<ActionResult<FinancialTransaction>> PostTransaction(
        FinancialTransaction financialTransaction
    )
    {
        context.FinancialTransactions.Add(financialTransaction);
        await context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetTransaction),
            new { id = financialTransaction.Id },
            financialTransaction
        );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Train>> PutTransaction(
        [FromRoute] int id,
        [FromBody] FinancialTransaction financialTransaction
    )
    {
        if (id != financialTransaction.Id)
        {
            return BadRequest();
        }

        context.Entry(financialTransaction).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TransactionExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTransaction(int id)
    {
        var transaction = await context.FinancialTransactions.FindAsync(id);

        if (transaction == null)
        {
            return NotFound();
        }

        context.FinancialTransactions.Remove(transaction);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool TransactionExists(int id)
    {
        return context.FinancialTransactions.Any(e => e.Id == id);
    }
}
