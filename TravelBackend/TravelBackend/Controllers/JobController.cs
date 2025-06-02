using Microsoft.AspNetCore.Mvc;
using TravelBackend.Services;
using TravelBackend.Models.DTOs;

[ApiController]
[Route("api/[controller]")]
public class JobController : ControllerBase
{
    private readonly JobService _jobService;
    public JobController(JobService jobService) => _jobService = jobService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var jobs = await _jobService.GetAllJobsAsync();
        return Ok(jobs);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActive([FromQuery] string? langCode = null)
    {
        var jobs = await _jobService.GetActiveJobsAsync(langCode);
        return Ok(jobs);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(int userId, [FromQuery] string? langCode = null)
    {
        var jobs = await _jobService.GetJobsByUserIdAsync(userId, langCode);
        return Ok(jobs);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string keyword, [FromQuery] string? langCode = null)
    {
        var jobs = await _jobService.SearchJobsAsync(keyword, langCode);
        return Ok(jobs);
    }

    [HttpPost("update-status")]
    public async Task<IActionResult> UpdateStatus(int jobId, bool isActive)
    {
        await _jobService.UpdateJobStatusAsync(jobId, isActive);
        return Ok();
    }
}
