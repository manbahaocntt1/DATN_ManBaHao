using Microsoft.AspNetCore.Mvc;
using TravelBackend.Services;
using TravelBackend.Models.DTOs;

[ApiController]
[Route("api/[controller]")]

public class ApplicationController : ControllerBase
{
    private readonly ApplicationService _applicationService;
    public ApplicationController(ApplicationService applicationService) => _applicationService = applicationService;

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(int userId)
    {
        var apps = await _applicationService.GetApplicationsByUserIdAsync(userId);
        return Ok(apps);
    }

    [HttpGet("job/{jobId}")]
    public async Task<IActionResult> GetByJob(int jobId)
    {
        var apps = await _applicationService.GetApplicationsByJobIdAsync(jobId);
        return Ok(apps);
    }

    [HttpPost("update-status")]
    public async Task<IActionResult> UpdateStatus(int applicationId, string status)
    {
        await _applicationService.UpdateApplicationStatusAsync(applicationId, status);
        return Ok();
    }
}
