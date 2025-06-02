using BCrypt.Net;
using TravelBackend.Entities;
using TravelBackend.Repositories;

namespace TravelBackend.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        //get all users
        public async Task<IEnumerable<Users>> GetAllUsersAsync(bool? isActive = null)
        {
            return await _userRepository.GetAllUsersAsync(isActive);
        }


        // Registration with password
        public async Task RegisterAsync(string fullName, string email, string password, string? nationality, string? preferredLang, string? avatarUrl)
        {
            if (await _userRepository.IsEmailExistsAsync(email))
                throw new Exception("Email already exists.");

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new Users
            {
                FullName = fullName,
                Email = email,
                PasswordHash = passwordHash,
                Nationality = nationality,
                PreferredLang = preferredLang,
                AvatarUrl = avatarUrl,
                UserRole = "traveler",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddUserAsync(user);
        }

        // Login with password
        public async Task<Users?> AuthenticateAsync(string emailOrUsername, string password)
        {
            var user = await _userRepository.GetByEmailAsync(emailOrUsername);
                       

            if (user == null || !user.IsActive)
                return null;

            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            return isValid ? user : null;
        }

        // Social login (find or create)
        public async Task<Users> SocialLoginAsync(string provider, string socialId, string fullName, string email, string? avatarUrl)
        {
            // Try to find user by social provider/id
            var user = await _userRepository.GetBySocialIdAsync(provider, socialId);

            if (user != null)
            {
                // Optionally update profile info from provider
                if (!string.IsNullOrEmpty(avatarUrl) && user.AvatarUrl != avatarUrl)
                {
                    user.AvatarUrl = avatarUrl;
                    await _userRepository.UpdateProfileAsync(user);
                }
                return user;
            }

            // If not found, create a new user
            user = new Users
            {
                FullName = fullName,
                Email = email,
                SocialProvider = provider,
                SocialId = socialId,
                AvatarUrl = avatarUrl,
                UserRole = "traveler",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                PasswordHash = "" // Not used for social login
            };

            await _userRepository.AddUserAsync(user);
            return user;
        }

        // Change password
        public async Task ChangePasswordAsync(int userId, string newPassword)
        {
            string newHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _userRepository.UpdatePasswordAsync(userId, newHash);
        }

        // Reset password (by email)
        public async Task<bool> ResetPasswordAsync(string email, string newPassword)
        {
            var user = await _userRepository.FindByEmailAsync(email);
            if (user == null) return false;
            string newHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _userRepository.UpdatePasswordAsync(user.UserId, newHash);
            return true;
        }

        // Update profile
        public async Task UpdateProfileAsync(Users user)
        {
            await _userRepository.UpdateProfileAsync(user);
        }
        //get user profile by id
        public async Task<Users?> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }


        // Admin: disable/enable user
        public async Task DisableUserAsync(int userId) => await _userRepository.DisableUserAsync(userId);
        public async Task EnableUserAsync(int userId) => await _userRepository.EnableUserAsync(userId);
    }
}
