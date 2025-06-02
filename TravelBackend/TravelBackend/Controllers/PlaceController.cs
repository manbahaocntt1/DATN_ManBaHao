using Microsoft.AspNetCore.Mvc;
using TravelBackend.Services;

[ApiController]
[Route("api/[controller]")]
public class PlaceController : ControllerBase
{
    private readonly PlaceService _placeService;
    public PlaceController(PlaceService placeService) => _placeService = placeService;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? langCode = null)
    {
        var places = await _placeService.GetAllPlacesAsync(langCode);
        return Ok(places);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? keyword, [FromQuery] string? category, [FromQuery] string? langCode)
    {
        var places = await _placeService.SearchPlacesAsync(keyword, category, langCode);
        return Ok(places);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, [FromQuery] string? langCode = null)
    {
        var place = await _placeService.GetPlaceByIdAsync(id, langCode);
        if (place == null) return NotFound();
        return Ok(place);
    }
    // **NEW**: Get all places of a tour
    [HttpGet("by-tour/{tourId}")]
    public async Task<IActionResult> GetByTour(int tourId, [FromQuery] string? langCode = null)
    {
        var places = await _placeService.GetPlacesByTourIdAsync(tourId, langCode);
        return Ok(places);
    }
}
