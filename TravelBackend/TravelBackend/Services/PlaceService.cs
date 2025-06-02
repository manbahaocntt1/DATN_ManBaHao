using TravelBackend.Entities;
using TravelBackend.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelBackend.Services
{
    public class PlaceService
    {
        private readonly IPlaceRepository _placeRepository;

        public PlaceService(IPlaceRepository placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public async Task<IEnumerable<Place>> GetAllPlacesAsync(string? langCode = null)
            => await _placeRepository.GetAllAsync(langCode);

        public async Task<Place?> GetPlaceByIdAsync(int id, string? langCode = null)
            => await _placeRepository.GetByIdAsync(id, langCode);

        public async Task<IEnumerable<Place>> SearchPlacesAsync(string? keyword = null, string? category = null, string? langCode = null)
            => await _placeRepository.SearchPlacesAsync(keyword, category, langCode);

        // **NEW**
        public async Task<IEnumerable<Place>> GetPlacesByTourIdAsync(int tourId, string? langCode = null)
            => await _placeRepository.GetPlacesByTourIdAsync(tourId, langCode);
    }
}
