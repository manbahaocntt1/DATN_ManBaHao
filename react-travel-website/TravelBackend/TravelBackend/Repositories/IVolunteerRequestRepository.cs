using TravelBackend.Entities;

namespace TravelBackend.Repositories
{
    public interface IVolunteerRequestRepository : IRepository<VolunteerRequest>
    {
        // Add a new request (inherited from IRepository)

        // Get all requests for a specific user
        Task<IEnumerable<VolunteerRequest>> GetRequestsByUserIdAsync(int userId);

        // Get all pending requests (for volunteers/admin)
        Task<IEnumerable<VolunteerRequest>> GetPendingRequestsAsync();

        // Update request status
        Task UpdateRequestStatusAsync(int requestId, string status);
        Task<List<VolunteerRequest>> GetOpenRequestsMatchingLanguagesAsync(string[] languages);
    }
}
