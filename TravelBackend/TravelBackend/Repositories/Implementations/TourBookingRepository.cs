using Microsoft.EntityFrameworkCore;
using TravelBackend.Data;
using TravelBackend.Entities;

namespace TravelBackend.Repositories.Implementations
{
    public class TourBookingRepository : Repository<TourBooking>, ITourBookingRepository
    {
        private readonly TravelDbContext _context;

        public TourBookingRepository(TravelDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TourBooking>> GetBookingsByUserIdAsync(int userId)
     => await _context.TourBookings
         .Include(b => b.Tour)
             .ThenInclude(t => t.Images)
         .Where(b => b.UserId == userId)
         .ToListAsync();

        public async Task<IEnumerable<TourBooking>> GetBookingsByTourIdAsync(int tourId)
            => await _context.TourBookings.Where(b => b.TourId == tourId).ToListAsync();

        public async Task<IEnumerable<TourBooking>> GetBookingsByTourAndDateAsync(int tourId, DateTime tourDate)
            => await _context.TourBookings.Where(b => b.TourId == tourId && b.TourDate == tourDate).ToListAsync();

        public async Task<int> GetTotalBookingsForTourAsync(int tourId)
            => await _context.TourBookings.CountAsync(b => b.TourId == tourId);

        public async Task AddBookingAsync(TourBooking booking)
        {
            await _context.TourBookings.AddAsync(booking);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<TourAvailability?> GetAvailabilityAsync(int tourId, DateTime tourDate)
        {
            return await _context.TourAvailability
                .FirstOrDefaultAsync(a => a.TourId == tourId && a.TourDate == tourDate);
        }

        public async Task<Tour?> GetTourAsync(int tourId)
        {
            return await _context.Tours.FindAsync(tourId);
        }

        public async Task<bool> CancelBookingAsync(int bookingId)
        {
            var booking = await _context.TourBookings.FindAsync(bookingId);
            if (booking == null)
                return false;

            // Check if payment status is pending
            if (booking.PaymentStatus != "pending")
                return false;

            // Check if tour date is at least 7 days in the future
            var now = DateTime.UtcNow.Date;
            if ((booking.TourDate - now).TotalDays < 7)
                return false;

            // Do the cancel
            booking.PaymentStatus = "cancelled";

            // Restore slots (as before)
            var availability = await _context.TourAvailability
                .FirstOrDefaultAsync(a => a.TourId == booking.TourId && a.TourDate == booking.TourDate);
            if (availability != null)
                availability.AvailableSlots += booking.Adults + booking.Children;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TourBooking>> GetAllBookingsAsync(
    int? userId = null,
    int? tourId = null,
    DateTime? fromDate = null,
    DateTime? toDate = null,
    string? paymentStatus = null)
        {
            var query = _context.TourBookings.AsQueryable();
            if (userId.HasValue) query = query.Where(b => b.UserId == userId.Value);
            if (tourId.HasValue) query = query.Where(b => b.TourId == tourId.Value);
            if (fromDate.HasValue) query = query.Where(b => b.TourDate >= fromDate.Value);
            if (toDate.HasValue) query = query.Where(b => b.TourDate <= toDate.Value);
            if (!string.IsNullOrEmpty(paymentStatus)) query = query.Where(b => b.PaymentStatus == paymentStatus);
            return await query.ToListAsync();
        }


    }
}
