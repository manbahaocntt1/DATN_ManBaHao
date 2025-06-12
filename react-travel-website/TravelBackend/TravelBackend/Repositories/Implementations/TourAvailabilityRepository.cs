using Microsoft.EntityFrameworkCore;
using TravelBackend.Data;
using TravelBackend.Entities;
using TravelBackend.Repositories;

namespace TravelBackend.Repositories.Implementations
{
    public class TourAvailabilityRepository : Repository<TourAvailability>, ITourAvailabilityRepository
    {
        private readonly TravelDbContext _context;
        public TourAvailabilityRepository(TravelDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TourAvailability>> GetByTourIdAsync(int tourId)
            => await _context.TourAvailability.Where(a => a.TourId == tourId).ToListAsync();

        public async Task<TourAvailability?> GetByTourAndDateAsync(int tourId, DateTime date)
            => await _context.TourAvailability.FirstOrDefaultAsync(a => a.TourId == tourId && a.TourDate == date);

        public async Task UpdateAvailableSlotsAsync(int availabilityId, int newAvailableSlots)
        {
            var availability = await _context.TourAvailability.FindAsync(availabilityId);
            if (availability != null)
            {
                availability.AvailableSlots = newAvailableSlots;
                _context.TourAvailability.Update(availability);
                await _context.SaveChangesAsync();
            }
        }
    }
}
