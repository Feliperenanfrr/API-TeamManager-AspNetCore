using TeamManager.Domain.Model;

namespace TeamManager.Domain.Interfaces.Repoitories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task<User> CreateAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<bool> DeleteAsync(int id);
    Task<bool> SoftDeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> ExistsByEmailAsync(string email);
    Task<int> GetCountAsync();
    Task<IEnumerable<User>> GetActiveUsersAsync();
    Task<User?> GetByEmailForAuthenticationAsync(string email);
}
