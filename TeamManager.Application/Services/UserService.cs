using AutoMapper;
using TeamManager.Domain.DTOs;
using TeamManager.Domain.Exceptions;
using TeamManager.Domain.Interfaces.Repoitories;
using TeamManager.Domain.Interfaces.Services;
using TeamManager.Domain.Model;

namespace TeamManager.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserResponseDto>>(users);
    }

    public async Task<UserResponseDto?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user != null ? _mapper.Map<UserResponseDto>(user) : null;
    }

    public async Task<UserResponseDto?> GetByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        return user != null ? _mapper.Map<UserResponseDto>(user) : null;
    }

    public async Task<UserResponseDto> CreateAsync(UserCreateDto createDto)
    {
        var emailExists = await _userRepository.ExistsByEmailAsync(createDto.Email);
        if (emailExists)
            throw new ConflictException($"Usuário com email {createDto.Email} já cadastrado");

        var user = _mapper.Map<User>(createDto);

        user.Password = BCrypt.Net.BCrypt.HashPassword(createDto.Password);

        var createdUser = await _userRepository.CreateAsync(user);
        return _mapper.Map<UserResponseDto>(createdUser);
    }

    public async Task<UserResponseDto?> UpdateAsync(int id, UserUpdateDto updateDto)
    {
        var existingUser = await _userRepository.GetByIdAsync(id);
        if (existingUser == null)
            throw new NotFoundException($"Usuário com ID {id} não encontrado");

        if (existingUser.Email != updateDto.Email)
        {
            var emailExists = await _userRepository.ExistsByEmailAsync(updateDto.Email);
            if (emailExists)
                throw new ConflictException(
                    $"Email {updateDto.Email} já está sendo usado por outro usuário"
                );
        }

        _mapper.Map(updateDto, existingUser);
        var updatedUser = await _userRepository.UpdateAsync(existingUser);
        return _mapper.Map<UserResponseDto>(updatedUser);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exists = await _userRepository.ExistsAsync(id);
        if (!exists)
            throw new NotFoundException($"Usuário com ID {id} não encontrado");

        return await _userRepository.DeleteAsync(id);
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var exists = await _userRepository.ExistsAsync(id);
        if (!exists)
            throw new NotFoundException($"Usuário com ID {id} não encontrado");

        return await _userRepository.SoftDeleteAsync(id);
    }

    public async Task<int> GetCountAsync()
    {
        return await _userRepository.GetCountAsync();
    }

    public async Task<IEnumerable<UserResponseDto>> GetActiveUsersAsync()
    {
        var users = await _userRepository.GetActiveUsersAsync();
        return _mapper.Map<IEnumerable<UserResponseDto>>(users);
    }

    public async Task<bool> ChangePasswordAsync(UserChangePasswordDto changePasswordDto)
    {
        var user = await _userRepository.GetByIdAsync(changePasswordDto.Id);
        if (user == null)
            throw new NotFoundException($"Usuário com ID {changePasswordDto.Id} não encontrado");

        if (!BCrypt.Net.BCrypt.Verify(changePasswordDto.CurrentPassword, user.Password))
            throw new UnauthorizedException("Senha atual incorreta");

        user.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);
        await _userRepository.UpdateAsync(user);

        return true;
    }

    public async Task<UserResponseDto?> AuthenticateAsync(UserLoginDto loginDto)
    {
        var user = await _userRepository.GetByEmailForAuthenticationAsync(loginDto.Email);
        if (user == null)
            return null;

        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            return null;

        return _mapper.Map<UserResponseDto>(user);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _userRepository.ExistsByEmailAsync(email);
    }
}
