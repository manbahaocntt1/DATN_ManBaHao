using Microsoft.AspNetCore.Mvc;
using TravelBackend.Services;
using TravelBackend.Models.DTOs;

[ApiController]
[Route("api/[controller]")]
public class VolunteerRequestController : ControllerBase
{
    private readonly VolunteerRequestService _requestService;
    public VolunteerRequestController(VolunteerRequestService requestService) => _requestService = requestService;

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(int userId)
    {
        var requests = await _requestService.GetRequestsByUserIdAsync(userId);
        return Ok(requests);
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetPending()
    {
        var requests = await _requestService.GetPendingRequestsAsync();
        return Ok(requests);
    }

    [HttpPost("update-status")]
    public async Task<IActionResult> UpdateStatus(int requestId, string status)
    {
        await _requestService.UpdateRequestStatusAsync(requestId, status);
        return Ok();
    }
}
