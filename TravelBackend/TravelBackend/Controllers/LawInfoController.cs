using Microsoft.AspNetCore.Mvc;
using TravelBackend.Services;


[ApiController]
[Route("api/[controller]")]
public class LawInfoController : ControllerBase
{
    private readonly LawInfoService _lawInfoService;
    public LawInfoController(LawInfoService lawInfoService) => _lawInfoService = lawInfoService;

    [HttpGet("category")]
    public async Task<IActionResult> GetByCategory([FromQuery] string? category, [FromQuery] string? lang)
    {
        var laws = await _lawInfoService.GetByCategoryAsync(category, lang);
        return Ok(laws);
    }

    [HttpGet("{lawId}")]
    public async Task<IActionResult> GetById(int lawId)
    {
        var law = await _lawInfoService.GetByIdAsync(lawId);
        if (law == null) return NotFound();
        return Ok(law);
    }
}
