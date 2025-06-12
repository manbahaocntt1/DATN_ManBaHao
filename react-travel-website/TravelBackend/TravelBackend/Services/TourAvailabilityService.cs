using TravelBackend.Entities;
using TravelBackend.Repositories;

namespace TravelBackend.Services
{
    public class TourAvailabilityService
    {
        private readonly ITourAvailabilityRepository _availabilityRepository;

        public TourAvailabilityService(ITourAvailabilityRepository availabilityRepository)
        {
            _availabilityRepository = availabilityRepository;
        }

        public async Task<IEnumerable<TourAvailability>> GetByTourIdAsync(int tourId)
            => await _availabilityRepository.GetByTourIdAsync(tourId);

        public async Task<TourAvailability?> GetByTourAndDateAsync(int tourId, DateTime date)
            => await _availabilityRepository.GetByTourAndDateAsync(tourId, date);

        public async Task UpdateAvailableSlotsAsync(int availabilityId, int newAvailableSlots)
            => await _availabilityRepository.UpdateAvailableSlotsAsync(availabilityId, newAvailableSlots);
    }
}
