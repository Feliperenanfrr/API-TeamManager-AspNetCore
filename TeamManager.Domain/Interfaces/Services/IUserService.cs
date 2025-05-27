using TeamManager.Domain.DTOs;

namespace TeamManager.Domain.Interfaces.Services;

public interface IUserService
{
    Task<IEnumerable<UserResponseDto>> GetAllAsync();
    Task<UserResponseDto?> GetByIdAsync(int id);
    Task<UserResponseDto?> GetByEmailAsync(string email);
    Task<UserResponseDto> CreateAsync(UserCreateDto createDto);
    Task<UserResponseDto?> UpdateAsync(int id, UserUpdateDto updateDto);
    Task<bool> DeleteAsync(int id);
    Task<bool> SoftDeleteAsync(int id);
    Task<int> GetCountAsync();
    Task<IEnumerable<UserResponseDto>> GetActiveUsersAsync();
    Task<bool> ChangePasswordAsync(UserChangePasswordDto changePasswordDto);
    Task<UserResponseDto?> AuthenticateAsync(UserLoginDto loginDto);
    Task<bool> EmailExistsAsync(string email);
}
