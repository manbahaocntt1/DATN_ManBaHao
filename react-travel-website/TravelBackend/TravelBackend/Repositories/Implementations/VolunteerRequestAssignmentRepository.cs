using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelBackend.Data;
using TravelBackend.Entities;

namespace TravelBackend.Repositories.Implementations
{
    public class VolunteerRequestAssignmentRepository : Repository<VolunteerRequestAssignment>, IVolunteerRequestAssignmentRepository
    {
        private readonly TravelDbContext _context;

        public VolunteerRequestAssignmentRepository(TravelDbContext context) : base(context)
        {
            _context = context;
        }

        // Get all assignments for a specific volunteer
        public async Task<IEnumerable<VolunteerRequestAssignment>> GetAssignmentsByVolunteerUserIdAsync(int volunteerUserId)
        {
            return await _context.VolunteerRequestAssignments
                .Where(a => a.VolunteerUserId == volunteerUserId)
                .Include(a => a.Request)
                .OrderByDescending(a => a.AssignedAt)
                .ToListAsync();
        }

        // Get all assignments for a specific request
        public async Task<IEnumerable<VolunteerRequestAssignment>> GetAssignmentsByRequestIdAsync(int requestId)
        {
            return await _context.VolunteerRequestAssignments
                .Where(a => a.RequestId == requestId)
                .Include(a => a.VolunteerUser)
                .OrderByDescending(a => a.AssignedAt)
                .ToListAsync();
        }

        // Update assignment status (invited, accepted, declined)
        public async Task UpdateAssignmentStatusAsync(int assignmentId, string status)
        {
            var assignment = await _context.VolunteerRequestAssignments.FindAsync(assignmentId);
            if (assignment != null)
            {
                assignment.Status = status;
                _context.VolunteerRequestAssignments.Update(assignment);
                await _context.SaveChangesAsync();
            }
        }

        // Get accepted volunteers for a request
        public async Task<IEnumerable<VolunteerRequestAssignment>> GetAcceptedVolunteersForRequestAsync(int requestId)
        {
            return await _context.VolunteerRequestAssignments
                .Where(a => a.RequestId == requestId && a.Status == "accepted")
                .Include(a => a.VolunteerUser)
                .ToListAsync();
        }

        // (Optional) Get active assignments for a volunteer (status: invited/accepted, request not completed/rejected)
        public async Task<IEnumerable<VolunteerRequestAssignment>> GetActiveAssignmentsForVolunteerAsync(int volunteerUserId)
        {
            return await _context.VolunteerRequestAssignments
                .Where(a => a.VolunteerUserId == volunteerUserId &&
                            (a.Status == "invited" || a.Status == "accepted") &&
                            (a.Request.Status == "pending" || a.Request.Status == "accepted"))
                .Include(a => a.Request)
                .OrderByDescending(a => a.AssignedAt)
                .ToListAsync();
        }
    }
}
