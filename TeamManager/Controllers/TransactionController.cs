using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamManager.Data;
using TeamManager.Model;

namespace TeamManager.Controllers;

[ApiController]
[Route("api/Transaction")]
public class TransactionController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Transaction>>> GetAllTransactions()
    {
        IEnumerable<Transaction> transactions = await context.Transactions.ToListAsync();
        return Ok(transactions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Transaction>> GetTransaction(int id)
    {
        var transaction = await context.Transactions.FindAsync(id);

        if (transaction == null)
        {
            return NotFound();
        }
        return Ok(transaction);
    }

    [HttpPost]
    public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
    {
        context.Transactions.Add(transaction);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Train>> PutTransaction(
        [FromRoute] int id,
        [FromBody] Transaction transaction
    )
    {
        if (id != transaction.Id)
        {
            return BadRequest();
        }

        context.Entry(transaction).State = EntityState.Modified;

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
        var transaction = await context.Transactions.FindAsync(id);

        if (transaction == null)
        {
            return NotFound();
        }

        context.Transactions.Remove(transaction);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool TransactionExists(int id)
    {
        return context.Transactions.Any(e => e.Id == id);
    }
}
