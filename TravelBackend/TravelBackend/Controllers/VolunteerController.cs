using Microsoft.AspNetCore.Mvc;
using TravelBackend.Models.DTOs;
using TravelBackend.Services;

[ApiController]
[Route("api/[controller]")]
public class VolunteerController : ControllerBase
{
    private readonly VolunteerService _volunteerService;
    public VolunteerController(VolunteerService volunteerService) => _volunteerService = volunteerService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var volunteers = await _volunteerService.GetAllVolunteersAsync();
        return Ok(volunteers);
    }

    [HttpGet("approved")]
    public async Task<IActionResult> GetApproved()
    {
        var volunteers = await _volunteerService.GetApprovedVolunteersAsync();
        return Ok(volunteers);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? language, [FromQuery] string? university)
    {
        var volunteers = await _volunteerService.SearchVolunteersAsync(language, university);
        return Ok(volunteers);
    }

    [HttpPost("approve")]
    public async Task<IActionResult> Approve([FromBody] ApproveVolunteerDto dto)
    {
        await _volunteerService.ApproveVolunteerAsync(dto.VolunteerId);
        return Ok("Volunteer approved.");
    }
}
