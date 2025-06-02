using Microsoft.EntityFrameworkCore;
using TravelBackend.Data;
using TravelBackend.Entities;
using TravelBackend.Repositories;

namespace TravelBackend.Repositories.Implementations
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        private readonly TravelDbContext _context;
        public JobRepository(TravelDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Job>> GetActiveJobsAsync(string? langCode = null)
            => await _context.Jobs.Where(j => j.IsActive && (langCode == null || j.LangCode == langCode)).ToListAsync();

        public async Task<IEnumerable<Job>> GetJobsByUserIdAsync(int userId, string? langCode = null)
            => await _context.Jobs.Where(j => j.PostedBy == userId && (langCode == null || j.LangCode == langCode)).ToListAsync();

        public async Task<IEnumerable<Job>> SearchJobsAsync(string keyword, string? langCode = null)
            => await _context.Jobs.Where(j =>
                (!string.IsNullOrEmpty(j.Title) && j.Title.Contains(keyword)) ||
                (!string.IsNullOrEmpty(j.Description) && j.Description.Contains(keyword)) &&
                (langCode == null || j.LangCode == langCode))
                .ToListAsync();

        public async Task UpdateJobStatusAsync(int jobId, bool isActive)
        {
            var job = await _context.Jobs.FindAsync(jobId);
            if (job != null)
            {
                job.IsActive = isActive;
                _context.Jobs.Update(job);
                await _context.SaveChangesAsync();
            }
        }
    }
}
