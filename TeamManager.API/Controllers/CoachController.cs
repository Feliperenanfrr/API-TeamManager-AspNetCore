using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamManager.Domain.Model;
using TeamManager.Infrastructure.Data;

namespace TeamManager.Controllers;

[ApiController]
[Route("api/coach")]
public class CoachController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Coach>>> GetAllCoaches()
    {
        IEnumerable<Coach> coaches = await context.Coaches.ToListAsync();

        return Ok(coaches);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Coach>> GetCoachById(int id)
    {
        var coach = await context.Coaches.FindAsync(id);
        if (coach == null)
        {
            return NotFound();
        }

        return coach;
    }

    [HttpPost]
    public async Task<ActionResult<Coach>> AddCoach(Coach coach)
    {
        context.Coaches.Add(coach);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCoachById), new { id = coach.Id }, coach);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Coach>> UpdateCoach(int id, Coach coach)
    {
        if (id != coach.Id)
        {
            return BadRequest();
        }

        try
        {
            context.Entry(coach).State = EntityState.Modified;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CoachExists(id))
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
    public async Task<ActionResult> DeleteCoach(int id)
    {
        var coach = await context.Coaches.FindAsync(id);

        if (coach == null)
        {
            return NotFound();
        }

        context.Coaches.Remove(coach);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool CoachExists(int id)
    {
        return context.Coaches.Any(e => e.Id == id);
    }
}
