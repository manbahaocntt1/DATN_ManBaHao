using TravelBackend.Entities;

namespace TravelBackend.Repositories
{
    public interface IVolunteerRepository : IRepository<Volunteer>
    {
        // Get all approved volunteers
        Task<IEnumerable<Volunteer>> GetApprovedVolunteersAsync();

        // Search volunteers by language or university
        Task<IEnumerable<Volunteer>> SearchVolunteersAsync(string? language = null, string? university = null);

        // Approve a volunteer
        Task ApproveVolunteerAsync(int volunteerId);
    }
}
