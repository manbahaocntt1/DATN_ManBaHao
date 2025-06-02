using TravelBackend.Entities;

namespace TravelBackend.Repositories
{
    public interface IUserRepository : IRepository<Users>
    {
        // Registration
        Task AddUserAsync(Users user);
        Task<bool> IsEmailExistsAsync(string email);
       

        // Authentication
        Task<Users?> GetByEmailAsync(string email);
       // Task<Users?> GetByUsernameAsync(string username);
       

        // Password management
        //Task<bool> ValidatePasswordAsync(string emailOrUsername, string password);
        Task UpdatePasswordAsync(int userId, string newPasswordHash);

        // Profile update
        Task UpdateProfileAsync(Users user);

        // For password reset (find by email, update password)
        Task<Users?> FindByEmailAsync(string email);

        // (Optional) For social login
        Task<Users?> GetBySocialIdAsync(string provider, string socialId);

        //Admin site

        Task<IEnumerable<Users>> GetAllUsersAsync(bool? isActive = null);
        Task UpdateUserAsync(Users user);
        Task DisableUserAsync(int userId);
        Task EnableUserAsync(int userId);
        Task<IEnumerable<Users>> SearchUsersAsync(string keyword);
    }
}
