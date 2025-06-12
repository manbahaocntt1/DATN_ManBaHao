using System.Collections.Generic;
using System.Threading.Tasks;
using TravelBackend.Entities;

namespace TravelBackend.Repositories
{
    public interface IVolunteerRequestAssignmentRepository : IRepository<VolunteerRequestAssignment>
    {
        // Get all assignments for a specific volunteer
        Task<IEnumerable<VolunteerRequestAssignment>> GetAssignmentsByVolunteerUserIdAsync(int volunteerUserId);

        // Get all assignments for a specific request
        Task<IEnumerable<VolunteerRequestAssignment>> GetAssignmentsByRequestIdAsync(int requestId);

        // Update assignment status (invited, accepted, declined)
        Task UpdateAssignmentStatusAsync(int assignmentId, string status);

        // Find accepted volunteer(s) for a request
        Task<IEnumerable<VolunteerRequestAssignment>> GetAcceptedVolunteersForRequestAsync(int requestId);

        // (Optional) Find requests assigned to volunteer and still open
        Task<IEnumerable<VolunteerRequestAssignment>> GetActiveAssignmentsForVolunteerAsync(int volunteerUserId);
    }
}
