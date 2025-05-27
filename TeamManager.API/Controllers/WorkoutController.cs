using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TeamManager.Domain.DTOs;
using TeamManager.Domain.Enum;
using TeamManager.Domain.Exceptions;
using TeamManager.Domain.Interfaces.Services;
using TeamManager.Domain.Model;

namespace TeamManager.Controllers;

[ApiController]
[Route("api/workout")]
public class WorkoutController : ControllerBase
{
    private readonly IWorkoutService _workoutService;

    public WorkoutController(IWorkoutService workoutService)
    {
        _workoutService = workoutService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Workout>>> GetAll()
    {
        try
        {
            var trains = await _workoutService.GetAllAsync();
            return Ok(trains);
        }
        catch (Exception ex)
        {
            return StatusCode(
                500,
                new { message = "Erro interno do servidor", details = ex.Message }
            );
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Workout>> GetById(int id)
    {
        try
        {
            var train = await _workoutService.GetByIdAsync(id);
            if (train == null)
                return NotFound(new { message = $"Treino com ID {id} não encontrado" });

            return Ok(train);
        }
        catch (Exception ex)
        {
            return StatusCode(
                500,
                new { message = "Erro interno do servidor", details = ex.Message }
            );
        }
    }

    [HttpPost]
    public async Task<ActionResult<TrainResponseDto>> Create([FromBody] TrainCreateDto createDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var train = await _workoutService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = train.Id }, train);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(
                500,
                new { message = "Erro interno do servidor", details = ex.Message }
            );
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TrainResponseDto>> Update(
        [FromRoute] int id,
        [FromBody] TrainUpdateDto updateDto
    )
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != updateDto.Id)
                return BadRequest(
                    new { message = "ID da URL não coincide com ID do corpo da requisição" }
                );

            var train = await _workoutService.UpdateAsync(id, updateDto);
            return Ok(train);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(
                500,
                new { message = "Erro interno do servidor", details = ex.Message }
            );
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _workoutService.DeleteAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(
                500,
                new { message = "Erro interno do servidor", details = ex.Message }
            );
        }
    }

    [HttpPatch("{id:int}/deactivate")]
    public async Task<ActionResult> SoftDelete(int id)
    {
        try
        {
            await _workoutService.SoftDeleteAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(
                500,
                new { message = "Erro interno do servidor", details = ex.Message }
            );
        }
    }

    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCount()
    {
        try
        {
            var count = await _workoutService.GetCountAsync();
            return Ok(new { count });
        }
        catch (Exception ex)
        {
            return StatusCode(
                500,
                new { message = "Erro interno do servidor", details = ex.Message }
            );
        }
    }

    [HttpGet("by-date-range")]
    public async Task<ActionResult<IEnumerable<TrainResponseDto>>> GetTrainsByDateRange(
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate
    )
    {
        try
        {
            var trains = await _workoutService.GetTrainsByDateRangeAsync(startDate, endDate);
            return Ok(trains);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(
                500,
                new { message = "Erro interno do servidor", details = ex.Message }
            );
        }
    }

    [HttpGet("by-type/{typeWorkou:int}")]
    public async Task<ActionResult<IEnumerable<TrainResponseDto>>> GetTrainsByType(
        TypeWorkout typeWorkou
    )
    {
        try
        {
            if (!Enum.IsDefined(typeof(TypeWorkout), typeWorkou))
                return BadRequest(new { message = "Tipo de treino inválido" });

            var trains = await _workoutService.GetTrainsByTypeAsync(typeWorkou);
            return Ok(trains);
        }
        catch (Exception ex)
        {
            return StatusCode(
                500,
                new { message = "Erro interno do servidor", details = ex.Message }
            );
        }
    }

    [HttpGet("today")]
    public async Task<ActionResult<IEnumerable<TrainResponseDto>>> GetTrainsFromToday()
    {
        try
        {
            var trains = await _workoutService.GetTrainsFromTodayAsync();
            return Ok(trains);
        }
        catch (Exception ex)
        {
            return StatusCode(
                500,
                new { message = "Erro interno do servidor", details = ex.Message }
            );
        }
    }

    [HttpGet("upcoming")]
    public async Task<ActionResult<IEnumerable<TrainResponseDto>>> GetUpcomingTrains()
    {
        try
        {
            var trains = await _workoutService.GetUpcomingTrainsAsync();
            return Ok(trains);
        }
        catch (Exception ex)
        {
            return StatusCode(
                500,
                new { message = "Erro interno do servidor", details = ex.Message }
            );
        }
    }
}
