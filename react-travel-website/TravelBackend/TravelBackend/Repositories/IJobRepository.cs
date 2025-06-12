using TravelBackend.Entities;

namespace TravelBackend.Repositories
{
    public interface IJobRepository : IRepository<Job>
    {
        // Get all active jobs
        Task<IEnumerable<Job>> GetActiveJobsAsync(string? langCode = null);

        // Get jobs by poster (user)
        Task<IEnumerable<Job>> GetJobsByUserIdAsync(int userId, string? langCode = null);

        // Search jobs by keyword
        Task<IEnumerable<Job>> SearchJobsAsync(string keyword, string? langCode = null);

        // (Optional) Update job status (active/inactive)
        Task UpdateJobStatusAsync(int jobId, bool isActive);
    }
}
