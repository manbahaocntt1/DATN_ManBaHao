using Microsoft.EntityFrameworkCore;
using TravelBackend.Data;
using TravelBackend.Entities;
using TravelBackend.Repositories;

namespace TravelBackend.Repositories.Implementations
{
    public class VolunteerRequestRepository : Repository<VolunteerRequest>, IVolunteerRequestRepository
    {
        private readonly TravelDbContext _context;
        public VolunteerRequestRepository(TravelDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VolunteerRequest>> GetRequestsByUserIdAsync(int userId)
            => await _context.VolunteerRequests.Where(r => r.UserId == userId).ToListAsync();

        public async Task<IEnumerable<VolunteerRequest>> GetPendingRequestsAsync()
            => await _context.VolunteerRequests.Where(r => r.Status == "pending").ToListAsync();

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
    }
}
