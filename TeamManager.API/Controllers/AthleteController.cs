using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamManager.Domain.Common;
using TeamManager.Domain.DTOs;
using TeamManager.Domain.Enum;
using TeamManager.Domain.Exceptions;
using TeamManager.Domain.Interfaces.Services;
using TeamManager.Domain.Model;
using TeamManager.Infrastructure.Data;

namespace TeamManager.Controllers;

[ApiController]
[Route("api/athlete")]
[Produces("application/json")]
public class AthleteController : ControllerBase
{
    private readonly IAthleteService _athleteService;

    public AthleteController(IAthleteService athleteService)
    {
        _athleteService = athleteService;
    }

    [HttpGet]
    [ProducesResponseType<PaginationResponse<AthleteResponseDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginationResponse<AthleteResponseDto>>> GetAllAthletes(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null,
        [FromQuery] string? sortBy = null,
        [FromQuery] bool sortDescending = false
    )
    {
        var request = new PaginationRequest(page, pageSize, search, sortBy, sortDescending);
        var athletes = await _athleteService.GetAllAthletesAsync(request);

        return Ok(athletes);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<AthleteResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AthleteResponseDto>> GetAthleteById(int id)
    {
        var athlete = await _athleteService.GetAthleteByIdAsync(id);

        if (athlete == null)
            return NotFound(new { message = $"Atleta com ID {id} não encontrado" });

        return athlete;
    }

    [HttpGet("by-position/{position:int}")]
    [ProducesResponseType<IEnumerable<AthleteResponseDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AthleteResponseDto>>> GetAthletesByPosition(
        Positions position
    )
    {
        var athletes = await _athleteService.GetAthletesByPositionAsync(position);
        return Ok(athletes);
    }

    [HttpGet("count")]
    [ProducesResponseType<int>(StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> GetAthleteCount()
    {
        var count = await _athleteService.GetAthletesCountAsync();
        return Ok(count);
    }

    [HttpPost]
    [ProducesResponseType<AthleteResponseDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AthleteResponseDto>> CreateAthlete(
        [FromBody] AthleteCreateDto athleteDto
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var createdAthlete = await _athleteService.CreateAthleteAsync(athleteDto);

            return CreatedAtAction(
                nameof(GetAthleteById),
                new { id = createdAthlete.Id },
                createdAthlete
            );
        }
        catch (BusinessException e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType<AthleteResponseDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AthleteResponseDto>> UpdateAthlete(
        int id,
        [FromBody] AthleteUpdateDto athleteDto
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (id != athleteDto.Id)
            return BadRequest(
                new { message = "ID da rota não confere com o ID do corpo da requisição" }
            );

        try
        {
            var updatedAthlete = await _athleteService.UpdateAthleteAsync(id, athleteDto);
            return Ok(updatedAthlete);
        }
        catch (NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (BusinessException e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteAthlete(int id)
    {
        try
        {
            await _athleteService.DeleteAthleteAsync(id);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
    }
}
