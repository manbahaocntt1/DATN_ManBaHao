using Microsoft.AspNetCore.Mvc;
using TravelBackend.Services;
using TravelBackend.Models.DTOs;
using TravelBackend.Entities;

[ApiController]
[Route("api/[controller]")]
public class TourController : ControllerBase
{
    private readonly TourService _tourService;
    public TourController(TourService tourService) => _tourService = tourService;

    [HttpGet]
    public async Task<IActionResult> GetAll(
    [FromQuery] bool? isActive = null,
    [FromQuery] string? lang = null,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 9)
    {
        var result = await _tourService.GetAllToursPagedAsync(isActive, lang, page, pageSize);
        var toursDto = result.Items.Select(MapToTourDto).ToList();

        return Ok(new
        {
            tours = toursDto,
            totalCount = result.TotalCount,
            totalPages = result.TotalPages,
            currentPage = result.CurrentPage
        });
    }
    private static TourDto MapToTourDto(Tour t)
    {
        return new TourDto
        {
            TourId = t.TourId,
            Title = t.Title,
            Description = t.Description,
            PriceAdult = t.PriceAdult,
            PriceChild = t.PriceChild,
            Location = t.Location,
            Type = t.Type,
            DurationDays = t.DurationDays,
            CreatedBy = t.CreatedBy,
            IsActive = t.IsActive,
            CreatedAt = t.CreatedAt,
            Images = t.Images?.Select(img => new TourImageDto
            {
                ImageId = img.ImageId,
                ImageUrl = img.ImageUrl,
                Caption = img.Caption
            }).ToList() ?? new List<TourImageDto>()
        };
    }



    [HttpGet("search")]
    public async Task<IActionResult> Search(
    [FromQuery] SearchTourDto dto,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 9)
    {
        var result = await _tourService.SearchToursPagedAsync(
            dto.Keyword, dto.Location, dto.Type, dto.MinPrice, dto.MaxPrice, dto.DurationDays, dto.LangCode, page, pageSize);

        var toursDto = result.Items.Select(MapToTourDto).ToList();

        return Ok(new
        {
            tours = toursDto,
            totalCount = result.TotalCount,
            totalPages = result.TotalPages,
            currentPage = result.CurrentPage
        });
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(int id)
    {
        var tour = await _tourService.GetTourDetailsAsync(id);
        if (tour == null) return NotFound();

        var dto = new TourDto
        {
            TourId = tour.TourId,
            Title = tour.Title,
            Description = tour.Description,
            PriceAdult = tour.PriceAdult,
            PriceChild = tour.PriceChild,
            Location = tour.Location,
            Type = tour.Type,
            DurationDays = tour.DurationDays,
            Images = tour.Images?.Select(img => new TourImageDto
            {
                ImageId = img.ImageId,
                ImageUrl = img.ImageUrl,
                Caption = img.Caption
            }).ToList() ?? new List<TourImageDto>(),

            Reviews = tour.Reviews?.Select(r => new TourReviewDto
            {
                ReviewId = r.ReviewId,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                UserFullName = r.User?.FullName,           // only if navigation property exists
                UserAvatarUrl = r.User?.AvatarUrl          // only if navigation property exists
            }).ToList() ?? new List<TourReviewDto>(),

            Availabilities = tour.Availabilities?.Select(a => new TourAvailabilityDto
            {
                AvailabilityId = a.AvailabilityId,
                TourDate = a.TourDate,
                TotalSlots = a.TotalSlots,
                AvailableSlots = a.AvailableSlots
            }).ToList() ?? new List<TourAvailabilityDto>(),

            Places = tour.TourPlaces?.Select(tp => new PlaceDto
            {
                PlaceId = tp.Place.PlaceId,
                Name = tp.Place.Name,
                Description = tp.Place.Description,
                ImageUrl = tp.Place.ImageUrl,
                Category = tp.Place.Category,
                Address = tp.Place.Address
            }).ToList() ?? new List<PlaceDto>()
        };

        return Ok(dto);
    }


    [HttpGet("personalized/{userId}")]
    public async Task<IActionResult> GetPersonalized(int userId, [FromQuery] string? lang = null)
    {
        var tours = await _tourService.GetPersonalizedToursAsync(userId, 10, lang);
        return Ok(tours);
    }

    [HttpGet("stats/{tourId}")]
    public async Task<IActionResult> GetStats(int tourId)
    {
        var stats = await _tourService.GetTourStatsAsync(tourId);
        return Ok(stats);
    }

    [HttpPost("activate/{tourId}")]
    public async Task<IActionResult> Activate(int tourId)
    {
        await _tourService.ActivateTourAsync(tourId);
        return Ok();
    }

    [HttpPost("deactivate/{tourId}")]
    public async Task<IActionResult> Deactivate(int tourId)
    {
        await _tourService.DeactivateTourAsync(tourId);
        return Ok();
    }


    [HttpGet("newest")]
    public async Task<IActionResult> GetNewestTours([FromQuery] int count = 5)
    {
        var tours = await _tourService.GetNewestToursAsync(count);
        return Ok(tours);
    }

    [HttpGet("top")]
    public async Task<IActionResult> GetTopRatingTours([FromQuery] int count = 5)
    {
        var tours = await _tourService.GetTopRatingToursAsync(count);
        return Ok(tours);
    }
}
