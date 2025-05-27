using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamManager.Domain.DTOs;
using TeamManager.Domain.Exceptions;
using TeamManager.Domain.Interfaces.Services;
using TeamManager.Domain.Model;
using TeamManager.Infrastructure.Data;

namespace TeamManager.Controllers;

[ApiController]
[Route("api/coach")]
public class CoachController : ControllerBase
{
    private readonly ICoachService _coachService;

    public CoachController(ICoachService coachService)
    {
        _coachService = coachService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CoachResponseDto>>> GetAll()
    {
        var coaches = await _coachService.GetAllAsync();
        return Ok(coaches);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CoachResponseDto>> GetById(int id)
    {
        if (id <= 0)
            return BadRequest("O ID do Coach deve ser maior que zero");

        var coach = await _coachService.GetByIdAsync(id);

        if (coach == null)
            return NotFound($"Coach com o ID {id} não foi encontrado");

        return Ok(coach);
    }

    [HttpPost]
    public async Task<ActionResult<CoachResponseDto>> Create([FromBody] CoachCreateDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var createdCoach = await _coachService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCoach.Id }, createdCoach);
        }
        catch (Exception e)
        {
            return BadRequest($"Erro ao criar técnico: {e.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CoachResponseDto>> Update(
        int id,
        [FromBody] CoachUpdateDto updateDto
    )
    {
        if (id <= 0)
            return BadRequest("O ID do Coach deve ser maior que zero");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var updatedCoach = await _coachService.UpdateAsync(id, updateDto);
            return Ok(updatedCoach);
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

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        if (id <= 0)
            return BadRequest("O ID do Coach deve ser maior que zero");

        try
        {
            var deleted = await _coachService.DeleteAsync(id);
            if (deleted)
                return NoContent();

            return BadRequest("Não foi possível deletar o coach");
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

    [HttpGet("role/{role}")]
    public async Task<ActionResult<IEnumerable<CoachResponseDto>>> GetByRole(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
            return BadRequest("Função não pode estar vazia");

        try
        {
            var coaches = await _coachService.GetByRoleAsync(role);
            return Ok(coaches);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCountAsync()
    {
        var count = await _coachService.GetCountAsync();
        return Ok(new { Count = count });
    }
}
