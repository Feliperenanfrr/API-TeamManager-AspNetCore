using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamManager.Data;
using TeamManager.Model;

namespace TeamManager.Controllers;

[Route("api/Athlete")]
[ApiController]
public class AthleteController : ControllerBase
{
    private readonly AppDbContext  _context;
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Athlete>>> GetAllAthletes()
    {
        IEnumerable<Athlete> athletes = await _context.Athletes.ToListAsync();

        return Ok(athletes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Athlete>> GetAthleteById(int id)
    {
        var athlete = await _context.Athletes.FindAsync(id);

        if (athlete == null)
        {
            return NotFound();
        }
        
        return athlete;
    }
    
    [HttpPost]
    public async Task<ActionResult<Athlete>> PostAthlete(Athlete athlete)  
    {
        _context.Athletes.Add(athlete);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetAthleteById), new { id = athlete.Id }, athlete);
    }

    [HttpPut]
    public void Put([FromBody] Athlete athlete)
    {
        
    }

    [HttpDelete]
    public void Delete([FromBody] Athlete athlete)
    {
        
    }
}