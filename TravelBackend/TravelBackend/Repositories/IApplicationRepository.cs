using TravelBackend.Entities;

namespace TravelBackend.Repositories
{
    public interface IApplicationRepository : IRepository<Appli>
    {
        // Add a new application (inherited from IRepository)

        // Get all applications for a specific user
        Task<IEnumerable<Appli>> GetApplicationsByUserIdAsync(int userId);

        // Get all applications for a specific job
        Task<IEnumerable<Appli>> GetApplicationsByJobIdAsync(int jobId);

        // Update application status
        Task UpdateApplicationStatusAsync(int applicationId, string status);
    }
}
