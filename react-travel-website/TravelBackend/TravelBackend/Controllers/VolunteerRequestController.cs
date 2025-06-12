using Microsoft.AspNetCore.Mvc;
using TravelBackend.Services;
using TravelBackend.Models.DTOs;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class VolunteerRequestController : ControllerBase
{
    private readonly VolunteerRequestService _requestService;
    public VolunteerRequestController(VolunteerRequestService requestService)
        => _requestService = requestService;

    // Traveler creates a new volunteer request
    [HttpPost("add")]
    public async Task<IActionResult> Create([FromBody] CreateVolunteerRequestDto dto)
    {
        // In a real app, get userId from token/claims
        int userId = dto.UserId; // For dev/testing only
        var requestId = await _requestService.CreateRequestAsync(userId, dto);
        return Ok(new { requestId });
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(int userId)
    {
        var requests = await _requestService.GetRequestsByUserIdAsync(userId);
        return Ok(requests); // This is now a List<VolunteerRequestDto>
    }

    // Volunteer sees pending/matching requests
    [HttpGet("available/{volunteerUserId}")]
    public async Task<IActionResult> GetAvailableForVolunteer(int volunteerUserId)
    {
        var requests = await _requestService.GetMatchingRequestsForVolunteer(volunteerUserId);
        return Ok(requests);
    }

    // Volunteer gets their current assignments
    [HttpGet("assignments/{volunteerUserId}")]
    public async Task<IActionResult> GetAssignmentsForVolunteer(int volunteerUserId)
    {
        var assignments = await _requestService.GetAssignmentsForVolunteerAsync(volunteerUserId);
        return Ok(assignments);
    }

    // Volunteer accepts an assignment
    [HttpPost("assignment/accept")]
    public async Task<IActionResult> AcceptAssignment([FromBody] VolunteerRequestAssignmentDto dto)
    {
        await _requestService.AcceptAssignmentAsync(dto.RequestId, dto.VolunteerUserId);
        return Ok(new { message = "Assignment accepted." });
    }

    // Volunteer declines an assignment
    [HttpPost("assignment/decline")]
    public async Task<IActionResult> DeclineAssignment([FromBody] VolunteerRequestAssignmentDto dto)
    {
        await _requestService.DeclineAssignmentAsync(dto.RequestId, dto.VolunteerUserId);
        return Ok(new { message = "Assignment declined." });
    }

    // Admin or automated system updates request status
    [HttpPost("update-status")]
    public async Task<IActionResult> UpdateStatus([FromQuery] int requestId, [FromQuery] string status)
    {
        await _requestService.UpdateRequestStatusAsync(requestId, status);
        return Ok(new { message = "Status updated." });
    }
}
