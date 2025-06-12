using Microsoft.AspNetCore.Mvc;
using TravelBackend.Services;
using TravelBackend.Models.DTOs;
using TravelBackend.Entities;

[ApiController]
[Route("api/[controller]")]
public class TourBookingController : ControllerBase
{
    private readonly TourBookingService _bookingService;
    public TourBookingController(TourBookingService bookingService) => _bookingService = bookingService;

    [HttpPost("book")]
    
    public async Task<IActionResult> Book([FromBody] CreateBookingDto dto)
    {
        var (ok, error, booking) = await _bookingService.CreateBookingAsync(dto);
        if (!ok)
            return BadRequest(error);

        // You can return booking info, bookingId, or message here.
        return Ok(new { message = "Booking created.", bookingId = booking!.BookingId, totalPrice = booking.TotalPrice });
    }


    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(int userId)
    {
        var bookings = await _bookingService.GetBookingsByUserIdAsync(userId);

        var bookingDtos = bookings.Select(b => new BookingDto
        {
            BookingId = b.BookingId,
            TourId = b.TourId,
            TourTitle = b.Tour?.Title ?? "",
            TourImage = b.Tour?.Images?.FirstOrDefault()?.ImageUrl, // If you have images
            TourDate = b.TourDate,
            Adults = b.Adults,
            Children = b.Children,
            TotalPrice = b.TotalPrice,
            PaymentMethod = b.PaymentMethod,
            PaymentStatus = b.PaymentStatus,
            Note = b.Note,
            CreatedAt = b.CreatedAt
        });

        return Ok(bookingDtos);
    }


    [HttpGet("tour/{tourId}")]
    public async Task<IActionResult> GetByTour(int tourId)
    {
        var bookings = await _bookingService.GetBookingsByTourIdAsync(tourId);
        return Ok(bookings);
    }

    [HttpPost("cancel/{bookingId}")]
    public async Task<IActionResult> Cancel(int bookingId)
    {
        var success = await _bookingService.CancelBookingAsync(bookingId);
        if (!success)
            return BadRequest("Booking cannot be cancelled. Only pending bookings at least 7 days ahead can be cancelled.");
        return Ok("Booking cancelled.");
    }

    [HttpGet("user/{userId}/has-booked/{tourId}")]
    public async Task<IActionResult> HasUserBookedTour(int userId, int tourId)
    {
        var hasBooked = await _bookingService.HasUserBookedTourAsync(userId, tourId);
        return Ok(new { hasBooked });
    }

}
