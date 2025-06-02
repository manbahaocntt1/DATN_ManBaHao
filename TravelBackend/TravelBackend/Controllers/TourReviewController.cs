using Microsoft.AspNetCore.Mvc;
using TravelBackend.Entities;
using TravelBackend.Models.DTOs;
using TravelBackend.Services;

[ApiController]
[Route("api/[controller]")]
public class TourReviewController : ControllerBase
{
    private readonly TourReviewService _reviewService;
    public TourReviewController(TourReviewService reviewService) => _reviewService = reviewService;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? tourId = null, [FromQuery] int? userId = null, [FromQuery] int? minRating = null, [FromQuery] int? maxRating = null, [FromQuery] DateTime? fromDate = null, [FromQuery] DateTime? toDate = null)
    {
        var reviews = await _reviewService.GetAllReviewsAsync(tourId, userId, minRating, maxRating, fromDate, toDate);
        return Ok(reviews);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateReviewDto dto)
    {
        var review = new TourReview
        {
            TourId = dto.TourId,
            UserId = dto.UserId,
            Rating = dto.Rating,
            Comment = dto.Comment,
            // Set additional properties if needed, e.g., CreatedAt = DateTime.UtcNow, IsApproved = false
        };

        await _reviewService.AddReviewAsync(review);
        return Ok("Review submitted for approval.");
    }


    [HttpGet("tour/{tourId}")]
    public async Task<IActionResult> GetByTour(int tourId)
    {
        var reviews = await _reviewService.GetReviewsByTourIdAsync(tourId);
        return Ok(reviews);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(int userId)
    {
        var reviews = await _reviewService.GetReviewsByUserIdAsync(userId);
        return Ok(reviews);
    }

    [HttpPost("moderate")]
    public async Task<IActionResult> Moderate([FromBody] ModerateReviewDto dto)
    {
        await _reviewService.ModerateReviewAsync(dto.ReviewId, dto.IsApproved);
        return Ok("Review moderation updated.");
    }
}
