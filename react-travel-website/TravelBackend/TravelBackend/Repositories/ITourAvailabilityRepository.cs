using TravelBackend.Entities;

namespace TravelBackend.Repositories
{
    public interface ITourAvailabilityRepository : IRepository<TourAvailability>
    {
        // Get all availabilities for a specific tour
        Task<IEnumerable<TourAvailability>> GetByTourIdAsync(int tourId);

        // Get availability for a tour on a specific date
        Task<TourAvailability?> GetByTourAndDateAsync(int tourId, DateTime date);

        // Update available slots
        Task UpdateAvailableSlotsAsync(int availabilityId, int newAvailableSlots);
    }
}
