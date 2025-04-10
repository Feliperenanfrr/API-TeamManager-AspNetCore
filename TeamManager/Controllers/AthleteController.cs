using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamManager.Data;
using TeamManager.Model;

namespace TeamManager.Controllers;


[ApiController]
[Route("api/athlete")]
public class AthleteController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Athlete>>> GetAllAthletes()
    {
        IEnumerable<Athlete> athletes = await context.Athletes.ToListAsync();

        return Ok(athletes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Athlete>> GetAthleteById(int id)
    {
        var athlete = await context.Athletes.FindAsync(id);

        if (athlete == null)
        {
            return NotFound();
        }
        
        return athlete;
    }
    
    [HttpPost]
    public async Task<ActionResult<Athlete>> PostAthlete(Athlete athlete)  
    {
        context.Athletes.Add(athlete);
        await context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetAthleteById), new { id = athlete.Id }, athlete);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAthlete([FromBody]Athlete athlete, [FromRoute]int id)
    {
        if (id != athlete.Id)
        {
            return BadRequest();
        }
        
        context.Entry(athlete).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AthleteExists(id))
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
    public async Task<ActionResult> DeleteAthlete(int id)
    {
        var athlete = await context.Athletes.FindAsync(id);

        if (athlete == null)
        {
            return NotFound();
        }
        
        context.Athletes.Remove(athlete);
        await context.SaveChangesAsync();
        
        return NoContent();
    }

    private bool AthleteExists(int id)
    {
        return context.Athletes.Any(e => e.Id == id);
    }
}