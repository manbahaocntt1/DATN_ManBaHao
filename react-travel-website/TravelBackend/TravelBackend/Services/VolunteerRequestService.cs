using TravelBackend.Entities;
using TravelBackend.Models.DTOs;
using TravelBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelBackend.Services
{
    public class VolunteerRequestService
    {
        private readonly IVolunteerRequestRepository _requestRepo;
        private readonly IUserRepository _userRepo;
        private readonly IVolunteerRequestAssignmentRepository _assignmentRepo;

        public VolunteerRequestService(
            IVolunteerRequestRepository requestRepo,
            IUserRepository userRepo,
            IVolunteerRequestAssignmentRepository assignmentRepo
        )
        {
            _requestRepo = requestRepo;
            _userRepo = userRepo;
            _assignmentRepo = assignmentRepo;
        }

        // Traveler creates a request
        public async Task<int> CreateRequestAsync(int userId, CreateVolunteerRequestDto dto)
        {
            var request = new VolunteerRequest
            {
                UserId = userId,
                Content = dto.Content,
                PreferredDate = dto.PreferredDate,
                RequiredLanguages = dto.RequiredLanguages,
                Status = "pending",
                RequestedAt = DateTime.UtcNow
            };
            await _requestRepo.AddAsync(request);
            // This must persist to get the real RequestId
            await _requestRepo.SaveChangesAsync();

            // Assign matching volunteers immediately (optional, but practical)
            var volunteers = await _userRepo.GetAllVolunteerProfilesDtoAsync();
            var langsRequired = dto.RequiredLanguages.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();

            foreach (var volunteer in volunteers)
            {
                var volunteerLangs = volunteer.Languages.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
                if (volunteerLangs.Intersect(langsRequired).Any() && volunteer.IsApproved)
                {
                    // Only invite approved volunteers matching at least one required language
                    var assignment = new VolunteerRequestAssignment
                    {
                        RequestId = request.RequestId,
                        VolunteerUserId = volunteer.UserId,
                        Status = "invited",
                        AssignedAt = DateTime.UtcNow
                    };
                    await _assignmentRepo.AddAsync(assignment);
                }
            }

            await _assignmentRepo.SaveChangesAsync(); // Save all assignments
            return request.RequestId;
        }

        // Traveler sees their own requests (returns DTOs!)
        public async Task<List<VolunteerRequestDto>> GetRequestsByUserIdAsync(int userId)
        {
            var requests = await _requestRepo.GetRequestsByUserIdAsync(userId);

            // Project to DTOs to avoid cycles!
            var result = requests.Select(r => new VolunteerRequestDto
            {
                RequestId = r.RequestId,
                Content = r.Content,
                PreferredDate = r.PreferredDate,
                RequiredLanguages = r.RequiredLanguages,
                Status = r.Status,
                RequestedAt = r.RequestedAt,
                Assignments = r.Assignments?.Select(a => new VolunteerRequestAssignmentDto
                {
                    AssignmentId = a.AssignmentId,
                    VolunteerUserId = a.VolunteerUserId,
                    Status = a.Status,
                    AssignedAt = a.AssignedAt,
                    VolunteerFullName = a.VolunteerUser?.FullName ?? "(unknown)"
                }).ToList() ?? new List<VolunteerRequestAssignmentDto>()
            }).ToList();

            return result;
        }
        // Volunteer sees matching requests
        public async Task<List<VolunteerRequest>> GetMatchingRequestsForVolunteer(int volunteerUserId)
        {
            var profile = await _userRepo.GetVolunteerProfileAsync(volunteerUserId);
            if (profile == null) return new List<VolunteerRequest>();
            var langs = profile.Languages.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
            return await _requestRepo.GetOpenRequestsMatchingLanguagesAsync(langs);
        }

        // Volunteer sees their assignments
        public async Task<IEnumerable<VolunteerRequestAssignment>> GetAssignmentsForVolunteerAsync(int volunteerUserId)
            => await _assignmentRepo.GetAssignmentsByVolunteerUserIdAsync(volunteerUserId);

        // Volunteer accepts an assignment
        public async Task AcceptAssignmentAsync(int requestId, int volunteerUserId)
        {
            var assignments = await _assignmentRepo.GetAssignmentsByRequestIdAsync(requestId);
            var assignment = assignments.FirstOrDefault(a => a.VolunteerUserId == volunteerUserId);
            if (assignment != null && assignment.Status == "invited")
            {
                await _assignmentRepo.UpdateAssignmentStatusAsync(assignment.AssignmentId, "accepted");
                // Optionally update request status to "accepted" if only one volunteer is allowed
                var request = await _requestRepo.GetByIdAsync(requestId);
                if (request != null)
                {
                    request.Status = "accepted";
                    _requestRepo.Update(request);
                    await _requestRepo.SaveChangesAsync();
                }
            }
        }

        // Volunteer declines an assignment
        public async Task DeclineAssignmentAsync(int requestId, int volunteerUserId)
        {
            var assignments = await _assignmentRepo.GetAssignmentsByRequestIdAsync(requestId);
            var assignment = assignments.FirstOrDefault(a => a.VolunteerUserId == volunteerUserId);
            if (assignment != null && assignment.Status == "invited")
            {
                await _assignmentRepo.UpdateAssignmentStatusAsync(assignment.AssignmentId, "declined");
            }
        }

        // Admin or system updates request status
        public async Task UpdateRequestStatusAsync(int requestId, string status)
            => await _requestRepo.UpdateRequestStatusAsync(requestId, status);
    }
}
