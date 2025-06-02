using Microsoft.EntityFrameworkCore;
using TravelBackend.Data;
using TravelBackend.Entities;
using TravelBackend.Repositories;

namespace TravelBackend.Repositories.Implementations
{
    public class ApplicationRepository : Repository<Appli>, IApplicationRepository
    {
        private readonly TravelDbContext _context;
        public ApplicationRepository(TravelDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appli>> GetApplicationsByUserIdAsync(int userId)
            => await _context.Applications.Where(a => a.UserId == userId).ToListAsync();

        public async Task<IEnumerable<Appli>> GetApplicationsByJobIdAsync(int jobId)
            => await _context.Applications.Where(a => a.JobId == jobId).ToListAsync();

        public async Task UpdateApplicationStatusAsync(int applicationId, string status)
        {
            var app = await _context.Applications.FindAsync(applicationId);
            if (app != null)
            {
                app.Status = status;
                _context.Applications.Update(app);
                await _context.SaveChangesAsync();
            }
        }
    }
}
