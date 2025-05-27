using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamManager.Application.Services;
using TeamManager.Domain.DTOs;
using TeamManager.Domain.Exceptions;
using TeamManager.Domain.Interfaces.Repoitories;
using TeamManager.Domain.Interfaces.Services;
using TeamManager.Domain.Model;
using TeamManager.Infrastructure.Data;

namespace TeamManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService  _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {message = "Erro interno do servidor", detail = ex.Message});
        }
    }

    [HttpGet("active")]
    [ProducesResponseType(typeof(IEnumerable<UserResponseDto>), 200)]
    public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetActiveUsers()
    {
        try
        {
            var users = await _userService.GetActiveUsersAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new {message = "Erro interno do servidor", detail = ex.Message});
        }
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(UserResponseDto), 200)]
    public async Task<ActionResult<UserResponseDto>> GetUserByID(int id)
    {
        try
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { message = $"Usuário com ID {id} não encontrado" });
            
            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new  {message = "Erro interno do servidor", detail = ex.Message});
        }
    }
    
    [HttpGet("email/{email}")]
    [ProducesResponseType(typeof(UserResponseDto), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<UserResponseDto>> GetUserByEmail(string email)
    {
        try
        {
            var user = await _userService.GetByEmailAsync(email);
            if (user == null)
                return NotFound(new { message = $"Usuário com email {email} não encontrado" });

            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro interno do servidor", details = ex.Message });
        }
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(UserResponseDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    public async Task<ActionResult<UserResponseDto>> CreateUser([FromBody] UserCreateDto createDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetUserByID), new { id = user.Id }, user);
        }
        catch (ConflictException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro interno do servidor", details = ex.Message });
        }
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(UserResponseDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(409)]
    public async Task<ActionResult<UserResponseDto>> UpdateUser(int id, [FromBody] UserUpdateDto updateDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != updateDto.Id)
                return BadRequest(new { message = "ID da URL não confere com ID do objeto" });

            var user = await _userService.UpdateAsync(id, updateDto);
            return Ok(user);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ConflictException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro interno do servidor", details = ex.Message });
        }
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> DeleteUser(int id)
    {
        try
        {
            var result = await _userService.DeleteAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro interno do servidor", details = ex.Message });
        }
    }
    
    [HttpPatch("{id:int}/deactivate")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> DeactivateUser(int id)
    {
        try
        {
            var result = await _userService.SoftDeleteAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro interno do servidor", details = ex.Message });
        }
    }
    
    [HttpPatch("change-password")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> ChangePassword([FromBody] UserChangePasswordDto changePasswordDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.ChangePasswordAsync(changePasswordDto);
            return Ok(new { message = "Senha alterada com sucesso" });
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (UnauthorizedException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (BadRequestException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro interno do servidor", details = ex.Message });
        }
    }
    
    [HttpPost("authenticate")]
    [ProducesResponseType(typeof(UserResponseDto), 200)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<UserResponseDto>> Authenticate([FromBody] UserLoginDto loginDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.AuthenticateAsync(loginDto);
            if (user == null)
                return Unauthorized(new { message = "Email ou senha inválidos" });

            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro interno do servidor", details = ex.Message });
        }
    }
    
    [HttpGet("email-exists/{email}")]
    [ProducesResponseType(typeof(bool), 200)]
    public async Task<ActionResult<bool>> EmailExists(string email)
    {
        try
        {
            var exists = await _userService.EmailExistsAsync(email);
            return Ok(new { exists });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro interno do servidor", details = ex.Message });
        }
    }
    
    [HttpGet("count")]
    [ProducesResponseType(typeof(int), 200)]
    public async Task<ActionResult<int>> GetUserCount()
    {
        try
        {
            var count = await _userService.GetCountAsync();
            return Ok(new { count });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro interno do servidor", details = ex.Message });
        }
    }





}
