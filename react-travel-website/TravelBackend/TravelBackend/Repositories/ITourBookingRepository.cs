using TravelBackend.Entities;

namespace TravelBackend.Repositories
{
    public interface ITourBookingRepository : IRepository<TourBooking>
    {
        // Add a new booking (inherited from IRepository or declare explicitly)
        Task AddBookingAsync(TourBooking booking);

        // Commit changes (helpful if you want to control transactionally)
        Task SaveChangesAsync();

        // Get all bookings for a specific user
        Task<IEnumerable<TourBooking>> GetBookingsByUserIdAsync(int userId);

        // Get all bookings for a specific tour
        Task<IEnumerable<TourBooking>> GetBookingsByTourIdAsync(int tourId);

        // Get bookings for a tour on a specific date
        Task<IEnumerable<TourBooking>> GetBookingsByTourAndDateAsync(int tourId, DateTime tourDate);

        // Get total bookings for statistics
        Task<int> GetTotalBookingsForTourAsync(int tourId);

        // Cancel a booking
        Task<bool> CancelBookingAsync(int bookingId);

        // Get all bookings with filters (for admin)
        Task<IEnumerable<TourBooking>> GetAllBookingsAsync(
            int? userId = null,
            int? tourId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string? paymentStatus = null
        );

        // ----- Extra: for booking validation -----
        // Get tour for price calculation
        Task<Tour?> GetTourAsync(int tourId);

        // Get availability for slot check
        Task<TourAvailability?> GetAvailabilityAsync(int tourId, DateTime tourDate);
        Task<bool> HasUserBookedTourAsync(int userId, int tourId);
    }
}
