using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelBackend.Data;
using TravelBackend.Entities;

namespace TravelBackend.Repositories.Implementations
{
    public class VolunteerRequestRepository : Repository<VolunteerRequest>, IVolunteerRequestRepository
    {
        private readonly TravelDbContext _context;

        public VolunteerRequestRepository(TravelDbContext context) : base(context)
        {
            _context = context;
        }

        // Get all requests for a specific user (traveler)
        public async Task<IEnumerable<VolunteerRequest>> GetRequestsByUserIdAsync(int userId)
        {
            return await _context.VolunteerRequests
                .Where(r => r.UserId == userId)
                .Include(r => r.Assignments)
                .OrderByDescending(r => r.RequestedAt)
                .ToListAsync();
        }

        // Get all pending requests (for volunteers/admin)
        public async Task<IEnumerable<VolunteerRequest>> GetPendingRequestsAsync()
        {
            return await _context.VolunteerRequests
                .Where(r => r.Status == "pending")
                .Include(r => r.Assignments)
                .OrderByDescending(r => r.RequestedAt)
                .ToListAsync();
        }

        // Update request status (accepted/completed/rejected/etc)
        public async Task UpdateRequestStatusAsync(int requestId, string status)
        {
            var request = await _context.VolunteerRequests.FindAsync(requestId);
            if (request != null)
            {
                request.Status = status;
                _context.VolunteerRequests.Update(request);
                await _context.SaveChangesAsync();
            }
        }

        // Get open requests matching any of the volunteer's languages
        public async Task<List<VolunteerRequest>> GetOpenRequestsMatchingLanguagesAsync(string[] languages)
        {
            // Simple matching: if RequiredLanguages contains any language in volunteer's list
            // (Assumes RequiredLanguages is a comma-separated string like "en,vi")
            return await _context.VolunteerRequests
                .Where(r => r.Status == "pending" &&
                            languages.Any(lang =>
                                EF.Functions.Like(r.RequiredLanguages, $"%{lang}%")))
                .Include(r => r.Assignments)
                .OrderByDescending(r => r.RequestedAt)
                .ToListAsync();
        }

        // (Optional) Override base methods as needed, or rely on Repository<T> implementation.
    }
}
