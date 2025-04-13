using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamManager.Data;
using TeamManager.Model;

namespace TeamManager.Controllers;

[ApiController]
[Route("api/train")]
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
    
}