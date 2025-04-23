using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamManager.Data;
using TeamManager.Model;

namespace TeamManager.Controllers;

[ApiController]
[Route("api/train")]
[Authorize]
public class TrainController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Train>>> GetAllTrains()
    {
        IEnumerable<Train> trains = await context.Trains.ToListAsync();

        return Ok(trains);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Train>> GetTrain(int id)
    {
        var train = await context.Trains.FindAsync(id);

        if (train == null)
        {
            return NotFound();
        }

        return Ok(train);
    }

    [HttpPost]
    public async Task<ActionResult<Train>> PostTrain(Train train)
    {
        context.Trains.Add(train);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTrain), new { id = train.Id }, train);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Train>> PutTrain([FromRoute] int id, [FromBody] Train train)
    {
        if (id != train.Id)
        {
            return BadRequest();
        }

        context.Entry(train).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TrainExists(id))
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
    public async Task<ActionResult> DeleteTrain(int id)
    {
        var train = await context.Trains.FindAsync(id);

        if (train == null)
        {
            return NotFound();
        }

        context.Trains.Remove(train);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool TrainExists(int id)
    {
        return context.Trains.Any(e => e.Id == id);
    }
}
