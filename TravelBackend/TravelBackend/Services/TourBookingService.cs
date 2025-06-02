using TravelBackend.Entities;
using TravelBackend.Repositories;
using System;
using TravelBackend.Models.DTOs;

namespace TravelBackend.Services
{
    public class TourBookingService
    {
        private readonly ITourBookingRepository _bookingRepository;

        public TourBookingService(ITourBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<(bool ok, string? error, TourBooking? booking)> CreateBookingAsync(CreateBookingDto dto)
        {
            if (dto.Adults < 1 && dto.Children < 1)
                return (false, "At least one adult or child is required.", null);

            var tour = await _bookingRepository.GetTourAsync(dto.TourId);
            if (tour == null)
                return (false, "Tour not found.", null);

            // Calculate price on server only
            decimal totalPrice = dto.Adults * tour.PriceAdult + dto.Children * tour.PriceChild;

            var availability = await _bookingRepository.GetAvailabilityAsync(dto.TourId, dto.TourDate);
            int totalRequested = dto.Adults + dto.Children;
            if (availability == null || availability.AvailableSlots < totalRequested)
                return (false, "Not enough slots available.", null);

            var booking = new TourBooking
            {
                UserId = dto.UserId,
                TourId = dto.TourId,
                TourDate = dto.TourDate,
                Adults = dto.Adults,
                Children = dto.Children,
                Note = dto.Note,
                PaymentMethod = dto.PaymentMethod,
                TotalPrice = totalPrice,
                PaymentStatus = "pending",
                CreatedAt = DateTime.UtcNow
            };


            await _bookingRepository.AddBookingAsync(booking);

            availability.AvailableSlots -= totalRequested;
            await _bookingRepository.SaveChangesAsync();

            return (true, null, booking);
        }


        // Other service methods...
        public async Task<IEnumerable<TourBooking>> GetBookingsByUserIdAsync(int userId)
            => await _bookingRepository.GetBookingsByUserIdAsync(userId);

        public async Task<IEnumerable<TourBooking>> GetBookingsByTourIdAsync(int tourId)
            => await _bookingRepository.GetBookingsByTourIdAsync(tourId);

        public async Task<bool> CancelBookingAsync(int bookingId)
    => await _bookingRepository.CancelBookingAsync(bookingId);


        public async Task<IEnumerable<TourBooking>> GetAllBookingsAsync(
            int? userId = null, int? tourId = null, DateTime? fromDate = null, DateTime? toDate = null, string? paymentStatus = null)
            => await _bookingRepository.GetAllBookingsAsync(userId, tourId, fromDate, toDate, paymentStatus);
    }
}
