using TravelBackend.Entities;
using TravelBackend.Repositories;

namespace TravelBackend.Services
{
    public class TourPlaceService
    {
        private readonly ITourPlaceRepository _tourPlaceRepository;

        public TourPlaceService(ITourPlaceRepository tourPlaceRepository)
        {
            _tourPlaceRepository = tourPlaceRepository;
        }

        public async Task<IEnumerable<TourPlace>> GetByTourIdAsync(int tourId)
            => await _tourPlaceRepository.GetByTourIdAsync(tourId);

        public async Task<IEnumerable<TourPlace>> GetByPlaceIdAsync(int placeId)
            => await _tourPlaceRepository.GetByPlaceIdAsync(placeId);
    }
}
