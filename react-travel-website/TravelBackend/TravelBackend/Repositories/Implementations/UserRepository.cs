using Microsoft.EntityFrameworkCore;
using TravelBackend.Data;
using TravelBackend.Entities;
using TravelBackend.Models.DTOs;

namespace TravelBackend.Repositories.Implementations
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        private readonly new TravelDbContext _context; 

        public UserRepository(TravelDbContext context) : base(context) // Ensure the correct type is used here
        {
            _context = context;
        }

        // Registration
        public async Task AddUserAsync(Users user)
        {
            await AddAsync(user);
            await SaveChangesAsync();
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        // Authentication
        public async Task<Users?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        

        // Password management
        

        public async Task UpdatePasswordAsync(int userId, string newPasswordHash)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.PasswordHash = newPasswordHash;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateProfileAsync(Users partialUser)
        {
            // 1. Load the existing user from DB
            var user = await _context.Users.FindAsync(partialUser.UserId);
            if (user == null)
                throw new Exception("User not found.");

            // 2. Update only the allowed fields (profile info)
            user.FullName = partialUser.FullName;
            user.Nationality = partialUser.Nationality;
            user.PreferredLang = partialUser.PreferredLang;
            user.AvatarUrl = partialUser.AvatarUrl;

            // 3. Save
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }


        // For password reset (find by email, update password)
        public async Task<Users?> FindByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        // (Optional) For social login
        public async Task<Users?> GetBySocialIdAsync(string provider, string socialId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.SocialProvider == provider && u.SocialId == socialId);
        }


        // Admin site
        public async Task<IEnumerable<Users>> GetAllUsersAsync(bool? isActive = null)
        {
            var query = _context.Users.AsQueryable();
            if (isActive.HasValue)
                query = query.Where(u => u.IsActive == isActive.Value);
            return await query.ToListAsync();
        }

        public async Task UpdateUserAsync(Users user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DisableUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.IsActive = false;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EnableUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.IsActive = true;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Users>> SearchUsersAsync(string keyword)
        {
            return await _context.Users
                .Where(u => u.FullName.Contains(keyword) || u.Email.Contains(keyword))
                .ToListAsync();
        }
        public async Task<VolunteerProfile?> GetVolunteerProfileAsync(int userId)
        {
            return await _context.VolunteerProfiles
                .Include(vp => vp.User) // Include navigation to Users if needed
                .FirstOrDefaultAsync(vp => vp.UserId == userId);
        }

        public async Task<List<VolunteerProfileDto>> GetAllVolunteerProfilesDtoAsync()
        {
            var profiles = await _context.VolunteerProfiles
                .Include(vp => vp.User)
                .ToListAsync();

            return profiles.Select(vp => new VolunteerProfileDto
            {
                UserId = vp.UserId,
                FullName = vp.User?.FullName,
                University = vp.University,
                Major = vp.Major,
                Languages = vp.Languages,
                ContactInfo = vp.ContactInfo,
                IsApproved = vp.IsApproved
            }).ToList();
        }

    }
}
